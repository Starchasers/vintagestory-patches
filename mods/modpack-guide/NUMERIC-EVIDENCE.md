# Toolsmith table content: installed-option and numeric evidence

## Scope and source identity

This is a static audit of the enabled prizC pack, not a live client registry dump.
Only JSON and statically decompiled code were read; the Toolsmith DLL was not
loaded as executable code. `table_inventory.json` is the complete machine-readable
option ledger used by `progression_content.py`. Every mapped option retains its
mapping record/source, stat record/source, exact resolved collectible and source.
Unresolved candidates are retained as exclusions rather than presented as items.

- Pack: `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted`.
- Exact archive: `mods/toolsmith_1.2.19.zip`.
- Archive SHA-256: `325ead64a9192ea7426d017e1c5141286f860f95c3af5dd34b79ad14f060974b`.
- Active settings: `config/ModConfig/Toolsmith.json` and
  `config/ModConfig/ToolsmithPartsStats.json` (hashes recorded in the inventory).
- `ToolsmithPartsStats.json: EnableEdits=false`: the JSON stat assets, not its
  user-editable stat snapshot, are authoritative.
- Base game collectibles: installed Vintage Story 1.22.7 assets under
  `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs`.
- Enabled archives are selected by the pack's `evpack.json`; inactive compatibility
  subtrees are not treated as installed. Neither `geoaddons` nor `mngeology` is
  enabled. Toolsmith's meteoricsteel/uraniumexpanded binding mappings are excluded.

## Coverage and grouping contract

| Category | Resolved mapping options | Authored material/product rows |
|---|---:|---:|
| handles | 5 | 5 |
| grips | 84 | 9 |
| bindings | 57 | 26 |
| treatments | 7 | 7 |

There are 26 native maintenance/workshop collectible variants: five whetstones,
sixteen grindstones (four stones × four directions), four workbench orientations
and sandpaper. They appear as four equipment/consumable rows plus a distinct
native reforging action row. Sandpaper has the native maintenance tag but is used
to craft carpented handles, not to sharpen metal or repair HP.
The no-grip, no-binding and untreated defaults are explicit additional rows.

Every resolved mapped id belongs to exactly one row in its category. Distinct
materials/acquisition routes stay separate even when their numbers match: e.g.
rope versus dry sinew; vanilla flax twine versus dyed Tailor's Delight twine;
normal Toolsmith leather strips versus Tailor's Delight strips; fat versus
rendered fat; all four installed oil inputs; metal parts versus cupronickel nails.
Only cosmetic variants of the same material/product share a row. Linen sheet
patterns, colours and equal-function maintenance stone appearances are grouped,
not claimed to have different mechanical tiers. Rows retain *all* exact codes.

Wool fabric strips require the active Wool compatibility patch to append the
`wool` item variant to `assets/toolsmith/itemtypes/fabricstrips.json`. This patch
was applied before resolution; the inventory records both active patch sources.
Wool twine and Wool cloth each have seventeen installed variants: plain, mordant,
eleven standard colours and four dark shades. Raw Wool has sixteen (no mordant).
Tailor's Delight adds
four grip-eligible dark cloth codes: darkblue, darkbrown, darkgreen and darkred.
No extra expanded-food olive-oil mapping is invented; only the installed flax
and seed food-oil mappings are included.

