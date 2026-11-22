# Kuboczoch Saw Container Guard 1.0.0

Compatibility addon for **SaltyWater's Saw Chiseling 0.1.2**, tested against
**Vintage Story 1.22.7**. Keep the original mod installed; this ZIP contains only
our guard assembly, metadata and this document. No upstream or game DLL is redistributed.

## Install

Back up the world. Put `KuboczochSawContainerGuard-1.0.0.zip` in `Mods` on **both the server
and every client**, alongside the original Saw Chiseling ZIP, then fully restart.
Do not replace the original DLL. The guard uses native Harmony on both sides.
Removing this ZIP rolls back the guard; it does not modify saves or repair already
converted containers. Restore a backup to recover previously lost contents.

Normal solid wood planks/logs remain convertible when the original mod permits
it. Containers are not converted regardless of contents. Their native interaction
is allowed to continue, rather than swallowing the right-click. Existing ordinary
chisel/microblock editing remains available.

## Protection policy and compatibility bounds

No block-name blacklist is used. Conversion is denied for:

- Native `BlockContainer` and subclasses, or native container/creature-container behaviors.
- Any `BlockEntityContainer` or `IBlockEntityContainer`, including modded subclasses.
- **Any other instantiated block entity or declared `EntityClass`**, even if empty,
  not yet instantiated, or not implementing an inventory interface.
- Missing/failed world, accessor, target or entity lookups.

This deliberately also protects stateful non-container blocks (machines, signs,
ground storage, etc.). It is safer to decline a saw conversion than discard state.
Ordinary existing microblocks are exempt only from the resolver/held editing check;
inventory-bearing microblocks are still denied and direct conversion stays denied.
A hypothetical mod storing inventory entirely outside block entities, declaring no
entity class, using no native container type/behavior and exposing no native inventory
contract is outside the discoverable contract and requires its own integration.

Saw Chiseling is optional: when absent, startup logs a no-op. When present, the guard
validates all three exact method signatures before patching. An incompatible version
raises a startup error, not silent apparent success. **Do not continue with an
incompatible-version error**: disable Saw Chiseling until the guard is updated.
Future upstream behavior/signature changes and other mods replacing these methods
are not certified by this release. Game dependency metadata is a minimum version,
not a claim that later game versions were tested.

## Build and native regression tests

Requires .NET SDK 10 and a legally obtained external Vintage Story 1.22.7 installation
containing `VintagestoryAPI.dll`, `Mods/VSSurvivalMod.dll`, and `Lib/0Harmony.dll`.
From the repository root:

```sh
python3 addons/SawContainerGuard/tools/package.py --game-path /path/to/vintagestory

dotnet run -c Release --project addons/SawContainerGuard/tests/NativeRegression.csproj \
  -p:GamePath=/path/to/vintagestory -- /path/to/original/SawChiseling.dll

dotnet run -c Release --project addons/SawContainerGuard/tests/NativeRegression.csproj \
  -p:GamePath=/path/to/vintagestory -- --absent

python3 -m unittest discover -s addons/SawContainerGuard/tests -p 'test_*.py' -v
```

`--dotnet /path/to/dotnet` selects an alternate SDK binary for packaging. Output is
`artifacts/KuboczochSawContainerGuard-1.0.0.zip` plus SHA256. Packaging uses a fixed allowlist,
fixed ZIP timestamps/permissions, verifies bytes and CRC after writing, and never
copies the game or upstream dependencies into the release.

Tests invoke the **exact original DLL methods** before and after our production
Harmony patches, using actual native SDK block/entity/inventory/stack classes and
minimal world/accessor proxies. Unpatched controls demonstrate replacement via
`SetBlock`; patched cases assert unchanged block/entity/inventory/stack identity,
quantities and serialized bytes. Empty/full reed, wood, labeled, reinforced-type,
modded and interface-only containers are covered, along with stateful/null/error
cases and normal wood conversion. The reinforced fixture is a native container type,
not a running reinforcement subsystem.

**This is native headless regression coverage, not a full game smoke test.**
The harness observes/skips replacement BE mesh initialization and dirty notification
on its own test fixture only. It does not run a live chunk manager, renderer,
network or save system. See `tests/README.md` in source for exact boundaries.

## Before using a valuable world

In a disposable copy with both mods installed on server/client:

1. Try empty/full reed, wood, labeled, reinforced and modded containers with a saw;
   confirm no conversion and contents retained, including after save/reload.
2. Confirm normal container opening, including normal/sneak and held-use variants.
3. Confirm allowed ordinary planks/logs still convert and wood microblocks still edit.
4. Check nonwood, claim-denied and reinforced solid wood retain upstream restrictions.
5. Check both logs for guard installation and absence of errors; test reconnect/restart.

These GUI/network/persistence checks have not been executed by the automated harness.
