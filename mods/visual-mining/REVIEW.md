# Independent review closure

## 1.0.4 independent read-only review

The first review caught a genuine FP camera aliasing gap and an enabled-tool retarget/stale-observation regression. Both were fixed before release: reset now visits explicit TP and FP managers with owned-only `StopSelfAnimation`, while `ClearObservation` restores chips/contact without stopping enabled animations. Tests now use the inherited real `EntityPlayer` client manager routing, not a fixed TP getter or server-side stand-in. A second independent read-only comparison against the actual archived 1.0.3 source reported no blockers. It confirmed config preservation, default-enabled toggles, owned-only manager cleanup, and enabled retarget/stale behavior. This review is not GPU/in-world acceptance.

## 1.0.3 independent read-only review

A separate reviewer compared the renderer with archived 1.0.2: only TopsoilMeshPatch and its initialization are production logic additions. It verified the actual engine microblock grass texture/UV2/renderpass path and JsonTesselator destination copying. It flagged untested secondary UV/tint channels and the snow/top-rotation appearance mismatch. Tests were then expanded to distinct base/secondary atlas rectangles, actual resolved grass assets, populated color maps, packed UV2 counts/ranges, correct destination renderpass and hash-exact channel restoration. The snow/random-rotation limitation is explicitly documented rather than claimed fixed. Existing transient-state, geometry, sound and gameplay behavior is unchanged.

The final validation uses production Initialize, 57 actual-asset fixtures and actual archived1.0.2 DLL RED. No GPU/in-world validation is claimed. The separate asset/animation investigation confirmed inherited shovel getters are present for every material, and exposed the original fast-break-before-contact timing limitation; timing remains unchanged rather than altering gameplay or inventing post-break contacts.

## 1.0.2 dispatcher repair review

A separate read-only reviewer reproduced the CPU dispatch failure by removing only the new `BleedingCubeTesselator` prefix and verified normal runs emit a changed mesh that resets exactly. No release blockers were found in the diff. The new cube and JSON targets are sibling classes, not inherited overrides, so the extra hooks do not double-carve existing targets. Cache/lighting/pool overloads and world/local coordinate transforms execute against VS 1.22.7, with no world voxel/block writes. The original transient microblock mesh approach is retained. Residual limitations: no GPU/in-game visual acceptance, no runtime FallingTree/other-mod stack; tests exercise production observer registration and production damage snapshots separately rather than starting a full player mining session. Existing immediate reset while a different mod temporarily changes mining inputs is unchanged.


## 1.0.1 runtime registration review

The real client log reports unavailable own animation metadata. A headless legacy reproduction fails at the same missing-metadata activation gate; the repaired path passes. An independent reviewer identified that injecting only inside LoadAnimator would miss cached animators and risk mismatched mesh/animator joint mappings. The final implementation instead prefixes the exact protected Entity.OnTesselation overload, supplies a private shape and sets shapeIsCloned before cache selection. A second read-only review of the revised lifecycle, metadata and fallback paths reported no release blockers. Release compilation, the expanded real-engine pipeline regression, sound and voxel suites and static audit pass. No rendered gameplay test was performed.

## Original 1.0.0 review (historical; did not reproduce runtime metadata absence)

A separate read-only reviewer inspected the first integrated sources and trusted engine references. Its blocking findings were addressed before packaging:

- The first observer targeted a method name absent from the actual engine. It now patches the exact `SystemMouseInWorldInteractions.ContinueBreakSurvival(BlockSelection, Block, float)` and reads `curBlockDmg` with a postfix only. The integration test actually installs and removes that patch, catching missing targets and signature/JIT failures.
- Hit sounds now resolve `Block.GetSounds(accessor, selection)`, not the raw `Block.Sounds` field, to preserve behavior/mod overrides.
- Standalone animation readiness checks include the actual selected client animator and named clip. Unknown/missing presentation retains the vanilla path. Initialization failures unpatch and log a warning.
- Shovel default contact is restored to source frame 15; speed remains visual 0.7. No tier scaling was introduced.
- Offhand use has no blocker. Explicit cosmetic one-hand animation variants remove left-arm tracks instead of imposing gameplay restrictions.
- Target/damage positions are checked for equality; resets cannot silently carry cosmetic progress to another coordinate.

All core hooks, sound transpiler and three tessellation hooks installed against the real 1.22.7 game assemblies in headless tests. The generated assets also deserialize through the real game Shape and AnimationMetaData types after being added to the actual supplied player/seraph JSON.

The review is not a graphical smoke test. Actual client rendering, lighting, low-frame-rate synchronization, remote-player appearance, combat animations and the owner's full mod set remain in-game acceptance items; see README.
