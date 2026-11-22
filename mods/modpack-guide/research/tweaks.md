# Tweaks audit: existing-guide corrections only

**Result:** 90 original world classifications rechecked; 0 new pages; 18 corrections tied to literal existing step labels. No shared implementation was changed.

## High-priority findings

- **yangcoifix is the liquid-container/entity interaction fix**, not an armor fix. This belongs in the existing animal/milk instructions. Its metadata description is readable when metadata key casing is handled correctly.
- **DanaTweaks config consumption is proven:** `Core.StartPre` → `ModConfig.ReadConfig<ConfigServer>` → `LoadModConfig` → constructor retaining prior fields. Enabled oven/firepit, plank/peat fuel, door-opening, rain collection, tree-seed and resin conveniences are real export rules when that config is installed on the server. `FourPlanksFromLog` and `EverySoilUnstable` are false. No milk/dough setting or `hardcodedConfig` override is evidenced in this supplied build.
- **ConfigureEverything is disabled module-by-module.** Its examples do not configure milk, dough, ovens, farmland or nutrition. All sixteen exported modules, including client map colors, have `Enabled=false`.
- **Real Smoke is a survival hazard:** exported enabled asphyxiation and static air-removal behavior require ventilation prose before indoor fire/oven instructions.
- **CBR patches side solidity, not light absorption:** all four chiseled/microblock definitions remove the all-side wildcard and set five faces true, up false. Room, cellar and smoke guarantees do not follow.
- **Core of Arts is not a pure library:** enabled liquid mixing and its native `artsguides-liquidmixing` page are player-facing. Manual Dough removes grid outputs with `code.Path` containing `dough`, but does not remove non-grid kneading recipes.
- **Milk branches differ:** fresh `game:milkportion` seals 24 h, 10 L → 10 L `dairyplus:separatingmilk`; EF separately cooks vanilla milk to `expandedfoods:pasteurizedmilkportion`. Its 225 satiety patch is **meal nutrition per litre**, not raw drink nutrition. Dairy Plus adds whey/soy/skim milk specifically to EF kneading recipes.
- **Temporal Symphony has entity AI patches**, including enabled Bellhead `bellalarm` spawning; it is not only storm sound/light. Exact effective riftweather settings are not established from the old exported YAML alone.
- Empty descriptions genuinely remain for Combat Overhaul, CommonLib, Overhaullib, OverhaullibLegacyCompat and PlayerModelLib. Those were decompiled rather than classified from blank metadata. Legacy compatibility includes actual quenching integration; PlayerModelLib contains inventory/light/stat infrastructure, so graphics-only would be inaccurate.

## Configuration authority / actual blockers

- No hardcodedConfig symbol/file occurs in the supplied DanaTweaks 4.1.0 static export, archive entry names, pack JSON text or scanned DLL string encodings. The inspected architecture is ConfigServer + ModConfig.ReadConfig, not an evidenced hardcodedConfig override. A different source/version containing that identifier is required to audit that alleged override; do not manufacture it.
- No separately installed mod named Config is in the supplied evpack Mods. If Config patches means another private patch bundle beyond these packaged native/ConfigureEverything/ConfigLib patches, it was not provided.
- OverhaullibLegacyCompat whole-assembly export failed in ILSpy with a NullReferenceException; targeted gameplay/quench types exported successfully. Exact player quenching controls and all species-specific effects still require their owning behavior/recipe or a runtime test.
- No live server/world was executed. Chiseled enclosure retention, smoke ventilation geometry and custom oven compatibility cannot be claimed runtime-verified.
- Temporal Symphony old exported YAML loader correspondence has not been fully traced; native enabled Bellhead AI patch independently proves non-cosmetic gameplay, but exact effective riftweather settings remain unverified.

The eViSTool export is **Kind=Client**, not a server dump. Read paths prove how settings are consumed when installed on an authoritative server, not the current multiplayer server state. No mod DLL was executed. ILSpy was used for static decompilation only.

## Exact editorial changes

### indoor-fire-smoke [major]

**Existing page:** `kuboczochmodpackguide-process-kitchen-firstfire`
**Existing step(s):** Build the firepit / Upgrade to pot meals

