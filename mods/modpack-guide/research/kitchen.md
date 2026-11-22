# Kitchen process evidence

Companion data: `research/kitchen.json`. Four authored process pages; player prose contains no source citations or audit boilerplate. Evidence belongs in the step `evidence` arrays and this document, not in rendered prose. No implementation files changed.

## Scope and native navigation

Exact pack root: `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted`.

`mods/archive.zip!member#key` is archive-member notation relative to that root; the fragment identifies a JSON/localization key rather than a filesystem filename. All cited step archive members were checked for existence. Read assets with JSON5: several native handbook and clayforming declarations are not strict JSON.

Installed archives used:

| Archive | SHA-256 |
|---|---|
| `mods/ACulinaryArtillery_2.0.0-dev.29.zip` | `926f4e94bea53eee15e0f60b8873bc9a9fd096c326cff6f20b68e1bdab22f714` |
| `mods/ExpandedFoods_2.0.0-dev.17.zip` | `13ca68a3cf21d3950c04a5e01ec054b459c2f1e99e62c01f72d2d6b99b879279` |
| `mods/DairyPlus_2.0.0.zip` | `6b31b32119e556372b809feee3a0277b8357f8a1b65feb7b8913b8b1550c1cb3` |

All linked native pageCodes occur literally in existing `native-registry.json`:

| Exact native pageCode | Declaration/source |
|---|---|
| `aculinaryartilleryguide` | ACA `assets/aculinaryartillery/config/handbook/24-aculinaryartilleryguide.json` |
| `kneadingmixingguide` | ACA `assets/aculinaryartillery/config/handbook/31-kneadingmixingguide.json` |
| `simmeringguide` | ACA `assets/aculinaryartillery/config/handbook/32-simmeringguide.json` |
| `expandedfoodsguide4` | EF `assets/expandedfoods/config/handbook/28-expandedfoodsguide4.json` |
| `creamguide` | Dairy `assets/dairyplus/config/handbook/31-creamguide.json` |
| `butterguide` | Dairy `assets/dairyplus/config/handbook/32-butterguide.json` |
| `butcheringguide` | existing native-registry entry; conceptual animal-processing cross-link |
| `gamemechanicinfo-farming` | existing native-registry entry; grain supply cross-link |
| `gamemechanicinfo-animalhusbandry` | existing native-registry entry; milk supply cross-link |
| `gamemechanicinfo-foodpreservation` | existing native-registry entry; storage cross-link |

Do not prefix the bare ACA/EF/Dairy guide codes with their asset domain. These are authored navigation references, not claims that their mechanics are absent from the original handbook. ACA also registers recipe guide content in code; preserve that existing evidence/implementation. This task neither reclassifies code-native recipes as missing nor invents computed cooking-page IDs. Item/block references in JSON are exact collectible AssetLocations, not speculative handbook aliases; the renderer must retain the native stack callback/page-routing behavior.

## Recipe chains and interactions

### 1. Bone broth: clarification is not gelatin

EF recipe members:

- `assets/expandedfoods/recipes/simmering/broth.json`: 100 water portions + one `game:bone` => 50 `expandedfoods:brothportion-bone`; recipe heat threshold 200 and duration 24. The visible page does not equate duration fields with in-game hours.
- `assets/expandedfoods/recipes/simmering/clarifiedbroth.json`: 10 `expandedfoods:brothportion-bone` + one `aculinaryartillery:eggwhiteportion` => 10 `expandedfoods:clarifiedbrothportion-bone`. **No bone ingredient here.**
- `assets/expandedfoods/recipes/simmering/gelatin.json`: 100 bone-broth portions + one bone => 25 `expandedfoods:gelatin-liquid`; then 10 liquid-gelatin portions + one bone + 100 water portions => one `expandedfoods:gelatin-raw`; then one raw gelatin + 100 water portions => one `expandedfoods:gelatin-plain`. The first two stages also accept `game:bone-tiny` in separate recipes. We show the ordinary-bone path, not all variants.
- Broth and clarified broth define `waterTightContainerProps.itemsPerLitre = 100` in `assets/expandedfoods/itemtypes/liquid/broth.json` and `clarifiedbroth.json`. ACA `assets/aculinaryartillery/itemtypes/liquid/eggwhite.json` has the same ratio. Thus clarification consumes 0.1 L broth plus 0.01 L egg white. The empty egg-white variant state is retained as the recipe's literal `aculinaryartillery:eggwhiteportion` code; no trailing hyphen was invented.
- Gelatin item and state definitions: `assets/expandedfoods/itemtypes/food/gelatin.json`. We keep the intermediate gelatin quantities in recipe portions where shown, rather than guessing a liquid unit conversion.

