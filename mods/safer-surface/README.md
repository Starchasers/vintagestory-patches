# Kuboczoch's Safer Surface

Server-only Vintage Story **1.22.7**, mod **0.1.0**. Independently implemented; no other mod is required. Game-provided Harmony is used, not bundled.

Caps **new player target acquisition** by drifters, shivers and bowtorns at **8 blocks**, only when **both monster and player** are on a stable, sufficiently sunlit daytime surface. At exactly 8 blocks or nearer, vanilla still decides. It does not make monsters passive.

## Behavior

- Existing same-monster/same-player active pursuit and the native recent-attacker retaliation window are untouched, including ranged attackers. Another player's pursuit does not exempt you. Native forgetting still ends pursuit; this mod stores no permanent aggro flags.
- Wolves, bears, animals, unrelated entities, boss/special drifters, non-player targets and vanilla-invalid targets remain vanilla by default.
- Either endpoint underground, dark/shadowed below the configured sunlight threshold, temporally unstable, during a temporal storm, or outside daylight: **vanilla**. A sunlit player beside a monster in dark shade does not activate the cap. Ordinary shade only matters if its actual light level falls below the threshold.
- Seasonal daylight uses **native solar position at local latitude and current calendar date**, not a fixed hour approximation. `SeasonalDaylight: true` completely replaces the 09:00–19:00 setting. It does not implement geographic longitude/time zones that the native calendar does not provide.
- Cave detection is conservative: endpoint must be at/above the native **world-generation terrain height**, and pass native sunlight. Torchlight cannot compensate for missing sunlight or underground height. No raycasts/roof/line-of-sight tests are introduced. Excavated terrain, unusual map generators or unmapped conditions can therefore fall back to vanilla rather than falsely claiming a safe surface.
- Unknown API state, other dimensions, missing chunks, incompatible AI bindings or unsupported game versions fail open to vanilla.

## Install

1. Stop the server; place `kuboczochssafersurface-0.1.0.zip` in its `Mods` folder. Do not unzip it. Remove older versions of this mod first.
2. Start the server. No client installation is required. Check the log for `[Safer Surface] Enabled` (or a fail-open diagnostic).
3. First start creates `ModConfig/kuboczochssafersurface.json` in the server data directory. Edit it only while stopped, then restart. There is no hot reload.

Malformed, null, duplicate/unknown-property, wrongly typed or out-of-range config is **not overwritten**: the mod logs a diagnostic and leaves vanilla enabled. Keep a backup before edits. A missing file gets defaults.

## Default configuration

```json
{
  "Enabled": true,
  "MaximumRange": 8,
  "HorizontalDistance": false,
  "SeasonalDaylight": true,
  "VanillaDuringTemporalStorms": true,
  "VanillaInDarkLocations": true,
  "VanillaInUnstableRegions": true,
  "VanillaDuringRetaliation": true,
  "PreserveExistingPursuit": true,
  "RequireBothEndpointsOnSurface": true,
  "DebugLogging": false,
  "DayStartHour": 9,
  "DayEndHour": 19,
  "MinimumSunElevation": 0,
  "MinimumSunlight": 12,
  "MinimumBlockLight": 0,
  "MinimumTemporalStability": 0.9,
  "SurfaceDepthTolerance": 0,
  "CacheTtlMs": 500,
  "CacheCapacity": 2048,
  "IncludeEntities": ["game:drifter-*", "game:shiver-*", "game:bowtorn-*"],
  "ExcludeEntities": ["*-boss*", "game:drifter-double*", "game:drifter-stilt", "game:drifter-bellhead", "game:drifter-deepsplit", "game:drifter-gearfoot", "game:shiver-double*", "game:bowtorn-double*"]
}
```

`MaximumRange`: finite >0 to 128. `HorizontalDistance: false` means true 3D Euclidean distance; true ignores vertical separation. Both use squared distances. This never expands native sensing range.

`MinimumSunElevation`: degrees, strict greater-than, −90 to 90, seasonal mode only. Fixed mode uses `[DayStartHour, DayEndHour)` with distinct hours: start in `[0,24)`, end in `[0,24]`. Midnight wrapping is supported (19–6 means 19:00 through just before 06:00). `MinimumSunlight` and `MinimumBlockLight`: native 0–32 thresholds (sunlight has an effective floor of 1 so pitch darkness remains vanilla with defaults); block light is an **additional** minimum, never an alternative to sunlight. `MinimumTemporalStability`: 0–1. `SurfaceDepthTolerance`: 0–8 blocks below generated terrain; **leave 0 for conservative underground fallback**. Raising it relaxes the underground test explicitly.

