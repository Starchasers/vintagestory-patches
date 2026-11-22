# Food handbook coverage and inventory review

## Verdict and limits

Every one of the **63 selected provider rows** has a scoped decision below. All **141 manifest providers** retain their audit classification and archive/evidence provenance in `coverage-status.json` → `manifest_inventory`: 63 food-related and 78 unrelated. “Unrelated” is the source audit scope decision, not a statement that the mod cannot affect gameplay.

**No provider is fully validated. No client or upstream assembly was executed.** This is an original static-evidence review, not a runtime certification, copied guide, or replacement recipe catalogue. This provider review is separate from production compile/own-code test results in `VERIFICATION.md`; those results are not live-client coverage.

The 0.3.0 addon redirects one helper call in ACA's Ingredient-for postfix, before generic-heading insertion: **independent localized Simmering/Kneading/Mixing sections, with the migrated helper contribution absent Ingredient for**. Native/unrelated ingredient relations, Created by, meal widgets and callbacks remain intact. No global category, catalogue, new recipes, tooltips, duplicate output lists or upstream asset text.

**Expanded EF source policy implemented, runtime acceptance NOT COMPLETE:** the native grid/barrel adapter classifies 205 grid + 30 barrel declarations (232 candidates, two nonfood exclusions and one unrelated-mod exclusion). Attribute operation nodes: 390 EF + 26 ACA + 13 EF Chef's Tricks (nodes overlap and are not unique recipes); existing native named process rendering is retained. EF's 330 recipe patch operations have unresolved runtime effects. See `NATIVE-SEAMS.md` and `native-operation-ledger.json`.

`recipe-ledger.json` maps 1,191 relevant recipe/patch rows, including every one of the 626 supported ACA-registry source declarations (203 simmering, 420 kneading, 3 mixing), plus 429 relevant attribute nodes and saved code-generation blockers. Every runtime destination remains explicitly unresolved; source declarations are not loaded recipe counts. Recipe activation/config, CooksInto exclusions and wildcard/template expansion are not inferred. Provider tables below retain the original static inventory; they do not certify wider relocation coverage.

## Status vocabulary

- **implemented** — narrowly scoped addon work described above, not provider-wide completion or a runtime pass.
- **native** — authored guides or process-specific native descriptions are statically evidenced; this does not establish every item, action, link or enabled condition.
- **needs-runtime** — relevant declarations/code exist, but resolved interaction/navigation evidence is needed before declaring a gap or implementing more.
- **unsupported** — no dedicated addon adapter is supplied for this infrastructure/support/compatibility role. This does not mean upstream lacks native help or is broken.

All four categories still require runtime checks. These are primary review dispositions, not mutually exclusive descriptions of upstream capabilities. Additional gaps are deferred unless process-specific safe evidence justifies a narrowly bounded change.

Primary dispositions: **implemented: 1**, **native: 19**, **unsupported: 14**, **needs-runtime: 29**.

## Audit provenance and reconciled numbers

Source audit: `/home/brunette/.hermes/cache/scratch/food-handbook-audit`. `FINDINGS.md` was read in full, together with `verification.json`, every matrix row and the complete provider classification inventory. SHA-256 digests of those source files are retained in the companion JSON for reproducibility.

The audit identifies pack SHA-256 `daafd4c1cb217b2553711128adfbd71511f5f0734cfb2927a6c4ffea02e0bb2d` and raw evpack.json SHA-256 `4b95dd815453356a543ebf4e8ba79ccd04f9bcbd5e83a5cf7de33f537d730cc1`. These are reported provenance, not a new archive rehash or authenticity signature. All 141 manifest archive comparisons matched and all source hash formats passed.

| Audit measure | Count |
|---|---:|
| `manifest_providers` | 141 |
| `manifest_unique_mod_ids` | 141 |
| `classified_food` | 63 |
| `classified_unrelated` | 78 |
| `manifest_hash_checks` | 141 |
| `manifest_hash_matches` | 141 |
| `all_hash_formats_valid` | true |
| `assets` | 728 |
| `item_declarations` | 381 |
| `block_declarations` | 347 |
| `item_files` | 378 |
| `block_files` | 338 |
| `recipe_files` | 1079 |
| `parsed_recipe_files` | 1078 |
| `parsed_recipe_declarations` | 4528 |
| `unparsed_recipe_files` | 1 |
| `patch_files` | 983 |
| `patch_declarations` | 6450 |
| `operation_property_records` | 8661 |
| `authored_guides` | 35 |
| `unique_authored_guide_destinations` | 35 |
| `guide_href_occurrences` | 294 |
| `unique_guide_href_destinations` | 206 |
| `parse_errors` | 5 |
| `coverage_provider_rows` | 63 |
| `upstream_dlls_in_output` | 0 |

