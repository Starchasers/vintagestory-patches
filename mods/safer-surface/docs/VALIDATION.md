# Verification report — test build 0.1.0

## Target and environment

- Vintage Story dedicated server / SDK **1.22.7**, verified from the native server `--help`, runtime native API metadata and a real server boot. The target was independently discovered from the user's existing project SDK, not inferred from another mod.
- External SDK used: `/home/brunette/.hermes/cache/scratch/motion-server1227`.
- Build tool: `.NET SDK 10.0.404`, executable `/home/brunette/.hermes/cache/scratch/motion-dotnet/dotnet`.
- No game, Harmony or third-party mod DLL is included in source control or the release archive. Source was independently implemented; proprietary decompilation was used for interface/call-site auditing only.

## Executed acceptance gates

```sh
DOTNET=/home/brunette/.hermes/cache/scratch/motion-dotnet/dotnet
GAME=/home/brunette/.hermes/cache/scratch/motion-server1227
"$DOTNET" run --project tests/PolicyTests.csproj -c Release
"$DOTNET" run --project tests/native/NativeTests.csproj -c Release -p:GameDir="$GAME"
python -m unittest discover -s tests -p 'test_*.py' -v
python tools/package.py --game-dir "$GAME" --dotnet "$DOTNET"
```

Results:

- **54 SDK-free policy/config assertions passed**: seasonal precedence, fixed-hour and midnight-wrapping boundaries, safety-toggle defaults, 8-block inclusive native behavior, 3D/horizontal range, dimensions, same/different pair behavior, vanilla-invalid/non-player/exclusion handling, invalid-value rejection and exact preservation of invalid config text. After warmup, **100,000 policy decisions allocated 0 bytes** on the calling thread. This measures the pure policy only, not live full-world frame time.
- **13 native SDK integration groups passed**, including real Harmony signature installation/removal and startup rollback, real native original-body counts on safe early-out fixtures, scope nesting/exception restoration, all ten native acquisition-task families, active pair-specific combat pursuit (not unrelated flee), ranged recent-attacker exemptions, optimized legacy predicates, notification rejection, cold environment sampling fixtures and cache invalidation/TTL/eviction. World/light/calendar interfaces are controlled fixtures, not a substitute for connected gameplay.
- Compiled mod IL audit passed: **100 method bodies / 420 method operands / 129 distinct external targets**. No added native sensing/search, raycast/LOS, pathfinding, original acquisition calls, scheduler update or callback registration. It permits only read-only access to native active-task slots. Native cooldown/dead-target fixtures observed each original method body exactly once and no added sensing/search/pathfinding calls.
- **15 Python packaging boundary tests passed**, including byte reproducibility, allowlist boundaries, SDK injection/traversal/symlink prevention, strict metadata, sensitive-text rejection, build failure and stale-output controls. Synthetic bytes are confined to package unit tests; the deliverable ZIP was built from actual production source.
- Production build: **0 warnings / 0 errors**. Rebuilt/packaged twice from distinct temporary output directories; the resulting ZIP SHA-256 was identical.

## Real dedicated-server smoke

Ran the actual 1.22.7 dedicated server with the production ZIP and an isolated scratch data directory, listening only on `127.0.0.1:42437`, one client maximum, server advertisement and UPnP disabled. No connected clients were used.

Final ZIP smoke log showed:

```text
[Safer Surface] Enabled 1.22.7 acquisition-only 8-block cap. Restart for config changes.
Dedicated Server now running on Port 42437 and ip 127.0.0.1!
```

`/stop` completed with **exit 0**. A separate negative-config boot logged `Configuration unavailable/invalid; original preserved, vanilla unchanged: MaximumRange must be finite and in (0,128]`, did not emit an enabled diagnostic, started normally and stopped with exit 0; the negative value was retained. SDK-free config tests additionally verify invalid files byte-for-byte.

Local evidence (scratch, not version-controlled):

- `/home/brunette/.hermes/cache/scratch/safer-surface-native-smoke/console.txt` — final ZIP valid-config boot.
- `/home/brunette/.hermes/cache/scratch/safer-surface-native-smoke/console-invalid.txt` — negative-config boot.
- `/home/brunette/.hermes/cache/scratch/safer-surface-native-smoke/console-valid.txt` — earlier valid-config boot.

The smoke world was disposable; no user's existing world/server/repository was modified.

## Deliverable

`dist/kuboczochssafersurface-0.1.0.zip` — **18,215 bytes**; four root entries:

- `KuboczochsSaferSurface.dll` — real compiled assembly, 27,648 bytes.
- `LICENSE`
- `README.md`
- `modinfo.json`

SHA-256: `f479c53c75e290cb0f43fe53cba6ab3d815cfe73361f6ca03810fd7c2ae6c71e`.

## Remaining limitations

No rendered client, actual monster/player combat, connected multiplayer encounter, projectile hit, live pursuit/forgetting, natural shade/cave boundary or seasonal/storm transition during combat was exercised. A dedicated-server successful load is not proof of those gameplay outcomes. No frame-time benchmark is claimed. The [manual checklist](NATIVE-SMOKE.md) covers those remaining tests.

Environmental evidence is deliberately conservative and bounded: generated terrain height can retain vanilla in excavated pits/unusual maps; unsupported dimensions and unknown data remain vanilla. Same-block changes can remain cached for the configured TTL, except global storm activation, which is checked before reuse. No native AI scheduler redesign is attempted. AI-overhaul mods replacing the audited 1.22.7 seams are not certified compatible.