ACA `assets/aculinaryartillery/lang/en.json#craftinginfo-simmering-text` documents the firepit/saucepan interface, four simmering slots, equal **numbers of recipe portions** (not equal litres), solids in the output slot and liquids remaining in the output container, which must be removed before further use. `assets/aculinaryartillery/blocktypes/saucepan-fired.json` expands to `aculinaryartillery:saucepan-blue-fired`, has a 5 L liquid container and a 4 L cooking-slot capacity. No claim that its capacities are ordinary cooking-pot meal capacities.

### 2. Pasta: knead, grid-process, dry or simmer

- EF `assets/expandedfoods/recipes/kneading/pasta.json`: one `game:flour-*` + 10 liquid-egg portions (`aculinaryartillery:eggyolkfullportion-*` OR `aculinaryartillery:eggyolkportion-*`) => one `expandedfoods:pasta-dough`.
- ACA `assets/aculinaryartillery/itemtypes/liquid/eggyolkfull.json`: chicken is a declared state, `itemsPerLitre = 100`, so the selected full-egg route uses 0.1 L. This page starts with liquid egg; it does not invent unsupported egg-cracking controls.
- ACA `assets/aculinaryartillery/blocktypes/mixingbowl.json` declares the blue bowl used by its own `lang/en.json#craftinginfo-mixing-text`. That native text distinguishes kneading without a pot from mixing a meal with a pot, the six ingredient slots, held-right-click operation and the lower-left cooking-pot slot.
- EF `assets/expandedfoods/recipes/grid/pastaprep.json`: stone above dough in a 1x2 grid => `pasta-flat`; hammer-tag or rolling-pin-tag alternatives exist. Sheet + knife-tag or cleaver-tag tool => `pasta-cut` via shapeless recipe. No tag-specific concrete tool was guessed for a stack reference.
- EF `assets/expandedfoods/itemtypes/food/mixing/pasta.json#attributes.bakingPropertiesByType`: cut => dried at baking temp 160; continued baking changes dried => charred, and can ultimately yield charcoal. Stop at dried.
- Same asset `transitionablePropsByType`: dried fresh-hours average 2592; fallback (fresh cut/dough/flat/cooked) 72; both eventually Perish to `game:rot`. These are **base transition parameters, not guaranteed remaining storage time**. The visible page uses a qualitative improvement, not a calendar promise.
- EF `assets/expandedfoods/recipes/simmering/pasta.json`: 100 water portions + one cut OR dried pasta => one cooked pasta. Charred is another accepted recipe branch, but not the recommended preservation endpoint.
- EF `assets/expandedfoods/lang/en.json#item-handbooktext-pasta` explicitly permits cooked pasta in stew or mixed meals and oven-drying cut noodles for storage. No new unsupported specific mixed-meal recipe is implied.

### 3. Dairy: settle, skim, churn, salt

- Dairy `assets/dairyplus/recipes/barrel/settling.json`: 10 L `game:milkportion` sealed for 24 hours => 10 L `dairyplus:separatingmilk`.
- `assets/dairyplus/recipes/grid/creamscoop.json`: fired bowl + stick => block `dairyplus:creamscoop`; asset is `assets/dairyplus/blocktypes/creamscoop.json`.
- `assets/dairyplus/lang/en.json#creamguide-text`: milk in multiples of 10 L; crouch with scoop and interact with barrel; complete the delicate operation or mix the milk back together. Cream in scoop, skim milk remains in barrel. It explicitly excludes cheddar from skim-milk uses.
- Exact liquid asset filenames: `assets/dairyplus/itemtypes/liquid/seperatingmilk.json` (filename misspelling preserved; collectible code is `separatingmilk`), `cream.json`, `skimmilk.json`.
- `assets/dairyplus/recipes/grid/butterchurn.json` makes `dairyplus:plungerchurn`; `assets/dairyplus/blocktypes/churns/plungerchurn.json` declares class `dairyplus.churn`, entityclass `dairyplus.bechurn`, liquid capacity 12 L. `lang/en.json#butterguide-text` documents lower-body inventory opening, cream at top, handle operation and butter collected at bottom. No unverified cream-to-butter yield or required number of strokes was added.
- `assets/dairyplus/recipes/grid/saltedbutter.json`: two unsalted butter + one salt => two salted butter; one salted butter + one salt => one twice-salted butter.
- `assets/dairyplus/itemtypes/food/butter.json#transitionablePropsByType`: unsalted / salted / twice-salted fresh-hour averages are 168 / 336 / 720; transition-hour averages 48 / 96 / 168; all ultimately Perish into `game:rot`. This supports real preservation through salting, not immunity from spoilage. `canSealCrock` exists, but no new butter-crock sealing controls or numerical seal buff were invented.

