# Farming / animal products process research

Pack: **Pszczoly 1.2.0**, manifest game **1.22.7**. Four concise draft pages in `farming.json`; no implementation edits.

## Validation

- 4 pages; rendered prose counts (title, intro, labels, steps, unknowns): 260, 298, 290, 266. Each is below 400 words.
- 29 distinct typed item/block references verified against definitions, variant states and exclusions.
- 14 exact existing native pageCodes verified; 79 evidence entries hashed in JSON.
- All relevant mods listed below are Enabled=true in evpack; this is not a server runtime check.
- Offline VintagestoryAPI.dll contains `v1.22.7`, matching manifest. No mod DLL was loaded.

## Critical pack-specific corrections

- **Livestock Welfare Breeding.Enabled=false.** `RequireBedding=true`, portion and pregnancy rules are nested in that disabled module. Do not teach them as live requirements, and do not extrapolate that all breeding is disabled. Hygiene/Weather/Thirst and death risks remain enabled.
- Textile YAML does **not** disable class restrictions. It enables recipes/patches. Raw Wool/Tailor/Dressmakers recipes still carry `requiresTrait: clothier` in relevant cases.
- Native guide IDs are not short English aliases: base examples are `gamemechanicinfo-farming`, `gamemechanicinfo-animalhusbandry`, `craftinginfo-leathermaking`. FGC uses the exact typo `langstrothguiden-fromgoldencombsguide`.
- FGC native prose links old `blue`/`gray` ceramic variants that its installed definitions do not declare. Draft references use declared `ashforest` variants. Fancy/new ceramic definition duplication is recorded as a loader caveat.
- Butchering fat rendering recipe output is `game:fat-rendered`, not the old prose link `item-fat`.
- Soaked hides are scraped **with a knife in the crafting grid**. Species-specific soaked hides have explicit conversion recipes; do not fabricate blanket aliases.
- Base leather prose says 24h soaking; the lime recipe says 20h. Recipe values take precedence; draft includes verified 72h weak/108h strong tannin stages.

## Evidence classification by mod

- **artofgrowingpatch 1.2.2** — asset-recipes + native item help; DLL interaction unverified. Crop drop patches, trough seed additions, bundle variants, seed-to-grain recipe and flax rinsing chain. aogbreedingaddon is absent from manifest, satisfying inverted crop-drop patch dependency.
- **manualharvesting 0.1.0** — installed metadata; runtime behavior unresolved. No executable interaction verified; do not promise exact right-click drops or harvest compatibility.
- **naturalfertilizer 1.5.1** — asset recipes + pack configuration. Manure slurry and compost recipes are present; automatic animal drop behavior is not established by recipes alone.
- **livestockwelfare 0.16.0** — native guide + authoritative pack module configuration. Hygiene/Weather/Thirst/Babies/Dogs/Treats Enabled=true; Breeding.Enabled=false. RequireBedding and pregnancy values remain inside disabled Breeding; Safety allows deaths.
- **betterhusbandry 1.4.0** — installed metadata + pack configuration; runtime unresolved. Present alongside disabled Welfare breeding module; not evidence that all animal reproduction is disabled.
- **genelib 3.2.1** — installed metadata; runtime unresolved. Present animal-genetics integration; no generation progression or fertility schedule inferred.
- **dairyplus 2.0.0** — installed metadata; runtime unresolved. Milk-processing gameplay not drafted without exact runtime/config chain; actual mod ID is dairyplus, not dairyexpansion.
- **catchlivestock 0.2.4** — installed metadata; runtime unresolved. Capture interactions excluded from verified process steps.
- **butchering 1.14.3** — native procedural guides + recipes + pack configuration. Hook→bleed→table→bone carcass chain; raw primemeat→rendered fat recipe. FieldHarvestingLightMode=false; ClassRecipesEnabled=true; loot multipliers 1.0.
- **fromgoldencombs 2.1.0** — native procedural guides + item/block variants + recipes; loader caveat. Exact unusual pageCodes preserved. Native blue/gray pottery links are not declared states in installed ceramic files; ashforest is declared. Duplicate fancy/new files and recipe resolution need runtime verification.
- **wool 1.9.6** — native procedural guides + recipes + YAML; compatibility candidate. Generation-one-or-higher shearing documented; fleece washing and twine/cloth recipes exist. Tailor compatibility folder is candidate integration, not proof of loader behavior.
- **tailorsdelight 2.2.2** — asset recipes + YAML. enable_vs_recipe_patches=true; spindle twine/plain cloth/linen conversion recipes exist. clothier gates remain in source recipes.
- **dressmakers 1.8.0** — asset recipes + YAML. Class/common/Nadiyan recipes and wool alternatives enabled. Enabling class clothing recipes does not remove requiresTrait gates.
- **shearlib 1.3.0** — installed dependency; runtime unresolved. Shearing infrastructure installed; not independently a player-facing process page.