Add an explicit ventilation warning before placing or lighting indoor fire sources: Real Smoke is enabled with asphyxiation, so vent smoke to open air or work outdoors. Do not imply that any enclosed shelter is safe for a firepit; do not prescribe a guaranteed-safe chimney geometry from MaxChimneyHeight alone.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/realsmoke_1.3.1.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/RealSmoke.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/RealSmoke.cs::RealSmokeConfig / EntityBehaviorRealSmoke.OnGameTick / smoke traversal`

### oven-dana-heating [major]

**Existing page:** `kuboczochmodpackguide-process-kitchen-dough-oven`
**Existing step(s):** Connect the three oven components / Fuel and bake

Keep the three-component Stone Bake Oven route unchanged. Add a distinct vanilla-compatible oven note: DanaTweaks can heat a BlockEntityOven directly above a burning non-construct firepit, and enabled oven fuel definitions include planks and peatbrick. Do not merge this vertical oven/firepit arrangement with Stone Bake Oven's side/front three-component layout or promise compatibility for custom oven entities.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/DanaTweaks-Server.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/DanaTweaks.cs::Core.StartPre / ConfigServer constructors / ModConfig.ReadConfig / HarmonyPatches / BlockEntityFirepit_OnBurnTick_Patch / Core.Start / BlockPatches / ItemPatches / PitkilnPatches / EntityBehaviorAutoPlantDroppedTreeSeeds`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::assets/danatweaks/patches/extraclayforming.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::assets/danatweaks/patches/wax-cheese-on-ground.json`

### oven-smoke [major]

**Existing page:** `kuboczochmodpackguide-process-kitchen-dough-oven`
**Existing step(s):** Fuel and bake / Try an oven pot roast

Add smoke/ventilation precautions for indoor firepits/heat sources, qualifying custom sources by their smoke integration. Do not describe a chimney solely as a decorative block stack.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/realsmoke_1.3.1.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/RealSmoke.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/RealSmoke.cs::RealSmokeConfig / EntityBehaviorRealSmoke.OnGameTick / smoke traversal`

### shelter-drfter-door [major]

**Existing page:** `world-survival`
**Existing step(s):** 2. Check the combat settings / 3. Retreat with a recovery plan

Add that this export enables DanaTweaks drifter door opening. Auto-closing doors are convenient but do not make the recovery shelter drifter-proof. Keep escape/recovery supplies away from an assumed secure openable entrance.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/DanaTweaks-Server.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/DanaTweaks.cs::Core.StartPre / ConfigServer constructors / ModConfig.ReadConfig / HarmonyPatches / BlockEntityFirepit_OnBurnTick_Patch / Core.Start / BlockPatches / ItemPatches / PitkilnPatches / EntityBehaviorAutoPlantDroppedTreeSeeds`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::assets/danatweaks/patches/extraclayforming.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::assets/danatweaks/patches/wax-cheese-on-ground.json`

### shelter-chiseling [major]

**Existing page:** `process-building-materials`
**Existing step(s):** 4. Obtain stone and shape details

