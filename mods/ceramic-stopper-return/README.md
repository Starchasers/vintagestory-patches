# Kuboczoch: Ceramic Stopper Return

Standalone Harmony compatibility mod for [issue #3](https://github.com/Starchasers/vintagestory-patches/issues/3). Requires Vintage Story **1.22.7** and A Culinary Artillery **2.0.0-dev.29 or newer**; native managed regression tests cover dev29 and dev32. No Expanded Foods dependency is added.

## Install / remove

Put `kuboczochceramicstopperreturn-1.0.0.zip` in `Mods` on **both client and server** (singleplayer uses the same installation), alongside ACA. Restart the game/server. Remove this ZIP and restart to uninstall. No content IDs or save format are added. Back up your save before testing any development-version mod combination.

The patch clones the removed stopper before ACA transfers it. This prevents a merge into the offhand from zeroing the stopper still referenced by the remaining bottles in a held stack. It also detaches a dropped stopper. ACA retains control of eligibility checks, container conversion, attribute cleanup, sounds, slot notifications, offhand transfer and ground-drop fallback. It applies to the same code path for all stopper materials, not unrelated inventory operations. It does **not** change the fallback to prefer main inventory.

Already-lost items and existing zero-sized nested stoppers are **not repaired**. Test with newly stoppered bottles; the fix prevents further corruption, it is not a save migration. A lone fresh stoppered bottle did not reproduce the reported loss in managed tests; the verified trigger is a **stack** of stoppered bottles.

## Quick game check (still required)

1. With the patch installed, make three freshly stoppered bottles and stack them in your active hand. Keep two matching ceramic stoppers in the offhand.
2. Uncork without holding Shift, aiming at air (valid in both tested ACA versions). Open all three. Expect three open bottles and **five stoppers total**; each remaining corked bottle must still return one stopper.
3. Repeat with an empty offhand, then with a full stack of matching stoppers. In the full-offhand case ACA drops the removed stopper on the ground; pick it up and verify the total.
4. Repeat a single bottle, Shift-to-stopper, and a different offhand item to check unchanged interaction routing. Check multiplayer slot sync after reconnecting.

Only **managed native-method tests** have run: real provider assemblies, real VS slots/stacks/container attributes, a real Harmony patch, and mocked world/inventory-manager services. No running game/client, rendering, network sync, sound, asset loading, or complete modpack validation is claimed. Await the user's game check before merging or closing the issue.

## Build / tests

From this directory with a separately obtained .NET 10 SDK and VS SDK:

```sh
python tools/package.py --game-path /path/to/game --dotnet /path/to/dotnet
dotnet run --project tests/Tests.csproj -c Release -p:GamePath=/path/to/game -p:AcaPath=/path/to/extracted/aca
# Expected nonzero exit with a stopper-conservation failure (unpatched baseline):
dotnet run --project tests/Tests.csproj -c Release -p:GamePath=/path/to/game -p:AcaPath=/path/to/extracted/aca -- --red
python -m unittest discover -s tests -p 'test_*.py' -v
```

`AcaPath` must contain `ACulinaryArtillery.dll`; run once per provider version, using the appropriate DLL. Only the test project references ACA/vanilla gameplay assemblies. Production uses runtime type lookup and compiles against the game API and shipped Harmony library, with copy-local disabled. Its dependency check requires ACA at load time; unexpected extraction IL fails explicitly instead of silently installing an ambiguous patch.

The deterministic packaging tool writes the ZIP and SHA256 under ignored `dist/`, with only the owned DLL, `modinfo.json`, and these two documentation files. It verifies archive bytes and CRCs; SDK/provider DLLs are never bundled. See [RESEARCH.md](RESEARCH.md) for provenance and exact test findings.