Unresolved mappings (not installed options):
- `bindings/fabricstrips-atleather`.
- `bindings/cordage`.
- `bindings/cord-bamboo`.
- `bindings/cord-bark`.
- `bindings/cord-coconut`.
- `bindings/cord-flax`.
- `bindings/cord-mixed`.
- `bindings/cord-nzflax`.
- `bindings/cord-palm`.
- `bindings/cord-papyrus`.
- `bindings/cord-porcupine`.
- `bindings/cord-reed`.
- `bindings/cord-sinew`.
- `bindings/cord-straw`.
- `bindings/cord-vine`.
- `bindings/cord-larch`.
- `bindings/cord-tuja`.
- `bindings/cord-honeylocust`.
- `bindings/cord-birch`.
- `bindings/cord-brideinwhite`.
- `bindings/cord-alder`.
- `bindings/cord-bearnut`.
- `bindings/cord-willow`.
- `bindings/cord-redwillow`.
- `bindings/cord-poplar`.
- `bindings/cord-aspen`.
- `bindings/cord-eucalyptus`.
- `bindings/cord-kapok`.
- `bindings/cord-sapele`.
- `grips/leather-normal-acacia`.
- `grips/leather-normal-aged`.
- `grips/leather-normal-baldcypress`.
- `grips/leather-normal-birch`.
- `grips/leather-normal-cypress`.
- `grips/leather-normal-ebony`.
- `grips/leather-normal-kapok`.
- `grips/leather-normal-larch`.
- `grips/leather-normal-maple`.
- `grips/leather-normal-pine`.
- `grips/leather-normal-purpleheart`.
- `grips/leather-normal-redwood`.
- `grips/leather-normal-walnut`.
- `grips/buckskin`.
- `treatments/plantoil-flax`.
- `treatments/plantoil-walnut`.
- `treatments/woodfinish-flax`.
- `treatments/woodfinish-walnut`.

The inventory also records all active Toolsmith-targeted patches and inactive
mapping sources, so equal numeric groups are inspectable rather than assumed.
Other enabled mods contribute empty Toolsmith regex overrides (Combat Overhaul,
Armory) and an Immersive Woodworking tool-regex addition; none supplies an extra
handle/grip/binding/treatment material mapping. Material mappings from Wool,
Tailor's Delight, Butchering, Wildcraft Herbs, Ancient Tools and Expanded Foods
are resolved against the enabled archive definitions, not merely their names.

## Handle species are not a stat dimension

`assets/toolsmith/config/toolsmith/parts/handles/handles-vanilla.json` assigns
one stat tag per handle collectible: stick, bone, crude, handle, professional.
`CollectibleBehaviorTinkeredTools.OnCreatedByCrafting` selects that tag from
`Collectible.Code.Path`, never from the wood texture attribute. Thus the same
handle type has the same stats across its cosmetic wood appearances.

The native `handle` and `carpentedhandle` JSON texture dimensions contain exactly
13 values: aged, birch, oak, maple, pine, acacia, kapok, baldcypress, larch,
redwood, ebony, walnut and purpleheart. Aged is a native texture choice, not a
botanical species. `crudehandle` has no wood texture dimension: do not manufacture
13 species-specific crude-handle variants. The exact dimension records and
source paths are retained under `nativeFamilies` in the inventory.

The existing `attribute_inventory.json`/native family converter is reused, not
replaced. Its decompile manifest archive SHA matches the exact archive above.
`toolsmithfamily` emits attributed stacks and opens native recipes using
`GuiHandbookItemStackPage.PageCodeForStack(stack)`. No fabricated attribute suffix,
synthetic page id or hypothetical collectible suffix is authored. Ordinary item
and block icons use the own `toolsmithitem` converter and the same native page-code
function; it omits the `game` domain and retains custom domains. Hand-authored
`item-game:code` links are invalid and are not generated.

## HP and speed formulas: sequential multipliers, not added percentages

Let B be the tool collectible's base maximum durability before Toolsmith's
head multiplier; h/b/t are the chosen handle/binding/treatment stat records.
`CollectibleBehaviorTinkeredTools.OnCreatedByCrafting`, retained in
`evidence/active-code/Toolsmith.ToolTinkering.Behaviors.CollectibleBehaviorTinkeredTools.decompiled.cs`,
performs these operations in order (lines 258–266, 295–302):

```
headMaxHP    = int(B * HeadDurabilityMult)
maxSharpness = int(B * SharpnessMult)
handleMaxHP  = int(B * h.baseHPfactor * (1 + h.selfHPBonus)
                   * (1 + t.handleHPbonus) * (1 + b.handleHPBonus))
bindingMaxHP = int(B * b.baseHPfactor * (1 + b.selfHPBonus)
                   * (1 + h.bindingHPBonus))
speedBonus   = h.speedBonus + grip.speedBonus
handleDamageChance = grip.chanceToDamage
```