Recipe files reconcile as 1,079 = 1,078 parsed + 1 unparsed; 4,528 counts parsed root declarations, not files or runtime recipe instances. The 728 asset declarations comprise 381 item and 347 block roots; declaration and file counts differ. Selected providers retain nonfood definitions. Patch declarations and overlapping operation-property matches are not unique player actions. Wildcards, variant groups and ByType maps are not expanded runtime stacks. Guide counts establish authored destinations, not semantic coverage; the 206 distinct href targets are not all resolved collectible pages. Base-game assets are not bundled in this audit.

## Parse failures, retained without repair

Four localization files and one truncated recipe failed parsing. No declaration count is fabricated for the unparsed recipe, and no upstream source is repaired by this addon review. Exact source failures from `parse_errors.json`:

| Source | Parser diagnostic |
|---|---|
| `dairyplus/assets/dairyplus/lang/fr.json` | `<string>:219 Unexpected "\r" at column 362` |
| `stonebakeoven/assets/stonebakeoven/lang/ja.json` | `<string>:8 Unexpected "\r" at column 122` |
| `stonebakeoven/assets/stonebakeoven/lang/pt-br.json` | `<string>:4 Unexpected "\r" at column 112` |
| `stonebakeoven/assets/stonebakeoven/lang/uk.json` | `<string>:4 Unexpected "\r" at column 109` |
| `tankardsandgoblets/assets/tankardsandgoblets/recipes/grid/alcoholbandage.json` | `<string>:187 Unexpected end of input at column 1` |

Audit originals remain in `unparsed_sources`, with the malformed recipe tracked by `unparsed_inventory.json`. These failures are not automatically evidence of the game's runtime behavior or proof that all other translations work.

## All selected providers

Each row is joined by exact `mod_id` to both source inventories. `coverage-status.json` retains per-row audit counts, exact authored guide destinations, candidate-method counts and evidence pointers. Counts of code symbols/methods are static leads only. No row has `full_provider_validated` or `runtime_verified` set to true.

