# AI acquisition coverage: Vintage Story 1.22.7

## Executable verification

`tests/native/NativeTests.csproj` is an SDK-backed console regression suite, not a game simulation. It references the **current** `src/SaferSurface.csproj` using the same `GameDir` property. It asserts the loaded SDK version is exactly `1.22.7`.

```sh
/home/brunette/.hermes/cache/scratch/motion-dotnet/dotnet run \
  --project tests/native/NativeTests.csproj -c Release \
  -p:GameDir=/home/brunette/.hermes/cache/scratch/motion-server1227
```

For another machine, substitute its .NET 10 executable and external, complete 1.22.7 server directory. The project copies dependency DLLs from the SDK root, `Lib/`, and `Mods/` **only into the test output directory**. It does not add SDK dependencies to the mod project or package. Do not commit `bin/` or `obj/`. Startup rollback fixtures write configuration beneath the test output directory, not a live server's data directory.

### What the suite checks

- Calls the production `AcquisitionGate.Install` against actual SDK assemblies and actual Harmony; compares the complete patch set with the exact sixteen expected native methods. Verifies prefix/finalizer/postfix roles, then unpatches and verifies removal. Extra scheduler, sensing, pathfinding, or unrelated patch targets fail this comparison.
- Calls the production `Initialize`, including compiled field/property delegates. Signature existence alone is not enough: these bindings must compile successfully.
- Injects an exception into Harmony's third patch operation for the real mod owner during `StartServerSide`, after two actual native methods have been patched. Verifies startup removes every partial patch and disposes the environment probe. The injection is test-only and unpatched after the test. The test fails if initialization prevents reaching the fault; it does not count that earlier failure as successful rollback coverage.
- Applies the actual production `Enter`, `Leave`, and `Filter` hooks to a small test harness. Verifies one original invocation, unchanged original-false, normal restoration, nested restoration, exception restoration, and out-of-scope behavior. Harness success is not represented as native gameplay success.
- Instruments the **original native method bodies** with test-only Harmony transpilers. Each of the four targetability predicates executes once for a dead-target fixture and remains false. Native `CanSense(Entity,double)` counters remain zero. No replacement native predicate implementation is used.
- Executes all ten original native `ShouldExecute()` methods using controlled native early-outs: no configured legacy seek targets, negative execution chance, and melee cooldown. Each invocation enters its native body once. `AiTaskTurretModeR` legitimately calls its native `AiTaskShootAtEntityR` base body once; the test explicitly checks that additional delegation rather than labeling it a replay. There are zero targetability/sensing calls on these early-outs, and the fake world rejects unexpected API operations. These tests are not successful acquisition/attack scenarios.
- Exercises production `Filter`/`FilterNotify` with SDK entity/task types, an actual native eight-slot `AiTaskManager`, controlled world APIs, and deliberately **primed endpoint-cache evidence**. It covers all ten task types: far-safe rejection, exact range boundary, original-false, out-of-scope calls, active-slot same-target preservation, unrelated pursuit, recent same-attacker preservation (including ranged tasks), unrelated attacker, stale attacker, storms, and `seekEntity` versus unrelated notifications. An entity subclass supplies the fixture's task-AI behavior; this is not a spawned game entity.
- Independently verifies cached endpoint evidence is invalidated by storms, position/dimension changes, and disposal. Priming the cache isolates acquisition logic; it does **not** prove environmental safety sampling in a loaded world.
- Decodes compiled mod IL across all declared method bodies, constructors, and compiler-generated methods. Resolves method operands (including delegate targets) and rejects direct calls to native sensing, original acquisition predicates/`ShouldExecute`, entity searches, ray tracing, pathfinding/navigation, and tick/callback registration. Scheduler access is limited to the read-only `AiTaskManager.ActiveTasksBySlot` getter. The run prints audited method/edge totals and SDK call edges. This is a structural/per-call-count performance guard, not a frame-time benchmark or a proof about arbitrary third-party mods.

### Regression findings fixed and verified

Native binding tests caught legacy `attackedByEntity`/`RecentlyAttacked` being resolved on the wrong base. They are now compiled against `AiTaskBaseTargetable`; modern bindings use `AiTaskBaseR`. Cold environment tests caught truncating negative chunk coordinates; chunk selection and local indexing now floor correctly. Both regressions are fixed in production source, not hidden by test workarounds.

The current native suite passes **13 checks**. Added cold-reader fixtures execute production environment sampling against controlled native interface implementations, covering solar precedence, light, terrain, torchlit underground rejection, temporal stability/storms, opt-out toggles, negative coordinates, cache TTL/eviction, calendar jumps, disposed-chunk invalidation, movement and endpoint independence. Only native stability noise is test-replaced in those fixtures; no production native stability logic is patched by the mod. Loaded-world gameplay remains a separate acceptance gate.

