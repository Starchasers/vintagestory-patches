using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;
using KuboczochInventoryHoverCleanup;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Dead.Inventories;

public class Proxy : DispatchProxy
{
    public System.Func<MethodInfo, object?[]?, object?> Handler = (_, _) => null;
    protected override object? Invoke(MethodInfo? method, object?[]? args) => Handler(method!, args);
    public static T Make<T>(System.Func<MethodInfo, object?[]?, object?> handler) where T : class
    { var p = Create<T, Proxy>(); ((Proxy)(object)p).Handler = handler; return p; }
}
public static class Program
{
    static int assertions;
    static void Check(bool ok, string name) { if (!ok) throw new Exception(name); assertions++; Console.WriteLine("PASS " + name); }
    static GuiElementItemSlotGridBase Grid(IInventory inventory)
    {
        var grid = (GuiElementItemSlotGridBase)RuntimeHelpers.GetUninitializedObject(typeof(GuiElementItemSlotGrid));
        AccessTools.Field(typeof(GuiElementItemSlotGridBase), "inventory").SetValue(grid, inventory);
        var parent = ElementBounds.Fixed(0, 0, 200, 200).WithParent(ElementBounds.Empty); parent.CalcWorldBounds();
        grid.Bounds = ElementBounds.Fixed(0, 0, 150, 150).WithParent(parent); grid.Bounds.CalcWorldBounds();
        var slotBounds = ElementBounds.Fixed(0, 0, 40, 40).WithParent(parent); slotBounds.CalcWorldBounds();
        grid.SlotBounds = new[] { slotBounds };
        grid.renderedSlots = new Vintagestory.API.Datastructures.OrderedDictionary<int, ItemSlot>();
        grid.renderedSlots.Add(4, inventory[4]); grid.hoverSlotId = -1;
        return grid;
    }
    static void Run(bool patched, bool outside)
    {
        var inv = new InventoryDeadBackpacks("deadbackpacks-test", null!);
        var bag = AccessTools.Field(typeof(InventoryDeadBackpacks), "bagInv").GetValue(inv)!;
        // Exercise Dead's real out-of-range indexer, without synthesizing a bag collectible.
        var slots = AccessTools.Field(bag.GetType(), "bagContents");
        slots.SetValue(bag, new List<ItemSlot> { new ItemSlot(inv) });
        var originalSlot = inv[4]; Check(originalSlot != null, "real Dead slot exists before shrink");
        var grid = Grid(inv);
        var unrelated = Grid(inv); unrelated.hoverInv = new InventoryGeneric(1, "other-test", null!); unrelated.hoverSlotId = 0;
        var leaves = new List<ItemSlot?>(); int enters = 0; bool tooltipVisible = false;
        ICoreClientAPI? api = null;
        var input = Proxy.Make<IInputAPI>((m, a) =>
        {
            if (m.Name == "TriggerOnMouseEnterSlot") { enters++; tooltipVisible = true; }
            if (m.Name == "TriggerOnMouseLeaveSlot")
            {
                var slot = (ItemSlot?)a![0]; leaves.Add(slot);
                grid.OnMouseLeaveSlot(api!, slot!); unrelated.OnMouseLeaveSlot(api!, slot!);
                // Stand-in listener records dispatch continuation, not a rendered tooltip test.
                tooltipVisible = false;
            }
            return null;
        });
        api = Proxy.Make<ICoreClientAPI>((m, _) => m.Name == "get_Input" ? input : null);
        grid.OnMouseMove(api, new MouseEvent(10, 10));
        Check(enters == 1 && grid.hoverSlotId == 4, "native mouse enter");
        slots.SetValue(bag, new List<ItemSlot>());
        Check(inv[4] == null, "real Dead indexer returns null after shrink");
        grid.renderedSlots.Clear(); grid.SlotBounds = Array.Empty<ElementBounds>();
        try
        {
            grid.OnMouseMove(api, new MouseEvent(outside ? 500 : 100, 100));
            Check(patched, "patched native leave completes");
        }
        catch (NullReferenceException) when (!patched) { Console.WriteLine("RED reproduced native OnMouseLeaveSlot NRE"); return; }
        Check(leaves.Count == 1 && ReferenceEquals(leaves[0], originalSlot), "leave receives exact retained slot");
        Check(grid.hoverSlotId == -1 && unrelated.hoverSlotId == 0, "own hover cleared; unrelated inventory preserved");
        Check(!tooltipVisible, "later leave listener reached");
        grid.OnMouseMove(api, new MouseEvent(500, 500));
        Check(leaves.Count == 1, "no duplicate leave after cleanup");
        // Re-enter after resize; valid leave must keep the freshly looked-up argument.
        var replacement = new ItemSlot(inv); slots.SetValue(bag, new List<ItemSlot> { replacement });
        grid.renderedSlots.Add(4, replacement);
        var b = ElementBounds.Fixed(0, 0, 40, 40).WithParent(grid.Bounds.ParentBounds); b.CalcWorldBounds(); grid.SlotBounds = new[] { b };
        grid.OnMouseMove(api, new MouseEvent(10, 10));
        var newest = new ItemSlot(inv); slots.SetValue(bag, new List<ItemSlot> { newest });
        grid.OnMouseMove(api, new MouseEvent(500, 500));
        Check(ReferenceEquals(leaves.Last(), newest) && enters == 2, "healthy replacement and re-entry unchanged");
    }
    static void NativeTransition()
    {
        var inv = new InventoryDeadBackpacks("deadbackpacks-transition", null!);
        var bag = AccessTools.Field(typeof(InventoryDeadBackpacks), "bagInv").GetValue(inv)!;
        var contents = AccessTools.Field(bag.GetType(), "bagContents");
        var first = new ItemSlot(inv); var second = new ItemSlot(inv);
        contents.SetValue(bag, new List<ItemSlot> { first, second });
        var grid = Grid(inv); grid.renderedSlots.Add(5, second);
        var bounds = ElementBounds.Fixed(50, 0, 40, 40).WithParent(grid.Bounds.ParentBounds); bounds.CalcWorldBounds();
        grid.SlotBounds = new[] { grid.SlotBounds[0], bounds };
        var leaves = new List<ItemSlot>();
        ICoreClientAPI? api = null;
        var input = Proxy.Make<IInputAPI>((m, a) => { if (m.Name == "TriggerOnMouseLeaveSlot") { leaves.Add((ItemSlot)a![0]!); grid.OnMouseLeaveSlot(api!, leaves.Last()); } return null; });
        api = Proxy.Make<ICoreClientAPI>((m, _) => m.Name == "get_Input" ? input : null);
        grid.OnMouseMove(api, new MouseEvent(10, 10)); grid.OnMouseMove(api, new MouseEvent(60, 10));
        Check(leaves.Count == 0 && grid.hoverSlotId == 5, "native slot-to-slot transition unchanged");
        contents.SetValue(bag, new List<ItemSlot>()); grid.renderedSlots.Clear(); grid.SlotBounds = Array.Empty<ElementBounds>();
        grid.OnMouseMove(api, new MouseEvent(100, 100));
        Check(leaves.Count == 1 && ReferenceEquals(leaves[0], second) && grid.hoverSlotId == -1, "native second enter replaces cached slot before shrink");
    }
    static void EdgeCases()
    {
        var inv = new InventoryGeneric(1, "generic-test", null!);
        var grid = (GuiElementItemSlotGridBase)RuntimeHelpers.GetUninitializedObject(typeof(GuiElementItemSlotGrid));
        var slot = inv[0]; int leaves = 0;
        var input = Proxy.Make<IInputAPI>((m, a) => { if (m.Name == "TriggerOnMouseLeaveSlot") { leaves++; Check(ReferenceEquals(a![0], slot), "empty-stack slot identity preserved"); } return null; });
        HoverCleanup.Leave(input, null, grid);
        Check(leaves == 0, "no synthetic null event without recorded enter");
        HoverCleanup.Enter(input, slot, grid); HoverCleanup.Leave(input, slot, grid); HoverCleanup.Leave(input, null, grid);
        Check(leaves == 1, "valid empty slot dispatch and cache clearing");
        var throwing = Proxy.Make<IInputAPI>((m, _) => { if (m.Name == "TriggerOnMouseLeaveSlot") throw new InvalidOperationException("listener"); return null; });
        try { HoverCleanup.Leave(throwing, slot, grid); throw new Exception("exception swallowed"); }
        catch (InvalidOperationException ex) when (ex.Message == "listener") { Check(true, "listener failures are not swallowed"); }
        try { HoverCleanup.Transpile(Array.Empty<CodeInstruction>()).ToList(); throw new Exception("unsupported IL accepted"); }
        catch (InvalidOperationException ex) when (ex.Message.StartsWith("Unsupported")) { Check(true, "unknown IL fails explicitly"); }
        var reentered = new ItemSlot(inv); var seen = new List<ItemSlot>();
        IInputAPI? reentrant = null;
        reentrant = Proxy.Make<IInputAPI>((m, a) => {
            if (m.Name == "TriggerOnMouseLeaveSlot") {
                seen.Add((ItemSlot)a![0]!);
                if (seen.Count == 1) HoverCleanup.Enter(reentrant!, reentered, grid);
            }
            return null;
        });
        HoverCleanup.Enter(reentrant, slot, grid); HoverCleanup.Leave(reentrant, null, grid); HoverCleanup.Leave(reentrant, null, grid);
        Check(seen.Count == 2 && ReferenceEquals(seen[1], reentered), "callback re-entry survives clear-before-dispatch");
        Check(new HoverCleanupMod().ShouldLoad(EnumAppSide.Client) && !new HoverCleanupMod().ShouldLoad(EnumAppSide.Server), "client-only loading");
    }
    public static int Main(string[] args)
    {
        RuntimeEnv.GUIScale = 1;
        Console.WriteLine("SDK " + GameVersion.OverallVersion);
        Check(GameVersion.OverallVersion == "1.22.7", "exact SDK version");
        var mod = new HoverCleanupMod();
        if (!args.Contains("--red")) mod.StartClientSide(null!);
        try { Run(!args.Contains("--red"), true); Run(!args.Contains("--red"), false); if (!args.Contains("--red")) { NativeTransition(); EdgeCases(); } }
        finally { mod.Dispose(); }
        Check(!Harmony.HasAnyPatches("kuboczoch.inventoryhovercleanup"), "dispose unpatches owner");
        Console.WriteLine($"{assertions} assertions passed"); return 0;
    }
}
