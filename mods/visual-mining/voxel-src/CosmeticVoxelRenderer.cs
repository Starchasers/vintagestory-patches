using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Client.Tesselation;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.Client.NoObf;
using Vintagestory.GameContent;

namespace Kuboczoch.VisualMining;

/// <summary>Client-only, transient mesh substitution. Call lifecycle/update methods on the client thread.</summary>
public static class CosmeticVoxelRenderer
{
    private const string PatchId = "kuboczoch.visualmining.voxels";
    private static readonly object Gate = new();
    private static readonly Dictionary<Key, Entry> Entries = new();
    private static ICoreClientAPI? api;
    private static Harmony? harmony;
    private static System.Func<BlockDamage, bool>? allowPresentation;
    private static readonly Dictionary<string, int> DiagnosticCounts = new();
    // Bounded per pipeline stage, including from the chunk tessellation worker.
    private static void Diagnostic(string stage, string detail)
    {
        lock (Gate)
        {
            DiagnosticCounts.TryGetValue(stage, out int count);
            if (count >= 8) return;
            DiagnosticCounts[stage] = count + 1;
            api?.Logger.Notification($"[Kuboczoch Visual Mining] {stage} #{count + 1}/8: {detail}");
        }
    }
    private readonly record struct Key(int X, int Y, int Z, int Dimension)
    {
        public static Key From(BlockPos p) => new(p.X, p.Y, p.Z, p.dimension);
        public BlockPos Position() => new BlockPos(Dimension).Set(X, Y, Z);
    }
    private sealed class Entry(int blockId)
    {
        public VoxelDamageState State = new(blockId);
        public float Observed;
        public float Committed;

        // Set only after the replacement mesh was appended successfully, not on mining/impact.
        public bool MeshReplaced;
    }

    /// <summary>Covers VS 1.22.7 cube/JSON/TopSoil dispatch paths and local crack decals. No gameplay patches.</summary>
    public static void Initialize(ICoreClientAPI capi, System.Func<BlockDamage, bool>? presentationGuard = null)
    {
        ArgumentNullException.ThrowIfNull(capi);
        if (api != null) Dispose();
        api = capi;
        lock (Gate) DiagnosticCounts.Clear();
        allowPresentation = presentationGuard;
        harmony = new Harmony(PatchId);
        try
        {
            harmony.CreateClassProcessor(typeof(CubeMeshPatch)).Patch();
            harmony.CreateClassProcessor(typeof(JsonMeshPatch)).Patch();
            harmony.CreateClassProcessor(typeof(BleedingCubeMeshPatch)).Patch();
            harmony.CreateClassProcessor(typeof(TopsoilMeshPatch)).Patch();
            harmony.CreateClassProcessor(typeof(BleedingJsonMeshPatch)).Patch();
            harmony.CreateClassProcessor(typeof(NeighborMeshPatch)).Patch();
            harmony.CreateClassProcessor(typeof(CrackDecalPatch)).Patch();
        }
        catch
        {
            harmony.UnpatchAll(PatchId);
            harmony = null;
            api = null;
            throw;
        }
    }

    /// <summary>
    /// Samples vanilla cumulative damage (1 - RemainingResistance / resistance), without changing it.
    /// Pass impactCommit=true only at the animation's impact to reveal damage accumulated since the previous impact.
    /// Repeated samples/commits with unchanged damage are no-ops. A decrease resets the cosmetic state.
    /// Selection.HitPosition is block-local, as supplied by the game.
    /// </summary>
    public static void Update(BlockDamage? damage, BlockSelection? selection, EnumTool tool, bool impactCommit = false)
    {
        if (api == null || damage?.Position == null || damage.Block == null) return;
        BlockPos pos = damage.Position;
        Diagnostic("mining-observation", $"{damage.Block.Code} at {pos}; remaining={damage.RemainingResistance}; tool={tool}; draw={damage.Block.DrawType}; opaque={(int)damage.Block.SideOpaque}; solid={(int)damage.Block.SideSolid}");
        if (selection?.Position == null || !Key.From(pos).Equals(Key.From(selection.Position)) ||
            selection.Face == null || selection.HitPosition == null || !Supported(damage.Block, tool))
        {
            Diagnostic("eligibility-rejected", $"{damage.Block.Code}: selection/face/hit mismatch or {Eligibility(damage.Block, tool)}");
            Reset(pos);
            return;
        }
        // Never retain a ghost mesh after vanilla has removed or replaced the block.
        if (api.World.BlockAccessor.GetBlock(pos).BlockId != damage.Block.BlockId)
        {
            Reset(pos);
            return;
        }
        float resistance = damage.Block.GetResistance(api.World.BlockAccessor, pos);
        if (!float.IsFinite(resistance) || resistance <= 0 || !float.IsFinite(damage.RemainingResistance)) return;
        float progress = Math.Clamp(1 - damage.RemainingResistance / resistance, 0, 1);
        Diagnostic("progress", $"{damage.Block.Code} at {pos}; remaining={damage.RemainingResistance}; resistance={resistance}; normalized={progress}; contact={impactCommit}");
        if (progress <= 0 || progress >= 1) { Reset(pos); return; }
        bool dirty = false;
        lock (Gate)
        {
            Key key = Key.From(pos);
            if (!Entries.TryGetValue(key, out Entry? entry) || entry.State.BlockId != damage.Block.BlockId)
            {
                // Bounded transient state; eviction only redraws client meshes.
                if (Entries.Count >= 128) return;
                entry = new Entry(damage.Block.BlockId);
                Entries[key] = entry;
            }

            if (progress + 0.0001f < entry.Observed)
            {
                entry.State = new VoxelDamageState(damage.Block.BlockId);
                entry.Committed = 0;
                entry.MeshReplaced = false;
                dirty = true;
            }
            entry.Observed = progress;
            float delta = progress - entry.Committed;
            if (impactCommit && delta > 0.0001f)
            {
                dirty |= entry.State.ApplyHit(selection.HitPosition, selection.Face, tool,
                    Math.Clamp(delta * 8, 0.45f, 1.75f), true);
                entry.Committed = progress;
                Diagnostic("impact-commit", $"{damage.Block.Code} at {pos}; progress={progress}; delta={delta}; redraw={dirty}");
            }
        }
        if (dirty) Redraw(pos);
    }

