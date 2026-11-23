using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace KuboczochDeadCorpseInventoryFix;

public sealed class HoverCleanupMod : ModSystem
{
    private Harmony? harmony;
    public override bool ShouldLoad(EnumAppSide side) => side == EnumAppSide.Client;
    public override void StartClientSide(ICoreClientAPI api)
    {
        harmony = new Harmony("kuboczoch.deadcorpseinventoryfix");
        harmony.Patch(AccessTools.Method(typeof(GuiElementItemSlotGridBase), "OnMouseMove"),
            transpiler: new HarmonyMethod(typeof(HoverCleanup), nameof(HoverCleanup.Transpile)));
    }
    public override void Dispose() { harmony?.UnpatchAll(harmony.Id); harmony = null; }
}

// Retain the actual entered slot: looking it up again is unsafe after a bag resize.
// Weak keys avoid retaining grids (and their inventories) after composer disposal.
public static class HoverCleanup
{
    private sealed class State { public ItemSlot? Slot; }
    private static readonly ConditionalWeakTable<GuiElementItemSlotGridBase, State> hovered = new();

    public static void Enter(IInputAPI input, ItemSlot slot, GuiElementItemSlotGridBase grid)
    {
        hovered.GetOrCreateValue(grid).Slot = slot;
        input.TriggerOnMouseEnterSlot(slot);
    }

    public static void Leave(IInputAPI input, ItemSlot? slot, GuiElementItemSlotGridBase grid)
    {
        // Prefer the original argument on every healthy path. Only repair a missing lookup.
        if (hovered.TryGetValue(grid, out var state))
        {
            slot ??= state.Slot;
            state.Slot = null; // Clear before event dispatch (which may reenter the GUI).
        }
        // With no recorded enter there is no slot identity to send to listeners.
        // OnMouseMove still performs its native hoverSlotId = -1 cleanup.
        if (slot != null) input.TriggerOnMouseLeaveSlot(slot);
    }

    public static IEnumerable<CodeInstruction> Transpile(IEnumerable<CodeInstruction> instructions)
    {
        var code = instructions.ToList();
        var enter = AccessTools.Method(typeof(IInputAPI), nameof(IInputAPI.TriggerOnMouseEnterSlot));
        var leave = AccessTools.Method(typeof(IInputAPI), nameof(IInputAPI.TriggerOnMouseLeaveSlot));
        if (code.Count(i => i.Calls(enter)) != 1 || code.Count(i => i.Calls(leave)) != 2)
            throw new InvalidOperationException("Unsupported slot-grid OnMouseMove: expected one enter and two leave dispatches.");
        foreach (var instruction in code)
        {
            if (instruction.Calls(enter) || instruction.Calls(leave))
            {
                // Move branch/exception metadata to the inserted load: branches must not skip it.
                var load = new CodeInstruction(OpCodes.Ldarg_0);
                load.labels.AddRange(instruction.labels); instruction.labels.Clear();
                load.blocks.AddRange(instruction.blocks); instruction.blocks.Clear();
                yield return load;
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(HoverCleanup),
                    instruction.Calls(enter) ? nameof(Enter) : nameof(Leave)));
            }
            else yield return instruction;
        }
    }
}
