# Supplement: installed food and fuel processes

Classified 13 requested enabled mods. Drafted 3 pages / 16 sourced steps. Verified 20 distinct native item/block codes, 9 native guide codes and 71 evidence references. All requested ZIP hashes match evpack.json.

## Important pack-specific findings

- No bundled DLL was executed: dnfile/dncil parsed PE metadata and IL as data. Static control-flow evidence is embedded for interactions not documented by JSON assets.
- Manual Firestarter and Firestarters are both enabled; their final event-handler precedence in the running game remains untested. Draft preserves the installed tutorial sequence and warns of environmental checks.
- Stone Bake Oven native structure is lateral Heat Source -> Heat Distribution -> Baking Space, NOT a vertical firebox/grill/chimney stack. Native broad clay-oven item compatibility is not individual runtime proof for Rustic Cookery pot roasts.
- Manual firestarter accessibility is OFF in this export. LakeIceToCutIce is OFF. Seasoned-to-aged curing is OFF. These exact exported values take precedence over any generic/default advice.
- 6 source text assets did not parse as JSON5. Raw files were still read; multiline localizations were text-inspected. The tankard alcoholbandage recipe is truncated and is not asserted active. See parseWarnings.
- Clickable references are statically verified against native item/block variant definitions and handbook pageCode declarations, not a client-rendered click test.
- The exported pack is point-in-time evidence; no claim that these remain the live server settings after the export.

## Player process drafts

### Kitchen - First fire and first meal

`kuboczochmodpackguide-process-kitchen-firstfire`

Kindle separate tinder, then choose a quick roast or a cooking-pot meal.

1. **Build the firepit** — Gather four dry grass and four firewood; craft a firestarter from two diagonal sticks. Sneak-right-click one dry grass onto solid ground, then right-click it with four firewood. Save another dry grass for separate tinder and two for torches.
   Clickable: `handbook://item-game:drygrass`, `handbook://item-game:firewood`, `handbook://item-game:firestarter`
   Evidence: `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!assets/manualfirestartertutorial/lang/en.json#supplies`; `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!assets/manualfirestartertutorial/lang/en.json#build`
2. **Kindle separate tinder** — Sneak-right-click another dry grass onto the ground beside the completed firepit. Aim the firestarter at this separate tinder and sneak-right-click to begin. Keep sneaking, stay still, and alternate left/right clicks until you hold glowing tinder.
   Clickable: `handbook://item-game:firestarter`
   Evidence: `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!assets/manualfirestartertutorial/lang/en.json#tinder`; `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!assets/manualfirestartertutorial/lang/en.json#kindle`; `mods/ManualFirestarter_0.1.3.zip!ManualFirestarter.dll#FirestartingModeClient.OnMouseDown`
3. **Light the pit** — Carry the glowing tinder to the completed firepit and right-click it. If ignition is refused, check the environmental warning: Firestarters also imposes weather restrictions on ignition tools.
   Evidence: `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!assets/manualfirestartertutorial/lang/en.json#light`; `mods/Firestarters_1.4.6.zip!assets/firestarters/lang/en.json#game:firestarters-environment-guide-text`
4. **Roast on a stick** — Put roastable raw food in your offhand and a normal stick in your main hand; right-click to skewer it. Hold right-click with the loaded stick aimed at a burning firepit to roast. Empty your offhand and Ctrl-right-click to remove the food.
   Clickable: `handbook://item-game:stick`, `handbook://item-stickroast:roastingstick`
   Evidence: `mods/StickRoast_0.1.0.zip!assets/stickroast/patches/roasting.json`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#RoastingStickBehavior.OnHeldInteractStart`
5. **Upgrade to pot meals** — Clay-form and pit-fire a pot and bowl. Put the fired pot in the firepit, fill its ingredient slots with a valid meal recipe and matching portion counts, then cook. Right-click the cooked pot with an empty bowl to serve.
   Evidence: `vs/assets/game/lang/en.json#craftinginfo-mealcooking-text`
6. **Wash out leftovers** — To empty a held meal bowl or cooked container, aim at water and hold right-click for about one second. Washing empties its contents, so finish edible food first. This is an added held interaction, not a requirement to wash every already-empty dish.
   Evidence: `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#AvailableContainers.CanWashNow`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#MouseHandler.OnMouseDown`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#WashDish.OnWashRequest`

Native guides: `handbook://craftinginfo-starterguide`, `handbook://firestarters-environment-guide`, `handbook://craftinginfo-mealcooking`

### Kitchen - Knead dough and build a stone oven

`kuboczochmodpackguide-process-kitchen-dough-oven`

Make dough by hand, cure better fuel, then bake in a larger oven.