## Exact native patch inventory

Namespace for the types below: `Vintagestory.GameContent`. Legacy throw/stay-in-range/turret implementations are in external `Mods/VSSurvivalMod.dll`; the other listed task types and both targetable bases are in external `Mods/VSEssentials.dll`. `AiTaskBase` and entity/API types come from external `VintagestoryAPI.dll`. The suite prints each bound declaring assembly and resolves actual declaring types/signatures before installing actual patches; the table describes the intended acquisition-only boundary.

| Family | Declared native methods | Hook |
|---|---|---|
| Legacy pursuit | `AiTaskSeekEntity.ShouldExecute(): bool` | acquisition scope prefix + restoring finalizer |
| Legacy melee | `AiTaskMeleeAttack.ShouldExecute(): bool` | same |
| Legacy ranged throw | `AiTaskThrowAtEntity.ShouldExecute(): bool` | same |
| Legacy ranged positioning | `AiTaskStayInRange.ShouldExecute(): bool` | same |
| Legacy turret | `AiTaskTurretMode.ShouldExecute(): bool` | same |
| Modern pursuit | `AiTaskSeekEntityR.ShouldExecute(): bool` | same |
| Modern melee | `AiTaskMeleeAttackR.ShouldExecute(): bool` | same |
| Modern ranged shoot | `AiTaskShootAtEntityR.ShouldExecute(): bool` | same |
| Modern ranged positioning | `AiTaskStayInRangeR.ShouldExecute(): bool` | same |
| Modern turret | `AiTaskTurretModeR.ShouldExecute(): bool` | same; native base delegation retained |
| Legacy general predicate | `AiTaskBaseTargetable.IsTargetableEntity(Entity,float): bool` | postfix; only removes native `true` within the same task's acquisition scope |
| Legacy optimized all-codes predicate | `AiTaskBaseTargetable.IsTargetableEntityNoTagsAll(Entity,float): bool` | same |
| Legacy optimized exact/prefix-codes predicate | `AiTaskBaseTargetable.IsTargetableEntityNoTagsNoAll(Entity,float): bool` | same |
| Modern predicate | `AiTaskBaseTargetableR.IsTargetableEntity(Entity,float): bool` | same |
| Legacy pursuit notification | `AiTaskSeekEntity.Notify(string,object): bool` | prefix; rejects only qualifying `seekEntity` requests |
| Modern pursuit notification | `AiTaskSeekEntityR.Notify(string,object): bool` | same |

The legacy optimized predicates matter: native configuration can bind those callbacks instead of the general predicate. Patching only the general predicate is incomplete. Native `CanSense`, search helpers, entity partition scans, task scheduling, start/continue/finish execution, movement, attack/projectile handling, path traversers, ray tracing, and pathfinder workers are **not** patched. The install-set test enforces that distinction.

## Bowtorn and continuity audit

The exact external asset `assets/survival/entities/lore/bowtorn.json` uses task AI, including `meleeattack`, **`stayinrange`**, and **`turretmode`**. Both corresponding legacy and modern implementation families are included above; covering seek/melee/throw alone would omit bowtorn positioning/turret acquisition. Asset identifiers and native type names are cited here; proprietary decompiled implementation text is not distributed in this repository.

Continuity is intentionally pair-specific:

- Pursuit exemption reads the owner's native **active eight slots**, not every configured task and not a world scan. An active supported legacy/modern combat task (seek, melee, throw/shoot, stay-in-range or turret) must reference the **same candidate entity**; an unrelated flee/idle task does not establish pursuit. Merely having another target or another player pursued does not grant exemption. The filter does not stop a running pursuit or rewrite its target/path.
- Retaliation exemption requires the task's native recent-attack predicate **and the same attacker object** as the candidate. This applies to ranged throw/shoot/turret/stay-in-range tasks as well as seek/melee. A different recent attacker or an expired attack does not exempt arbitrary players.
- An unknown/missing task manager, incompatible slot layout, missing environmental data, or failed adapter read fails open to native behavior. This must not be confused with proof that the feature is active.

Audit references outside the repository: the exact 1.22.7 server directory passed as `GameDir`; native types listed above; bowtorn asset path above. Local inspection notes were derived from `/home/brunette/.hermes/cache/scratch/ai-audit-1227/` and the external SDK, not bundled as source.

## Not tested here

No spawned in-game mob/player encounter, rendered client, live pursuit, actual melee/projectile hit, network synchronization, line-of-sight search, pathfinding route, calendar/terrain/lighting/stability sampling in a loaded world, storm transition during combat, or frame-time performance measurement is claimed by this suite. Successful native predicates/`ShouldExecute` on fully initialized gameplay tasks remain outside its scope. Native headless server startup/smoke and packaging verification are separate gates, and even a successful headless startup is not a native gameplay acceptance test.
