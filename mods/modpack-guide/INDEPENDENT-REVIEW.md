# Independent review and disposition — 1.4.0

Review artifacts are `research/independent-process-review.md` and `research/independent-process-followup.md`. These are genuine read-only child reviews of actual generated ZIP content and exact installed sources; they are not GUI evidence. Their findings remain unedited as history. Counts in reviewer self-reports are not acceptance totals; programmatic acceptance is authoritative (102 steps, not the follow-up's mistaken 131).

## Findings addressed

1. **Critical: vanilla log-tannin instructions contradicted active Immersive Woodworking.** Emitted leather page now makes weak/strong tannin from tanning bark; wood page explains stripping and optional drying/quern branch. Wood and leather pages link bidirectionally. Static `IwSystem.cs` removal logic and `DisableVanillaLogTannin=true` are checked; original vanilla log recipes are tagged inactive in source readback.
2. **Major: DanaTweaks not reviewed deeply enough.** Added `research/tweaks.json`/`.md`, consuming-config/static implementation evidence and 90 enhanced classifications. Matrix `effectiveResearch` replaces shallow metadata-only interpretations. Emitted pages mention oven heating, doors, rain collection versus irrigation, forestry and relevant compatibility limits. No unsupported provider-specific setting is asserted.
3. **Major: Manual Dough incorrectly implied offhand-only water works.** Static `TransformFlourToDough`, `TryConsumeWaterFromHotbar` and `TryConsumeWaterFromContainer` show the actual paths. Emitted guide now requires selected flour plus another hotbar water item or targeted placed container, explicitly rejecting offhand water alone. Direct source and emitted-text regression added.
4. **Minor: hide stage wording ambiguous.** Now explicit: scraped hide + weak tannin → prepared hide; prepared hide + strong tannin → leather. Native prepared-hide/leather recipe source checks added.
5. **Metadata casing/empty descriptions:** case-insensitive metadata plus direct native patches verified `cbr` side-solidity effects and `yangcoifix` shader/tessellation fixes. A display name/empty description is not used as evidence of no effect.
6. **Missing source paths/aliases:** source readback now resolves every emitted step citation, including named recipe aliases, archive-root delimiters and directories as directory inventories. 489 resolved references, zero missing-source errors; no alias is treated as a fabricated collectible code.
7. **Native woodworking page:** `craftinginfo-woodworking` is dynamically registered, not absent because it is not JSON. Verified owner source and installed assembly UTF-16 constant; explicitly represented as code-native registration in the fixture. The test does not pretend to start the owning mod.

## Executed after fixes

`python verify_release.py` passed after the Manual Dough/hide-state fixes. Source checks, actual emitted content, 38 Python tests, new-page geometry/callbacks and immutable baseline preservation all passed. The final install is rebuilt deterministically by that path. `TEST-REPORT.md` and `test-results-1.4.0.txt` contain current acceptance evidence.

No known progression-critical source conflict remains in emitted instructions. Temporal Overhaul's exact active provider/parser controls, custom smoke emitters and live multiplayer config application remain bounded research/runtime limits; exact unsupported advice is omitted, not guessed.

No graphical client, full modpack startup, GPU item render, live recipe enumeration or gameplay interaction was tested. Static source review and native API fixture execution do not establish those properties.