Where discussing enclosure or chiseled walls, state the actual CBR patch: all four chiseled/microblock definitions have five sideSolid faces true, but up=false; the all wildcard is removed. It does not patch lightAbsorption. Do not infer airtightness, thermal retention or smoke safety from this name or from visual darkness.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/ChiseledBlockRetention-2.0.1.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/ChiseledBlockRetention-2.0.1.zip::assets/cbr/patches/survival-blocktypes-chiseled-chiseledblock-snowcovered.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/ChiseledBlockRetention-2.0.1.zip::assets/cbr/patches/survival-blocktypes-chiseled-chiseledblock.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/ChiseledBlockRetention-2.0.1.zip::assets/cbr/patches/survival-blocktypes-chiseled-microblock-snowcovered.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/ChiseledBlockRetention-2.0.1.zip::assets/cbr/patches/survival-blocktypes-chiseled-microblock.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/Immersive Light_0.2.7.zip::modinfo.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/Immersive Light.cs::ChunkIlluminator / door and trapdoor patches`

### farming-config-not-examples [major]

**Existing page:** `process-farming-animal-care`
**Existing step(s):** Prepare a field

Retain temperature/nutrient/rotation advice. Add no ConfigureEverything crop/fertility numbers: both modules are explicitly disabled, and their examples are not active overrides. DanaTweaks EverySoilUnstable=false also must not be presented as enabled universal soil collapse.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/ConfigureEverything-v3.0.0.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/ConfigureEverything`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/ConfigureEverything.cs::ConfigureEverythingSystem.AssetsFinalize`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/DanaTweaks-Server.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/DanaTweaks.cs::Core.StartPre / ConfigServer constructors / ModConfig.ReadConfig / HarmonyPatches / BlockEntityFirepit_OnBurnTick_Patch / Core.Start / BlockPatches / ItemPatches / PitkilnPatches / EntityBehaviorAutoPlantDroppedTreeSeeds`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::assets/danatweaks/patches/extraclayforming.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::assets/danatweaks/patches/wax-cheese-on-ground.json`

### farming-collection-not-irrigation [major]

**Existing page:** `process-farming-animal-care`
**Existing step(s):** Prepare a field

If adding DanaTweaks rain collection, describe it only as optional collection into compatible containers exposed to sufficient rain. It neither waters every crop automatically nor guarantees 0.01 L/s under all weather; keep ordinary field-water requirements.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/DanaTweaks-Server.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/DanaTweaks.cs::Core.StartPre / ConfigServer constructors / ModConfig.ReadConfig / HarmonyPatches / BlockEntityFirepit_OnBurnTick_Patch / Core.Start / BlockPatches / ItemPatches / PitkilnPatches / EntityBehaviorAutoPlantDroppedTreeSeeds`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::assets/danatweaks/patches/extraclayforming.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::assets/danatweaks/patches/wax-cheese-on-ground.json`

### forestry-seed-resin [contextual]

**Existing page:** `process-wood-to-workshop`
**Existing step(s):** Split your fuel

Mention enabled DanaTweaks self-planting dropped tree seeds after a minimum delay and resin regrowth as optional maintenance conveniences, not guaranteed instant planting or unlimited immediate resin. FourPlanksFromLog=false: do not replace the existing actual woodworking/log yields with a four-plank shortcut.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/DanaTweaks-Server.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/DanaTweaks.cs::Core.StartPre / ConfigServer constructors / ModConfig.ReadConfig / HarmonyPatches / BlockEntityFirepit_OnBurnTick_Patch / Core.Start / BlockPatches / ItemPatches / PitkilnPatches / EntityBehaviorAutoPlantDroppedTreeSeeds`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::assets/danatweaks/patches/extraclayforming.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DanaTweaks-v4.1.0.zip::assets/danatweaks/patches/wax-cheese-on-ground.json`

### dough-grid-scope [major]

**Existing page:** `kuboczochmodpackguide-process-kitchen-dough-oven`
**Existing step(s):** Knead dough

Keep Manual Dough's water-target interaction, but specify its real removal scope: when DisableDoughRecipes=true, it removes every GridRecipe whose resolved output code.Path contains "dough", irrespective of namespace. This does not remove non-grid Expanded Foods kneading recipes. Core of Arts enables native liquid mixing and its separate patch relaxes vanilla dough flour allowedVariants; cross-link artsguides-liquidmixing as an ingredient-specific branch, not a promise that every flour/liquid works in Manual Dough.

**Sources:**
- `/home/brunette/.hermes/cache/scratch/tweaks-static/ManualDough.cs::StartServerSide / RemoveDoughRecipes / LoadConfig`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/ManualDoughConfig.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CoreOfArts_1.2.2.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/Arts/CoreOfArts.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/CoreOfArts.cs::CoreOfArts.Start / COAInLiquidMixing`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CoreOfArts_1.2.2.zip::assets/coreofart/config/handbook/01-liquidmixing.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CoreOfArts_1.2.2.zip::assets/coreofart/patches/doughgrid.json`

### milk-separate-paths [major]

**Existing page:** `kuboczochmodpackguide-process-kitchen-dairy`
**Existing step(s):** Let the milk separate

Add that sealing fresh milk for Dairy Plus separation is one route, not pasteurization. Expanded Foods separately patches vanilla milk combustion to produce expandedfoods:pasteurizedmilkportion at meltingPoint 100, meltingDuration 5 with requiresContainer=true. Do not call that output a Dairy Plus item, assume all milk variants have it, or confuse the configured smelting value with a wall-clock guarantee. Keep the verified 10 L/24 h separating-milk barrel route distinct.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/ExpandedFoods_2.0.0-dev.17.zip::assets/expandedfoods/patches/milk.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DairyPlus_2.0.0.zip::assets/dairyplus/itemtypes/liquid/seperatingmilk.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DairyPlus_2.0.0.zip::assets/dairyplus/recipes/barrel/settling.json`