Actual runtime operations use floats and truncate the final HP, so an algebraic
factor is not a promise of a fractional in-game HP bar. Tables give **own HP
before external support**, explicitly state base factor and own bonus, and explain
which different part receives support. There is no treatment multiplier on the
binding and no grip HP multiplier on either part.

### Handles

| Handle | Base × B | Own HP bonus | Own HP × B | Binding support | Speed |
|---|---:|---:|---:|---:|---:|
| Stick | 1 | 0% | 1 | 0% | 0% |
| Bone | 1 | 5% | 1.05 | 5% | 0% |
| Crude handle | 1 | 0% | 1 | 5% | 0% |
| Carved handle | 1.2 | 5% | 1.26 | 20% | 5% |
| Carpented handle | 1.5 | 10% | 1.65 | 40% | 10% |

### Bindings

Recovery is a *remaining HP fraction threshold*, not a random drop probability.
The breakage branch in `TinkeringUtility.HandleBrokenTinkeredTool` returns a
binding at `remaining / maximum >= recoveryPercent`; below threshold, metal
bindings yield `Rand.Next(2,4)` matching bits (2 or 3), nonmetal bindings do not
return. Deliberate disassembly uses a separate stricter branch: only a pristine
binding returns intact, and a worn metal one yields matching bits.

`ToolsmithModSystem.CalculateBindingTiers` derives tier from the own HP factor:
≤1 => 0; >1 and <1.75 => 1; ≥1.75 and <2.5 => 2; ≥2.5 and <3 => 3; ≥3 => 4.
The tier is not the head's mining/metal tier.

| Binding | Base × B | Own bonus | Own HP × B | Handle support | Recovery HP | Tier |
|---|---:|---:|---:|---:|---:|---:|
| Dry grass | 1 | 0% | 1 | 0% | 100% | 0 |
| Cattail tops | 1 | 0% | 1 | 0% | 100% | 0 |
| Papyrus tops | 1 | 0% | 1 | 0% | 100% | 0 |
| Flax twine | 1.2 | 10% | 1.32 | 5% | 90% | 1 |
| Tailor's Delight twine | 1.2 | 10% | 1.32 | 5% | 90% | 1 |
| Wool twine | 1.2 | 10% | 1.32 | 5% | 90% | 1 |
| Rawhide strips | 1.25 | 15% | 1.4375 | 5% | 80% | 1 |
| Rope | 1.25 | 15% | 1.4375 | 5% | 70% | 1 |
| Dry sinew | 1.25 | 15% | 1.4375 | 5% | 70% | 1 |
| Cloth strips | 1.3 | 20% | 1.56 | 10% | 75% | 1 |
| Wool strips | 1.3 | 20% | 1.56 | 10% | 75% | 1 |
| Leather strips | 1.5 | 30% | 1.95 | 10% | 60% | 2 |
| Tailor's Delight leather strips | 1.5 | 30% | 1.95 | 10% | 60% | 2 |
| Sturdy leather strips | 1.8 | 30% | 2.34 | 25% | 55% | 2 |
| Hot pitch glue | 2 | 30% | 2.6 | 30% | 100% | 3 |
| Cold sinew glue | 2 | 30% | 2.6 | 30% | 100% | 3 |
| Hot hide glue | 2 | 30% | 2.6 | 30% | 100% | 3 |
| Copper nails and strips | 1.4 | 10% | 1.54 | 10% | 90% | 1 |
| Tin bronze nails and strips | 1.7 | 20% | 2.04 | 20% | 50% | 2 |
| Bismuth bronze nails and strips | 1.7 | 25% | 2.125 | 20% | 50% | 2 |
| Black bronze nails and strips | 1.7 | 30% | 2.21 | 20% | 50% | 2 |
| Iron nails and strips | 1.8 | 30% | 2.34 | 20% | 45% | 2 |
| Cupronickel nails and strips | 1.7 | 20% | 2.04 | 20% | 50% | 2 |
| Meteoric iron nails and strips | 1.9 | 50% | 2.85 | 25% | 45% | 3 |
| Steel nails and strips | 2.2 | 60% | 3.52 | 40% | 35% | 4 |
| Metal parts | 1.7 | 20% | 2.04 | 20% | 50% | 2 |