1. **Knead dough** — Hold flour, aim at water, and keep right-click held for about one second to make matching-grain dough. Start with spelt flour for spelt dough. Looking at water avoids needing a portable water container. This pack enables removal of the dough grid recipes targeted by Manual Dough.
   Clickable: `handbook://item-game:flour-spelt`, `handbook://item-game:dough-spelt`
   Evidence: `config/ModConfig/ManualDoughConfig.json`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.OnGameTick`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.TransformFlourToDough`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#<>c__DisplayClass11_2.<OnGameTick>b__0`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.StartServerSide`
2. **Cure better fuel** — Shapelessly combine a knife and 2 ordinary firewood for 2 prepared firewood, then let it cure into seasoned fuel; the base curing transition is 96 in-game hours. Alternatively surround resin or beeswax with 8 firewood to craft 8 seasoned firewood directly. The exported pack disables further curing into aged firewood; raw firewood remains allowed for first-fire construction and clay ovens.
   Clickable: `handbook://item-fseasonedfirewood:fpreparedfirewood`, `handbook://item-fseasonedfirewood:fseasonedfirewood`
   Evidence: `mods/tseasonedfirewood_1.2.12.zip!assets/fseasonedfirewood/recipes/grid/tfirewood.json`; `mods/tseasonedfirewood_1.2.12.zip!assets/fseasonedfirewood/itemtypes/resource/firewood-prepared.json`; `config/ModConfig/seasonedfirewood_config.json`; `mods/tseasonedfirewood_1.2.12.zip!fseasonedfirewood.dll#SeasonedFirewoodModSystemRewrite.AssetsLoaded`
3. **Connect the three oven components** — Craft one Oven Heat Source, one Oven Heat Distribution and one Oven Baking Space using the linked component recipes. The native guide lists three copper plates and your chosen stone for the basic set. Place Heat Distribution to the left or right of Heat Source, then place Baking Space in front of Heat Distribution. These are adjacent functional blocks, not a vertical grill-and-chimney stack.
   Clickable: `handbook://block-stonebakeoven:ovencontrollerplain-granite-cold-north`, `handbook://block-stonebakeoven:ovengrillplain-granite-cold-north`, `handbook://block-stonebakeoven:ovenbakingtopplain-granite`
   Evidence: `mods/stonebakeoven_1.4.0.zip!assets/stonebakeoven/lang/en.json#game:stone-bake-oven-text`; `mods/stonebakeoven_1.4.0.zip!assets/stonebakeoven/recipes/grid/plain.json`
4. **Fuel and bake** — Put burnable fuel into Oven Heat Source and light it with a torch. Place dough on Oven Baking Space just as you would in a clay oven, then wait for it to bake. Add more distribution and baking spaces for larger batches, but remember that heat falls with distance from the source.
   Clickable: `handbook://item-game:dough-spelt`
   Evidence: `mods/stonebakeoven_1.4.0.zip!assets/stonebakeoven/lang/en.json#game:stone-bake-oven-text`
5. **Try an oven pot roast** — Rustic Cookery is a separate branch: with tongs in your offhand, work a roasting dish on a lit firepit. Render fat, sear a whole meat slab, add sides and braising liquid, then remove the roast and finish it in a preheated clay oven. Place the baked roast and right-click with an empty bowl to serve.
   Evidence: `mods/rusticcookery_1.2.0.zip!assets/rusticcookery/lang/en.json#game:handbook-mealrecipe-directionstext-rusticcookery-potroast`

Native guides: `handbook://stone-bake-oven`, `handbook://craftinginfo-mealcooking`

### Kitchen - Tea, drinkware and cold storage

`kuboczochmodpackguide-process-kitchen-tea-storage`

Brew a known-safe flower, use a reusable tankard, and store food in purpose-built containers.

1. **Brew safe floral tea** — Use a cooking pot on a lit firepit. For one floral-tea portion, use 1 L water and one cornflower; wild daisy and golden poppy are other listed floral ingredients. Cook to obtain hot floral tea. Do not substitute arbitrary flowers: lily of the valley and the listed lupines produce toxic tea.
   Clickable: `handbook://block-game:flower-cornflower-free`, `handbook://item-game:waterportion`, `handbook://item-simpletea:teaportion-floral-hot`
   Evidence: `vs/assets/game/lang/en.json#craftinginfo-mealcooking-text`; `mods/simpletea_1.2.0.zip!assets/simpletea/recipes/cooking/tea.json`
2. **Craft a tankard** — Use a 2 by 2 grid: stick/plank in the first row, empty/plank in the second. The wooden tankard is a liquid container for tea; it holds 1 L and transfers 0.5 L at a time. Metal tankards and goblets are alternatives, not prerequisites.
   Clickable: `handbook://block-tankardsandgoblets:tankard-wood`
   Evidence: `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/recipes/grid/basictankardrecipe.json`; `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/blocktypes/tankard.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`
3. **Use hot or cold tea** — Drink tea hot when practical: the liquid asset gives hot tea a base four-hour freshness period before its cold variant. Cold tea eventually decays back to water. Medicinal tea has a separate ingredient list; toxic tea damages health, so keep it apart from safe drinks.
   Clickable: `handbook://item-simpletea:teaportion-floral-cold`
   Evidence: `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/recipes/cooking/tea.json`