### milk-dough-ingredient-links [major]

**Existing page:** `kuboczochmodpackguide-process-kitchen-dairy`
**Existing step(s):** Skim carefully

Add an actual downstream use for skim milk: Dairy Plus explicitly extends Expanded Foods kneading/dough inputs with dairyplus:whey, dairyplus:soymilk and dairyplus:skimmilk under dependsOn expandedfoods. This is that recipe branch, not proof the Manual Dough water-target interaction accepts any milk.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/DairyPlus_2.0.0.zip::assets/dairyplus/patches/expandedfoods/kneadingdough.json`

### milk-config-scope [major]

**Existing page:** `kuboczochmodpackguide-process-kitchen-dairy`
**Existing step(s):** Let the milk separate

Do not attribute milk nutrition, freshness or processing changes to ConfigureEverything: its relevant modules are disabled. The Expanded Foods milkportion patch sets nutritionPropsPerLitreWhenInMeal satiety=225 and foodcategory=Dairy; that is meal nutrition, not raw drinking nutrition, bulk yield or milk-portion volume.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/ExpandedFoods_2.0.0-dev.17.zip::assets/game/patches/milkportion.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/ConfigureEverything-v3.0.0.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/ConfigureEverything`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/ConfigureEverything.cs::ConfigureEverythingSystem.AssetsFinalize`

### genetics-disabled-welfare-scope [major]

**Existing page:** `process-farming-animal-care`
**Existing step(s):** Do not impose disabled breeding rules / Return nutrients

Retain the warning that Livestock Welfare Breeding.Enabled=false is not all breeding disabled. Make GeneLib's genetics/multiply and Natural Fertilizer GeneLib compatibility explicit where discussing species breeding and manure; do not universalize the 18–28 h compatibility delay or inbreeding setting across animals lacking those behaviors.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/genelib_3.2.1.zip::modinfo.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/Genelib.cs::registration / GeneticMultiply / EntityBehaviorGenetics`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/genelib.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/naturalfertilizer-server.json`

### legacy-equipment-not-vanilla [major]

**Existing page:** `process-ore-to-equipment`
**Existing step(s):** Cool, maintain, then progress

Do not dismiss OverhaullibLegacyCompat as infrastructure. Add an item-specific armor-quenching/compatibility cross-reference after equipment finishing; keep Manual Quenching requirements tied to recipes and quenchable attributes instead of claiming every armor piece can be quenched. The available static types prove integration, not a tested universal armor recipe.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/overhaulliblegacycompat_1.2.15.zip::modinfo.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/CombatOverhaul.CombatOverhaulSystem.cs`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/CombatOverhaul.Integration.ArmorQuenchComponents.cs`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/CombatOverhaul.Integration.QuenchablePatchGate.cs`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/CombatOverhaul.Integration.ArmorQuenchShatterChancePatch.cs`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/overhaulliblegacy-armorquenching.json`

### temporal-enemy-caveat [major]

**Existing page:** `world-survival`
**Existing step(s):** 2. Check the combat settings

