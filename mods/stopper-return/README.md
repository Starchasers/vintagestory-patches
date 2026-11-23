# Kuboczoch: Stopper Return

Standalone ACA compatibility patch, version **1.0.3**, for **all stopper materials**.

| Offhand | Removed stopper |
| --- | --- |
| Empty | Goes into the offhand |
| Matching stopper with space | Merges up to the collectible's maximum stack size |
| Full matching stopper stack | Drops on the ground; offhand stays full |
| Different stopper type | Drops on the ground; offhand remains unchanged |
| Unrelated non-stopper item | Existing ACA interaction routing remains unchanged |

The patch clones the removed nested stopper before native transfer/drop and relaxes the comparison only when **both collectibles are marked `isLiquidContainerStopper`**. Remaining bottles retain their stopper. No inventory-wide patch, provider edit or saved-data repair is used.

## Changes in 1.0.3

- Share one process-local Harmony patch between integrated server/client ModSystem instances. Either instance can dispose first; the patch remains until the last instance exits. Failed installation rolls back only its own owner.
- Return transformed bottles on the server only, preventing ACA dev29's client-side insertion/extraction inventory additions. dev32 already has server gates. Native held-slot prediction, stopper transfer and server drop fallback remain intact; waxing paths are unchanged.
- Native insertion tests preserve all six nested stoppers and find no live offhand alias: insertion is **not** patched speculatively. The literal zero-count offhand is reproduced through the original extraction aliasing when the 1.0.2 duplicate-start failure removes the patch.

## Install / remove

Stop the game/server. Remove older Stopper Return ZIPs and extracted copies from **both client and server**. This includes `kuboczochceramicstopperreturn-1.0.0.zip`, `kuboczochceramicstopperreturn-1.0.1.zip` and `kuboczochstopperreturn-1.0.2.zip`; the old ceramic mod ID can otherwise load alongside the renamed mod and install duplicate patches.

Put only `kuboczochstopperreturn-1.0.3.zip` in `Mods` on **both client and server** (singleplayer uses the same installation), alongside ACA. Restart to install/remove. No content IDs or save format are added. Back up your save before testing development-version mods.

Identity: **Kuboczoch: Stopper Return**, mod ID `kuboczochstopperreturn`, Harmony ID `kuboczoch.stopperreturn`, assembly `KuboczochStopperReturn.dll`, namespace `Kuboczoch.StopperReturn`.

Tested with actual VS **1.22.7** managed assemblies and ACA **2.0.0-dev.29 / dev.32**. Already-corrupted zero-sized nested stoppers are not repaired. **Full running-game/network/HUD validation remains pending**; no pack-wide compatibility claim. See [RESEARCH.md](RESEARCH.md) and [REGRESSION-1.0.3.md](REGRESSION-1.0.3.md).

## Build and tests

Requirements: Python 3, .NET 10 SDK, external VS 1.22 game directory including game/API/survival/essential/Harmony assemblies. No game/provider SDK is redistributed.

```sh
python tools/package.py --game-path /path/to/game --dotnet /path/to/dotnet
python -m unittest discover -s tests -p 'test_*.py' -v
python tests/run_native.py --dotnet /path/to/dotnet --game-path /path/to/game \
  --provider dev29 /path/to/extracted/aca29 /path/to/ACA-dev29.zip \
  --provider dev32 /path/to/extracted/aca32 /path/to/ACA-dev32.zip \
  --mod-assembly bin/Release/net10.0/KuboczochStopperReturn.dll \
  --baseline-assembly /path/to/1.0.2/KuboczochStopperReturn.dll \
  --output /path/to/evidence
```

The optional baseline DLL reproduces 1.0.2's duplicate-start failure and isolates its remaining dev29 authority defect. Every run records loaded provider/mod SHA256. The exact supplied release DLL replaces the harness's linked copy before execution.

Per provider, the existing offhand matrix covers **4,797 cases**, including **47 stopper variants**, all ordered different-type pairs, empty/matching/near-full/full offhands, sequential one/three-bottle openings, unrelated routing and fail-closed IL guards. Its world/inventory services remain doubles.

The new native flow uses actual **ACA insertion and extraction**, **PlayerInventoryManager.TryGiveItemstack**, **InventoryPlayerHotbar**, native slots/merges, and separate client/server inventories reconciled through actual binary inventory-tree serialization. It starts both ModSystem instances, inserts six stoppers, consolidates through native merge, opens six bottles, moves the loose stopper after four openings, checks pre-sync client return additions and checks full-inventory server/client drop behavior. Lifecycle tests cover duplicate/repeated Start, both disposal orders, reload, repeated disposal, concurrent peers and failed-install owner isolation.

**Limits:** transport is an explicit authoritative full-inventory snapshot, not game packet handlers or dirty-slot scheduling. World/player/events/registry are API doubles; no running game, asset loader, rendering or HUD. The harness reproduces the literal zero-size offhand and the unwanted client-return mutation, not the exact rendered ghost-icon timeline.

The deterministic ZIP allowlists our DLL, manifest, README, research and regression notes, with fixed timestamps/permissions and verified payload bytes. No provider/game/Harmony DLLs or test harness ship. Raw local evidence and a compact checked-in summary support the regression notes.

## Pending in-game acceptance

Use fresh bottles, not previously corrupted stacks. Start singleplayer and confirm no `ACA stopper extraction IL changed` startup exception. Insert six stoppers into six bottles; confirm no persistent hotbar ghosts. Consolidate, extract four stoppers, move them to another slot, then extract the final two: six open bottles and six loose stoppers must remain. Repeat across bark/ceramic/glass/metal/wood with empty, partial/near-full/full matching and different stopper offhands. Check server/client agreement, restart persistence, unrelated interactions and dedicated-server reconnect. No save mutation, deployment or upgrade is performed by these tests.
