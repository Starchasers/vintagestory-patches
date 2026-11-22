using HarmonyLib;
using RoughSeas.Shipyard;
using Vintagestory.API.Common;

namespace KuboczochPitchGlueCompat;

/// <summary>Substitute raw resin only for construction Pitch work; native lifecycle and saves remain untouched.</summary>
public sealed class ResinConstructionMod : ModSystem
{
    public const string HarmonyId = "kuboczoch.pitchgluecompat.resin";
    public const string ActionKey = "kuboczochpitchgluecompat:shipyard-resin";
    private Harmony? harmony;
    // RoughSeas uses the default execution order (0.1); finish after its material registration.
    public override double ExecuteOrder() => 1;
    public override bool ShouldLoad(EnumAppSide side) => side is EnumAppSide.Server or EnumAppSide.Client;

    public override void Start(ICoreAPI api)
    {
        if (harmony != null) return;
        if (api.ModLoader.GetMod("roughseas")?.Info.Version != "5.0.1")
        {
            api.Logger.Warning("[Resin Construction] Requires RoughSeas 5.0.1; resin substitution disabled.");
            return;
        }
        var installed = new Harmony(HarmonyId);
        installed.Patch(AccessTools.Method(typeof(Laying), nameof(Laying.For), [typeof(FramingStage), typeof(FramingConfig)]),
            postfix: new HarmonyMethod(typeof(ResinConstructionMod), nameof(AfterLayingFor)));
        harmony = installed;
    }

    public override void AssetsFinalize(ICoreAPI api)
    {
        if (harmony == null) return;
        // Server finalization installs the native behavior before collectible synchronization.
        // Client finalization also repairs a locally constructed registry; idempotent with sync.
        var resin = api.World.GetItem(new AssetLocation("game:resin"));
        if (resin == null)
        {
            api.Logger.Error("[Resin Construction] game:resin missing; cannot attach native framing behavior.");
            return;
        }
        if (resin.CollectibleBehaviors?.OfType<CollectibleBehaviorFraming>().Any() == true) return;
        resin.CollectibleBehaviors = [.. resin.CollectibleBehaviors ?? [], new CollectibleBehaviorFraming(resin)];
    }

    private static void AfterLayingFor(FramingStage __0, ref Laying __result)
    {
        if (__0.Work == FramingWork.Pitch)
            __result = new PieceLaying(new AssetLocation("game:resin"), ActionKey);
    }

    public override void Dispose()
    {
        harmony?.UnpatchAll(HarmonyId);
        harmony = null;
        base.Dispose();
    }
}