    public static void CommitImpact(BlockDamage? damage, BlockSelection? selection, EnumTool tool) => Update(damage, selection, tool, true);

    /// <summary>Reset one block, or all blocks when omitted. Redraws original vanilla meshes.</summary>
    public static void Reset(BlockPos? position = null)
    {
        List<BlockPos> positions = new();
        lock (Gate)
        {
            if (position != null)
            {
                if (Entries.Remove(Key.From(position))) positions.Add(position.Copy());
            }
            else
            {
                positions.AddRange(Entries.Keys.Select(k => k.Position()));
                Entries.Clear();
            }
        }
        foreach (BlockPos pos in positions) { Diagnostic("restore", $"vanilla redraw at {pos}"); Redraw(pos); }
    }

    public static void Dispose()
    {
        Reset();
        harmony?.UnpatchAll(PatchId);
        harmony = null;
        api = null;
        allowPresentation = null;
    }

    private static bool Supported(Block block, EnumTool tool) =>
        tool is EnumTool.Pickaxe or EnumTool.Axe or EnumTool.Shovel && IsFullCube(block);

    private static string Eligibility(Block block, EnumTool tool)
    {
        if (tool is not (EnumTool.Pickaxe or EnumTool.Axe or EnumTool.Shovel)) return "unsupported tool";
        if (block.ForFluidsLayer) return "fluid layer";
        if (!block.SideOpaque.All) return "not all sides opaque";
        if (!block.SideSolid.All) return "not all sides solid";
        if (block.CollisionBoxes is not { Length: 1 }) return "collision box count is not one";
        if (!IsFullCube(block)) return $"collision bounds are not unit cube: {block.CollisionBoxes[0]}";
        return "eligible full cube";
    }

    private static bool IsFullCube(Block block)
    {
        if (block == null || block.ForFluidsLayer || !block.SideOpaque.All || !block.SideSolid.All ||
            block.CollisionBoxes is not { Length: 1 }) return false;
        Cuboidf box = block.CollisionBoxes[0];
        return Math.Abs(box.X1) < 0.0001f && Math.Abs(box.Y1) < 0.0001f && Math.Abs(box.Z1) < 0.0001f &&
            Math.Abs(box.X2 - 1) < 0.0001f && Math.Abs(box.Y2 - 1) < 0.0001f && Math.Abs(box.Z2 - 1) < 0.0001f;
    }

    // MarkBlockDirty on the CLIENT accessor invalidates render meshes only; never SetBlock/ExchangeBlock/Commit.
    private static void Redraw(BlockPos pos)
    {
        api?.World.BlockAccessor.MarkBlockDirty(pos);
        foreach (BlockFacing face in BlockFacing.ALLFACES) api?.World.BlockAccessor.MarkBlockDirty(pos.AddCopy(face));
    }

    private static int NeighborFlags(int x, int y, int z, int dimension)
    {
        int flags = 0;
        lock (Gate)
        {
            for (int i = 0; i < 6; i++)
            {
                var offset = TileSideEnum.OffsetByTileSide[i];
                if (Entries.TryGetValue(new Key(x + offset.X, y + offset.Y, z + offset.Z, dimension), out Entry? entry) &&
                    entry.Committed > 0 && entry.State.HasOpenBoundary(-offset.X, -offset.Y, -offset.Z))
                    flags |= TileSideEnum.ToFlags(i);
            }
        }
        return flags;
    }