| Provider | Role | Disposition | Scoped review / deferred checks |
|---|---|---|---|
| `aculinaryartillery` | food-processing | **implemented** | One helper call in the ACA Ingredient-for postfix is redirected to independent localized Simmering/Kneading/Mixing sections. Migrated ACA contributions are absent generic Ingredient for; native/unrelated relations and meal callbacks remain intact. Other ACA/EF families are NOT COMPLETE. |
| `artofgrowingpatch` | crop-production | **native** | Native item/block help covers harvesting and tool interactions. No separate guide is required to count that help; crop variants and custom interactions still need client checks. |
| `attributerenderinglibrary` | food-compatibility-library | **unsupported** | Rendering/nutrition support library; no dedicated addon adapter or independent player process is supplied. |
| `egocaribautomapmarkers` | food-foraging-information | **needs-runtime** | Food-resource markers are relevant acquisition support, not recipes. Marker behavior and handbook relationships need runtime inspection. |
| `betterhusbandry` | animal-husbandry | **needs-runtime** | Milking and livestock patches warrant interaction/config checks; the static patch census does not establish handbook coverage. |
| `bettertraders` | food-production-worldgen | **needs-runtime** | Outpost crop/farmland/trough schematics explain inclusion. Worldgen presence does not establish a handbook process or an addon integration. |
| `bloodtrail` | hunting-support | **needs-runtime** | Prey tracking patches are acquisition support; neither patch count nor a blood effect proves handbook guidance. |
| `bricklayers` | food-vessels | **native** | Three native guides and vessel recipes are retained. Ceramic/glass guidance is not a food-item-by-item coverage verdict. |
| `butchering` | animal-food-processing | **native** | Two native guides document carcass workflows. Actual carcass states, drops and enabled integrations remain runtime work. |
| `carryon` | food-equipment-transport | **needs-runtime** | Quern carryability is the food-specific connection. Carry interactions and storage side effects are not implemented here. |
| `cartwrightscaravan` | food-storage-transport | **needs-runtime** | Cart food-spoilage and butchery-storage declarations need resolved storage/transport checks, not inferred recipe navigation. |
| `cartcarryonpatch` | food-storage-transport-compatibility | **unsupported** | Barrel transport compatibility is conditional on both mods; no dedicated addon patch or new food process is supplied. |
| `catchlivestock` | animal-husbandry | **needs-runtime** | Basket capture is a custom husbandry interaction; supported animal states and client help require runtime checks. |
| `chiseltools` | food-storage | **native** | A native guide exists; food-category spoilage declarations require actual storage-state confirmation. |
| `yangcoifix` | drink-compatibility | **unsupported** | Drink-container interaction ordering is a compatibility fix, not a handbook adapter implemented by this addon. |
| `commercially` | food-trade-compatibility | **needs-runtime** | Food-container trade code is statically indicated. Shop transfer, container contents and handbook behavior are unverified. |
| `configureeverything` | food-configuration-support | **unsupported** | Configuration infrastructure can alter crop, nutrition and perish behavior; no adapter or live settings validation is supplied. |
| `containersbundle` | food-storage | **needs-runtime** | Food cupboards and spoil-speed code are inventory evidence. Do not infer a cold shelf or validated preservation behavior. |
| `coreofartspatch` | food-processing-compatibility | **native** | One native liquid-mixing guide exists. Its process remains separate from ACA ingredient-helper changes. |
| `dairyplus` | dairy-processing | **native** | Three native guides plus description/code evidence distinguish skimming, churning and cheese stages. Aging is not ordinary grid crafting; French localization has a parse failure. |
| `danatweaks` | food-workflow-tweaks | **needs-runtime** | Crock crafting and handling/hunger hooks are relevant; enabled tweaks and resulting handbook UI remain unverified. |
| `equusdestrier` | animal-husbandry | **needs-runtime** | Feeding, milking and manure compatibility justify inclusion. Live animal behavior and config conditions remain unverified. |
| `equusferus` | animal-husbandry | **needs-runtime** | Wild-horse feeding, milking and fertilizer integrations need live registration/config checks; no riding-only exclusion was made. |
| `efchefstricks` | animal-feed-compatibility | **native** | A separate native module guide exists, including animal-feed relevance. Do not collapse this module into Core coverage. |
| `expandedfoods` | food-content-core | **native** | Four native guides and ACA-backed recipes already provide downstream evidence. Bone already has broth/pre-gelatin links; no duplicate output list is added. ACA-hook benefits are conditional, not provider-wide validation. |
| `efmealsmodule` | food-meals | **native** | Its own native guide and cooking recipes remain native. ACA meal page construction/navigation must not be replaced by a simple output-stack link. |
| `em` | food-storage-compatibility | **needs-runtime** | FoodShelves glass-jar compatibility explains selection; unrelated industrial recipes remain inventoried without being called edible food. |
| `extrainfo` | food-handbook-information | **needs-runtime** | Native information hooks concern feed targets and cooking progress. Their coexistence with handbook patches needs a client check. |
| `foodshelves` | food-storage | **native** | The native storage guide is retained. Jar restrictions, display and preservation conditions still require resolved assets. |
| `footprints` | hunting-support | **needs-runtime** | Prey footprints support hunting, not a new recipe. Tracking visibility and player guidance remain runtime questions. |
| `fromgoldencombs` | apiculture-crop-production | **native** | Three native guides distinguish hive work and pollination. Crop integrations and hive output still need runtime validation. |
| `genelib` | animal-husbandry-library | **unsupported** | Husbandry genetics/lactation infrastructure is retained in scope but has no dedicated addon adapter. |
| `handbookoptimizer` | food-handbook-compatibility | **unsupported** | Food indexing and recipe-matching compatibility is relevant infrastructure. No dedicated adapter is supplied; cache/patch coexistence belongs on the runtime checklist. |
| `immersivecorpsedrop` | animal-carcass-harvesting | **needs-runtime** | Harvest loot dropping is the documented scope connection. No claim of corpse carrying or complete drop navigation is made. |
| `immersivewoodworking` | food-equipment-compatibility | **needs-runtime** | Fruitpress and food-equipment compatibility are evidenced. Dynamic guide registration and enabled patches need runtime confirmation. |
| `jaunt` | animal-husbandry-support | **needs-runtime** | Animal hunger/eating support is relevant; a bundled sample horse definition is not proof of a registered runtime animal. |
| `kevinsfurniture` | food-workshop-furniture | **needs-runtime** | Stove, meal-container and ripening symbols indicate custom food-workshop behavior; actual handbook guidance remains unverified. |
| `livestockwelfare` | animal-husbandry | **native** | A native animal-care guide exists. Welfare, malnutrition and food-bowl state still require runtime checks. |
| `manualdishes` | food-vessel-cleaning | **native** | Native descriptions and wash interaction evidence exist without a standalone guide. Cleaning is a held/world interaction, not a missing grid recipe. |
| `manualdough` | food-processing | **needs-runtime** | Held-use dough production is statically evidenced. Export config sets DisableDoughRecipes=true and both timing values to 1000; active config and input/output stages remain unverified. |
| `manualharvesting` | crop-harvesting | **needs-runtime** | Crop-aware harvesting code is present, but resulting help and interaction targets need runtime inspection. |
| `morepiles` | animal-husbandry-equipment-storage | **unsupported** | Henbox stacking is equipment storage only; no addon adapter or edible-food-pile feature is claimed. |
| `naturalfertilizer` | crop-fertilization | **needs-runtime** | Dung, compost and fertilizer need state/transition checks. Four static recipe declarations do not enumerate custom crop effects. |
| `pewter` | food-vessels | **needs-runtime** | Meal and liquid vessel declarations exist. Contents, preservation and resolved variants need runtime validation. |
| `pagoldsilver` | food-vessel-compatibility | **needs-runtime** | Food-vessel variants inherit Pewter integration conditions; no provider-wide navigation guarantee follows from static recipes. |
| `realsmokeartofgrowingfirepit` | food-firepit-compatibility | **unsupported** | Visual firepit compatibility only. No addon adapter and no inferred smoking/preservation process. |
| `rusticcookery` | food-processing | **native** | Native descriptions and four code-registered meals are evidenced despite no guide JSON. Roasting, braising, eggs and smoking need separate runtime stages, not a copied recipe list. |
| `shearlib` | animal-husbandry-library | **unsupported** | Living-animal harvesting library retained for husbandry provenance; no dedicated addon process adapter. |
| `shelfobsessed` | food-display-compatibility | **needs-runtime** | Food/vessel shelf patches require conditional target resolution. Their static presence is not proof of active display behavior. |
| `simpletea` | beverages | **native** | The native beverage guide and cooking declarations are retained; actual portions and navigation remain runtime checks. |
| `slowtox` | beverage-compatibility | **unsupported** | Beverage/intoxication compatibility is statically relevant, but has no dedicated addon adapter or validated safety UI. |
| `statushudcont` | nutrition-information | **unsupported** | Nutrition HUD is an information adjunct, not a handbook process implemented here. |
| `stickroast` | food-processing | **native** | Native roast-fork descriptions cover loading, heating and raw/cooked/burned states. Client interaction and stage visibility remain unverified. |
| `stonebakeoven` | food-processing | **native** | A native oven guide exists; cooking-top/oven state needs runtime checks. Japanese, Brazilian Portuguese and Ukrainian localization parse failures remain upstream. |
| `tankardsandgoblets` | beverage-vessels | **needs-runtime** | Beverage/milking vessels are inventoried; the truncated alcoholbandage recipe remains unparsed, with no fabricated declaration or upstream repair. |
| `terraprety` | food-foraging-worldgen | **needs-runtime** | Mushroom worldgen supports foraging. Spawn/harvest results and navigation cannot be proven from patches alone. |
| `toolsmith` | food-tool-compatibility | **native** | Seven native guides exist; rolling-pin/hoe/scythe compatibility is food-relevant but not proof every tool process is covered. |
| `universaldisplaylib` | food-display-library | **unsupported** | Perish information/display infrastructure remains in scope; no dedicated addon adapter or storage behavior verification. |
| `vinconomy` | food-trade-storage | **needs-runtime** | Meal transfer and shop decay behavior require container-content and active-config testing; static symbols are not validated trade workflows. |
| `windowdisplaypatches` | food-display-compatibility | **needs-runtime** | Beverage-vessel compatibility is evidenced. The empty vanilla-food patch contributes no active food additions. |
| `wool` | animal-husbandry-crop-fertilization | **native** | Two native guides and fertilizer recipes justify broad husbandry/crop scope. Textile content is retained without relabeling it as food. |
| `infinitumcore` | food-pickup-support-library | **unsupported** | Cooking-container/quern pickup infrastructure has no dedicated addon adapter. |
| `infinitumpickuphud` | food-processing-information | **unsupported** | Pickup reporting for pots/firepits/querns is an information adjunct; no handbook process adapter is implemented. |

