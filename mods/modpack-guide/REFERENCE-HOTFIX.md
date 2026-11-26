# Modpack Guide 1.4.1: missing collectible references

## Diagnosis

The reported client startup log contains:

```text
Mod exception during event LevelFinalize. Will skip to next event
Exception: Unregistered guide reference: oneroof:oneroof-redclay-standard-none-free
  at KuboczochModpackGuide.Client.ToolsmithItemReferences.Convert(...)
  at Vintagestory.API.Common.VtmlUtil.Richtextify(...)
  at Vintagestory.GameContent.GuiHandbookTextPage.Init(...)
  at Vintagestory.GameContent.ModSystemSurvivalHandbook.createDialog()
  at Vintagestory.GameContent.ModSystemSurvivalHandbook.Event_LevelFinalize()
```

The subsequent H-key exception is a null dereference in `OnSurvivalHandbookHotkey`. A missing optional-provider icon must not prevent constructing the handbook dialog. This defect belongs to Modpack Guide, not Japanese Gear. No Harmony exception suppression or hotkey null guard is added.

## Change

- Missing item/block references log the exact missing code and are skipped.
- Mixed lists retain registered stacks and their existing native slideshow/click behavior.
- Entirely unavailable or empty references render `[Unavailable guide item]` rather than throw. Surrounding guide text remains visible.
- Shipped content is unchanged; only the client DLL and manifest version change.

## Build and package

Use an installed .NET 10 SDK and the actual Vintage Story 1.22.7 assemblies:

```sh
dotnet run --project client/tests/FamilyTests.csproj -c Release -p:GameDir=/path/to/vs-1.22.7 -- attribute_inventory.json client/assets/kuboczochmodpackguide/config/toolsmith-families.json
python package_reference_hotfix.py
```

The deterministic packager uses the checked-in `generated-addon` snapshot, compares every packaged resource and DLL byte on readback, checks ZIP CRC, and rejects additional DLLs. It does not bundle game libraries.

## Verification

- Red: the added missing-One-Roof test reproduced the original `FormatException` against the unmodified converter.
- Green: native 1.22.7 API tests cover missing block, missing item, empty reference, mixed valid/missing references, warning diagnostics, provider present, and native `VtmlUtil.Richtextify` processing with surrounding text.
- Existing native snapshot/click tests pass for the 85 attributed Toolsmith stacks.
- Full historical Python discovery is not green in this restored checkout: 18 tests run, 13 errors, mostly missing historical ZIP inputs under `dist/`, plus missing justified title evidence for `gamemechanicinfo-farming`. Those scripts/assets were not changed by this fix; the checked-in guide content snapshot is packaged without regeneration.
- These are real native API/headless regression checks, not a claim of rendered in-game H-key acceptance on the friend's full pack.

## Install and acceptance

Replace `KuboczochModpackGuide-1.4.0.zip` with `KuboczochModpackGuide-1.4.1.zip` in the client's Mods folder. Do not keep both. This is client-side; no server update is required. Keep Japanese Gear installed for the retest.

Fully exit/relaunch, join a disposable test world using the same pack, press H, open the building-materials guide, and verify missing optional references show a text placeholder while registered icons still open native handbook pages. Confirm the previous startup FormatException is gone. Preserve `client-main.log` if any further failure occurs.