    private static bool Tesselate(TCTCache vars)
    {
        ICoreClientAPI? capi = api;
        if (capi == null || vars?.block == null || !IsFullCube(vars.block)) return false;
        List<uint> cuboids;
        Entry entry;
        VoxelDamageState state;
        Key key = new(vars.posX, vars.posY, vars.posZ, vars.dimension);
        lock (Gate)
        {
            if (!Entries.TryGetValue(key, out entry!) ||
                entry.State.BlockId != vars.block.BlockId || entry.Committed <= 0) return false;
            // A rebuild may fall back to vanilla (for example when lighting is unavailable).
            entry.MeshReplaced = false;
            state = entry.State;
            cuboids = entry.State.BuildCuboids();
        }
        BlockPos pos = new BlockPos(vars.dimension).Set(vars.posX, vars.posY, vars.posZ);
        long light = vars.tct.jsonTesselator.SetUpLightRGBs(vars);
        int oldFlags = vars.drawFaceFlags;
        for (int i = 0; i < 6; i++) vars.drawFaceFlags |= TileSideEnum.ToFlags(i);
        try
        {
            MeshData mesh = BlockEntityMicroBlock.CreateMesh(capi, cuboids, new[] { vars.block.BlockId }, null, pos, null, 0);
            vars.tct.jsonTesselator.AddJsonModelDataToMesh(mesh, light != 4934475 ? 1 : 0, vars, vars.tct, null);
        }
        finally { vars.drawFaceFlags = oldFlags; }
        lock (Gate)
        {
            // A concurrent reset/replacement must not bless a different entry or reset state.
            if (Entries.TryGetValue(key, out Entry? current) && ReferenceEquals(current, entry) &&
                ReferenceEquals(current.State, state)) current.MeshReplaced = true;
        }
        Diagnostic("mesh-replaced", $"{vars.block.Code} world={pos}; draw={vars.block.DrawType}; cuboids={cuboids.Count}; chunk pool append completed");
        return true;
    }

    private static bool BeforeTesselate(TCTCache vars)
    {
        try { if (Tesselate(vars)) return false; }
        catch (Exception error)
        {
            Diagnostic("mesh-error", $"{vars?.block?.Code}: {error}");
            throw;
        }
        if (vars?.block != null) vars.drawFaceFlags |= NeighborFlags(vars.posX, vars.posY, vars.posZ, vars.dimension);
        return true;
    }

    private static bool HasMeshReplacement(BlockDamage damage)
    {
        if (damage?.Position == null || damage.Block == null) return false;
        lock (Gate)
        {
            return Entries.TryGetValue(Key.From(damage.Position), out Entry? entry) &&
                entry.State.BlockId == damage.Block.BlockId && entry.Committed > 0 && entry.MeshReplaced;
        }
    }

    [HarmonyPatch(typeof(SystemRenderDecals), "OnPlayerBreakingBlock")]
    private static class CrackDecalPatch
    {
        private static bool Prefix(BlockDamage blockDamage)
        {
            // Skip only the local supported target's vanilla full-cube crack overlay.
            // Never alter the damage record or final-break/unbreaking cleanup.
            var capi = api;
            var slot = capi?.World.Player?.InventoryManager.ActiveHotbarSlot;
            var tool = slot?.Itemstack?.Collectible.GetTool(slot);
            if (capi == null || tool == null || blockDamage?.Block == null ||
                !Supported(blockDamage.Block, tool.Value) || !HasMeshReplacement(blockDamage)) return true;
            try { return allowPresentation?.Invoke(blockDamage) != true; }
            catch { return true; }
        }
    }
    [HarmonyPatch(typeof(CubeTesselator), "Tesselate")]
    private static class CubeMeshPatch { private static bool Prefix(TCTCache vars) => BeforeTesselate(vars); }
    [HarmonyPatch(typeof(JsonTesselator), "Tesselate")]
    private static class JsonMeshPatch { private static bool Prefix(TCTCache vars) => BeforeTesselate(vars); }
    // Cube=10 dispatches here, not CubeTesselator (Transparent=12). These are independent classes.
    [HarmonyPatch(typeof(BleedingCubeTesselator), "Tesselate")]
    private static class BleedingCubeMeshPatch { private static bool Prefix(TCTCache vars) => BeforeTesselate(vars); }
    // Peat, raw clay and grass-covered soil use the independent TopSoil=14 dispatcher.
    // The engine microblock mesher already preserves its packed secondary grass UVs/render pass.
    [HarmonyPatch(typeof(TopsoilTesselator), "Tesselate")]
    private static class TopsoilMeshPatch { private static bool Prefix(TCTCache vars) => BeforeTesselate(vars); }
    [HarmonyPatch(typeof(BleedingJsonTesselator), "Tesselate")]
    private static class BleedingJsonMeshPatch { private static bool Prefix(TCTCache vars) => BeforeTesselate(vars); }
    [HarmonyPatch(typeof(ChunkTesselator), "TesselateBlock", new[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
    private static class NeighborMeshPatch
    {
        private static void Prefix(int index3d, int lX, int baseX, int posZ, byte[] ___currentChunkDraw32, TCTCache ___vars)
        {
            int flags = NeighborFlags(baseX + lX, ___vars.posY, posZ, ___vars.dimension);
            if (flags != 0) ___currentChunkDraw32[index3d] |= (byte)flags;
        }
    }
}