4. **Chill food correctly** — Select shelves and baskets by their accepted contents, not their appearance. Keep the Cooling Cabinet doors closed. Add Cut Ice to its dedicated drawer and close that too: the extra cooling needs both compartments closed. Replace melting ice regularly.
   Clickable: `handbook://block-foodshelves:coolingcabinet-normal-east`, `handbook://item-foodshelves:cutice`
   Evidence: `mods/foodshelves_3.1.1.zip!assets/foodshelves/lang/en.json#foodshelvesguide-text`
5. **Maintain an ice reserve** — Store spare Cut Ice in an Ice Keeper to prevent melting. For baskets inside a cabinet, right-click with accepted food to insert it; Ctrl inserts multiple, while Ctrl with an empty hand removes one item. Read the block tooltip for its actual freshness, curing or drying effect rather than assuming every shelf refrigerates.
   Clickable: `handbook://block-foodshelves:icekeeper-normal-east`
   Evidence: `mods/foodshelves_3.1.1.zip!assets/foodshelves/lang/en.json#foodshelvesguide-text`

Native guides: `handbook://simpletea-guide`, `handbook://foodshelvesguide`, `handbook://gamemechanicinfo-foodpreservation`

## Classification and connections

### efchefstricks 1.0.0-dev.6

kitchen byproducts and utility recipes; enabled in the pack manifest.

**playerImpact**: Adds crumb feed, fresh resin, salt crystals, washed eggshells and oil/lard utility recipes.
Evidence: `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/lang/en.json#efchefstricksguide-text`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/recipes/barrel/washedeggshell.json`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/recipes/barrel/saltcrystal.json`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!modinfo.json`

**vanillaDelta**: Adds processing and substitutions; it is not a new cooking station.
Evidence: `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/lang/en.json#efchefstricksguide-text`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/recipes/barrel/washedeggshell.json`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/recipes/barrel/saltcrystal.json`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!modinfo.json`

**configStatus**: No dedicated exported config identified in the complete config inventory. Native recipe assets are the audited baseline.
Evidence: `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/lang/en.json#efchefstricksguide-text`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/recipes/barrel/washedeggshell.json`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/recipes/barrel/saltcrystal.json`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!modinfo.json`

**crossModInteractions**: Metadata requires expandedfoods >=2.0.0-dev.12; native guide also names ACA. Recipe assets include oil-powered mechanical components and conditional superfuel recipes.
Evidence: `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/lang/en.json#efchefstricksguide-text`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/recipes/barrel/washedeggshell.json`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/recipes/barrel/saltcrystal.json`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!modinfo.json`

**Practical route**: Seal 8 eggshells with 1 L water to wash them; use the washed output for lime. Salt-water/brine crystals are another branch; consult the actual barrel recipe and salt-crystal processing attributes rather than inventing a quern recipe file.
Evidence: `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/lang/en.json#efchefstricksguide-text`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/recipes/barrel/washedeggshell.json`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!assets/efchefstricks/recipes/barrel/saltcrystal.json`; `mods/ExpandedFoods_ChefsTricks_1.0.0-dev.6.zip!modinfo.json`

Existing guide codes: `kuboczochmodpackguide-process-kitchen-broth`, `process-powders-to-ceramics`
Native guide codes: `efchefstricksguide`

### efmealsmodule 1.0.0-dev.9

expanded pot and mixing meals; enabled in the pack manifest.

**playerImpact**: Native recipes add au gratin, compote, hot pasta and pilaf; mixing recipes add cold pasta, salad and yogurt meals.
Evidence: `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/lang/en.json#expandedfoodsguide3-text`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/recipes/cooking/pastahot.json`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/recipes/mixing/pastacold.json`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!modinfo.json`

**vanillaDelta**: Extends existing meals and ingredient categories; do not attribute dumpling/sushi recipes from other modules to this installed module.
Evidence: `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/lang/en.json#expandedfoodsguide3-text`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/recipes/cooking/pastahot.json`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/recipes/mixing/pastacold.json`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!modinfo.json`

**configStatus**: No dedicated exported config identified; patch dependencies determine conditional compatibility.
Evidence: `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/lang/en.json#expandedfoodsguide3-text`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/recipes/cooking/pastahot.json`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/recipes/mixing/pastacold.json`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!modinfo.json`

**crossModInteractions**: Metadata requires expandedfoods >=2.0.0-dev.10. The native guide describes ACA liquid eggs and EF broth/oil toppings.
Evidence: `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/lang/en.json#expandedfoodsguide3-text`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/recipes/cooking/pastahot.json`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/recipes/mixing/pastacold.json`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!modinfo.json`

