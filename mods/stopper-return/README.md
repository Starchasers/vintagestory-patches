# Kuboczoch: Stopper Return

Standalone ACA compatibility patch, version **1.0.2**, for **all stopper materials**. Prevents nested stopper loss when opening stacked bottles and permits opening while holding a different stopper type.

| Offhand | Removed stopper |
| --- | --- |
| Empty | Goes into the offhand |
| Matching stopper with space | Merges up to the collectible's maximum stack size |
| Full matching stopper stack | Drops on the ground; offhand stays full |
| Different stopper type | Drops on the ground; offhand remains unchanged |
| Unrelated non-stopper item | Existing ACA interaction routing remains unchanged |

The matching/empty transfer and failed-transfer ground drop remain ACA's native behavior. The patch narrowly relaxes its collectible comparison only when **both collectibles are marked `isLiquidContainerStopper`**. It also clones the removed nested stack before any transfer/drop so remaining bottles retain their stopper. No ceramic-specific code, inventory-wide patch, provider edit or saved-data repair is used.

## Install / remove

Stop the game/server. **Before installing, delete the old `kuboczochceramicstopperreturn-1.0.0.zip` and `kuboczochceramicstopperreturn-1.0.1.zip` files from `Mods` on both client and server.** Also remove any renamed copies or extracted installations of the old mod. Version 1.0.2 changes the mod ID, so the loader will not treat the old and new IDs as the same mod; keeping both can load duplicate patches.

Put only `kuboczochstopperreturn-1.0.2.zip` in `Mods` on **both client and server** (singleplayer uses the same installation), alongside ACA. Restart to install/remove. No content IDs or save format are added. Back up your save before testing development-version mods.

The complete identity is now **Kuboczoch: Stopper Return**: mod ID `kuboczochstopperreturn`, Harmony ID `kuboczoch.stopperreturn`, assembly `KuboczochStopperReturn.dll`, namespace `Kuboczoch.StopperReturn`. Version 1.0.2 changes naming only; stopper-return behavior is unchanged from 1.0.1.

Tested at the native managed-method level with VS **1.22.7** and actual ACA **2.0.0-dev.29 / dev.32**. Already-corrupted zero-sized nested stoppers are not repaired. Full client/server game testing remains pending; pack-wide compatibility is not asserted. See [RESEARCH.md](RESEARCH.md).

## Build and tests

Requirements: Python 3, .NET 10 SDK, external VS 1.22 game directory containing `VintagestoryAPI.dll`, `Mods/VSSurvivalMod.dll` and `Lib/0Harmony.dll`. Nothing from the game/provider SDK is redistributed.

```sh
python tools/package.py --game-path /path/to/game --dotnet /path/to/dotnet
python -m unittest discover -s tests -p 'test_*.py' -v
python tests/run_native.py --dotnet /path/to/dotnet --game-path /path/to/game \
  --provider dev29 /path/to/extracted/aca29 /path/to/ACA-dev29.zip \
  --provider dev32 /path/to/extracted/aca32 /path/to/ACA-dev32.zip \
  --mod-assembly bin/Release/net10.0/KuboczochStopperReturn.dll \
  --output /path/to/evidence
```

The runner discovers usable stopper variants from each actual ZIP (inheritance, attributes and variant states), verifies the copied provider DLL, and saves build logs, manifests, red/green logs and a JSON summary. In both tested providers: **47 variants** across bark (1), fired ceramic (10), glass (11), metal (12), wood (13). Unfired ceramic is not a usable stopper; optional Wildcraft patch additions are not base-provider assets and are not included in this discovery.

Per provider: **4,796 cases**, including all ordered different-type pairs, empty/matching/near-full/full offhands, one/three bottles opened sequentially, preserved unrelated-item routing, stack/reference and stopper/container conservation, and fail-closed IL guards. Original provider and previous clone-only behavior are expected reds; current patched behavior is green. Real ACA/VS methods, native offhand merge logic and nested stacks are exercised; world/inventory services are test doubles. The test fixture is not an asset-loader or running game.

The deterministic ZIP allowlists only our DLL, manifest, README and research, with fixed timestamps and permissions and verified payload bytes. No ACA/game/Harmony assemblies or test harness ship.

## Pending in-game acceptance

Use fresh stoppered bottles, not previously corrupted stacks. Test bark, ceramic, glass, metal and wood with empty, matching partial/near-full/full and **different stopper** offhands; repeat on stacked bottles. Each open must return exactly one stopper, via offhand merge or ground drop, and every unopened bottle must keep its own stopper. Different-type offhand stacks must remain unchanged. Check non-stopper interactions still behave as before, plus client/server agreement and restart persistence. No deployment or upgrades are performed by these build/tests.
