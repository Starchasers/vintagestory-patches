# 1.0.3 peat/clay root cause and verification boundary

## Proven defect

Actual 1.22.7 `soil/peat.json` and `soil/clay.json` both declare `BlockSoilDeposit`, Soil material, **TopSoil drawtype and renderpass**. Peat has none/verysparse grass; clay has blue/red/fire × none/verysparse. `BlockSoilDeposit` inherits full-cube collision, side-opaque and side-solid defaults. All pass existing eligibility. Clay is not an unsupported thin shape, and the shovel does not lose its inherited hit-animation getter: all 16 material variants in `itemtypes/tool/shovel.json` declare `shoveldig`.

`ChunkTesselator` maps **TopSoil=14 to independent TopsoilTesselator**, which does not inherit any previously patched cube/JSON method. 1.0.2 observed progress and committed a transient chip entry, then redrew the unchanged vanilla mesh. Adding a material whitelist would not fix that. The only production logic addition in 1.0.3 is the Topsoil renderer prefix and its registration in the real initializer. Existing geometry eligibility, animation settings, damage/audio observers and every gameplay instruction remain unchanged.

## Real RED/GREEN

The same current test sources compile against the actual archived 1.0.2 DLL in a separate harness (required because its assembly identity is 1.0.2.0). Real production Initialize and actual ChunkTesselator dispatch then reproduce peat: unchanged **24 vertices / 36 indices**, no hook/light/source/emission calls despite a committed impact. This is an actual runtime regression failure, not a static source assertion or an artificial private-prefix-only test.

With 1.0.3, all **57 fixtures / 56 shovel-target variants** pass actual dispatch, mesh pool emission, strict damage-observation/contact sequencing, duplicate-contact checks, and reset. Peat/clay emit **1076 vertices / 1614 indices**, with interior geometry and exactly one replacement stage traversal. All restoration hashes include XYZ, primary UV, RGBA, flags, indices, packed secondary grass UV and color-map data. Actual game constructor defaults are used; selected asset properties and ByType rules determine class, textures, resistance, drawtype and renderpass. Sand/gravel's base cube shape is tessellated from the real shape asset by the real ShapeTesselator. World services and atlas positions are controlled read-only test boundaries, not an active game.

23 TopSoil fixtures verify the resolved variant-specific `specialSecondTexture`, packed UV2 count/range, nonzero grass color-map data, source TopSoil renderpasses and the destination TopSoil LOD1 pool. The engine's existing BlockEntityMicroBlock mesher already produces those channels; JsonTesselator preserves them. The independent 1.0.2 BleedingCube/BleedingJson hooks remain intact.

Activation is checked separately: actual metadata/shape registration, initialized real ClientAnimator, inherited shovel getter for all 16 material variants, animation-manager start and actual animator frame advancement across the configured contact. Full SystemMouseInWorldInteractions gameplay execution is **not** simulated; its real observer registration is tested, then snapshots are fed to the production renderer. This is stronger CPU evidence, not a rendered play session.

## Limits and non-fixes

- No new peat/clay runtime log or live render was available. Earlier inability to mine was guest mode, not this mod; no permission, game-mode, mining-lock or server change is made.
- Original shovel contact remains frame 15 at speed .7. A sufficiently fast legitimate break can finish before first visual contact, resetting the transient target before chips/audio/contact particles happen. This is separate from the reproduced renderer miss; no mining delay or synthetic post-break impact is added. Nominal contact is about 0.714 seconds before any easing. Do not promise a visible carved intermediate block for every instant/fast break.
- Vanilla TopsoilTesselator selects snowed secondary textures when snow is above and randomizes the top grass UV. Existing microblock substitution does not exactly reproduce those two appearance details while carved. Reset re-enters the vanilla renderer and restores its exact channels. Snowy appearance needs live-client validation.
- Only opaque full unit cubes on supported cube/JSON/TopSoil dispatcher paths are claimed. Partial, translucent, panned layers and custom drawtypes stay outside this coverage matrix. Unsupported targets retain the vanilla mesh/crack fallback.
- GPU upload, actual in-world audiovisual alignment, other animation/render mods and multiplayer are not tested here. No game/original DLLs or game asset fixtures are redistributed.

Machine-readable fixture list/reference hashes and raw logs are in source `evidence/1.0.3/`. Reproduce with `tests/run_verification.py`; optional `--legacy-install` accepts the original 1.0.2 install ZIP. Old distribution ZIPs remain unchanged.

---

# Historical 1.0.2 root cause and verification boundary

## Actual reported session

