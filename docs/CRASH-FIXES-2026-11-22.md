# Crash fixes for the 22 November 2026 session

Two independent client failures were investigated on Vintage Story 1.22.7. They are intentionally fixed in separate mods and pull requests, not a catch-all exception suppressor.

## Rowing animation lifecycle

- Implementation: [Quiet Rough Seas PR #3](https://github.com/Starchasers/vintagestory-quietroughseas/pull/3), version **1.8.1**, pinned here at `5f8052aa14416a9073c6a6e66abfda1985a7ef1c`.
- Depends on existing [Quiet Rough Seas PR #2](https://github.com/Starchasers/vintagestory-quietroughseas/pull/2), preserving the installed 1.8.0 animal-lightning protection.
- Guards only Rough Seas `MountStrokes.Pace`, `DrawnShare` and `Finish` animation-state reads while their manager/animator is unavailable. Healthy virtual lookup and stroke bookkeeping remain intact.
- Parent verification: 195 runtime assertions; five intended failing controls; 65 current assertions and 2,000 seeded cases; four intended failing current controls. Official 1.22.7 dedicated-server startup compiled the installable source ZIP, installed the patches against the official Rough Seas 5.0.1 binary, reached RunGame and shut down cleanly.

## Disappearing inventory-slot hover

- Separate implementation: [collection PR #4](https://github.com/Starchasers/vintagestory-patches/pull/4), **Kuboczoch: Dead Corpse Inventory Fix 1.0.0**. This pin-only coordination PR does not contain that implementation.
- Exact VS 1.22.7 and Dead 2.9.8 methods reproduced a null leave-event slot after bag-content shrink. The patch retains the entered slot per GUI grid and substitutes it only when the leave lookup returns null, allowing native callbacks and cleanup to continue.
- Parent verification: unpatched native failure reproduced in both leave branches; patched production Harmony path passed 29 assertions; two packaging tests passed; build completed without warnings or errors.
- Neither the `spyglass` Harmony ID nor the launcher's combined crash summary proves those mods caused the hover failure.

## Review and rollout

1. Quiet Rough Seas PRs #2 and #3 were squash-merged in that order. This gitlink references the accepted main commit `5f8052aa14416a9073c6a6e66abfda1985a7ef1c`, whose source tree matches the tested rowing implementation.
2. Collection PR #4 was independently squash-merged as `e2c4fd35141f80359e8d45179209052788726a73`. Both fixes are needed to cover both reported failure classes.
3. Back up the mod set. Replace `quietroughseas_1.8.0.zip` with `quietroughseas_1.8.1.zip` on clients and server; keep Rough Seas 5.0.1 installed. Do not keep duplicate versions. Preserve the existing Quiet Rough Seas configuration.
4. Install `kuboczochdeadcorpseinventoryfix-1.0.0.zip` on affected clients; it is client-only and not required on the server. Remove earlier `inventoryhovercleanup-1.0.0.zip` and `kuboczochinventoryhovercleanup-1.0.0.zip` previews first if present: their mod IDs differ, so older ZIPs must not coexist with this release.
5. Restart clients/server. Test occupied rowing boats, mounting/dismounting, nearby players and first-person variants; test corpse bag-content changes, inventory switching and GUI close/reopen.

No live world, installed modpack or server was modified. Rendered multiplayer boat behavior and physical inventory/tooltip interaction have not been validated. Headless reproductions and native server startup are not a claim that the entire modpack is crash-free. Historical Windows native event-log failures on other dates were not assigned a cause or silently treated as fixed.
