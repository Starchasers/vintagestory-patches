# Kuboczoch Firepit Tinder Return 1.0.0

Standalone compatibility addon for Vintage Story **1.22.7** and Art of Growing **1.2.2** (mod ID `artofgrowingpatch`). Install the release ZIP in `Mods` on **server and clients**, alongside Art of Growing. Do not unpack it. Restart the game/server. No upstream files need changing. Remove the ZIP to uninstall; existing blocks stay valid because only stock/AoG block IDs are used.

## Behaviour and scope

- New firepits built with `game:drygrass` use `game:firepit-construct1`.
- Straw (`game:thatch`) continues using `artofgrowing:firepit-construct1`.
- Native progression preserves each domain through construct1–construct4 and cold/lit/extinct.
- Breaking **construction stages 1–4** returns respectively one fresh dry grass or one straw, plus the original 0/1/2/3 firewood. Dry grass's previous drying/aging progress is not restored.
- Completed cold/lit/extinct firepits retain their existing drop rules (no extra tinder refund). This is a construction-material correction, not a new permanent refund for consumed tinder.
- **Old AoG firepits are ambiguous**: both materials previously produced the same AoG block ID with no material marker. They continue returning straw during construction. This addon cannot reconstruct their origin and does not mass-convert them.
- Other construction items, ground-storage/pit-kiln handling, claims checks, placement checks, consumption, sounds, animation and recipes are not replaced. The Harmony transpiler changes one firepit-code literal in AoG's own placement method.

## Why code rather than a global drop patch?

Exact retained pack Pszczoly 1.5.0 includes ArtOfGrowing_1.2.2.zip. Its dry-grass patch and straw asset both register `AOGItemDryGrass`; that class's shift-placement always selects the AoG firepit. AoG construction firepit assets return `game:thatch`; stock game construction firepits already return `game:drygrass`. A global AoG drop replacement would break straw. Selecting the existing stock identity for dry grass fixes both materials without new blocks, persistent attributes, copied upstream assets, or a global drop override.

The transpiler requires exactly one matching literal in the exact instance method signature. It fails visibly for an incompatible AoG implementation rather than silently broad-patching a different method. The Harmony patch is shared/refcounted across client/server ModSystem instances in an integrated process, and removed only after the last owner disposes.

## Build and native-assembly regression tests

Requires .NET 10 SDK, a legally obtained Vintage Story 1.22.7 server/game directory, and the user's original nested modpack ZIP (not included).

```sh
python3 tools/build.py --vs /path/to/vintagestory --pack /path/to/modpack.zip --dotnet /path/to/dotnet
```

This extracts test-only AoG DLL/assets into ignored/local `audit`, builds the original addon, invokes the actual AoG item placement method before/after Harmony, and calls real `BlockFirepit.GetDrops` / `TryConstruct` against both original asset drop tables. Fake world/claims/accessor interfaces supply deterministic boundaries; production placement/drop/progression logic is not reimplemented. Tests include all construction stages, completed states, both materials, permissions/placement rejection, item use, and multi-owner patch disposal. The harness is **headless**, not a graphical client playtest or a full modpack gameplay test.

ZIP packaging uses explicit allowlists, deterministic metadata, verified readback, and does not redistribute game DLLs, AoG DLL/code/assets, extracted reference material, or build intermediates. Source archive includes only original code, tests, tools, modinfo, this document and LICENSE. Run outputs and hashes are under `evidence`/`dist`.

## Manual client acceptance still required

On a backup/test world with the retained pack and addon on both sides: shift-place dry grass and straw, break separate examples at construct1–4, check material and firewood counts, finish/ignite/extinguish one of each, relog, and confirm no extra refund. Check dry-grass drying interactions remain normal. For legacy AoG firepits expect their unchanged straw result. No graphical/client playtest or live-server deployment is claimed by the headless regression suite.
