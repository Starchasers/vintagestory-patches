# 1.0.0 verification

- External dependency: pack `Nb7Rd`, nested `Auto Map Markers 5.0.3 - Vintage Story 1.22.zip`; mod ID `egocaribautomapmarkers`, version `5.0.3`.
- Dependency DLL SHA256: `945a35ee8d8409767b9d86ad1c8ddcdb92020fa876ecdb7ba1dcfcb91ac236dc`.
- Game production assemblies/assets: Vintage Story 1.22.7; .NET 10.
- Native regression: **42 assertions passed**, including unpatched baseline, actual Harmony targets, UI-layout category removal, registry projection, native `BlockMushroom` identification, protobuf payload roundtrip, server title formatting, save/reload, expanded-child enablement migration and repeated production lifecycle install/dispose. Exact output: `native-result-lines.txt`.
- Packaging: **12 Python tests passed**. Real mod build: **0 warnings, 0 errors**.
- Installable archive SHA256: `6c839d1b9e412e945c08b804344aa9761cfa1a169c8feb0e2bbed118143770ad`.
- Packaging the real built assembly twice produced byte-identical archives. ZIP CRC check passed; exact contents: `README.md`, `UnifiedMushroomMarkers.dll`, `modinfo.json`. No dependency DLLs bundled.

These tests read a disposable copy of the real pack definitions/settings. They do not edit the user's installed configuration. No rendered client, multiplayer session, player interaction, map rendering or world-save migration was exercised. Old pins deliberately remain untouched. Sample mushroom codes cover edible, poisonous, psychedelic and mod-added patterns; they are representative test objects, not a claim that live world blocks were loaded.
