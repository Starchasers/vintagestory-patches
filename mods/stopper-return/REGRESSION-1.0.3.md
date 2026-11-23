# 1.0.3 regression evidence

## Falsifiable hypotheses

1. **Integrated duplicate installation disables the extraction fix. Confirmed.** The user's client log records `ACA stopper extraction IL changed; refusing an ambiguous patch` in Stopper Return's Start. Running two actual ModSystem.Start calls against the actual 1.0.2 release DLL reproduces it for ACA dev29 and dev32. Harmony repeats the same transpiler over its already-transformed output; 1.0.2 then unpatches the shared owner. 1.0.3 shares a locked, reference-counted installation.
2. **Held insertion stores a live offhand stopper reference that is consumed later. Falsified for the tested native insertion flow.** Actual provider code clones the offhand stack before SetStopper. Across six native held insertions, stored nested stopper totals are `1,2,3,4,5,6`, with no nested stack aliasing the live input. Native inventory consolidation does not lose stoppers. No speculative insertion clone patch was added.
3. **dev29 creates returned bottles client-side; dev32 does not. Confirmed at the native inventory mutation seam.** The dev29 baseline produces one new client return bottle on each stacked insertion and extraction before authoritative synchronization, and client-side drops with a full inventory. dev32 already server-gates those calls. 1.0.3 suppresses only the first two of five TryGiveItemstack calls, including their paired client drop fallback.
4. **Unpatched extraction aliases a nested stopper and can expose a literal zero-count offhand. Confirmed.** Native insertion, consolidation and extraction produce offhand counts `1,1,1,1`; after moving the one loose stopper away, the next openings show `0,0`. Final conservation is six open bottles but one loose stopper. The existing detached-extraction fix works once its installation survives integrated startup.

## Actual native output

Both provider DLLs were byte-compared with their official ZIPs. VS 1.22.7 managed assemblies were used. Per provider:

| Mode | dev29 | dev32 |
| --- | --- | --- |
| Original extraction/offhand matrix | 4,797 cases, 4,512 failures | 4,797 cases, 4,512 failures |
| Previous clone-only matrix | 4,797 cases, 4,324 failures | 4,797 cases, 4,324 failures |
| 1.0.3 matrix | 4,797 cases, **0 failures** | 4,797 cases, **0 failures** |
| Original native paired flow | 14 failed assertions; 1 loose / 6 open | 6 failed assertions; 1 loose / 6 open |
| 1.0.2 single-install paired flow | 12 authority/drop assertions fail; 6 loose / 6 open | 0 failures; 6 loose / 6 open |
| 1.0.2 double Start | extraction-IL exception, shared owner removed | same |
| 1.0.3 paired flow, double Start | **0 failures; 6 loose / 6 open** | **0 failures; 6 loose / 6 open** |
| 1.0.3 lifecycle | pass | pass |

The native paired flow checks insertion ownership without calling GetStopper as an observer: GetStopper itself removes `timeFrozen` and would alter merge eligibility. It uses raw native contents for observation. The two sides use distinct native inventories and binary inventory-tree roundtrips; reconciled inventories are byte-equal and do not share stack references.

The live lifecycle tests use actual Harmony and actual mod Start/Dispose, including repeated Start, both disposal orders, reload, repeated Dispose, eight concurrent instances and a deliberately failing install under a different Harmony owner that must not remove the live peer. Full-inventory insertion and extraction each drop exactly one transformed bottle on the server and none on the client.

The prior behavior matrix remains intact across 47 actual stopper variants and every ordered different-type pair: empty receives; matching merges up to maximum; full/different drops; unrelated items retain ACA routing. Python packaging/discovery tests: **11 passed**.

## Evidence and limits

`tests/evidence/1.0.3/` contains compact captured result lines and provider/mod hashes. The runner saves full raw stdout/stderr and build logs to its requested evidence directory, verifies provider ZIP/DLL identity and can replace its linked assembly with the exact release DLL before execution. With the optional 1.0.2 baseline, the test executable is compiled against that lower assembly version so both binaries load without synthetic assembly rewriting.

This is **not full-game verification**: world/player/events/registry interfaces are test doubles, assets are represented by minimal native collectibles, and authoritative reconciliation uses full binary inventory snapshots rather than network packet dispatch, dirty-slot scheduling or timing. No HUD rendering is exercised. The unwanted client inventory mutation is reproduced, not the exact video's rendered ghost-icon pattern. Dedicated-server reconnect, real singleplayer startup, restart persistence and visual behavior still require in-game testing. Existing corrupted stacks are not repaired; no user save was opened or changed.