## Exact native guides

- `apiculture-fromgoldencombsguide` — `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/FromGoldenCombs-1.22-v2.1.0.zip!/assets/fromgoldencombs/config/handbook/FGC01-fromgoldencombsguide.json`
- `butcheringguide` — `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/butchering_1.14.3.zip!/assets/butchering/config/handbook/01_butcheringguide.json`
- `butcheringinfo` — `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/butchering_1.14.3.zip!/assets/butchering/config/handbook/02_butcheringinfo.json`
- `craftinginfo-dyemaking` — `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets/survival/config/handbook/24-dyemaking.json`
- `craftinginfo-leathermaking` — `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets/survival/config/handbook/26-leathermaking.json`
- `craftinginfo-mealcooking` — `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets/survival/config/handbook/04-mealcooking.json`
- `gamemechanicinfo-animalhusbandry` — `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets/survival/config/handbook/08-animalhusbandry.json`
- `gamemechanicinfo-farming` — `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets/survival/config/handbook/06-farming.json`
- `gamemechanicinfo-foodpreservation` — `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets/survival/config/handbook/07-foodpreservation.json`
- `langstrothguiden-fromgoldencombsguide` — `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/FromGoldenCombs-1.22-v2.1.0.zip!/assets/fromgoldencombs/config/handbook/FGC03-fromgoldencombs-langstrothguide.json`
- `livestockwelfare-guide` — `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/livestockwelfare_0.16.0.zip!/assets/livestockwelfare/config/handbook/livestockwelfare.json`
- `pollination-fromgoldencombsguide` — `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/FromGoldenCombs-1.22-v2.1.0.zip!/assets/fromgoldencombs/config/handbook/FGC02-fromgoldencombs-pollination.json`
- `wool:gamemechanicinfo-animalshearing` — `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/wool-1.9.6.zip!/assets/wool/config/handbook/animalhusbandryshearing.json`
- `wool:gamemechanicinfo-woolknitting` — `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/wool-1.9.6.zip!/assets/wool/config/handbook/woolknitting.json`

## Process drafts

### Fields → food, feed and animal care (`process-farming-animal-care`)

Reserve seed before turning crops into food or feed. Art of Growing adds bundle processing; not all Livestock Welfare modules are enabled.

- **Prepare a field:** Hoe suitable soil, provide water and protect crops from animals. Match crops to temperature and nutrient needs; rotate crops or replenish depleted nutrients.
- **Dry the harvest:** Mature rye drops wet grain bundles under the installed crop patch. Shift-right-click bundles onto the ground and wait for them to dry.
- **Thresh and allocate:** Use a stick on a placed dry bundle stack to separate grains from straw. Keep seeds for replanting. The knife-plus-rye-seed crafting recipe yields rye grain for the kitchen.
- **Feed appropriately:** Stock a trough with species-appropriate feed. Art of Growing adds rye seeds to the large trough; this does not prove every animal accepts that feed.
- **Maintain living conditions:** Welfare Hygiene, Weather and Thirst modules are enabled. Provide clean surroundings, weather protection and water; its Safety settings allow animal deaths. These are not decorative animal-care rules.
- **Do not impose disabled breeding rules:** Breeding.Enabled is false. RequireBedding=true and pregnancy/portion values are inside that disabled module: do not teach them as active requirements. This does not establish that all breeding is disabled; Better Husbandry and GeneLib are installed too.
- **Return nutrients:** Natural Fertilizer supplies manure slurry and compost barrel recipes. Follow their item recipes before applying fertilizer; an installed recipe does not prove every animal produces its ingredients.

Unknowns: Manual Harvesting, Better Husbandry, GeneLib, Catch Livestock and DairyPlus DLL interactions are not live-tested; no fertility, capture or milk schedule is asserted.

### Carcass → kitchen, bones and leather (`process-butchering-leather`)

Process animals at the installed stations, then keep food handling separate from the hide barrel line.

