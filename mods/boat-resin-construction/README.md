# Kuboczoch Boat Resin Construction 1.3.0

Replaces **all Rough Seas pitch-glue construction steps with raw vanilla resin**. One resin is consumed for each completed sealing action. No bucket, liquid glue, heating or extraction recipe is needed. Other construction materials and native action durations are unchanged.

## Install / upgrade

1. Back up the world; test a copy first where possible.
2. Remove older `KuboczochPitchGlueCompat` releases (1.0.0, 1.1.0 or any experimental extraction build) from client and server Mods folders. Do not load them alongside this replacement. This keeps the same mod ID for upgrade detection.
3. Place `KuboczochBoatResinConstruction-1.3.0.zip` in client **and server** `VintagestoryData/Mods`, still zipped. Restart both. In singleplayer install it once in your local Mods folder.
4. Keep Rough Seas enabled. The code is validated for **exactly Rough Seas 5.0.1** and refuses other versions. Dependency metadata expresses a minimum; the runtime equality guard enforces the validated version.

Game minimum 1.22.4. External SDK used for native tests identifies as 1.22.7; other game versions are not separately exercised. No game or third-party DLL is distributed.

## Use

Hold normal **Resin** (`game:resin`) in main hand. Aim at unfinished canoe at its sealing/pitching stage and **hold normal right-click without Shift** for its usual construction duration. Each finished seam consumes **one resin**. A bark canoe with four untouched seams therefore needs four resin. Existing completed seams and other construction progress are not reset.

Glue is **not an alternative** at these construction steps anymore. Glue crafting and unrelated uses of glue elsewhere in the modpack remain unchanged. Boats with no glue construction stage receive no new resin requirement. This applies to all Rough Seas hull stages whose native work type is Pitch; it does not rewrite independent boat systems in unrelated mods.

## Implementation

A narrowly scoped Harmony hook replaces only native `Laying.For` results for `FramingWork.Pitch` with native `PieceLaying(game:resin)`. This reuses Rough Seas' normal item recognition, displayed material, duration, cancellation, stage advancement and server-side one-item consumption. Native framing behavior is attached idempotently to vanilla resin. No bucket lifecycle workaround, Manual Dishes hook, new glue item, extraction recipe or saved-data conversion is included.

Removal restores normal glue requirements after a full restart. Already completed work is not undone. Remove the mod from both sides if rolling back.

## Verification limits

101 native regression checks pass, including resin-only material recognition, unpatched controls, one resin per seam, early-stop/cancel safety, original 7.5-second bark-canoe action duration, client non-consumption, native sealing and partial saved-progress compatibility. Enumeration covers all 7 retained Rough Seas vessel entity files, 5 construction configs and 32 stages: only the bark-canoe Pitch stage needs substitution; other boats have no glue stage to replace.

Headless regression fixtures use the retained Rough Seas DLL and native Vintage Story classes. World/player APIs and hulls are controlled fixtures. Native server startup is a separate check, not a rendered-client boat-building test. Full gameplay in the user's modpack still needs confirmation; this implements the requested resin substitution rather than claiming to identify the earlier bucket incompatibility.

## Build and test

Requires .NET SDK 10, licensed external Vintage Story SDK, and the retained RoughSeas 5.0.1 DLL. Set external paths explicitly; never redistribute those binaries.

```sh
dotnet build mod/KuboczochPitchGlueCompat.csproj -c Release -p:GamePath=/path/to/vintagestory -p:RoughSeasPath=/path/to/RoughSeas.dll
dotnet run --project tests/Tests.csproj -c Release -p:GamePath=/path/to/vintagestory -p:RoughSeasPath=/path/to/RoughSeas.dll -- /path/to/extracted/roughseas/assets/roughseas .
python3 package.py
```

Packaging uses allowlisted owned files, fixed ZIP metadata, CRC/byte read-back and SHA-256. Source packages exclude SDKs, native DLLs and decompiled upstream code.
