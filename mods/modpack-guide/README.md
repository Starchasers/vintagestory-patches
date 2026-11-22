# Pszczoly Modpack handbook — cross-mod processes 1.4.0

Client-only replacement of `kuboczochmodpackguide`. Replace the previous guide ZIP; do not install two versions together. No pack configuration, recipe, statistic or gameplay asset changes. The 1.3.1 baseline directory is untouched.

## Deliverables

- `dist/KuboczochModpackGuide-1.4.0.zip` — installable replacement.
- `dist/KuboczochModpackGuide-1.4.0-source.zip` — source, generated resources, inventories, research, static source evidence, review records, test logs and immutable baseline inputs.
- `dist/SHA256SUMS-1.4.0` — actual install/source checksums.

## Actual authored content

The root remains **Pszczoly Modpack - Mod Index**: all seven existing mod categories first, then **Our guides**. Existing category pages gain a link to their process-guide hub. All 75 mod pages, category destinations, existing Toolsmith page definitions, Toolsmith text/stats and the presentation DLL remain byte-identical to 1.3.1. Only root/category navigation text, new guide resources and version/description change.

There are **22 process pages plus seven topic hubs**, inside the existing Pszczoly handbook category. Each process is numbered short cards, representative native clickable item/station pictures, short actions and native recipe/guide links. Process pages contain **242–348 words including navigation and icon labels**. There are no giant species/colour inventories, duplicated icons per page or invented item aliases. The unchanged `toolsmithitem` converter is namespace-neutral internally: it renders native clickable slideshow components and is reused without modifying the client DLL.

- Fields, crop bundles, seeds, suitable feed, welfare, fertilizer; carcass → kitchen/bones/hides → bark tanning/leather; bees → honey/wax; flax/fleece → yarn/cloth/clothes.
- First fire and smoke safety; manual dough and stone/vanilla oven branches; pasta/drying; broth/clarification/gelatin; milk/cream/butter/pasteurization; bottles, tea and storage.
- Wood splitting/sawing/bark → workshops/tannin; ore → metal equipment; powders/glazed ceramics; work-supporting storage; scaffolds/shingles/roofs/carving; heraldry/decor; pewter/tableware/quivers.
- Overland planning/waypoints/hauling; sea preparation and mooring; combat/recovery/shelter; separate coin systems and small trades.

These are cross-mod paths, not invented universal tiers. All exact quantities/variants not needed for the short explanation remain in native recipe pages. A representative icon is not an assertion that every cosmetic/material variant has equal stats.

## Exhaustive pack reconciliation and evidence

Exact export: `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted`; trusted API/assets **1.22.7**. `coverage-matrix.json` / `COVERAGE-MATRIX.md` reconcile **146/146 enabled manifest IDs**, with source paths, archive hashes, research classifications, config/module scope, relevant process/category links and explicit support/exclusion dispositions. **105 IDs have relevant process links; 41 are documented support/exclusions.** This is not a claim that every item from every mod gets a new recipe tutorial. Libraries, UI, ambience and uncertain runtime-only effects are not silently omitted or turned into fabricated progression gates.

`research/*.json` and `.md` are domain-specific research history. `process_editorial.py` applies subsequent source-backed review corrections before generation; original draft prose is deliberately retained as superseded audit history, not shipped text. `effectiveResearch` prioritizes the deeper tweaks/config audit where earlier metadata-only classifications were inadequate.

- `process-reference-inventory.json`: **133/133** distinct new item/block references expanded from exact static definitions, world properties, selective variants and relevant content patches; exact source/type readback. This is not full modloader registration.
- `process-source-readback.json`: **102 steps, 489 resolved source citations, zero missing-source errors**. Named evidence aliases are resolved to actual archive members/config/static implementation bytes and SHA-256. Reduced research excerpts match the source subsets. Disabled vanilla log-tannin recipes are explicitly marked inactive; no loaded recipe registry is claimed.
- `evidence/process-static/`: copied static CIL/decompiled implementation and owner-source evidence plus hashes. Installed third-party mod assemblies are never executed or redistributed outside the pre-existing own-guide baseline archives.
- Existing `table_inventory.json`, `NUMERIC-EVIDENCE.md`, `INVENTORY-SCOPE.md`: unchanged Toolsmith evidence; independent source audit still checks 200 mappings, 153 installed options and 30 source assets.

Important pack deltas are explicit: disabled **Livestock Welfare breeding module** is not disabled animal breeding; Immersive Woodworking removes vanilla **log → tannin** and uses bark; Manual Dough uses active flour plus water in another hotbar slot or a targeted container, not offhand water alone; Real Smoke can suffocate; DanaTweaks changes vanilla oven heating/fuel and doors. No example configuration is promoted over consumed implementation/config sources. Unresolved provider-specific limits remain in research and are not invented player instructions.

## Rebuild and validate

Requires the supplied pack/base assets, Python with `json5`, and the provisioned SDK at `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/dotnet/dotnet`.

```sh
cd /home/brunette/.hermes/cache/scratch/pszczoly-process-guides
python verify_release.py
python package_release.py
```

`test-results-1.4.0.txt` contains actual subprocess output. The current path audits sources/coverage, builds 1.4.0, reconstructs native registration from static inputs, exercises native VTML/link callbacks and actual new-page font/layout geometry, runs 38 Python regressions, and proves consecutive install builds identical. Prior versioned scripts/logs are historical baseline evidence, not the 1.4.0 build path.

## Verified boundaries

All **118 shipped custom texts** tokenize with the trusted native VTML parser; **256 authored progression anchors** resolve in the reconstructed native registry. All 29 new pages pass **464 actual-page layouts** (widths 240/320/420/550 × GUI scales 0.75/1/1.5/2), **2,288 native item-click callbacks**, **3,504 native anchor-click callbacks** and **32,673 assertions**. Existing Toolsmith layouts/callbacks and all 85 attributed family snapshots also pass.

There is **no graphical client** here. These are actual trusted API/Cairo/font/hit-test/registration-method executions on static fixtures, not screenshots, GPU rendering, full mod startup or gameplay crafting. Dynamic code-native guide registrations are evidence-backed fixture entries, not proof that their owning mods started. Config exports describe this pack snapshot, not a live multiplayer server. Temporal Overhaul provider/parser details and custom smoke sources have bounded unknowns documented in research; guides do not assert exact unverified provider settings.

A real-client smoke test remains: open Pszczoly → Our guides, inspect narrow cards, scroll/hover/click images and native guides, verify current-world recipe contents and test representative processes. No live GUI/render or full-pack runtime claim is made.