Add that Temporal Symphony is not merely ambience: its native Bellhead Shiver patch adds bellalarm AI with mob spawning. Do not promise vanilla enemy behavior, infer lightning damage, or quote exact riftweather effects from an exported YAML whose current loader has not been traced.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/entity/bellheadmodifier.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/bellhead.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/corrupt.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/deep.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/deepsplit.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/nightmare.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/stilt.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/surface.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/tainted.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/thunderlord/animations.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/entity/bellrework.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/entity/sound-bell.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/entity/sound-bowtorn.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/entity/sound-drifter.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/entity/sound-shiver.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/riftweather/survival-config-riftweather-chance.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/riftweather/survival-config-riftweather-durationHours.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/riftweather/survival-config-riftweather-mobSpawnMul.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/riftweather/survival-config-riftweather-startTotalHours.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/shiver/bellhead.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/shiver/corrupt.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/shiver/deep.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/shiver/nightmare.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/shiver/stilt.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/shiver/surface.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/shiver/tainted.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TemporalSymphony_2.3.2.zip::assets/temporalsymphony/patches/shapes/thunderlord.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/TemporalSymphony.cs`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/temporalsymphony.yaml`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/TemporalSymphonyServerConfig.json`

### milking-interaction-order [major]

**Existing page:** `process-farming-animal-care`
**Existing step(s):** Maintain living conditions

Add a practical interaction-order note: with a compatible liquid container, target the milkable animal entity directly rather than an adjacent block. yangcoifix lets that entity's interaction run before drink handling, but it does not grant milk from an ineligible animal, change welfare requirements, or establish a new modifier key. Cross-link the dairy processing page for what to do with obtained milk.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/yangcoifix.zip::modinfo.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/yangcoifix.cs::YangCOIFixSystem.ShouldLoad / OnInWorldAction / TryInteractEntityFirst / SendEntityInteraction`
- `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/VintagestoryAPI.dll::Vintagestory.API.Common.EnumEntityAction`

### transport-native-branches [editorial]

**Existing page:** `world-overland`
**Existing step(s):** 2. Choose a forgiving route / 4. Make repeat journeys easier

