using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace SawContainerGuard;

/// <summary>Only patches SaltyWater's optional mod; does not patch native block replacement.</summary>
public sealed class SawContainerGuardModSystem : ModSystem
{
    private Harmony? harmony;
    public override double ExecuteOrder() => 1.1;

    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        var upstream = AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == "SawChiseling");
        if (upstream == null)
        {
            api.Logger.Notification("[SawContainerGuard] Saw Chiseling absent; no patches needed.");
            return;
        }
        // Fail loudly for an incompatible installed upstream rather than silently leaving it destructive.
        harmony = new Harmony("kuboczoch.sawcontainerguard");
        if (!GuardPatches.Install(harmony, upstream))
            throw new InvalidOperationException("SawContainerGuard: installed Saw Chiseling has no supported patch type.");
        api.Logger.Notification("[SawContainerGuard] Container conversion guard installed on {0}.", api.Side);
    }

    public override void Dispose()
    {
        harmony?.UnpatchAll(harmony.Id);
        harmony = null;
        base.Dispose();
    }
}

public static class GuardPatches
{
    /// <summary>Validate all signatures before making any changes; absence is an intentional no-op.</summary>
    public static bool Install(Harmony harmony, Assembly upstream)
    {
        var type = upstream.GetType("SawChiseling.SawChiselPatch", false);
        if (type == null) return false;
        var targets = new[]
        {
            Find(type, "TryResolveTargetBlock", typeof(IWorldAccessor), typeof(BlockPos), typeof(Block).MakeByRefType()),
            Find(type, "PlaceChiseledBlock", typeof(IWorldAccessor), typeof(BlockPos), typeof(Block)),
            Find(type, "HandleInteractStart", typeof(CollectibleObject), typeof(ItemSlot), typeof(EntityAgent), typeof(BlockSelection), typeof(EntitySelection), typeof(bool), typeof(EnumHandHandling).MakeByRefType())
        };
        var prefixes = new[] { nameof(ResolvePrefix), nameof(PlacePrefix), nameof(InteractPrefix) };
        try
        {
            for (var i = 0; i < targets.Length; i++)
                harmony.Patch(targets[i], prefix: new HarmonyMethod(typeof(GuardPatches), prefixes[i]) { priority = Priority.First });
            return true;
        }
        catch
        {
            // Roll back only our own partially installed prefixes, never another mod's patches.
            foreach (var target in targets) harmony.Unpatch(target, HarmonyPatchType.Prefix, harmony.Id);
            throw;
        }
    }

    private static MethodInfo Find(Type type, string name, params Type[] args)
    {
        var method = type.GetMethod(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, args, null);
        if (method == null || method.ReturnType != typeof(bool))
            throw new NotSupportedException($"SawContainerGuard: incompatible upstream method {type.FullName}.{name}; supported: Saw Chiseling 0.1.2.");
        return method;
    }

    // Existing microblock editing is not conversion. Placement never receives this exemption.
    private static bool IsProtected(IWorldAccessor? world, BlockPos? pos, bool allowExistingEditing)
    {
        if (world == null || pos == null) return true;
        try
        {
            var accessor = world.BlockAccessor;
            if (accessor == null) return true;
            var block = accessor.GetBlock(pos);
            if (block == null) return true;
            if (block is BlockContainer || block.BlockBehaviors?.Any(behavior =>
                    behavior is BlockBehaviorContainer || behavior is BlockBehaviorCreatureContainer) == true)
                return true;
            var entity = accessor.GetBlockEntity(pos);
            if (entity is BlockEntityContainer || entity is IBlockEntityContainer) return true;
            // Never exempt an inventory-bearing subclass from the container check.
            if (allowExistingEditing && block is BlockMicroBlock &&
                (entity == null || entity is BlockEntityMicroBlock)) return false;
            // EntityClass catches not-yet-created/unloaded BEs. Reject *all* other BEs
            // so custom inventories and external state cannot be destroyed even without an interface.
            return !string.IsNullOrEmpty(block.EntityClass) || entity != null;
        }
        catch
        {
            // Unknown/unavailable chunk state must never authorize a destructive conversion.
            return true;
        }
    }

    private static bool ResolvePrefix(IWorldAccessor world, BlockPos pos, ref Block block, ref bool __result)
    {
        if (!IsProtected(world, pos, allowExistingEditing: true)) return true;
        block = null!;
        __result = false;
        return false;
    }

    private static bool PlacePrefix(IWorldAccessor world, BlockPos pos, Block sourceBlock, ref bool __result)
    {
        if (sourceBlock != null && !IsProtected(world, pos, allowExistingEditing: false)) return true;
        __result = false;
        return false;
    }

    private static bool InteractPrefix(EntityAgent byEntity, BlockSelection blockSel, ref bool __result)
    {
        if (!IsProtected(byEntity?.World, blockSel?.Position, allowExistingEditing: true)) return true;
        // This method is itself a Harmony prefix: true resumes the native held handler.
        // Do not suppress opening containers, consume the event, or enqueue client requests.
        __result = true;
        return false;
    }
}
