# Cosmetic voxel renderer integration

Production sources: `CosmeticVoxelRenderer.cs` and `VoxelDamageState.cs`, namespace `Kuboczoch.VisualMining`.
Compile/include these TWO files only in the main mod (do not recursively include tests or generated obj files). No gameplay patches or original mod dependencies. References: VintagestoryAPI, VintagestoryLib, VSSurvivalMod and 0Harmony. Main mod should NOT call PatchAll over this assembly: renderer Initialize installs its own four private presentation patches (three mesh hooks plus a local crack-decal filter).

```csharp
// StartClientSide
CosmeticVoxelRenderer.Initialize(capi);
// Each continuous-damage update, AFTER vanilla updates BlockDamage:
CosmeticVoxelRenderer.Update(damage, selection, tool);
// Exactly when your animation reaches impact:
CosmeticVoxelRenderer.CommitImpact(damage, selection, tool);
// Equivalent: Update(damage, selection, tool, impactCommit: true).
// On mining cancel / target change / block break / player leaving world:
CosmeticVoxelRenderer.Reset(); // or Reset(previousPosition)
// Dispose:
CosmeticVoxelRenderer.Dispose();
```

Call these methods on the client thread. tool is a nonnullable EnumTool; guard a nullable held-tool before calling. selection must correspond to damage.Position; HitPosition is the game-supplied block-local point. Sample AFTER vanilla updates remaining resistance, not before; impacts with unchanged progress intentionally do not double-carve. Continuous damage accumulates as observed progress; committed impacts reveal the accumulated delta. Update does not mutate the supplied BlockDamage. Source block replacement, healed/reset damage, completion and unsupported geometry reset cosmetic state when sampled. Parent must call Reset on cancellation/block break even if there is no next sample; this avoids stale cosmetics when a block at the same coordinate is later replaced by the same block ID.

Only opaque, solid, single-full-cube blocks and pickaxe/axe/shovel are supported. No tool-restriction bypass, drops, reinforcement logic, resistance changes, block replacement, network messages or persistence. Client MarkBlockDirty invalidates target and neighboring meshes; original block data is not changed. State is lock-protected for tessellation workers, keyed by position+dimension and capped at 128 entries.

Standalone validation (net10.0):
`dotnet build VoxelRenderer.csproj --nologo`
`dotnet run --project tests/VoxelTests.csproj`

Tests exercise 18 tool/face combinations with 12 sequential hits, monotonic removal, nonempty surviving cuboids, opened boundaries and exact undo. They also install/uninstall all three Harmony tessellation patches against the actual supplied game assemblies. These are not a graphical in-game smoke test: appearance, lighting and chunk rebuild timing still require launching the client.
