# Inventory Hover Cleanup

Small client-only compatibility fix for the inventory-slot leave crash reported with
Vintage Story **1.22.7** and Dead **2.9.8**. No server installation or Dead dependency
is required. Install `inventoryhovercleanup-1.0.0.zip` in your client Mods directory.

When a hovered bag-content slot disappears, the native grid looks it up again and
can dispatch a null slot. A receiving grid dereferences `slot.Inventory` and crashes.
This mod remembers the actual entered slot per grid and uses it **only when that
leave lookup is null**, so normal leave listeners still run with a real slot and
native hover cleanup continues. Healthy non-null arguments are passed unchanged,
even if the inventory now contains a replacement slot. No blanket exception handling,
no inventory mutation, no looting/network changes, no boat patches.

The single Harmony transpiler targets `GuiElementItemSlotGridBase.OnMouseMove`:
one enter and two leave dispatch sites. It does not patch Dead or Spyglass. An
unsupported method layout fails explicitly rather than silently applying a partial
patch. Weak-keyed state does not keep disposed grids alive; retained slots are
cleared before leave callbacks. If no enter was recorded and lookup is null, there
is no valid slot to dispatch; native local hover reset still runs.

## Build and test

Requires .NET 10 and a separately obtained **1.22.7** SDK/installation (including
`VintagestoryAPI.dll` and `Lib/0Harmony.dll`). The official server SDK contains the
same client API methods needed for these headless managed tests. Tests additionally
require the original Dead 2.9.8 release's extracted `Dead.dll`.

```sh
# From this directory; replace paths with your own external installations.
dotnet run --project tests/Tests.csproj -c Release -p:GamePath=/path/to/game -p:DeadPath=/path/to/dead -- --red
dotnet run --project tests/Tests.csproj -c Release -p:GamePath=/path/to/game -p:DeadPath=/path/to/dead
python -m unittest discover -s tests -p 'test_*.py' -v
python tools/package.py --game-path /path/to/game --dotnet /path/to/dotnet
```

`--red` asserts that the unpatched game throws the reported NRE in both leave
branches; it succeeds only if that failure is reproduced. Green installs the
**production mod's real Harmony transpiler** on the real game method. Tests seed a
real Dead backpack inventory's bag-content list and then shrink it, exercising its
actual indexer. They do not construct real bag items or render a composer. Input
and client APIs are managed proxies; grid objects are allocated without GPU-backed
constructors. Receiver `OnMouseLeaveSlot` is real. Coverage includes native hover
reset, exact previous slot identity, unrelated inventory state, callback continuation,
no duplicate leave, re-entry, valid replacement slots, empty slots, cache clearing,
exception propagation, unsupported IL rejection, client-only loading and unpatching.

Packaging produces `dist/inventoryhovercleanup-1.0.0.zip`, with a deterministic
allowlist of the owned DLL, modinfo and these two documentation files. No game,
Harmony or third-party DLL is distributed.

## Validation limits

Managed tests are **not** a rendered gameplay test. Actual on-screen tooltip hiding,
Dead tab switching, bag item removal, close/reopen and the full modpack (including
Downed, Spyglass and Immersive Corpse Drop) still need in-game validation. Listener
continuation is tested, not the native tooltip renderer. No claim is made that
Spyglass caused the failure. See [research and exact binary provenance](RESEARCH.md).

Authors: Kuboczoch. Game and external mod code remain their authors' property;
external assemblies and decompiled source are not included.