**Practical route**: Choose a native cooking or mixing recipe first. Hot pasta uses the cooking-pot route; cold pasta uses mixing. Native guide says soup can use broth or vegetable juice rather than water, and liquid egg replaces shell eggs in its scrambled-egg recipe.
Evidence: `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/lang/en.json#expandedfoodsguide3-text`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/recipes/cooking/pastahot.json`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!assets/efmealsmodule/recipes/mixing/pastacold.json`; `mods/ExpandedFoods_MealsModule_1.0.0-dev.9.zip!modinfo.json`

Existing guide codes: `kuboczochmodpackguide-process-kitchen-pasta`, `kuboczochmodpackguide-process-kitchen-broth`, `kuboczochmodpackguide-process-kitchen-dairy`
Native guide codes: `expandedfoodsguide3`

### firestarters 1.4.6

weather-gated ignition; enabled in the pack manifest.

**playerImpact**: Makes ignition tool-dependent; guide covers handdrill, bowdrill, flint-and-steel, quartz, spyglass and torch conditions.
Evidence: `mods/Firestarters_1.4.6.zip!assets/firestarters/lang/en.json#game:firestarters-environment-guide-text`; `mods/Firestarters_1.4.6.zip!assets/firestarters/patches/patch-firestarter.json`; `mods/Firestarters_1.4.6.zip!modinfo.json`

**vanillaDelta**: Changes the reliability of existing ignition methods; meaningful first-fire impact, not just decorative tools.
Evidence: `mods/Firestarters_1.4.6.zip!assets/firestarters/lang/en.json#game:firestarters-environment-guide-text`; `mods/Firestarters_1.4.6.zip!assets/firestarters/patches/patch-firestarter.json`; `mods/Firestarters_1.4.6.zip!modinfo.json`

**configStatus**: All eight Firestarter config files are present. Vanilla firestarter: temperature/rain/wind checks, minimum 10°C. Bowdrill and torch: checks enabled, minimum 5°C. Quartz: minimum 10°C, hours 9-15, rain/wind/cloud checks. Spyglass: minimum 7°C, hours 8-16, rain/wind/cloud checks. Flint-and-steel: rain/wind checks and enabled 10% break chance. Miscellaneous: both wind and backpack torch extinction enabled.
Evidence: `config/ModConfig/Firestarter/BowDrillConfig.json`; `config/ModConfig/Firestarter/FirestartersModConfig.json`; `config/ModConfig/Firestarter/FlintAndSteelConfig.json`; `config/ModConfig/Firestarter/MiscellaneousConfig.json`; `config/ModConfig/Firestarter/QuartzFirestarterConfig.json`; `config/ModConfig/Firestarter/SpyglassFirestarterConfig.json`; `config/ModConfig/Firestarter/TorchFirestarterConfig.json`; `config/ModConfig/Firestarter/VanillaFirestarterConfig.json`

**crossModInteractions**: Manual Firestarter separately changes the vanilla tinder route. Both providers are enabled; their final combined handler precedence is not verified live.
Evidence: `mods/Firestarters_1.4.6.zip!assets/firestarters/lang/en.json#game:firestarters-environment-guide-text`; `mods/Firestarters_1.4.6.zip!assets/firestarters/patches/patch-firestarter.json`; `mods/Firestarters_1.4.6.zip!modinfo.json`

**Practical route**: Read the native environmental guide and tool warning; change conditions or tool when refused rather than assuming every tool shares the same weather limits.
Evidence: `mods/Firestarters_1.4.6.zip!assets/firestarters/lang/en.json#game:firestarters-environment-guide-text`; `mods/Firestarters_1.4.6.zip!assets/firestarters/patches/patch-firestarter.json`; `mods/Firestarters_1.4.6.zip!modinfo.json`

Existing guide codes: `process-wood-to-workshop`, `world-survival`
Native guide codes: `firestarters-environment-guide`

### foodshelves 3.1.1

functional food storage; enabled in the pack manifest.

**playerImpact**: Adds categorized shelves, baskets and preservation furniture, including ice-cooled cabinets and non-melting ice reserve storage.
Evidence: `mods/foodshelves_3.1.1.zip!assets/foodshelves/lang/en.json#foodshelvesguide-text`

**vanillaDelta**: Not all containers refrigerate or accept all foods.
Evidence: `mods/foodshelves_3.1.1.zip!assets/foodshelves/lang/en.json#foodshelvesguide-text`

**configStatus**: GlobalBlockBuffs=true, LakeIceToCutIce=false, GlobalPerishMultiplier=1, CooledBuff=.3, IceMeltRate=1. In particular, do not instruct players to convert lake ice directly: that option is disabled. Client configuration only changes presentation.
Evidence: `config/ModConfig/FoodShelvesServer.json`; `config/ModConfig/FoodShelvesClient.json`

**crossModInteractions**: Native guide explicitly requires EF for barrel-rack liquid curing; baskets inside cabinets inherit cabinet preservation.
Evidence: `mods/foodshelves_3.1.1.zip!assets/foodshelves/lang/en.json#foodshelvesguide-text`