Add a bounded native-reference branch: Cartwrights Caravan has cart/sled construction and wheel/grip/storage recipes; Destrier and Ferus have species-specific saddlery; spyglass requires its smithing tube and grid assembly. Do not represent all five as missing metadata or conflate Carry On portability with vehicle construction; keep unverified mounting/hitching controls out.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/entities/nonliving/cart.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/entities/nonliving/marketstall.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/entities/nonliving/sled.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/cartcanopies.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/cartflute.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/cartgrips.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/cartgripsprimitive.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/cartsideboards.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/cartsidetents.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/constructionbasiccart.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/constructionmarketstall.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/constructionsled.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/constructionslimcart.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/conversion.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/schematics.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/scrapedye.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/signconversion.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/signs.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/storage.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/CartwrightsCaravan_1.9.1.zip::assets/cartwrightscaravan/recipes/grid/wheels.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/spyglass_0.6.1.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/spyglass_0.6.1.zip::assets/spyglass/recipes/smithing/spytube.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/spyglass_0.6.1.zip::assets/spyglass/recipes/grid/spyglass.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/spyglass.cs`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/entities/destrier/destrier-unhandled.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/entities/destrier/destrier.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/entities/destrier/hitchingpost.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/entities/destrier/traderdestrier.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/recipes/barrel/wearables.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/recipes/grid/armor.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/recipes/grid/bags.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/recipes/grid/blanket.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/recipes/grid/hitchingpost.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/recipes/grid/saddle.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/equusdestrier/recipes/grid/scrapedye.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/game/entities/humanoid/trader-female-equusdestrier.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusdestrier_1.1.2.zip::assets/game/entities/humanoid/trader-male-equusdestrier.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/equusdestrier.cs`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusferus_1.0.6.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusferus_1.0.6.zip::assets/equusferus/entities/ferus/ferus-unhandled.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusferus_1.0.6.zip::assets/equusferus/entities/ferus/ferus-wild.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusferus_1.0.6.zip::assets/equusferus/entities/ferus/ferus.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusferus_1.0.6.zip::assets/equusferus/recipes/barrel/wearables.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusferus_1.0.6.zip::assets/equusferus/recipes/grid/bridleandsaddle.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/equusferus_1.0.6.zip::assets/equusferus/recipes/grid/scrapedye.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/equusferus.cs`

### terrain-slab-scope [editorial]

**Existing page:** `process-building-materials`
**Existing step(s):** 4. Obtain stone and shape details

Include Terrain Slabs as a recipe-backed terrain-material branch and link its actual slab recipe rather than claiming it is cosmetic or importing full-block fertility/retention behavior into partial blocks.

**Sources:**
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/evpack.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/terrainslabs_1.0.17.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/terrainslabs_1.0.17.zip::assets/terrainslabs/recipes/grid/cob.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/terrainslabs_1.0.17.zip::assets/terrainslabs/recipes/grid/crackedrock.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/terrainslabs_1.0.17.zip::assets/terrainslabs/recipes/grid/glacierice.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/terrainslabs_1.0.17.zip::assets/terrainslabs/recipes/grid/gravel.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/terrainslabs_1.0.17.zip::assets/terrainslabs/recipes/grid/muddygravel.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/terrainslabs_1.0.17.zip::assets/terrainslabs/recipes/grid/rock.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/terrainslabs_1.0.17.zip::assets/terrainslabs/recipes/grid/sand.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/terrainslabs_1.0.17.zip::assets/terrainslabs/recipes/grid/snowblock.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/terrainslabs_1.0.17.zip::assets/terrainslabs/recipes/grid/soil.json`
- `/home/brunette/.hermes/cache/scratch/tweaks-static/TerrainSlabs.cs`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/terrainslabs-server.json`

## Full classification disposition

The JSON retains every original identifier/classification and source, adds case-insensitive metadata evidence, native inventory counts, explicit `vanillaDelta`, interactions, existing/native guide codes and a narrow per-mod technical exclusion where appropriate. Retained prior coverage is not presented as newly runtime-verified.

| Mod ID | Status | Existing owning process / exclusion |
|---|---|---|
| `walkingstick` | covered-static | Equipable walking aids; crude stick has no speed config, AutoStep true, Hunger -5. |
| `watersheds` | covered-static | World-generation overhaul: route-finding context, not a craft unlock. |
| `rivers` | covered-static | Adds rivers; do not promise boat current transport with Quiet Rough Seas. |
| `terraprety` | covered-static | Natural-terrain world-generation redesign. |
| `kscartographytable` | covered-static | Table-local waypoint exchange; advanced table also exchanges explored map areas. |
| `trailmodmaro` | covered-static | Only players create trails; trail and trampled-soil devolution are configured, not disabled. |
| `roughseas` | covered-static | Weather, vessel damage, repair, swamping and mooring systems. |
| `coins` | covered-static | `world-trade` |
| `coinage` | covered-static | `world-trade` |
| `downed` | covered-static | `world-survival` |
| `bloodtrail` | covered-static | `world-survival` |
| `immersivecorpsedrop` | covered-static | `world-survival` |
| `noticeboard` | covered-static | `world-trade` |
| `quietroughseas` | covered-with-code-verification-limit | Declares quiet logs, axe-only boat hit damage, faster paddled boats, suppression of Rivers pushing boats. |
| `catchledge` | covered-config-no-invented-controls | `world-overland`, `world-water` |
| `chargedjump` | covered-config-no-invented-controls | `world-overland`, `world-water` |
| `climbmountain` | covered-config-no-invented-controls | `world-overland`, `world-water` |
| `crawlanddive` | covered-config-no-invented-controls | `world-overland`, `world-water` |
| `vaultandroll` | covered-config-no-invented-controls | `world-overland`, `world-water` |
| `sprintmomentum` | covered-config-no-invented-controls | `world-overland`, `world-water` |
| `carryon` | covered-config-no-invented-controls | `world-overland`, `world-water` |
| `combatoverhaulfork` | verified-static-combat-and-recipe-integration | `process-ore-to-equipment`, `world-survival` |
| `armoryfork` | covered-config-no-invented-controls | `world-survival` |
| `handtohand` | covered-config-no-invented-controls | `world-trade` |
| `cartwrightscaravan` | verified-native-transport-recipes-controls-limited | `world-overland`, `process-wood-to-workshop`, `process-building-materials` |
| `cartcarryonpatch` | classified-no-standalone-progression | `world-overland`, `world-water` |
| `equusdestrier` | verified-native-equipment-and-genetics-controls-limited | `world-overland`, `process-farming-animal-care` |
| `equusferus` | verified-native-equipment-and-genetics-controls-limited | `world-overland`, `process-farming-animal-care` |
| `bettertraders` | classified-no-standalone-progression | `world-trade` |
| `remotetradercheckmod` | classified-no-standalone-progression | `world-trade` |
| `vinconomy` | classified-no-standalone-progression | `world-trade` |
| `commercially` | classified-no-standalone-progression | `world-trade` |
| `tabletopgames` | classified-no-standalone-progression | `world-trade` |
| `egocaribautomapmarkers` | classified-no-standalone-progression | Creates configurable map markers on interaction; do not imply every object is automatically marked. |
| `prospecttogether` | classified-no-standalone-progression | Stores and shares prospecting readings; does not itself prospect or create ore. |
| `spyglass` | verified-native-smithing-and-grid-branch | `world-overland`, `process-ore-to-equipment` |
| `footprints` | classified-no-standalone-progression | Animals and players leave footprints; potentially useful tracking, not a crafting chain. |
| `clickuptorches` | classified-no-standalone-progression | `world-survival` |
| `slowtox` | classified-no-standalone-progression | `world-survival` |
| `toolbelt` | classified-no-standalone-progression | Belts can hold tools; recipe and use belong to equipment/storage research. |
| `placeonslabs` | classified-no-standalone-progression | `process-building-materials` |
| `terrainslabs` | verified-native-building-material-branch | `process-building-materials` |
| `sortablestorage` | classified-no-standalone-progression | Adds a storage sort button; QoL, not new material progression. |
| `radialcrafting` | classified-no-standalone-progression | Radial crafting menu; do not replace native recipe constraints with imagined recipes. |
| `danatweaks` | verified-config-consumed-player-deltas | `kuboczochmodpackguide-process-kitchen-firstfire`, `kuboczochmodpackguide-process-kitchen-dough-oven`, `kuboczochmodpackguide-process-kitchen-dairy`, `process-farming-animal-care`, `process-building-materials`, `world-survival`, `process-powders-to-ceramics` |
| `configureeverything` | verified-disabled-in-export | `kuboczochmodpackguide-process-kitchen-dough-oven`, `kuboczochmodpackguide-process-kitchen-dairy`, `process-farming-animal-care`, `process-ore-to-equipment` |
| `cbr` | verified-native-patch-shelter-delta | `process-building-materials`, `kuboczochmodpackguide-process-kitchen-firstfire` |
| `yangcoifix` | verified-static-liquid-entity-interaction-ordering | `process-farming-animal-care`, `kuboczochmodpackguide-process-kitchen-dairy` |
| `attributerenderinglibrary` | excluded-as-standalone-progression | Attribute-driven item rendering infrastructure; native inventory does not establish a separate survival recipe chain. |
| `commonlib` | verified-static-technical-library | Shared config and helper implementation; no own survival item/recipe chain. Blank description is not the exclusion evidence. |
| `configlib` | excluded-as-standalone-progression | Config GUI and conditional configuration infrastructure; consumers and individual enabled settings own gameplay changes. |
| `coreofartspatch` | verified-player-liquid-mixing-not-pure-library | `kuboczochmodpackguide-process-kitchen-dough-oven`, `process-farming-animal-care` |
| `vsimgui` | excluded-as-standalone-progression | Immediate-mode GUI integration library; no native survival item/recipe chain. |
| `genelib` | verified-animal-behavior-framework | `process-farming-animal-care` |
| `handbookoptimizer` | excluded-as-standalone-progression | Handbook display/search optimization; process content is owned by its supplying mods. |
| `jaunt` | excluded-as-standalone-progression | Utility/navigation mod rather than a documented resource-processing chain; do not invent a transport recipe or normal-player teleport entitlement. |
| `jsonpatcheslib` | excluded-as-standalone-progression | JSON asset-patch infrastructure; consumers own recipes and enabled transformations. |
| `overhaullib` | verified-static-technical-library | `process-ore-to-equipment`, `world-survival` |
| `overhaulliblegacycompat` | verified-gameplay-compatibility-armor-quenching | `process-ore-to-equipment`, `world-survival` |
| `playermodellib` | verified-model-inventory-and-stat-infrastructure | `world-survival` |
| `universaldisplaylib` | excluded-as-standalone-progression | Display/storage block infrastructure consumed by other mods; no separate manufacturing chain established here. |
| `shearlib` | verified-shearing-behavior-framework | `process-farming-animal-care`, `process-textiles` |
| `infinitumcore` | excluded-as-standalone-progression | Shared Infinitum UI/runtime infrastructure; prospecting/pickup modules own displayed features. |
| `cavesymphony` | excluded-as-standalone-progression | Cave ambience, not a documented material-processing chain. |
| `farseer` | excluded-as-standalone-progression | View-distance/rendering configuration; no native production chain. |
| `footsteps` | verified-static-audio-only | Footstep audio/frame hook, not a progression mechanic. |
| `immersivefoliage` | excluded-as-standalone-progression | Vegetation rendering/animation; no native production chain. |
| `immersivelanterns` | verified-static-animation-no-fuel-chain | `process-building-materials` |
| `immersivelight` | verified-static-light-propagation | `process-building-materials`, `process-farming-animal-care` |
| `juicyores` | excluded-as-standalone-progression | Ore presentation feedback; no independently established ore conversion chain. |
| `mistsofstability` | verified-static-stability-visualization | `world-survival` |
| `realsmoke` | verified-survival-hazard-not-cosmetic | `kuboczochmodpackguide-process-kitchen-firstfire`, `kuboczochmodpackguide-process-kitchen-dough-oven`, `process-building-materials`, `world-survival` |
| `realsmokeartofgrowingfirepit` | verified-native-smoke-compatibility | `kuboczochmodpackguide-process-kitchen-firstfire`, `kuboczochmodpackguide-process-kitchen-dough-oven` |
| `stickstepsounds` | excluded-as-standalone-progression | Walking-stick sound compatibility; walkingstick owns the item recipe. |
| `sungaze` | excluded-as-standalone-progression | Solar visual/environmental effect rather than a documented resource process; do not infer damage from the name. |
| `temporalsymphony` | verified-entity-and-riftweather-not-just-ambience | `world-survival` |
| `kuboczochvisualmining` | excluded-as-standalone-progression | Visual mining feedback; does not replace the ore/crushing/smelting process. |
| `extrainfo` | excluded-as-standalone-progression | Extra item/block information UI; no acquisition stages. |
| `itempickupnotifier` | excluded-as-standalone-progression | Pickup notification UI; no acquisition recipe. |
| `playerlistrevived` | excluded-as-standalone-progression | Player-list UI; no resource process. |
| `scrollbarcreativeinventorytabs` | excluded-as-standalone-progression | Creative inventory UI; not a survival crafting stage. |
| `statushudcont` | excluded-as-standalone-progression | Status HUD; reports state rather than providing a production chain. |
| `infinitumpickuphud` | excluded-as-standalone-progression | Pickup HUD module; no separate acquisition/processing steps. |
| `infinitumprospectinghud` | excluded-as-standalone-progression | Prospecting HUD presentation; prospecting tools/world mechanics remain consumer/vanilla processes. |
| `kuboczochfoodhandbook` | excluded-as-standalone-progression | Existing handbook content; cross-link rather than generate a redundant process about the guide itself. |
| `kuboczochhandbookrefresh` | excluded-as-standalone-progression | Handbook refresh/content integration; no survival recipe chain. |
| `kuboczochmodpackguide` | excluded-as-standalone-progression | Existing index/guide content; not an additional gameplay mechanic. |
| `kuboczochtutorialskip` | excluded-as-standalone-progression | Tutorial-state utility; not a resource chain. |
| `kuboczochwoodworkingtutorial` | excluded-as-standalone-progression | Existing tutorial content; native woodworking process remains the subject. |
| `manualfirestartertutorial` | excluded-as-standalone-progression | Existing firestarter tutorial content; ignition process belongs to firestarter mods. |

## Source files and verification

- Machine-readable deliverable: `research/tweaks.json` (`pages: []`).
- Static exported C# evidence: `/home/brunette/.hermes/cache/scratch/tweaks-static/*.cs`; archive/member citations in JSON are the durable pack sources.
- Source validation: all cited files and archive members exist; all correction page codes and step labels exist; all 90 classification IDs are unique.
- Both source schema/JSON parsing and output counts were checked programmatically. No loaded-world smoke/room/collision, riding or quenching test was claimed.
