using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.API.Common;

namespace Kuboczoch.StopperReturn;

public sealed class StopperReturnMod : ModSystem
{
    public const string HarmonyId = "kuboczoch.stopperreturn";
    private static readonly object LifecycleLock = new();
    private static int users;
    private bool acquired;

    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        lock (LifecycleLock)
        {
            if (acquired) return;
            Install(new Harmony(HarmonyId));
            users++;
            acquired = true;
        }
    }

    public static MethodInfo Target => AccessTools.Method(
        AccessTools.TypeByName("ACulinaryArtillery.BlockStoppableContainer")
            ?? throw new InvalidOperationException("A Culinary Artillery is required."),
        "OnHeldInteractStart", new[] { typeof(ItemSlot), typeof(EntityAgent), typeof(BlockSelection),
            typeof(EntitySelection), typeof(bool), typeof(EnumHandHandling).MakeByRefType() })
        ?? throw new MissingMethodException("ACA OnHeldInteractStart signature changed.");

    public static void Install(Harmony harmony)
    {
        lock (LifecycleLock)
        {
            // Integrated server and client load distinct ModSystem instances in
            // the same process. Never apply the same transpiler twice.
            var method = AccessTools.Method(typeof(StopperReturnMod), nameof(DetachRemovedStopper));
            if (Harmony.GetPatchInfo(Target)?.Transpilers.Any(p => p.owner == harmony.Id && p.PatchMethod == method) == true)
                return;
            try { harmony.Patch(Target, transpiler: new HarmonyMethod(method)); }
            catch
            {
                // Roll back only our attempted patch, never another live owner.
                harmony.Unpatch(Target, HarmonyPatchType.Transpiler, harmony.Id);
                throw;
            }
        }
    }

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
        // dev29 gives transformed containers on both sides; dev32 already
        // gates these paths to the server. Suppress only the two bottle-return
        // calls on clients, reporting success so the native drop fallback is
        // suppressed too. Leave sealing/waxing and normal prediction intact.
        var gives = code.Select((instruction, index) => (instruction, index))
            .Where(x => x.instruction.opcode == OpCodes.Callvirt
                && x.instruction.operand is MethodInfo method
                && method.DeclaringType == typeof(IPlayerInventoryManager)
                && method.Name == nameof(IPlayerInventoryManager.TryGiveItemstack)
                && method.ReturnType == typeof(bool)
                && method.GetParameters().Select(p => p.ParameterType).SequenceEqual(new[] { typeof(ItemStack), typeof(bool) }))
            .Select(x => x.index).ToArray();
        if (gives.Length != 5)
            throw new InvalidOperationException("ACA container return IL changed; refusing an ambiguous patch.");
        foreach (int index in gives.Take(2).Reverse())
        {
            var loadEntity = new CodeInstruction(OpCodes.Ldarg_2);
            loadEntity.labels.AddRange(code[index].labels);
            loadEntity.blocks.AddRange(code[index].blocks);
            code[index].labels.Clear(); code[index].blocks.Clear();
            code[index].opcode = OpCodes.Call;
            code[index].operand = AccessTools.Method(typeof(StopperReturnMod), nameof(GiveReturnedBottle));
            code.Insert(index, loadEntity);
        }
        return code;
    }

    public static bool CanRemoveWith(CollectibleObject? offhand, CollectibleObject? removed) =>
        ReferenceEquals(offhand, removed) || (IsStopper(offhand) && IsStopper(removed));

    private static bool IsStopper(CollectibleObject? collectible) =>
        collectible?.Attributes?["isLiquidContainerStopper"].AsBool() == true;

    public static ItemStack Detach(ItemStack stopper) => stopper.Clone();

    public static bool GiveReturnedBottle(IPlayerInventoryManager manager, ItemStack bottle, bool notify, EntityAgent entity) =>
        entity.World.Side == EnumAppSide.Client || manager.TryGiveItemstack(bottle, notify);

    public override void Dispose()
    {
        lock (LifecycleLock)
        {
            if (acquired)
            {
                acquired = false;
                if (--users == 0)
                    new Harmony(HarmonyId).Unpatch(Target, HarmonyPatchType.Transpiler, HarmonyId);
            }
        }
        base.Dispose();
    }
}