`doc_8e983586add2_client-main.log` line 1227 registers 8 local animation pairs; line 1359 reports contact for `kvm-axe-onehand` on `game:log-grown-birch-ud`. That proves animation activation, not mesh emission. FallingTree 0.5.2 is present. No matching renderer exception appears in the targeted client-log search.

## Engine path traced against the 1.22.7 reference assemblies

1. `SystemMouseInWorldInteractions.ContinueBreakSurvival` loads/creates `curBlockDmg` and assigns the result of `block.OnGettingBroken(...)` to its `RemainingResistance`. That invokes the active collectible's `OnBlockBreaking`. `ItemAxe.OnBlockBreaking` uses `FindTree`, calculates/stores `treeResistance` and delegates to base with **dt / treeResistance**. It slows damage; it does not inflate remaining block resistance. No cosmetic baseline rewrite is needed or included. The existing observer reads the post-call snapshot, stores a copied selection and checks its 200-ms observation lifetime at animation contact. Existing tool/game-mode/input/stack checks and reset behavior are unchanged.
2. Real `assets/survival/blocktypes/wood/woodtyped/log.json` supplies `BlockLog`, Wood material and `drawtype: cube`. Birch texture expansion uses `*-ud` plus `{wood}=birch`. It has no collision/side-solid/side-opaque overrides: real constructor defaults are full cube and flags 63; RenderPass Opaque is numeric 0. Thus rejecting logs because of enum truthiness/null side flags was NOT the demonstrated bug.
3. The actual `BlockTesselatorManager`/`ChunkTesselator` dispatch table maps Cube=10 to `BleedingCubeTesselator`. The previously patched `CubeTesselator` is used for Transparent=12. Opaque JSON similarly dispatches through independent `BleedingJsonTesselator`. They do not inherit the previously patched Tesselate methods. Therefore observation and commit could succeed but chunk redraw re-emitted the unchanged vanilla cube.
4. Fix adds both missing sibling-class prefixes to the existing renderer. It retains the user-owned original transient voxel/cuboid algorithm, `BlockEntityMicroBlock.CreateMesh`, texture source, `JsonTesselator.SetUpLightRGBs`, seven-argument `AddJsonModelDataToMesh`, and client mesh invalidation/restoration. TCTCache posX/Y/Z are world coordinates while lx/ly/lz locate geometry inside the chunk; the integration deliberately uses (47,82,-40) and local (15,18,24) to distinguish them.
5. `MeshReplaced` is set only after successful pool emission. Existing crack fallback remains until that point. Reset removes entries and invalidates the target plus its six neighbors, restoring vanilla tessellation. Bounded diagnostics expose observation, eligibility rejection (reason), progress, commit, mesh replacement and restoration; tessellation exceptions are logged and rethrown rather than silently hidden.

## RED/GREEN test

`tests/DispatchMeshTests.cs` constructs a real `ChunkTesselator` dispatcher, uses real BlockLog constructor geometry/flag defaults and actual birch asset drawtype/textures, calls the exact six-argument `ChunkTesselator.TesselateBlock`, and collects real initialized chunk MeshData pools. It executes real microblock mesh creation, light setup and pool append, observed using postfix-only counters. It does not mock those production methods. World queries, an atlas service and unstarted-client light values are controlled services; no GPU context is created. Strict accessor proxies reject gameplay writes.

Normal: vanilla 24v/36i hash ED5D9DF1D819109B; contact-committed mesh 336v/504i hash 026565AD8CEE19C3, contains interior vertices; reset exactly restores 24v/36i hash ED5D9DF1D819109B.

`KVM_REPRO_DISPATCH_LEGACY=1 dotnet run --project tests/IntegrationTests.csproj -c Release` removes only the new opaque cube prefix. Expected failure: committed mesh remains 24v/36i ED5D9DF1D819109B, with zero production hook/light/source/emission deltas. Unset the variable to run GREEN. Retained `KVM_REPRO_LEGACY=1` separately reproduces the earlier animation-registration defect.

## Honest limits

This is CPU chunk-world-mesh verification, not rendered visual acceptance. The available reference installation contains assemblies and partial assets, not a runnable full client; no DISPLAY, WAYLAND_DISPLAY, Xvfb, xvfb-run, glxinfo or weston was available. No full client was started, no player axe swing was rendered, and FallingTree's loaded patches were not exercised. Damage caller/observer lifetime are source/IL traces plus hook-install tests; they are not a fully simulated player mining loop. An in-game full-stack smoke test is still required. This fixes a directly reproduced engine-path defect without adding a server mod, network protocol, block writes or gameplay changes.