### 4. Bottles: a practical container and stopper chain

ACA members:

- `assets/aculinaryartillery/recipes/grid/blocks/glassbottle.json`: saw-tag tool with glass slab => four `bottle-glass-{color}-fired`; quartz is an allowed variant. We selected quartz so no conditional colored-glass provider is required.
- `assets/aculinaryartillery/blocktypes/bottles/glassbottle.json`: material `glass`, quartz color, fired/corked/waxed states; 1 L capacity and 0.1 L transfer size. Open fired state enables held liquid transfer and drinking; corked/waxed states disable both and set `isTopOpened = false`.
- `assets/aculinaryartillery/recipes/simmering/boiledbark.json`: dry oak bark + 100 water portions => boiled oak bark. The visible page begins at dry bark; bark-harvesting controls were not proven and are not invented. Conditional `ancienttools` and `indappledgroves` recipes are not needed by this route.
- `assets/aculinaryartillery/recipes/grid/items/stopper-cork.json`: knife above boiled oak bark, 1x2 grid => sixteen `stopper-bark-cork`.
- `assets/aculinaryartillery/itemtypes/bark.json` explicitly declares `oak` and `dry`/`boiled`; `itemtypes/stopper-bark.json` declares the `bark-cork` stopper and `isLiquidContainerStopper = true`.
- `lang/en.json#block-handbooktext-stopper` documents insertion into bottles to seal and preserve liquids. `#block-handbooktext-bottle` documents slower spoilage in bottles, stronger preservation when stoppered, and high-quality cork enabling aging. `#block-handbooktext-bottlerack` recommends a cellar away from temperature fluctuations. This is source-backed interaction documentation, not a live-tested control sequence; no unsupported mouse gesture was specified.
- Cork's authored `liquidContainerStopperPerishRateByType["*-bark-cork"] = 0.5`; wood/ceramic/metal variants declare 0.9 and glass 1. Those numbers are configuration evidence only: the exact runtime composition with bottle/cellar rates was **not** traced in this task, so the player text deliberately promises no percentage, multiplier or precise shelf life. Stopper-dependent preservation is supported by native descriptive text; permanence is not.

## Vanilla references and pack settings

Exact vanilla assets root: `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets` (VS 1.22.7).

- `survival/itemtypes/liquid/waterportion.json`: code `waterportion`, itemsPerLitre 100. This backs water conversion in the broth/pasta recipes.
- `survival/itemtypes/liquid/dairy/milkportion.json`: code `milkportion`, itemsPerLitre 100.
- `survival/itemtypes/resource/bone.json`: code `bone`.
- `survival/itemtypes/resource/crushed/salt.json`: code `salt`.
- `survival/itemtypes/food/flour.json`: flour variants; rye is the chosen illustrative concrete ingredient, not the only accepted flour.

Exported `config/ModConfig/aculinaryartillery.json` was inspected. It supplies oven/meal-healing/spile settings; no new claimed exact cooking timer was derived from these keys. An exported config is not proof of a live server value.

Food Shelves was evaluated but kept outside this ACA/EF/Dairy-only deliverable. Its `config/ModConfig/FoodShelvesServer.json` sets `GlobalBlockBuffs=true`, `LakeIceToCutIce=false`, `GlobalPerishMultiplier=1.0`, `CooledBuff=0.3`, `IceMeltRate=1.0`. The shipped cooling-cabinet description's advertised percentages were not copied as verified pack results; their runtime formulas would need separate static tracing. Do not tell players to saw lake ice from the unqualified recipe list while that option is false.

## Validation and limitations

Static validation executed against `kitchen.json`:

- Four pages: 212 / 234 / 237 / 219 words, including title, intro, step labels and guide labels; every page below 400 words.
- Every step contains evidence; every referenced ZIP/member exists.
- Every guide destination resolves in supplied `native-registry.json`.
- All modded item/block references expand from the exact archive's declared code and variant groups, including type checks (cream scoop/churn/bottles are blocks; butter/pasta/liquids are items).
- Vanilla water, milk, bone, salt and rye flour checked against exact-version item assets.

No bundled DLL was executed. No full mod startup, recipe loading, in-game clicking, live spoilage timing, recipe ingredient-provider patch or GUI test was performed. These data pages preserve native guides and collectible references; implementation must use the existing native callback/link machinery, especially for attributed pasta nutrition stacks and ACA's generated recipe guides. The evidence supports authored assets and native documented interactions, not a claim that every server/runtime configuration has been exercised.