Examples calculated with decimal arithmetic:
- Carved handle: `1.2 * 1.05 = 1.26 × B`.
- Carpented handle: `1.5 * 1.10 = 1.65 × B`.
- Leather binding with carved-handle support: `1.5 * 1.30 * 1.20 = 2.34 × B`.
- Oil-treated carved handle before binding support: `1.26 * 1.65 = 2.079 × B`.

### Grips

No grip: +0% speed and 100% handle damage chance when eligible. A lower damage
chance is better, but **80% chance does not mean 80% damage reduction**. This
chance only controls eligible handle HP loss, not head or binding wear.

| Grip family | Speed bonus | Eligible handle damage chance |
|---|---:|---:|
| Flax twine | 0% | 95% |
| Wool twine | 0% | 95% |
| Linen sheets | 10% | 90% |
| Linen cloth | 10% | 90% |
| Tailor's Delight dark cloth | 10% | 90% |
| Wool cloth | 10% | 90% |
| Wool | 10% | 90% |
| Normal leather | 20% | 80% |
| Sturdy leather | 30% | 65% |

### Treatments

Amounts, solid/liquid selection and drying duration come from each exact
`treatments/*` part mapping; handle HP bonus comes from the corresponding stat
record. Oils use the generated liquid-container recipe path, not a collectible
portion placed alone in an inventory slot.

| Input | Handle HP bonus | Carved / carpented drying (game hours) | Recipe amount |
|---|---:|---:|---|
| Fat | 20% | 12 / 24 | 1 solid |
| Rendered fat | 20% | 12 / 24 | 1 solid |
| Beeswax | 50% | 12 / 24 | 1 solid |
| Flax oil | 65% | 24 / 48 | 0.1 L |
| Olive oil | 65% | 24 / 48 | 0.1 L |
| Expanded Foods flax oil | 65% | 24 / 48 | 0.1 L |
| Expanded Foods seed oil | 65% | 24 / 48 | 0.1 L |

`RecipeRegisterModSystem.RegisterHandleCustomisationRecipes` creates native
recipes only for handle `canTakeGrip` / `canTakeTreatment` mappings. Stick accepts
neither. Bone and crude handles accept a grip but no treatment. Carved and
carpented handles accept both. The eligibility is in the parts mappings, not
inferred from a material label. `CollectibleBehaviorToolHandle.OnCreatedByCrafting`
doubles mapping drying hours for the professional stat tag. It rejects treatment
of an already gripped handle: treat first, then grip. Treatment and grip each
occupy one handle attribute slot; ingredients do not stack their bonuses.
Drying must complete before assembly. This corrects the preliminary audit's
bone-grip, crude-treatment and professional-drying claims.

## Maintenance: installed options, damage eligibility and rounding

Exact definitions: `assets/toolsmith/itemtypes/whetstone.json`,
`assets/toolsmith/blocktypes/grindstone.json`, `assets/toolsmith/blocktypes/workbench.json`.
The inventory expands every native variant and lists its source.

- Whetstones: chert, granite, andesite, basalt, peridotite; **300 durability each**.
- Grindstones: granite, sandstone, shale, andesite; north/east/south/west each.
- Workbench: north/east/south/west. Its direction is not a mechanical upgrade.
- Sandpaper: `assets/toolsmith/itemtypes/sandpaper.json`; one consumable plus a
  knife and support beam makes a carpented handle. Crude handles instead use a
  knife and firewood, not a log.
