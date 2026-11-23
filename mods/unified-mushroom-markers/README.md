# Kuboczoch: Unified Mushroom Markers

Client-side compatibility patch for **Auto Map Markers 5.0.3** and Vintage Story **1.22.x** (built and headlessly tested against 1.22.7).

## What changes

- Auto Map Markers' settings contain exactly one **Mushrooms** row. The poisonous category is **removed from the UI**, not renamed or left as a second row.
- Mushroom safety/psychedelic expanders and subcategories are absent.
- Both original mushroom asset-pattern sets feed one detection entry. Edible, poisonous, psychedelic and mod-added `mushroom-*` blocks use the same marker settings.
- New markers have the literal title **Mushrooms**, the neutral color **`#A89880`**, and the **`mushroom`** icon. Species/dynamic titles and existing safety-specific presentation overrides are suppressed. Coordinate suffixes, if enabled globally, remain available.
- The unified Enabled switch works for both former categories. Initial effective enablement is merged with OR (selected child settings are used for formerly expanded categories); pinning and coverage radius come from the former edible category. Other markers and global enablement are unchanged.

Title/color/icon are intentionally fixed while this patch is installed. Editing those values in Auto Map Markers' icon editor does not override this policy.

## Install

Install `KuboczochUnifiedMushroomMarkers-1.0.0.zip` alongside Auto Map Markers on the **client**, then restart. Do not install the source directory. No replacement of Auto Map Markers or editing of its definition files is required; the patch ZIP contains no third-party DLLs. This patch does not remove Auto Map Markers' own server requirement.

## Existing waypoints and removal

**Existing saved pins are not migrated, renamed, recolored or deleted.** They may still reveal poisonous/edible names, colors or species. Review/remove old mushroom pins manually if that information should disappear. Automatically identifying old pins would risk modifying unrelated or manually edited waypoints.

Original marker-definition JSON is not rewritten. Auto Map Markers can persist the shared neutral settings through its normal save action; back up its settings before installation if you want to restore the exact former colors/titles after uninstalling. Removing the patch restores upstream categories on restart; persisted neutral settings may remain.

## Compatibility and verification

The inspected modpack contains `Auto Map Markers 5.0.3 - Vintage Story 1.22.zip` (`egocaribautomapmarkers`, version 5.0.3), with registry-based detection and mushroom safety/psychedelic subentries. The patch targets those actual production methods, not an older 5.0.1 implementation. It installs Harmony patches during client `StartPre`, before normal initialization; incompatible method signatures fail patch installation rather than pretending success. Later versions are not guaranteed compatible.

Regression tests load the external production Auto Map Markers and game DLLs, install the actual Harmony patches, and exercise real settings loading, UI-layout construction, registry building and `ThingIdentifier`. They check category absence, neutral presentation, species suppression, enabled-state sharing, and unrelated resin entries. This is a **headless production-assembly test**, not a rendered client/world smoke test. The test blocks use representative asset codes; no graphical map rendering or real player interaction is claimed.

## Build and test

Requires .NET 10, an external Vintage Story 1.22 installation and the external `automapmarkers.dll` extracted from the installed 5.0.3 ZIP. No proprietary game or dependency binaries belong in this repository.

```sh
python tools/package.py --game-path "$GAME" --automap-markers-path "$AMM_DLL" --dotnet "$DOTNET"
python -m unittest discover -s tests -p 'test_*.py' -v
```

For production-assembly tests, extract the modpack's `_core.json` and `settings.json` into a disposable directory (the test reads these, not the live game configuration):

```sh
"$DOTNET" run --project tests/Tests.csproj -c Release \
  -p:GamePath="$GAME" -p:AutoMapMarkersPath="$AMM_DLL" \
  -- "$GAME" "$FIXTURE_DIR/_core.json"
```

Packaging uses a fixed three-file allowlist, fixed ZIP metadata, atomic output replacement and a SHA256 sidecar. Build inputs and target DLL paths are validated; external dependencies are not bundled.