**Practical route**: Keep cabinet and ice drawer closed, replace melting Cut Ice, and put spare ice in an Ice Keeper. Use each block’s accepted-content handbook entry.
Evidence: `mods/foodshelves_3.1.1.zip!assets/foodshelves/lang/en.json#foodshelvesguide-text`

Existing guide codes: `kuboczochmodpackguide-process-kitchen-bottles`, `process-workshop-storage`
Native guide codes: `foodshelvesguide`

### manualdishes 0.1.2

held washing; enabled in the pack manifest.

**playerImpact**: Adds aimed-at-water washing for held meal and cooked-container blocks.
Evidence: `vs/assets/game/lang/en.json#craftinginfo-mealcooking-text`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#AvailableContainers.CanWashNow`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#MouseHandler.OnMouseDown`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#WashDish.OnWashRequest`

**vanillaDelta**: Vanilla already supports emptying dishes by tossing them into water; this adds an explicit held interaction, not a hygiene debuff or mandatory clean-dish gate.
Evidence: `vs/assets/game/lang/en.json#craftinginfo-mealcooking-text`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#AvailableContainers.CanWashNow`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#MouseHandler.OnMouseDown`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#WashDish.OnWashRequest`

**configStatus**: No dedicated exported config or bundled configuration asset identified. Mouse handler registers a 1000ms hold callback.
Evidence: `vs/assets/game/lang/en.json#craftinginfo-mealcooking-text`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#AvailableContainers.CanWashNow`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#MouseHandler.OnMouseDown`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#WashDish.OnWashRequest`

**crossModInteractions**: Works with base BlockMeal / BlockCookedContainerBase and their empty-container attributes; support for every custom vessel is not established.
Evidence: `vs/assets/game/lang/en.json#craftinginfo-mealcooking-text`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#AvailableContainers.CanWashNow`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#MouseHandler.OnMouseDown`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#WashDish.OnWashRequest`

**Practical route**: Aim at water holding the meal bowl/cooked container and hold right-click about one second. It empties the dish, discarding edible contents; finish the meal first.
Evidence: `vs/assets/game/lang/en.json#craftinginfo-mealcooking-text`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#AvailableContainers.CanWashNow`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#MouseHandler.OnMouseDown`; `mods/ManualDishes_0.1.2.zip!ManualDishes.dll#WashDish.OnWashRequest`

Existing guide codes: `kuboczochmodpackguide-process-kitchen-bottles`
Native guide codes: none statically declared / used here

### manualdough 1.1.8

manual kneading and targeted grid-recipe removal; enabled in the pack manifest.

**playerImpact**: Converts held flour into matching grain dough with water.
Evidence: `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.TransformFlourToDough`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.OnGameTick`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.StartServerSide`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.TryConsumeWaterFromHotbar`

**vanillaDelta**: StartServerSide reads DisableDoughRecipes and calls RemoveDoughRecipes; the configured route replacement is backed by IL, not just a setting name.
Evidence: `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.TransformFlourToDough`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.OnGameTick`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.StartServerSide`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.TryConsumeWaterFromHotbar`

**configStatus**: HoldDuration=1000ms, CooldownDuration=1000ms, DisableDoughRecipes=true. These are the exact serialized settings; no FlourToDoughRatio or LitersOfWaterPerDough setting is present.
Evidence: `config/ModConfig/ManualDoughConfig.json`

**crossModInteractions**: The water path derives the dough code from the held flour in the same asset domain. Blood conversion explicitly looks up butchering:blooddough-<grain>; ordinary water dough does not require blood.
Evidence: `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.TransformFlourToDough`

**Practical route**: Hold spelt flour, aim at water and hold right-click about one second for spelt dough. Hotbar water-container supply is another supported route.
Evidence: `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.TransformFlourToDough`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.OnGameTick`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.StartServerSide`; `mods/ManualDough_1.1.8.zip!ManualDough.dll#ManualDough.TryConsumeWaterFromHotbar`

Existing guide codes: `kuboczochmodpackguide-process-kitchen-pasta`
Native guide codes: none statically declared / used here

### manualfirestarter 0.1.3

separate-tinder ignition workflow; enabled in the pack manifest.

**playerImpact**: Requires ground tinder, sneaking alternating clicks and carrying glowing tinder to the built firepit.
Evidence: `vs/assets/game/lang/en.json#craftinginfo-starterguide-text`; `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!assets/manualfirestartertutorial/lang/en.json`; `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!README.md`; `mods/ManualFirestarter_0.1.3.zip!modinfo.json`

**vanillaDelta**: Stock starter-guide direct ignition wording needs this modpack qualification.
Evidence: `vs/assets/game/lang/en.json#craftinginfo-starterguide-text`; `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!assets/manualfirestartertutorial/lang/en.json`; `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!README.md`; `mods/ManualFirestarter_0.1.3.zip!modinfo.json`

