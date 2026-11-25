# 1.0.0 verification

- External dependency: pack `Nb7Rd`, nested `Auto Map Markers 5.0.3 - Vintage Story 1.22.zip`; mod ID `egocaribautomapmarkers`, version `5.0.3`.
- Dependency DLL SHA256: `945a35ee8d8409767b9d86ad1c8ddcdb92020fa876ecdb7ba1dcfcb91ac236dc`.
- Game production assemblies/assets: Vintage Story 1.22.7; .NET 10.
- Native regression: **43 assertions passed**, including unpatched baseline, actual Harmony targets, UI-layout category removal, registry projection, native `BlockMushroom` identification, protobuf payload roundtrip, server title formatting, save/reload, expanded-child enablement migration and repeated production lifecycle install/dispose. Exact output: `native-result-lines.txt`.
- Packaging: **14 Python tests passed** (12 packaging, 2 config-preservation/enabling). Real mod build: **0 warnings, 0 errors**.
- Installable archive SHA256: `73de379903666a77061e1e9f672a70f0b157619d2528992ab01e554ee045a454`.
- Packaging the real built assembly twice produced byte-identical archives. ZIP CRC check passed; exact contents: `README.md`, `UnifiedMushroomMarkers.dll`, `modinfo.json`. No dependency DLLs bundled.

The separate delivered enabled-config run passed **44 native assertions**, including explicit tracking/global enablement: `enabled-config-result-lines.txt`. Config SHA256: `333d81d96324c0276d1ec34c6a194aae659637f3df6bb09205a575fce5a05969`. Its unrelated values match the source pack under a canonical-JSON SHA256 assertion after excluding only the two mushroom override/expand-state keys.

These tests read a disposable copy of the real pack definitions/settings at the actual provider `AutoMapMarkers/settings.json` subpath. They do not edit the user's installed configuration. No rendered client, multiplayer session, player interaction, map rendering or world-save migration was exercised. Old pins deliberately remain untouched. Sample mushroom codes cover edible, poisonous, psychedelic and mod-added patterns; they are representative test objects, not a claim that live world blocks were loaded.
