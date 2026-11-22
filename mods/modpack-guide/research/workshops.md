# Workshops research and draft evidence

Deliverable: `research/workshops.json`, four process pages, not implementation changes. Preserve existing Toolsmith pages and attributed-family rendering. No mod DLL was executed.

## Evidence path convention

- `mods/ARCHIVE.zip!assets/...` is an exact ZIP member under pack root `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted`.
- `config/...` and `evpack.json` are relative to that same root.
- `vs:assets/...` is relative to `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs`.
- Absolute paths point to already-present audit/decompiled/source evidence. Those inputs were read, not modified.

## Scope and active-state findings

The manifest's `Mods` entries were parsed by exact `ModId` and `Enabled`, not inferred from config presence. All mods named by these draft pages are enabled, including **Sortable Storage**. `em` is the actual Expanded Matter ID/asset namespace. Kevin's Furniture and Shelf Obsessed are retained as separate catalog/display coverage rather than padding these process pages with visual variants.

This is a **static pack audit**, not a live server registry dump. Asset existence, variant expansion, recipe files, manifest state, config values, conditional patches and available decompiled registration code were checked. Runtime recipe availability can still be changed by code mods; the draft avoids presenting dummy templates or legacy Toolsmith config fields as active recipes.

### Woodworking: active interaction, not dummy grid recipes

`config/ModConfig/ImmersiveWoodworking/general.json` has:

- `DisableVanillaFirewoodCrafting`, `DisableVanillaPlankCrafting`, `DisableVanillaDebarkCrafting`, `DisableVanillaLogTannin`: true.
- `KeepTraitRecipes`: true; do not claim every trait-specific recipe disappears.
- `AllowVanillaSaw`: true; `RemovePitSaw`: false.
- `CraftableSawhorse`: false; `AutoRestock`: true.
- Sawmill, chopper and bark chain enabled.

`/home/brunette/.hermes/cache/scratch/woodworking-tutorial/IwSystem.cs` explicitly declares the split `general.json` field list, then uses the configuration when filtering grid recipes (around lines 660 onward). `RegisterSawhorseCraftingRecipe` returns immediately when `CraftableSawhorse` is false (around line 896). The ZIP's `recipes/grid/choppingblock.json` is **enabled:false** and functions as a template for code-driven in-world crafting, not permission to tell players to craft it in the grid. The two-trestle stick → stick → rope sequence and the end/side sawing/debarking interactions are explicit in `assets/immersivewoodworking/lang/en.json` (`sawhorse-handbook-text`, `choppingblock-handbook-text`).

Toolsmith workbench is the grid recipe `PPP,QSQ,Q_Q`, with P/Q plank ingredients and S a reusable `game:saw-*`. Output is exactly `toolsmith:workbench-north`. Grindstone is `PSP,QQQ,Q_Q`, with planks and a rock (granite, sandstone, shale or andesite); output for the displayed branch is `toolsmith:grindstone-granite-north`. Both recipe files are cited directly in the JSON. The draft does not substitute a dummy sawhorse recipe for the real construction sequence.

### Ore → metal → assembly

`ImmersiveOreCrushConfig.json` has `DisableNuggetRecipes:true` and `UseVanillaYields:true`. Therefore the explicit standard/crystallized yield numbers elsewhere in that config are not a reliable promise for the active yield branch. No numeric nugget yields are printed. The hammer blacklist is empty. `modinfo.json` establishes the mod's in-world ore-crushing mechanic.

The ore page deliberately routes alloy ratios, bloomery/iron processing, anvil work and steelmaking to existing native guides instead of inventing a universal copper-style casting process. `ManualQuenching_1.0.10.zip!modinfo.json` describes cooling hot metals without dropping them; this does not justify importing universal bonus claims from unrelated overhaul-library quenching configuration.

