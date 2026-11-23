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

    // Widen only the stopper-to-stopper compatibility comparison, then detach
    // the extraction local. Native transfer/drop and unrelated routing stay intact.
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
        // Both supported provider builds use a null-safe getter followed by
        // this exact collectible reference comparison. Preserve branch labels.
        int comparison = reads[0] + 7;
        if (comparison >= reads[1] || code[reads[0] + 1].opcode != OpCodes.Dup
            || code[comparison - 1].operand is not MethodInfo getter
            || getter.DeclaringType != typeof(ItemStack) || getter.Name != "get_Collectible"
            || code[comparison].opcode != OpCodes.Ceq)
            throw new InvalidOperationException("ACA stopper compatibility IL changed; refusing an ambiguous patch.");
        code[comparison].opcode = OpCodes.Call;
        code[comparison].operand = AccessTools.Method(typeof(StopperReturnMod), nameof(CanRemoveWith));
        code.Insert(reads[1] + 1, new CodeInstruction(OpCodes.Call,
            AccessTools.Method(typeof(StopperReturnMod), nameof(Detach))));
        return code;
    }

    public static bool CanRemoveWith(CollectibleObject? offhand, CollectibleObject? removed) =>
        ReferenceEquals(offhand, removed) || (IsStopper(offhand) && IsStopper(removed));

    private static bool IsStopper(CollectibleObject? collectible) =>
        collectible?.Attributes?["isLiquidContainerStopper"].AsBool() == true;

    public static ItemStack Detach(ItemStack stopper) => stopper.Clone();

    public override void Dispose()
    {
        harmony?.UnpatchAll(HarmonyId);
        harmony = null;
        base.Dispose();
    }
}
