using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;

namespace Kuboczoch.VisualMining;

public sealed class ToolVisualSettings
{
    public bool Enabled { get; set; } = true;
    public float AnimationSpeed { get; set; } = 1f;
    public float ContactFrame { get; set; } = 6f;
    public float CameraShake { get; set; } = .15f;
    internal void Normalize(float frames, float contact, float speed) {
        AnimationSpeed = float.IsFinite(AnimationSpeed) ? Math.Clamp(AnimationSpeed, .1f, 3f) : speed;
        ContactFrame = float.IsFinite(ContactFrame) ? Math.Clamp(ContactFrame, 0.1f, frames - 1) : contact;
        CameraShake = float.IsFinite(CameraShake) ? Math.Clamp(CameraShake, 0, 1) : .15f;
    }
}
public sealed class VisualMiningConfig
{
    public ToolVisualSettings Pickaxe { get; set; } = new();
    public ToolVisualSettings Axe { get; set; } = new() { ContactFrame = 21 };
    public ToolVisualSettings Shovel { get; set; } = new() { ContactFrame = 15, AnimationSpeed = .7f };
    public bool VoxelChips { get; set; } = true;
    public bool ImpactSounds { get; set; } = true;
    public bool ImpactParticles { get; set; } = true;
    public void Normalize() {
        Pickaxe ??= new(); Axe ??= new() { ContactFrame = 21 }; Shovel ??= new() { ContactFrame = 15, AnimationSpeed = .7f };
        Pickaxe.Normalize(52, 6, 1); Axe.Normalize(40, 21, 1); Shovel.Normalize(40, 15, .7f);
    }
    public ToolVisualSettings? For(EnumTool? tool) => tool switch { EnumTool.Pickaxe => Pickaxe, EnumTool.Axe => Axe, EnumTool.Shovel => Shovel, _ => null };
}

/// <summary>Pure frame crossing state: never owns or returns gameplay damage.</summary>
public sealed class ContactTracker
{
    private object? identity;
    private int iteration;
    private float previous;
    private bool fired;
    public void Reset() { identity = null; fired = false; previous = 0; }
    public bool Sample(object key, int loop, float frame, float contact) {
        if (!float.IsFinite(frame) || !float.IsFinite(contact)) { Reset(); return false; }
        if (!Equals(identity, key)) { identity = key; iteration = loop; previous = frame; fired = frame >= contact; return false; }
        bool wrapped = iteration != loop || frame < previous;
        if (wrapped) { fired = false; previous = 0; iteration = loop; }
        bool hit = !fired && previous < contact && frame >= contact;
        if (hit) fired = true;
        previous = frame;
        return hit;
    }
}