**configStatus**: AccessibilityEnabled=false; TargetValue=100; GainPerValidClick=2; DecayTickIntervalMs=100; DecayPerTick=5. The accessibility override exists in LoadOrCreate but is NOT active in this exported pack configuration.
Evidence: `config/ModConfig/ManualFirestarterConfig.json`; `mods/ManualFirestarter_0.1.3.zip!ManualFirestarter.dll#ManualFirestarterConfig.LoadOrCreate`

**crossModInteractions**: Tutorial addon manualfirestartertutorial 1.2.1 supplies exact controls and adapts to enabled Art of Growing straw. Firestarters is also enabled; final handler precedence is not live-tested.
Evidence: `vs/assets/game/lang/en.json#craftinginfo-starterguide-text`; `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!assets/manualfirestartertutorial/lang/en.json`; `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!README.md`; `mods/ManualFirestarter_0.1.3.zip!modinfo.json`

**Practical route**: Build the pit, place separate tinder nearby, sneak-right-click that tinder with the firestarter, keep still and alternate clicks. Carry hot tinder to the completed pit and right-click.
Evidence: `vs/assets/game/lang/en.json#craftinginfo-starterguide-text`; `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!assets/manualfirestartertutorial/lang/en.json`; `mods/KuboczochManualFirestarterTutorial-1.2.1.zip!README.md`; `mods/ManualFirestarter_0.1.3.zip!modinfo.json`

Existing guide codes: `process-wood-to-workshop`, `world-survival`
Native guide codes: none statically declared / used here

### rusticcookery 1.2.0

hands-on braising and pot roast; enabled in the pack manifest.

**playerImpact**: Adds roasting dishes/lids/tongs, sliced ingredients, meat slabs and smoking furniture.
Evidence: `mods/rusticcookery_1.2.0.zip!assets/rusticcookery/lang/en.json`

**vanillaDelta**: Adds fat-rendering/searing/softening/liquid stages beyond ordinary cooking-pot slots.
Evidence: `mods/rusticcookery_1.2.0.zip!assets/rusticcookery/lang/en.json`

**configStatus**: ConfigVersion=5; SlabSize=6; AutoSlabs and ExtraSlabs=true; SmokingSeconds=2880; PotRoastBakeSeconds=240; BraiseFatSeconds=20, Brown=30, Soften=30, Reduce=120, Roux=30; FullSmokeAmount=64; UncoveredDishPerishMul=1.5. These are exported base settings, not a live-world timing guarantee.
Evidence: `config/ModConfig/rusticcookery.json`

**crossModInteractions**: Native pot-roast directions explicitly accept rendered fat/lard/cooking oil and EF broth, and name the clay oven for finishing. Exported SlabRules include Butchering and animal-mod entity patterns; each remains conditional on matching entities.
Evidence: `mods/rusticcookery_1.2.0.zip!assets/rusticcookery/lang/en.json`; `config/ModConfig/rusticcookery.json`

**Practical route**: Fire a clay-formed roasting dish, set it on a lit firepit and hold tongs offhand. Render fat, sear a whole meat slab, soften sides, add liquid, then finish the removed pot roast in a preheated clay oven and serve with bowls.
Evidence: `mods/rusticcookery_1.2.0.zip!assets/rusticcookery/lang/en.json`

Existing guide codes: `process-butchering-leather`, `kuboczochmodpackguide-process-kitchen-broth`
Native guide codes: none statically declared / used here

### tseasonedfirewood 1.2.12

fuel preparation and vanilla-fuel rebalance; enabled in the pack manifest.

**playerImpact**: Prepared firewood cures into seasoned fuel; runtime also edits raw/aged firewood and pit-kiln fuel.
Evidence: `mods/tseasonedfirewood_1.2.12.zip!assets/fseasonedfirewood/recipes/grid/tfirewood.json`; `mods/tseasonedfirewood_1.2.12.zip!assets/fseasonedfirewood/itemtypes/resource/firewood-prepared.json`; `mods/tseasonedfirewood_1.2.12.zip!fseasonedfirewood.dll#SeasonedFirewoodModSystemRewrite.AssetsLoaded`

**vanillaDelta**: Raw wood remains allowed for first-fire construction and clay ovens in this export. Shipped seasoned-to-aged transition is disabled by pack configuration.
Evidence: `mods/tseasonedfirewood_1.2.12.zip!assets/fseasonedfirewood/recipes/grid/tfirewood.json`; `mods/tseasonedfirewood_1.2.12.zip!assets/fseasonedfirewood/itemtypes/resource/firewood-prepared.json`; `mods/tseasonedfirewood_1.2.12.zip!fseasonedfirewood.dll#SeasonedFirewoodModSystemRewrite.AssetsLoaded`

