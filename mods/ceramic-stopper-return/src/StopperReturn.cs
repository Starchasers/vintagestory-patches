using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.API.Common;

namespace Kuboczoch.CeramicStopperReturn;

public sealed class StopperReturnMod : ModSystem
{
    public const string HarmonyId = "kuboczoch.ceramicstopperreturn";
    private Harmony? harmony;

    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        harmony = new Harmony(HarmonyId);
        try { Install(harmony); }
        catch { harmony.UnpatchAll(HarmonyId); harmony = null; throw; }
    }

    public static MethodInfo Target => AccessTools.Method(
        AccessTools.TypeByName("ACulinaryArtillery.BlockStoppableContainer")
            ?? throw new InvalidOperationException("A Culinary Artillery is required."),
        "OnHeldInteractStart", new[] { typeof(ItemSlot), typeof(EntityAgent), typeof(BlockSelection),
            typeof(EntitySelection), typeof(bool), typeof(EnumHandHandling).MakeByRefType() })
        ?? throw new MissingMethodException("ACA OnHeldInteractStart signature changed.");

    public static void Install(Harmony harmony) => harmony.Patch(Target,
        transpiler: new HarmonyMethod(typeof(StopperReturnMod), nameof(DetachRemovedStopper)));

    // Only the extraction into the returned-stopper local is changed. The first
    // getter is an offhand-compatibility check, not a transfer.
    public static IEnumerable<CodeInstruction> DetachRemovedStopper(IEnumerable<CodeInstruction> instructions)
    {
        var code = instructions.ToList();
        var reads = code.Select((instruction, index) => (instruction, index))
            .Where(x => (x.instruction.opcode == OpCodes.Call || x.instruction.opcode == OpCodes.Callvirt)
                && x.instruction.operand is MethodInfo method
                && method.DeclaringType?.FullName == "ACulinaryArtillery.BlockStoppableContainer"
                && method.Name == "GetStopper" && method.ReturnType == typeof(ItemStack)
                && method.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(ItemStack) }))
            .Select(x => x.index).ToArray();
        if (reads.Length != 2 || !code[reads[1] + 1].IsStloc())
            throw new InvalidOperationException("ACA stopper extraction IL changed; refusing an ambiguous patch.");
        code.Insert(reads[1] + 1, new CodeInstruction(OpCodes.Call,
            AccessTools.Method(typeof(StopperReturnMod), nameof(Detach))));
        return code;
    }

    public static ItemStack Detach(ItemStack stopper) => stopper.Clone();

    public override void Dispose()
    {
        harmony?.UnpatchAll(HarmonyId);
        harmony = null;
        base.Dispose();
    }
}