- All whetstone/grindstone stone variants use the same sharpening code. No
  unsupported extra geology stones or invented stone-efficiency tiers are shown.

Active config values are `HeadDurabilityMult=5`, `SharpnessMult=1.5`,
`GrindstoneSharpenPerTick=1`, `ShouldHoningDamageHead=true`,
`HoningDamageMult=1`, `SharpWear=0.15`, `BluntWear=0.02`,
`HandleBreakSurvivalThreshold=0.05`, `StickBreakSurvivalThreshold=0.25`.

Statically decompiled `TinkeringUtility.ActualSharpenTick` is shared by
`ItemWhetstone.OnSharpenTick` and the grindstone path. It computes
`ceil(0.01 * maxSharpness)` restored sharpness each tick. The whetstone separately
loses 1 durability per tick. First honing is exempt from head-HP damage;
subsequent honing can reduce head HP. Neither is a head-HP repair operation.
Fresh metal heads can need honing before convenient normal use.

`CollectibleBehaviorTinkeredTools.OnItemDamage` uses the *pre-damage sharpness
fraction*: ≥98% avoids ordinary head/handle wear, with one binding nick if the
binding was pristine; below 98% enables head wear and binding wear; below 95%
also enables handle wear. The grip roll is once per eligible damage unit. At a
fully blunt edge extra head damage occurs. SharpWear/BluntWear are branch-specific
rolls, not a universal percentage of every tool action. `GetMiningSpeed` uses
≥90% => +5% sharpness speed, ≤33% => −10%, and applies the combined handle/grip
speed contribution after that. Displayed integer percentages can obscure those
float thresholds; pages avoid claiming exact elapsed actions from HP bars.

`TinkeringUtility.HandleBrokenTinkeredTool` requires **strictly more than** 25%
remaining handle HP for stick/bone, or more than 5% for crafted handles, to return
the handle. These are survival thresholds, not random return probabilities.

For reproducible deeper code inspection, the existing static decompiler is
`/home/brunette/.hermes/cache/scratch/woodworking-tutorial/tools/ilspycmd`, with
DOTNET_ROOT pointing at the adjacent `dotnet` directory. The retained
`evidence/attributes/Toolsmith.dll` was byte-compared with the archive DLL and
matched before static inspection. Source ZIPs omit target assemblies but retain
the text evidence, including `TinkeringUtility.cs`, `BlockEntityGrindstone.cs`,
`CollectibleBehaviorToolHandle.cs`, `RecipeRegisterModSystem.cs` and their input
archive identity. No target assembly was executed.

## VTML/native API contract and verification performed

All six old pageCodes are retained. Titles start with literal `Toolsmith -`.
The getting-started entry is short and links all five detail pages plus native
crafting/wear/reforging pages. Detailed options live on the relevant pages.

Tables contain only `toolsmithrow` children; every row has exactly three
`toolsmithcell` children. Header cells use ordinary `<strong>`. Cells contain
supported richtext, the clickable `toolsmithitem` plain-reference renderer or the
existing `toolsmithfamily` attributed renderer. There are no HTML table tags, fake
space-aligned columns, fabricated item ids, or replacement family converter.

Executed checks: import and render `pages()`; six exact pageCodes/title prefix;
parse every page as XML/VTML; verify every table row has exactly three cells;
verify no duplicate explicit href per page; assert every native-family code
belongs to the existing attributed inventory; assert every ordinary itemstack
code belongs to a resolved installed option; assert each resolved mapping is
covered exactly once and grouped rows have identical stat and noncosmetic
mapping records. Header/default-inclusive table row counts: handles 6, grips 11,
bindings 28, treatments 9, maintenance 6. Entry has no table and three unique
starter item references. See `test-results-1.3.0.txt` for executed native API,
registry, callback and all-page geometry checks, and `README.md` for commands.
The entry remains bounded to 330 words; complete tables instead bound each cell
and test its native wrapping geometry. This is not a real-client GUI rendering
claim; the graphical smoke-test boundary is explicit in the README.