**configStatus**: PreparedFirewoodCuringHours=96; SeasonedFirewoodBurnTemperature=500 and BurnDuration=160; RawFirewoodBurnTemperature=700 and BurnDuration=24; AgedFirewoodBurnTemperature=700 and BurnDuration=160. RawFirewoodCanStartFirepit=true and RawFirewoodCanBeOvenFuel=true; EnableSeasonedToAgedTransition=false. Runtime IL applies settings and removes the disabled Cure transition.
Evidence: `config/ModConfig/seasonedfirewood_config.json`; `mods/tseasonedfirewood_1.2.12.zip!fseasonedfirewood.dll#SeasonedFirewoodModSystemRewrite.AssetsLoaded`

**crossModInteractions**: Mod ID tseasonedfirewood uses asset namespace fseasonedfirewood. Runtime normalizes aged transition target to game:agedfirewood, not an invented mod ID namespace.
Evidence: `mods/tseasonedfirewood_1.2.12.zip!assets/fseasonedfirewood/recipes/grid/tfirewood.json`; `mods/tseasonedfirewood_1.2.12.zip!assets/fseasonedfirewood/itemtypes/resource/firewood-prepared.json`; `mods/tseasonedfirewood_1.2.12.zip!fseasonedfirewood.dll#SeasonedFirewoodModSystemRewrite.AssetsLoaded`

**Practical route**: Shapelessly combine a knife and 2 firewood for 2 prepared firewood; let it cure. Alternatively surround resin, beeswax or rendered fat with 8 firewood to craft 8 seasoned firewood directly. The next seasoned-to-aged transition is disabled.
Evidence: `mods/tseasonedfirewood_1.2.12.zip!assets/fseasonedfirewood/recipes/grid/tfirewood.json`; `config/ModConfig/seasonedfirewood_config.json`

Existing guide codes: `process-wood-to-workshop`, `process-ore-to-equipment`
Native guide codes: none statically declared / used here

### simpletea 1.2.0

cooking-pot drinks; enabled in the pack manifest.

**playerImpact**: Floral, wild, medicinal and toxic teas have hot/cold variants. Toxic tea harms health; medicinal tea has health effects.
Evidence: `mods/simpletea_1.2.0.zip!assets/simpletea/lang/en.json#game:simpleteaguide-text`; `mods/simpletea_1.2.0.zip!assets/simpletea/recipes/cooking/tea.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`

**vanillaDelta**: Uses cooking-pot recipes, not an invented kettle station. Hot tea cools, cold tea eventually turns back into water.
Evidence: `mods/simpletea_1.2.0.zip!assets/simpletea/lang/en.json#game:simpleteaguide-text`; `mods/simpletea_1.2.0.zip!assets/simpletea/recipes/cooking/tea.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`

**configStatus**: No dedicated exported config identified; use the bundled recipe and liquid transition assets.
Evidence: `mods/simpletea_1.2.0.zip!assets/simpletea/lang/en.json#game:simpleteaguide-text`; `mods/simpletea_1.2.0.zip!assets/simpletea/recipes/cooking/tea.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`

**crossModInteractions**: The asset supplies hydrationByType, but these declarations alone do not establish an active thirst provider. Standard nutritionPropsPerLitre defines floral/wild satiety and medicinal/toxic health. Do not add an undocumented Useful Stuff buff dependency.
Evidence: `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/lang/en.json#game:simpleteaguide-text`

**Practical route**: Cook 1 L water plus one cornflower for floral tea. Do not substitute toxic flowers; open medicinal tea ingredients before brewing a healing drink.
Evidence: `mods/simpletea_1.2.0.zip!assets/simpletea/lang/en.json#game:simpleteaguide-text`; `mods/simpletea_1.2.0.zip!assets/simpletea/recipes/cooking/tea.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`

Existing guide codes: `process-farming-animal-care`, `kuboczochmodpackguide-process-kitchen-bottles`
Native guide codes: `simpletea-guide`

### stickroast 0.1.0

early held-food roasting; enabled in the pack manifest.

**playerImpact**: Patches ordinary sticks with StickRoasting behavior and creates a roastingstick carrying food.
Evidence: `mods/StickRoast_0.1.0.zip!assets/stickroast/patches/roasting.json`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#RoastingStickBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.IsRoastable`

**vanillaDelta**: Adds main/offhand roasting at a burning firepit instead of a new pot-meal category or grid-crafted skewer.
Evidence: `mods/StickRoast_0.1.0.zip!assets/stickroast/patches/roasting.json`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#RoastingStickBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.IsRoastable`

**configStatus**: No dedicated exported config identified; cooking properties are read from roastable food.
Evidence: `mods/StickRoast_0.1.0.zip!assets/stickroast/patches/roasting.json`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#RoastingStickBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.IsRoastable`

**crossModInteractions**: IsRoastable checks eligibility dynamically. Shipped patches adjust raw redmeat/poultry/bushmeat/fish storage flags; compatibility with every modded food is not established.
Evidence: `mods/StickRoast_0.1.0.zip!assets/stickroast/patches/roasting.json`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#RoastingStickBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.IsRoastable`