- **Bring the body home:** Right-click a supported dead animal to carry it in a free bag slot; small bodies fit ordinary inventory slots. Field harvesting sacrifices yield. FieldHarvestingLightMode is false.
- **Skin and bleed:** Place the body on a skinning hook. Hold right-click with a knife to skin it; wait for its bled-out color before removing it. Work promptly because bodies rot on stations.
- **Butcher and sort:** Put the bled-out body on a butchering table and process it with a knife or cleaver. Break the remaining bone carcass for bones. Send meat to cooking/preservation and skins to the appropriate hide recipes.
- **Render fat:** The installed pot recipe uses two litres water and two raw primemeat to make one rendered fat. Its output is fat-rendered, not the generic fat link in the older guide prose.
- **Soak and scrape:** Make limewater from lime and water. Seal a compatible raw hide in the limewater recipe. Craft the soaked hide with a knife in the grid to obtain scraped hide; species-specific recipes convert supported skins to size-based scraped hides.
- **Tan twice:** Make weak tannin with oak or acacia logs and water; use another log with weak tannin for strong tannin. Seal scraped hide in weak tannin for 72 hours; then prepared hide in strong tannin for 108 hours. A small hide needs two litres per stage and yields one plain normal leather.
- **Use leather:** Use plain normal leather in the relevant equipment recipe. Sewn body/leg armor also requires flax twine. Do not substitute pelts or arbitrary species skins for generic tanning ingredients.

Unknowns: No universal conversion from every species skin to generic hide is asserted. Carcass/station and animal-mod interactions were not executed.

### Bees → repeatable honey and wax (`process-bees`)

Keep a populated skep for transferring bees into reusable ceramic or Langstroth hives. The upgraded hives do not propagate like vanilla skeps.

- **Establish bees:** Use the skep route to obtain a populated colony, then move it to a flower-rich apiary. Keep that occupied skep for populating upgraded hives.
- **Build a ceramic hive:** Clay-form and fire a ceramic brood pot and hive top. Transfer the skep colony to the pot and attach the top. The hive retains its colony through the top-harvesting cycle. Interacting with the brood pot while holding the populated skep consumes the skep.
- **Harvest and reset:** When the top is ready, remove it and use a knife to harvest the comb; replace the emptied top. Process comb for honey and wax without destroying the populated brood pot.
- **Prepare Langstroth frames:** Build the base, supers, brood box and rack from the native Langstroth guide. Wax linen and cut it into frame liners; fit liners to empty frames on the rack before production.
- **Assemble and repeat:** Put lined frames in supers, close them and stack them on the base; add the populated brood box on top. Harvest ready frames on the rack with a knife, repair as instructed, then return frames and hive parts.
- **Integrate with fields:** The installed pollination guide describes benefits for nearby crops, berries and fruit trees. Use that native guide instead of assuming vanilla-only bee behavior or one universal effective radius.

Unknowns: Honey yield, pollination reach, timing and frame wear are not live-tested. Native Langstroth text contains inconsistent wax totals; no exact shopping list is repeated. Native prose links to obsolete blue/gray ceramic codes; drafts instead use ashforest states actually declared in installed definitions. Duplicate fancy/new ceramic definitions require loader/runtime checking.

### Flax and fleece → cloth and clothes (`process-textiles`)

Flax passes through bundle processing before spinning. Wool has a separate washable fleece line; cloth, twine and block linen are distinct materials.

- **Dry and thresh flax:** Mature flax drops wet grain bundles. Place and dry them, then thresh the dry stack with a stick. Reserve the recovered flax seed; keep dry flax straw for fiber.
- **Ret, rinse and soften:** Soak dry straw in fresh water. Rinse wet straw using the installed water-container or barrel recipe to obtain clean straw. Hold right-click with clean straw to crumple it, then process crumpled straw with a ridge in the offhand to separate fiber.
- **Spin and weave linen:** Tailor’s Delight adds spindle-assisted flax twine. Four twine around a stick make plain cloth; plain cloth converts to a normal linen block and returns the stick. Respect any displayed class requirement.
- **Shear and wash fleece:** The native guide requires living generation-one-or-higher animals and recommends shears; knives risk injury. Wash generic plain fleece with 0.5 litre limewater for 18 hours to obtain generic plain fibers. Species and colors are separate variants.
- **Spin and weave wool:** Use the enabled spinning recipe; the installed Tailor’s Delight compatibility assets add spindle recipes. Wool twine plus a stick makes portable wool cloth; the square-twine recipe makes a wool block.
- **Sew the selected garment:** Dressmakers recipes may require dyed cloth, leather bundles, colored twine and an awl. The pack enables common/class/Nadiyan clothing and wool alternatives; this is not removal of clothier trait gates.

Unknowns: Conditional compatibility-folder loading and offhand processing need runtime validation. Raw class-restricted recipes do not prove all-class crafting access.

## Evidence limits

Installed guides are authored explanations, asset JSON is static definition/recipe evidence, and ModConfig is requested configuration. Custom DLL execution, conditional folder loading, patch precedence, actual drop resolution and cross-mod compatibility remain untested. JSON classifies these separately rather than upgrading any to runtime proof. The data intentionally does not claim a complete recipe for milk/capture/genetics mods whose executable behavior was not established.