## Navigation and deferred gaps

Bone already has native downstream ingredient links to broth portion/pre-gelatin through ACA. The safe change clarifies the process and targets the existing exact stage, not a second list or an invented final product shortcut. Native Created by and meal recipe navigation remain owned by upstream.

Guide JSON absence alone does not establish missing help: Art of Growing, Rustic Cookery, Manual Dishes and StickRoast have native description/interaction evidence. Manual Dough's exported configuration disables ordinary dough recipes; custom held use must not be inferred from a missing recipe file. DairyPlus aging, cleaning, harvest and carcass interactions must remain distinct from grid recipes. Dynamic Immersive Woodworking guide registration remains unresolved.

No additional feature code is justified by this review. Conditional patches, active configuration, arbitrary code-generated processes, runtime variant resolution and handbook/search widgets remain unproven. Any future addition needs a reproducible process-specific gap and a safe resolved target first.

## Runtime acceptance checklist — not executed

- [ ] Not executed: load the exact pack in an authorized client; record game, ACA, addon, Harmony and dependency versions plus active configs and logs.
- [ ] Confirm the sole hook redirects exactly one helper call inside ACA GetHandbookIngredientForPatch.Postfix, applies once, and fails safely for unexpected method/IL shape; verify unload/unpatch behavior.
- [ ] Open actual kneading, simmering and mixing ingredient entries; confirm independent sections, migrated contributions absent Ingredient for, no orphan generic heading, exact existing intermediate/output stage destinations, back navigation and stack attributes. Do not substitute wildcard navigation candidates for resolved pages.
- [ ] For bone, confirm existing broth portion and pre-gelatin links remain singular and open the intended stages; follow later stages separately rather than inventing a direct final-output recipe.
- [ ] Open mixing entries and confirm their existing meal widgets are relocated to the separate Mixing section and absent generic Ingredient for. Check native meal page components, serving/ingredient attributes, click delegates and Created by navigation are unchanged.
- [ ] Compare enabled versus disabled addon views: no extra recipes, tooltips, duplicate ingredient/output lists or gameplay mutation; native cooking/barrel/grid recipes remain intact.
- [ ] Check Handbook Optimizer caches, Extra Info, attribute rendering and pickup/display libraries together; repeat searches after recipe registration and page/cache rebuilds.
- [ ] For all selected providers, resolve conditional patches, dependencies, variants and ByType maps with the asset manager; compare actual item/block pages, guides, custom interactions and code-generated recipes.
- [ ] Exercise Manual Dough active config/held use, Manual Dishes wash contexts, DairyPlus cheese/churn/skim stages, Rustic Cookery meals and roast states, StickRoast states, Art of Growing harvests and dynamic Immersive Woodworking guides.
- [ ] Inspect food storage/perish conditions, transport, carcass processing, husbandry, feed, crop/fertilizer/pollination and beverage/trade integrations separately; guide existence alone does not pass these workflows.
- [ ] Check English and affected locales; observe actual game handling of the five malformed sources without modifying upstream data. Record exact failures and missing translations.
- [ ] For each future verified case record provider/version/config, resolved source and target page identity, observed behavior, logs and pass/fail. Do not upgrade an entire provider from a single successful case.

## Machine-readable companion

`coverage-status.json` contains all 63 decisions and all 141 compact manifest provenance records, including unrelated-provider classifications. It retains identifiers, archive filenames, source hashes and evidence paths, but not upstream guide bodies, localization prose, asset definitions or binaries. External audit paths are provenance references and may not exist on another machine. This review does not redistribute upstream assemblies or establish digital authenticity.


## 0.3 native scope update

Native grid/barrel and food transition-key adapters now exist; previous statements that those families have no adapter are superseded. See NATIVE-SEAMS.md, native-operation-ledger.json and REFINEMENT-BLOCKERS.md. Source-policy tests and metadata checks are real; live client acceptance remains unverified.