**Practical route**: Hold stick main-hand and roastable food offhand, right-click to skewer, then hold right-click at a burning firepit. Empty offhand and Ctrl-right-click to remove food.
Evidence: `mods/StickRoast_0.1.0.zip!assets/stickroast/patches/roasting.json`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#RoastingStickBehavior.OnHeldInteractStart`; `mods/StickRoast_0.1.0.zip!StickRoast.dll#StickRoastingBehavior.IsRoastable`

Existing guide codes: `process-butchering-leather`
Native guide codes: none statically declared / used here

### stonebakeoven 1.4.0

expandable heat-distribution baking oven; enabled in the pack manifest.

**playerImpact**: Adds Oven Heat Source, Oven Heat Distribution and Oven Baking Space blocks. A basic build requires three copper plates and chosen stone in its component recipes.
Evidence: `mods/stonebakeoven_1.4.0.zip!assets/stonebakeoven/lang/en.json#game:stone-bake-oven-text`; `mods/stonebakeoven_1.4.0.zip!assets/stonebakeoven/recipes/grid/plain.json`

**vanillaDelta**: Expands baking capacity using a heat-distribution network, rather than a stacked firebox/meat-grill/chimney assembly. Native guide compares larger baking jobs with the clay oven.
Evidence: `mods/stonebakeoven_1.4.0.zip!assets/stonebakeoven/lang/en.json#game:stone-bake-oven-text`

**configStatus**: No dedicated exported config identified. Native assets define construction and component variants.
Evidence: `mods/stonebakeoven_1.4.0.zip!assets/stonebakeoven/lang/en.json#game:stone-bake-oven-text`; `mods/stonebakeoven_1.4.0.zip!assets/stonebakeoven/recipes/grid/plain.json`

**crossModInteractions**: Native guide reports support for items baked in the vanilla clay oven. That is a broad author claim, not individual live proof for Rustic Cookery pot roasts; its own directions still name clay ovens.
Evidence: `mods/stonebakeoven_1.4.0.zip!assets/stonebakeoven/lang/en.json#game:stone-bake-oven-text`; `mods/rusticcookery_1.2.0.zip!assets/rusticcookery/lang/en.json`

**Practical route**: Place Heat Distribution to the left or right of Heat Source, then Baking Space in front of Heat Distribution. Put burnable material in Heat Source and light with a torch; place dough on Baking Space as in a clay oven. Additional distribution/baking blocks expand the build, with less heat farther away.
Evidence: `mods/stonebakeoven_1.4.0.zip!assets/stonebakeoven/lang/en.json#game:stone-bake-oven-text`

Existing guide codes: `process-wood-to-workshop`
Native guide codes: `stone-bake-oven`

### tankardsandgoblets 1.4.4

functional liquid drinkware; enabled in the pack manifest.

**playerImpact**: Adds tankards/goblets as liquid containers with display/storage behavior; wooden tankard capacity 1 L and transfer increment .5 L.
Evidence: `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/blocktypes/tankard.json`; `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/recipes/grid/basictankardrecipe.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`

**vanillaDelta**: Functional serving vessels, not a new brewing mechanic.
Evidence: `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/blocktypes/tankard.json`; `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/recipes/grid/basictankardrecipe.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`

**configStatus**: No dedicated exported config identified; the wooden vessel properties are native block attributes.
Evidence: `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/blocktypes/tankard.json`; `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/recipes/grid/basictankardrecipe.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`

**crossModInteractions**: Wood tankard uses BlockLiquidContainerTopOpened, MilkingContainer, shelvable/displayable flags. Tea is containable liquid: asset-supported pairing, not live-client transfer proof.
Evidence: `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/blocktypes/tankard.json`; `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/recipes/grid/basictankardrecipe.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`

**Practical route**: Craft stick/plank over empty/plank in a 2 by 2 grid, then use the wooden tankard for liquids. Open other vessel recipes rather than assuming identical material costs.
Evidence: `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/blocktypes/tankard.json`; `mods/TankardsandGoblets_v1.4.4.zip!assets/tankardsandgoblets/recipes/grid/basictankardrecipe.json`; `mods/simpletea_1.2.0.zip!assets/simpletea/itemtypes/liquid/teaportion.json`

Existing guide codes: `kuboczochmodpackguide-process-kitchen-bottles`, `process-workshop-storage`
Native guide codes: none statically declared / used here

## Verification detail

The JSON companion contains archive SHA-256 verification, text-asset and complete exported-config inventories, exact relevant config snapshots, per-code variant resolution, per-evidence-source checks, and static IL with method names and offsets. No DLLs were loaded or run. Pages use exactly `{code,title,intro,steps,guides,mods}` and each step exactly `{label,text,items,evidence}`.