**Preserve Toolsmith:** `evidence/attributes/RecipeRegisterModSystem.cs` reads the world's actual grid recipes and discovers tool heads before generating/rewriting Toolsmith assembly recipes. The current field used there is `EnableGridRecipesForToolCrafting`; `evidence/active-config/Toolsmith.Config.ToolsmithConfigs.decompiled.cs` declares that current field, while old `Toolsmith.json` contains legacy names including `EnableGridRecipes`. Do not treat the old key as proof that the current gate is enabled. The draft routes to Toolsmith's existing assembly, durability/wear and reforging guides and leaves its specialized pages intact.

### One complete materials branch, including transformations

All paths below are in `bricklayers-3.3.4.zip`:

1. `recipes/barrel/glazingbase.json`: water + fire/blue/red clay → `glazingbase-clay`; slurry + `em:powdered-ore-quartz` → `glazingbase-clear`. Milky base is a separate cassiterite branch.
2. `recipes/grid/frit/frit.json`, blue copper recipe: `LCC,QQB`; L = lime, C = `em:powdered-metal-copper`, Q = powdered quartz (ingredient quantity 2), B = powdered borax; output 4 `bricklayers:frit-mix-blue`.
3. `itemtypes/resource/frit.json`: `combustiblePropsByType` converts mix → baked frit at melting point 950; `grindingPropsByType` maps baked → ground frit. These are item transformations, not missing grid recipes.
4. `recipes/barrel/glazings.json`: 1 litre clear base + 1 ground frit, seal 48 hours → colored clear glaze. Blue is explicitly allowed.
5. `recipes/barrel/glazing_bowls.json`: burned bowls + glaze → raw glazed bowls. `blocktypes/clay/bowlglazed.json` then fires raw → burnt using `{glazing}-{color}-{pattern}`, melting point 950.

The displayed branch is clear/blue/plain. Native glazing guide key is **`bricklayers:craftinginfo-glazedceramics-text`**, including its prefix; it describes pulverizing then grinding mineral materials and lists the equipment. Exact powder definitions are in `expanded_matter-3.8.3.zip!assets/em/itemtypes/resource/powdered-{ore,metal}.json`.

### Storage recipes and actual useful integrations

- Resource bin recipe `purposefulstorage_2.3.1.zip!assets/purposefulstorage/recipes/grid/resources/resourcebin.json`: `P__,P_P,PPP`, **planks only**, output `resourcebin-normal-east`. North is another valid orientation from the definition, used as the draft icon. One recipe preserves `PSAttributes.wood`; the generic recipe also exists. Restrictions are group-based, including ore chunks, nuggets/bits, charcoal, quartz, sticks and specific powders, not arbitrary chest contents.
- Expanded Matter additionally ships `assets/em/compatibility/purposefulstorage/patches/ps_storagebin.json`, adding `em:ash-*` and `em:powdered-*` to the bin's powder group. This compatibility-folder asset is useful evidence but **is not by itself runtime loader proof**; the page does not require it to claim every EM material is accepted.
- Food cupboard recipe in Containers Bundle: `WNW,WBW,WNW`; W = planks (quantity 2 per occupied cell), N = nails-and-strips, B = nails-and-strips (quantity 2). **No saw or fired bowl is required.** Output is `containersbundle:foodcupboard-east` with `type:normal-generic`. Block definition declares this default type, 36 slots, vegetable multiplier 0.85 and grain multiplier 0.75. No blanket food-preservation claim is made.
- `purposefulstorage/.../patches/carryon.json` adds a Carryable behavior to the resource bin with `dependsOn:carryon`; `containersbundle/.../patches/carryon.json` explicitly does the same for foodcupboard. Both mods and Carry On are enabled. These exact patches support the relocation connection without assuming all modded containers work.
- Sortable Storage's manifest entry is enabled and its `modinfo.json` describes a sort button. Sorting is not presented as automatic material routing.

An enabled-mod ZIP patch scan found the above carry patches, the EM bin extension and Purposeful Storage's Butchering integration among matches for the chosen workstation/container targets; no JSON patch override targeting the selected frit/glaze, Toolsmith workbench/grindstone or Woodworking grid-template paths was found by those exact path searches. This does not establish absence of code-driven changes.