public sealed class VisualMiningMod : ModSystem
{
    internal static VisualMiningMod? Instance;
    private ICoreClientAPI? api;
    internal VisualMiningConfig Config = new();
    private Harmony? harmony;
    private long tick;
    private BlockDamage? damage;
    private BlockSelection? selection;
    private ItemStack? stack;
    private long observedAt;
    private readonly ContactTracker contact = new();
    private bool enabled;
    private bool assetsAvailable;
    private LocalAnimationAssets? localAnimations;
    private bool registrationLogged;
    private bool contactLogged;
    private bool animatorWaitLogged;
    internal const string Prefix = "kvm-";
    public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Client;
    public override void StartClientSide(ICoreClientAPI capi) {
        api = capi;
        if (capi.ModLoader.IsModEnabled("immersivemining")) {
            capi.Logger.Warning("[Kuboczoch Visual Mining] Disabled: remove Immersive Mining before using this standalone replacement. Its gameplay changes are NOT undone by this mod.");
            return;
        }
        bool createConfig = false;
        try {
            var saved = capi.LoadModConfig<VisualMiningConfig>("kuboczochvisualmining.json");
            createConfig = saved == null;
            Config = saved ?? new();
        }
        catch (Exception e) { capi.Logger.Warning("[Kuboczoch Visual Mining] Invalid config; defaults used without overwriting your file: " + e.Message); Config = new(); }
        Config.Normalize();
        // Missing properties receive initializer defaults in memory. Never rewrite an existing
        // config (including explicit false values or fields from another version).
        if (createConfig) capi.StoreModConfig(Config, "kuboczochvisualmining.json");
        try {
        Instance = this;
        harmony = new Harmony("kuboczoch.visualmining.animation");
        InstallCorePatches(harmony);
        harmony.Patch(AccessTools.Method(typeof(Entity), "OnTesselation", new[] { typeof(Shape).MakeByRefType(), typeof(string), typeof(bool).MakeByRefType() }), prefix: new HarmonyMethod(typeof(VisualMiningMod), nameof(BeforeTesselation)));
        MiningSoundPatch.Install(ShouldSuppressSound);
        if (!MiningSoundPatch.Installed) capi.Logger.Warning("[Kuboczoch Visual Mining] Hit sound replacement disabled: " + MiningSoundPatch.LastError);
        if (Config.VoxelChips) CosmeticVoxelRenderer.Initialize(capi, value => value.Position.Equals(capi.World.Player?.CurrentBlockSelection?.Position) && IsMining(capi.World.Player, capi.World.Player?.CurrentBlockSelection, capi.World.Player?.InventoryManager.ActiveHotbarSlot));
        capi.Event.LevelFinalize += OnLevelReady;
        capi.Event.LeaveWorld += OnLeaveWorld;

        tick = capi.Event.RegisterGameTickListener(OnTick, 0);
        enabled = true;
        } catch (Exception e) {
            Dispose();
            capi.Logger.Warning("[Kuboczoch Visual Mining] Disabled safely: presentation initialization failed: " + e);
        }
    }
    public static void InstallCorePatches(Harmony target) {
        var observer = AccessTools.Method(typeof(Vintagestory.Client.NoObf.SystemMouseInWorldInteractions), "ContinueBreakSurvival", new[] { typeof(BlockSelection), typeof(Block), typeof(float) });
        var getter = AccessTools.Method(typeof(CollectibleObject), nameof(CollectibleObject.GetHeldTpHitAnimation));
        if (observer == null || getter == null) throw new MissingMethodException("Unsupported game mining presentation methods");
        try {
            target.Patch(getter, postfix: new HarmonyMethod(typeof(VisualMiningMod), nameof(AnimationPostfix)));
            target.Patch(observer, postfix: new HarmonyMethod(typeof(VisualMiningMod), nameof(DamageObserved)));
        } catch { target.UnpatchAll(target.Id); throw; }
    }
    private static void BeforeTesselation(Entity __instance, ref Shape entityShape, ref bool shapeIsCloned) {
        var self = Instance;
        if (self?.localAnimations == null || self.api?.World.Player?.Entity?.EntityId != __instance.EntityId || entityShape == null) return;
        self.localAnimations.RegisterMetadata(__instance.Properties.Client);
        self.localAnimations.PrepareTesselation(ref entityShape, ref shapeIsCloned);
    }
    private void OnLevelReady() {
        var entity = api?.World?.Player?.Entity;
        if (entity?.Properties?.Client == null) return;
        try {
            localAnimations ??= new LocalAnimationAssets(new[] { "pickaxenew", "axenew", "axechophorizontalnew", "shovelnew" }.Select(name => api!.Assets.Get(new AssetLocation("kuboczochvisualmining", "patches/animations/" + name + ".json")).ToText()));
        } catch (Exception error) {
            assetsAvailable = false;
            api!.Logger.Warning("[Kuboczoch Visual Mining] Local animation registration failed; vanilla presentation retained: " + error.Message);
            return;
        }
        localAnimations.RegisterMetadata(entity.Properties.Client);
        assetsAvailable = localAnimations.Registered(entity.Properties.Client);
        // The first tessellation can precede LevelFinalize. Request a normal client-only
        // rebuild so the LoadAnimator hook also repairs already-created FP/TP animators.
        if (entity.SelfFpAnimManager?.Animator?.GetAnimationState("kvm-pickaxe") == null || entity.AnimManager?.Animator?.GetAnimationState("kvm-pickaxe") == null) entity.MarkShapeModified();
        if (!registrationLogged) {
            api!.Logger.Notification("[Kuboczoch Visual Mining] Registered 8 local animation pairs; awaiting player animator readiness where necessary.");
            registrationLogged = true;
        }
    }
    internal bool IsMining(IPlayer? player, BlockSelection? block, ItemSlot? slot) {
        if (!enabled || !assetsAvailable || api == null || player?.Entity == null || player.Entity.EntityId != api.World.Player?.Entity.EntityId || block?.Position == null || slot?.Itemstack == null) return false;
        if (player.CurrentEntitySelection != null || !player.Entity.Controls.LeftMouseDown || player.WorldData.CurrentGameMode != EnumGameMode.Survival) return false;
        if (player.CurrentBlockSelection?.Position?.Equals(block.Position) != true) return false;
        var settings = Config.For(slot.Itemstack.Collectible.GetTool(slot));
        return settings?.Enabled == true && api.World.BlockAccessor.GetBlock(block.Position).Id != 0;
    }
    private string AnimationCode(EnumTool tool, IClientPlayer player, BlockSelection block) {
        string code = tool switch { EnumTool.Pickaxe => "kvm-pickaxe", EnumTool.Shovel => "kvm-shovel", _ => Horizontal(api!, block) ? "kvm-axe-horizontal" : "kvm-axe" };
        return player.Entity.LeftHandItemSlot?.Empty == false ? code + "-onehand" : code;
    }
    private bool CanPresent(IClientPlayer player, BlockSelection block, ItemSlot slot) {
        var tool = slot.Itemstack?.Collectible.GetTool(slot);
        if (tool == null || Config.For(tool)?.Enabled != true) return false;
        var manager = player.CameraMode == EnumCameraMode.FirstPerson ? player.Entity.SelfFpAnimManager : player.Entity.AnimManager;
        bool ready = manager?.Animator != null && manager.GetAnimationState(AnimationCode(tool.Value, player, block)) != null;
        if (!ready && !animatorWaitLogged) {
            api!.Logger.Notification("[Kuboczoch Visual Mining] Local animator not ready; retaining vanilla until player tessellation completes.");
            animatorWaitLogged = true;
        }
        return ready;
    }
    private bool ShouldSuppressSound(IPlayer player, BlockSelection block, ItemSlot slot) => Config.ImpactSounds && IsMining(player, block, slot) && player is IClientPlayer local && CanPresent(local, block, slot);
    internal static void AnimationPostfix(ItemSlot slot, Entity byEntity, ref string __result) {
        var self = Instance;
        var player = self?.api?.World.Player;
        if (self == null || player == null || byEntity == null || slot.Itemstack == null || player.Entity.EntityId != byEntity.EntityId || !self.IsMining(player, player.CurrentBlockSelection, slot) || !self.CanPresent(player, player.CurrentBlockSelection, slot)) return;
        var tool = slot.Itemstack.Collectible.GetTool(slot);
        if (tool != null) __result = self.AnimationCode(tool.Value, player, player.CurrentBlockSelection);
    }
    private static bool Horizontal(ICoreClientAPI capi, BlockSelection sel) {
        string path = capi.World.BlockAccessor.GetBlock(sel.Position).Code.Path;
        return path.StartsWith("log-placed-") && (path.EndsWith("-ns") || path.EndsWith("-we"));
    }
    private void Observe(BlockDamage value) {
        var player = api?.World.Player; var slot = player?.InventoryManager.ActiveHotbarSlot;
        if (!IsMining(player, player?.CurrentBlockSelection, slot) || value.Position.Equals(player!.CurrentBlockSelection.Position) != true) { Reset(); return; }
        if (stack != null && !ReferenceEquals(stack, slot!.Itemstack)) Reset();
        else if (damage != null && damage.Position.Equals(value.Position) != true) ClearObservation();
        damage = value; selection = player!.CurrentBlockSelection.Clone(); stack = slot!.Itemstack; observedAt = api!.World.ElapsedMilliseconds;
        if (Config.VoxelChips) CosmeticVoxelRenderer.Update(value, selection, stack!.Collectible.GetTool(slot)!.Value);
    }
    internal static void DamageObserved(BlockDamage? ___curBlockDmg) {
        var self = Instance;
        if (self == null) return;
        if (___curBlockDmg == null || ___curBlockDmg.RemainingResistance <= 0) self.Reset();
        else self.Observe(___curBlockDmg);
    }
    private void OnTick(float dt) {
        var capi = api; var player = capi?.World.Player; var slot = player?.InventoryManager.ActiveHotbarSlot;
        if (!IsMining(player, player?.CurrentBlockSelection, slot) || (stack != null && !ReferenceEquals(stack, slot!.Itemstack))) { Reset(); return; }
        // Missing/stale damage is not the end of an enabled held-hit animation.
        // Retargeting and slow tools must keep their normal animation/contact pipeline.
        if (damage == null) return;
        if (capi == null || selection == null || !IsMining(player, selection, slot) || capi.World.ElapsedMilliseconds - observedAt > 200 || capi.World.BlockAccessor.GetBlock(selection.Position).Id != damage.Block.Id) { ClearObservation(); return; }
        EnumTool tool = slot!.Itemstack!.Collectible.GetTool(slot)!.Value;
        var settings = Config.For(tool)!;
        string animation = AnimationCode(tool, player!, selection);
        var manager = player!.CameraMode == EnumCameraMode.FirstPerson ? player.Entity.SelfFpAnimManager : player.Entity.AnimManager;
        var running = manager?.GetAnimationState(animation);
        if (running == null || !manager!.IsAnimationActive(animation)) { contact.Reset(); return; }
        // Metadata speed is presentation only; never tier-scaled and never used in mining calculations.
        if (running.meta != null) running.meta.AnimationSpeed = settings.AnimationSpeed;
        object key = (selection.Position.ToString(), tool, slot.Itemstack, manager, animation);
        if (!contact.Sample(key, running.Iterations, running.CurrentFrame, settings.ContactFrame)) return;
        if (!contactLogged) {
            capi.Logger.Notification("[Kuboczoch Visual Mining] First animated mining contact observed: " + animation + "; block=" + damage.Block.Code);
            contactLogged = true;
        }
        if (Config.VoxelChips) CosmeticVoxelRenderer.CommitImpact(damage, selection, tool);
        if (Config.ImpactSounds && MiningSoundPatch.Installed) {
            var sounds = damage.Block.GetSounds(capi.World.BlockAccessor, selection)?.GetHitSound(player);
            if (sounds != null) {
                var p = selection.Position;
                // Local client only. The client engine path does not broadcast tick impacts; server
                // merely processes final break. dualCallByPlayer prevents local duplicate playback.
                capi.World.PlaySoundAt(sounds.Value, p.X + .5, p.Y + .5, p.Z + .5, p.dimension, player, 1);
            }
        }
        if (settings.CameraShake > 0) capi.World.AddCameraShake(settings.CameraShake);
        if (Config.ImpactParticles) {
            var particles = new BlockBreakingParticleProps { blockdamage = damage };
            particles.Init(capi);
            capi.World.SpawnParticles(particles, player);
        }
    }
    private void ClearObservation() {
        if (damage != null) CosmeticVoxelRenderer.Reset();
        damage = null; selection = null; stack = null; contact.Reset();
    }
    private void Reset() {
        ClearObservation();
        var entity = api?.World?.Player?.Entity;
        // AnimManager can alias SelfFpAnimManager in first person. Always visit
        // the explicit TP and FP managers rather than trusting camera routing.
        StopOwnedAnimations(entity?.TpAnimManager);
        StopOwnedAnimations(entity?.SelfFpAnimManager);
    }
    private static void StopOwnedAnimations(PlayerAnimationManager? manager) {
        if (manager == null) return;
        // Include the engine's -fp/-ifp variants, but never call StopHeldHitAnim:
        // that would also stop vanilla or another mod's newly selected held animation.
        foreach (var code in manager.ActiveAnimationsByAnimCode.Keys.Where(code => code.StartsWith(Prefix, StringComparison.Ordinal)).ToArray()) manager.StopSelfAnimation(code);
    }
    private void OnLeaveWorld() { Reset(); assetsAvailable = false; }
    public override void Dispose() {
        enabled = false; Reset();
        if (api != null) { if (tick != 0) api.Event.UnregisterGameTickListener(tick); api.Event.LevelFinalize -= OnLevelReady; api.Event.LeaveWorld -= OnLeaveWorld; }
        MiningSoundPatch.Dispose(); CosmeticVoxelRenderer.Dispose(); harmony?.UnpatchAll("kuboczoch.visualmining.animation");
        if (ReferenceEquals(Instance, this)) Instance = null;
        base.Dispose();
    }
}