Keep the six safety/continuity toggles **true** for the described behavior. Advanced overrides intentionally depart from it: `VanillaDuringTemporalStorms:false` removes the global-storm bypass (local stability can still bypass); `VanillaInDarkLocations:false` skips light thresholds; `VanillaInUnstableRegions:false` skips the local stability threshold; `VanillaDuringRetaliation:false` removes the recent-attacker exemption; `PreserveExistingPursuit:false` removes the same-pair exemption during new task acquisitions, but still never patches active maintenance; `RequireBothEndpointsOnSurface:false` relaxes only the hostile's terrain-height test, not the player's nor either endpoint's daylight/light/stability tests. `DebugLogging:true` prints one additional startup diagnostic, never per-entity spam. Missing native environment support warns once/session and remains vanilla.

Entity lists use case-sensitive full asset codes and `*` wildcards (max two per pattern). Exclusions win; at most 128 patterns/list. Defaults conservatively exclude special drifter variants; unrelated bosses are outside the include list. Custom inclusion can intentionally change the affected families.

`CacheTtlMs`: 0–10000 ms (0 disables reuse). `CacheCapacity`: 16–65536 entries. Cache identity includes entity ID, floored block coordinates, dimension and surface-test policy; entering another block forces a fresh read immediately. Changes within the same block (shade, daylight, local stability, terrain edits) may take up to the TTL to appear; **storm activation is checked immediately**, disposed sampled chunks cannot supply cached evidence, and calendar jumps exceeding one game minute since sampling invalidate the entry. Cache eviction is bounded FIFO, on demand; no scheduled refreshes or extra entity loop.

## Performance contract

The mod adds **zero** raycasts, LOS tests, A*, pathfinding, native sensing or native search calls. It never invokes an original method itself. Existing vanilla callback cadence/search scheduling is retained; rejection may reduce downstream native work, never request new work. It does not redesign or globally synchronize vanilla AI scheduling.

Added work for a successful native candidate inside an existing acquisition callback: type/context checks; compiled native owner/retaliation accessors; bounded cached family eligibility; squared range/dimension check; inspect exactly eight native active-task slots for the same pair; then at most two cached environment queries. Eligibility is cached for up to 512 asset codes (unknown beyond that falls back to vanilla). Cache-hit path allocates no application objects. A cache miss reads existing chunk/map height, sunlight/block light, local stability and native solar position; it creates one `BlockPos`, one solar-input `Vec3d` and the native solar-return vector in seasonal mode. No per-tick updates, searches, reflection, LINQ or delegates are created in the callback. Reflection and accessor compilation happen at startup only. [AI coverage and test evidence](docs/AI-COVERAGE.md) describe precise seams and limitations.

## Build and verify

Requires .NET SDK **10** and a separately obtained licensed **Vintage Story 1.22.7** installation/server SDK. SDK files and proprietary DLLs must stay outside this repository/archive.

```sh
dotnet run --project tests/PolicyTests.csproj -c Release
python -m unittest discover -s tests -p 'test_*.py' -v
dotnet run --project tests/native/NativeTests.csproj -c Release -p:GameDir=/path/to/game
python tools/package.py --game-dir /path/to/game --dotnet dotnet
```

The last command compiles real production source and writes `dist/kuboczochssafersurface-0.1.0.zip` plus `.zip.sha256`. ZIP root allowlist: our DLL, `modinfo.json`, this README and `LICENSE`. It excludes native SDK, Harmony, test binaries, logs and local paths. ZIP metadata/order is fixed; identical build inputs yield identical archives. No signing key is required. `--game-dir` is mandatory; no stale-binary/skip-build packaging switch exists.

Hosted CI runs SDK-free policy/config and package boundary tests only. Native tests/build need your external licensed SDK. Tests with stubbed world interfaces are not client gameplay tests; see [validation](docs/VALIDATION.md) and the [manual smoke checklist](docs/NATIVE-SMOKE.md). Version 0.1.0 is a **test build**, not a claim of completed multiplayer gameplay validation.