## Exact reference checks

All **25 unique item/block references** in the JSON resolve to asset definitions plus their declared variant groups. Vanilla item definitions with world-property expansion were checked against `assets/survival/worldproperties/block/wood.json`, `block/metal.json` and horizontal orientations. This matters: the existing `vanilla-codes.json` list omits `game:plank-oak` despite its real load-from-properties definition.

| Exact code(s) | Definition |
|---|---|
| `game:plank-oak` | `vs:assets/survival/itemtypes/resource/plank.json` + wood property oak |
| `game:ingot-copper`, `game:ingot-iron` | `vs:assets/survival/itemtypes/resource/ingot.json` + metal properties copper/iron |
| `game:firewood`, `game:charcoal`, `game:stick` | Corresponding files under `vs:assets/survival/itemtypes/resource/` |
| `game:saw-copper` | `vs:assets/survival/itemtypes/tool/saw.json` |
| `game:crucible-blue-fired` | `vs:assets/survival/blocktypes/clay/fired/crucible.json` |
| `immersivewoodworking:choppingblock`, `immersivewoodworking:sawhorse` | Corresponding files under that ZIP's `assets/immersivewoodworking/blocktypes/` |
| `immersivewoodworking:pitsaw-copper` | `assets/immersivewoodworking/itemtypes/tool/pitsaw.json` |
| `toolsmith:workbench-north`, `toolsmith:grindstone-granite-north` | Corresponding Toolsmith `blocktypes` definitions; recipes above |
| `em:powdered-ore-quartz`, `em:powdered-metal-copper` | Corresponding `assets/em/itemtypes/resource/powdered-*.json` |
| `bricklayers:glazingbase-clay`, `bricklayers:glazingbase-clear` | `assets/bricklayers/itemtypes/liquid/glazingbase.json` |
| `bricklayers:frit-mix-blue`, `bricklayers:frit-baked-blue`, `bricklayers:frit-ground-blue` | `assets/bricklayers/itemtypes/resource/frit.json` |
| `bricklayers:glazing-clear-blue` | `assets/bricklayers/itemtypes/liquid/glazing.json` |
| `bricklayers:glazedbowl-raw-clear-blue-plain`, `bricklayers:glazedbowl-burnt-clear-blue-plain` | `assets/bricklayers/blocktypes/clay/bowlglazed.json` |
| `purposefulstorage:resourcebin-normal-north` | `assets/purposefulstorage/blocktypes/resources/resourcebin.json` |
| `containersbundle:foodcupboard-east` | `assets/containersbundle/blocktypes/wood/foodcupboard.json` |

## Native guide resolution

Every draft `guides[].code` except the dynamically registered Woodworking overview exists exactly in the supplied `native-registry.json`. Toolsmith uses **`craftinginfo-toolsmithcraftingmethods`**, **`craftinginfo-toolsmithdurabilityandwear`**, **`craftinginfo-toolsmithreforging`**; do not rename these or prefix them with `toolsmith:`. Bricklayers page codes do include `bricklayers:`.

Woodworking's exact page is **`craftinginfo-woodworking`**, not a guessed per-machine page. It is absent from the static registry because it is dynamically inserted:

- Existing source `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/iw-source/src/Handbook/WoodworkingHandbookInfo.OwnPages.cs`, line 53: `const string GuidePageCode = "craftinginfo-woodworking"`; following code registers/composes the overview and substitutes config-dependent fragments.
- Installed `immersivewoodworking_1.3.13.zip!ImmersiveWoodworking.dll` contains that exact UTF-16 code string (read as bytes, never loaded).
- Installed English sawhorse/pitsaw text directly links `handbook://craftinginfo-woodworking`.

No imaginary ore-crushing, storage or furniture native guide codes were added. Final JSON/schema, word limits, evidence paths and guide references were checked with Python; this remains a researched draft, not an in-client visual test.
