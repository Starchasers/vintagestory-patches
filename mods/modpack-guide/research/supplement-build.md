# Building, decoration and metal/equipment supplement

**Scope:** 17 authoritative IDs, three practical page drafts, 22 unique statically verified native item/block references. All 17 enabled archive hashes match evpack.json. No DLL loaded/executed; no shared research files edited.

## Classification ledger

| Mod ID | Pack version | Classification | Config evidence |
|---|---|---|---|
| `barkbeetle` | 1.0.6 | cross-link / resource cue | No dedicated matching export found |
| `chiseltools` | 1.17.9 | native guide + construction cross-link | qpchiseltoolserversettings.json |
| `fallingtree` | 0.5.2 | existing woodworking-guide safety amendment | fallingtree.json |
| `heraldry` | 2.0.0 | decorative-process core station | No dedicated matching export found |
| `heraldrybanners` | 2.0.0 | decorative-process product | No dedicated matching export found |
| `capes` | 2.0.0 | decorative wearable product | No dedicated matching export found |
| `manualshingle` | 1.0.4 | construction material acquisition amendment | ManualShingleConfig.json |
| `oneroof` | 1.12.0 | construction-process core material | OneRoofConfig.json |
| `pewter` | 1.5.0 | equipment/metal-process supplement | No dedicated matching export found |
| `pagoldsilver` | 1.1.0 | metal/tableware extension | No dedicated matching export found |
| `quiversfork` | 0.9.32 | equipment-process supplement | quiversandsheaths.yaml |
| `sawchiseling` | 0.1.2 | chiseling tweak / bounded caveat | No dedicated matching export found |
| `scaffolding` | 1.3.1 | construction-process utility | scaffolding.json |
| `stoneharvesting` | 1.2.2 | construction raw-material amendment | StoneHarvesting.json |
| `windchimes` | 1.5.4 | decorative-process product | windchimes-client.json, windchimes-server.json |
| `windowdisplay` | 1.1.4 | decorative-process product + display station | universaldisplaylibconfig.json |
| `windowdisplaypatches` | 1.0.9 | display compatibility content; co-cover with Window Display | No dedicated matching export found |

## Important boundaries

- `windowdisplaypatches` is content-only JSON compatibility, with direct Pewter display patches; it is not a bundled Harmony DLL or a separate crafting station.
- `pagoldsilver` really adds **both gold and silver**. Keep full member paths: gold/silver and block/recipe basenames collide. Both sets are preserved in JSON evidence.
- `manualshingle` metadata establishes RMB use; config documents three-second/two-shingle batches and two-clay-hand bowls, but does not establish a brick/sneak recipe.
- `sawchiseling` is universal/required on both sides. Metadata explicitly permits saw-based wood chiseling; JSON patches supply the saw animation.
- `capes` does not declare a dependency on Heraldry Banners. Actual dependencies are retained literally in JSON.
- Raw blue vanilla shingles fire to black. The practical roof example uses red, whose One Roof variant is statically present.
- Config exports are not proof of live application. No durability formula, safe scaffold span, collision injury, greenhouse sealing or code-only gesture was invented.

## Practical page drafts

### `process-building-materials` — Building: scaffolding, roofing and carved details

Prepare access and roofing material before finishing the building. Manual Shingle changes acquisition; shingles still need firing. One Roof supplies its own roof blocks, while the shipped Chisel Tools guide handles detailed voxel work.

**1. Craft scaffolding**
Open the native recipe: BBB / SRS / S_S, with planks B, sticks S and rope R. It yields four scaffolds. The exported MaxStability=6 is not proof of a safe six-block unsupported span; do not design around that assumption.
Native references: `block:scaffolding:scaffolding-top-ns`
Evidence keys: `scaffold`, `scaffolding:scaffolding.json`

**2. Make and fire shingles**
Manual Shingle adds right-mouse-button shingle/bowl making. The export specifies three-second batches, two shingles per batch and no required table. Its assets do not establish the exact shingle hand setup, so no brick/sneak instruction is supplied here. Raw red shingles fire to burned red at the native 850°C threshold. Raw blue instead fires to black, not blue.
Native references: `item:game:shingle-raw-red`, `item:game:shingle-burned-red`
Evidence keys: `manual-meta`, `manualshingle:ManualShingleConfig.json`, `native-shingles`

**3. Assemble One Roof roofing**
For red clay use __S / _S_ / S_S: four burned red shingles yield four redclay-standard roof blocks. Separate one-for-one recipes switch standard ↔ dynamic. Use the actual material code, not an invented generic roof-clay. The mod advertises automatic roof building; this export does not explain its placement shortcuts.
Native references: `block:oneroof:oneroof-redclay-standard-none-free`, `block:oneroof:oneroof-redclay-dynamic-none-free`
Evidence keys: `roof`, `roof-convert`

**4. Obtain stone and shape details**
Stone Harvesting explicitly supports obtaining full rock blocks with hammer and chisel. Saw Chiseling permits wood chiseling with a saw and supplies sawing animation patches. For repeated details open the shipped Chisel Tools guide: the pantograph copies/pastes and saves shapes; the hand planer removes or adds groups of voxels. Exact harvesting gestures and wear costs remain unverified.
Native references: `item:chiseltools:handplaner-iron`, `item:chiseltools:pantograph-steel`
Evidence keys: `stone-meta`, `stoneharvesting:StoneHarvesting.json`, `saw-meta`, `chisel-guide`, `chisel-text`

Guides: `process-wood-to-workshop`, `process-powders-to-ceramics`, `craftinginfo-clayforming`, `chiseltools:chiseltools-guide`
Mods: `manualshingle`, `oneroof`, `scaffolding`, `stoneharvesting`, `chiseltools`, `sawchiseling`

### `process-heraldic-decor` — Decoration: heraldry, windows and chimes

Planks, cloth, rope and dyes support several decorative products. Make the base item before customizing it at the Heraldry Workbench. Display windows and chimes do not replace dye production or establish room insulation.

**1. Build the Heraldry Workbench**
Gather planks, sticks, resin and plain cloth using the native workbench recipe. Its hammer and chisel are tools with declared durability costs of 20 and 50. Bark Beetle adds an ambient sound to resin-bearing logs and can help locate the resin ingredient.
Native references: `block:heraldry:heraldryworkbench-east`
Evidence keys: `heraldry-table`, `resin`

**2. Make a base and apply heraldry**
Both banner and cape recipes use one plank and two plain cloth pieces. Put the product in the workbench input. Its UI identifies liquid containers holding dye, or water as bleach, and separate Patterns/Shapes tabs. Colors/layers are item attributes, not separate red-cape collectible codes. Follow the native dye-making guide for the liquid supply.
Native references: `item:heraldrybanners:banner`, `item:capes:cape`
Evidence keys: `banner`, `cape`, `heraldry-controls`

**3. Craft a window and arrange the display**
Four planks in _A_ / A_A / _A_ yield customwindow-plain. Shared Universal Display Library config documents Raise/lower display item (default Left Shift) + mouse wheel, with a 0.25-voxel step. Precise placement is enabled and item overlap disabled in this export. RoomSafeOpening=false does not establish greenhouse/cellar sealing. Window Display Patches also adjusts placement particles for Pewter lamps.
Native references: `block:windowdisplay:customwindow-plain`
Evidence keys: `window`, `windowdisplay:universaldisplaylibconfig.json`, `windowdisplaypatches:assets/windowdisplaypatches/patches/pewter.json`

**4. Add a chime**
The wooden wind chime needs rope, one plank and five sticks: _A_ / _B_ / CDC, with two sticks in each C slot and one in D. The copper version uses two bottom slots of two copper rods instead. It is an audio decoration, not a wind meter. Client config controls volume; this export sets main multiplier 3 and maximum distance 16.
Native references: `block:windchimes:windchime-wood`, `block:windchimes:windchime-copper`
Evidence keys: `chimes`, `windchimes:windchimes-client.json`

Guides: `process-textiles`, `process-wood-to-workshop`, `craftinginfo-dyemaking`
Mods: `heraldry`, `heraldrybanners`, `capes`, `barkbeetle`, `windowdisplay`, `windowdisplaypatches`, `windchimes`

### `process-tableware-equipment` — Workshop: metal tableware and an arrow quiver

Pewter extends casting into tableware, not a new weapon tier. Prepare the alloy and mold before final assembly. Quivers And Sheaths combines leather and a metal buckle into ammunition-storage equipment.

**1. Prepare the alloy and mold**
Pewter requires tin 80–95%, lead 5–10% and copper 1–5%, all within the same alloy batch. A 90/7/3 split satisfies the recipe. Clayform the Tableware Bowl Mold from blue, red or fire clay using its native pattern, then fire it. The shown blue raw/fired molds are actual native codes; clayforming is not an ordinary crafting-grid recipe.
Native references: `item:game:ingot-pewter`, `block:pewter:tablewaremold-blue-raw-bowl`, `block:pewter:tablewaremold-blue-fired-bowl`
Evidence keys: `alloy`, `pewter-metal`, `bowl-mold`, `raw-molds`

**2. Cast tableware, then assemble**
The bowl mold declares 100 metal units and a four-bowl output. A pewter bowl holds one litre or one meal serving. Bowl + candle makes a candlelamp; matching crock base + lid combine shapelessly into a crock. pagoldsilver adds gold and silver variants in the pewter domain, but static variants do not prove that a running mold accepts those melts: check the native item guidance before committing precious metal.
Native references: `block:pewter:bowl-pewter`, `block:pewter:candlelamp-pewter-up`, `block:pewter:crock-pewter`, `block:pewter:bowl-silver`, `block:pewter:bowl-gold`
Evidence keys: `fired-molds`, `bowl`, `candle`, `crock`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/gold/bowl.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/silver/bowl.json`

**3. Stitch an arrow quiver**
Use the basic LNL / LFL / LLL recipe: plain normal leather, an iron buckle, and an arrow marked as a zero-wear pattern tool rather than a declared consumed ingredient. The output is beltbag-back-quiver-arrows in the quiversandsheaths domain, despite mod ID quiversfork. Asset and exported config indicate three ammunition slots. Colored/material variants change attributes, not this collectible code; this is not a claim of extra ordinary backpack slots.
Native references: `item:quiversandsheaths:beltbag-back-quiver-arrows`
Evidence keys: `quiver`, `quiver-slots`, `quiversfork:quiversandsheaths.yaml`

Guides: `process-ore-to-equipment`, `process-butchering-leather`, `gamemechanicinfo-foodpreservation`, `craftinginfo-smelting`, `craftinginfo-leathermaking`
Mods: `pewter`, `pagoldsilver`, `quiversfork`

## Per-mod findings

### `barkbeetle`
Adds ambient bark-beetle sound to resin logs.
Vanilla delta: Direct patch to vanilla resin-log sounds; not an alternate resin recipe.
Config: No dedicated matching exported config found; do not infer unconfigurable or default runtime behavior.
- Resin feeds Heraldry Workbench recipe; complement, not replacement, for Immersive Woodworking bark handling.
Boundary: Exact sound cadence/range and code-driven additions not established.
Related existing guides: `process-wood-to-workshop`
Evidence keys: `resin`

### `chiseltools`
Native guide describes hand planer, hand wedge, pantograph, ladder/path makers, paint brush/scraper, and chiselable decorative objects.
Vanilla delta: Adds voxel editing/copying/material and paint workflow beyond ordinary chisel.
Config: Exported provider/shared config inspected; not proof of runtime application.
- Saw Chiseling also touches chiseling; do not assert an exact compatibility conflict. Stone Harvesting supplies full blocks; Toolsmith/manual tools affect acquisition, not verified here.
Boundary: Export uses pantographInstantMode=true, enablePantographLeftClickCopy=true, fixedToolWear=false; no IL tracing or exact durability formula verified.
Related existing guides: `process-wood-to-workshop`, `process-ore-to-equipment`
Evidence keys: `chisel-guide`, `chisel-text`, `chiseltools:qpchiseltoolserversettings.json`

### `fallingtree`
Tree-felling mod; exact metadata: Trees now doesn't simply disappear. Exported placement/domino/direction fields inspected.
Vanilla delta: Changes tree-felling presentation/behavior, not a new standalone crafting chain.
Config: Exported provider/shared config inspected; not proof of runtime application.
- Pair with existing woodworking guide and its Immersive Woodworking/rooted-stump safety context.
Boundary: Config exports place=false, placeLeaves=false, domino=false, fallDir=backwards. No claim about injury, collision, item drops, or implemented meaning of flags without code trace.
Related existing guides: `process-wood-to-workshop`
Evidence keys: `falling-meta`, `fallingtree:fallingtree.json`

### `heraldry`
Craftable Heraldry Workbench; UI accepts banners, tabards, caparisons, capes or shields and dye/water containers.
Vanilla delta: Adds heraldic pattern/shape customization, not a replacement for vanilla dye production.
Config: No dedicated matching exported config found; do not infer unconfigurable or default runtime behavior.
- Heraldry Banners and Capes depend on this station; exact dye guide remains craftinginfo-dyemaking. Resin requirement intersects barkbeetle.
Related existing guides: `process-textiles`, `craftinginfo-dyemaking`
Evidence keys: `heraldry-table`, `heraldry-controls`

### `heraldrybanners`
Craft banner from one plank and two plain cloth pieces; supplied pole variants and attribute-driven heraldic layers.
Vanilla delta: Adds customizable hanging/display products.
Config: No dedicated matching exported config found; do not infer unconfigurable or default runtime behavior.
- Depends on heraldry and attributerenderinglibrary; pattern/color attributes must survive any native item slideshow.
Related existing guides: `process-textiles`, `craftinginfo-dyemaking`
Evidence keys: `banner`

### `capes`
Plain cape recipe uses one plank and two plain cloth pieces; wearable asset declares neck clothing category and HeraldryProperties.
Vanilla delta: Adds heraldic capes; no armor benefit claimed.
Config: No dedicated matching exported config found; do not infer unconfigurable or default runtime behavior.
- Heraldry workbench input supported; modinfo dependencies are game 1.22.0, heraldry 2.0.0, attributerenderinglibrary 3.0.0. Heraldry Banners is a companion, not a declared dependency.
Boundary: Item attributes, not separate cape-color collectible codes, carry patterns.
Related existing guides: `process-textiles`, `craftinginfo-dyemaking`
Evidence keys: `cape`, `heraldry-controls`

### `manualshingle`
Modinfo explicitly adds right-mouse-button shingle and bowl making; clay offhand flag patch and exported kneading settings provide static evidence.
Vanilla delta: Adds manual kneading path; exported config also documents two-clay-hand bowl operation. Does not demonstrate removal of vanilla clayforming.
Config: Exported provider/shared config inspected; not proof of runtime application.
- Feeds fired-shingle OneRoof recipes; firing follows native shingle properties / existing clay guide.
Boundary: No source-backed main-hand tool or sneak prerequisite: do NOT invent brick/sneak controls. Exported comments identify two-clay-hand bowl making, main-hand clay use for shingle kneads, 3-second batches, 2 shingles/batch, RequireTable=false. Exact activation/consumption still code-driven and not live-tested.
Related existing guides: `process-powders-to-ceramics`, `craftinginfo-clayforming`
Evidence keys: `manual-meta`, `manualshingle:patch-clay-offhand.json`, `manualshingle:ManualShingleConfig.json`

### `oneroof`
Auto-building roof blocks with direct material recipes and vanilla-roof conversions; standard/dynamic conversion recipes.
Vanilla delta: Adds one roof block system rather than manually selecting every vanilla roof shape.
Config: Exported provider/shared config inspected; not proof of runtime application.
- Manual Shingle feeds clay roofing; woodworking supplies planks and roof trim. BrickLayersCompat=true is only an exported flag, not dependency presence proof.
Boundary: WoodenVariants=true; client auto-shaping controls and generated compatibility recipes not traced.
Related existing guides: `process-powders-to-ceramics`, `process-wood-to-workshop`
Evidence keys: `roof`, `roof-convert`, `oneroof:OneRoofConfig.json`

### `pewter`
Adds leaded pewter alloy and cast tableware molds; bowl is 1 L / 1 serving; bowl+candle makes candlelamp; crock base+lid assembly.
Vanilla delta: Extends smelting into tableware, rather than a stronger tool tier.
Config: No dedicated matching exported config found; do not infer unconfigurable or default runtime behavior.
- Alloy links metallurgy; usable bowls/crocks link existing food storage/kitchen; no lead-poisoning mechanic inferred.
Related existing guides: `process-ore-to-equipment`, `gamemechanicinfo-foodpreservation`, `kuboczochmodpackguide-process-kitchen-dairy`, `kuboczochmodpackguide-process-kitchen-bottles`, `craftinginfo-smelting`
Evidence keys: `alloy`, `bowl-mold`, `fired-molds`, `bowl`, `candle`, `crock`

### `pagoldsilver`
JSON patches add both gold and silver variants to Pewter bowls, candlelamps, candlelabras, crocks, crock parts and jugs, plus assembly recipes.
Vanilla delta: Extends Pewter products, not a new vanilla metal tier.
Config: No dedicated matching exported config found; do not infer unconfigurable or default runtime behavior.
- Depends on pewter:* (no pinned 1.5.0 requirement in addon metadata); inherited Pewter attributes and shared material recipes underpin the variants. Asset namespace remains pewter.
Boundary: Content-only ZIP, no DLL. Variant definitions and recipes verified statically; molten-gold/silver acceptance by Pewter mold class and live patch application were not executed. Do not fabricate alloy proportions for gold/silver.
Related existing guides: `process-ore-to-equipment`
Evidence keys: `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/gold/bowl-meal.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/gold/bowl.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/gold/candlelabra.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/gold/candlelamp.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/gold/crock.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/gold/crockbase.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/gold/crocklid.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/gold/jug.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/silver/bowl-meal.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/silver/bowl.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/silver/candlelabra.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/silver/candlelamp.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/silver/crock.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/silver/crockbase.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/silver/crocklid.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/blocktypes/metal/silver/jug.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/recipes/grid/gold/candlelabra.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/recipes/grid/gold/candlelamp.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/recipes/grid/gold/metalbit.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/recipes/grid/gold/metalcrock.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/recipes/grid/silver/candlelabra.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/recipes/grid/silver/candlelamp.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/recipes/grid/silver/metalbit.json`, `pagoldsilver:assets/pagoldsilver/patches/pewter/recipes/grid/silver/metalcrock.json`

### `quiversfork`
Craftable quivers, sheaths and tool holsters; arrow quiver declares three ammo slots and arrow eligibility tags/wildcards.
Vanilla delta: Adds gear-slot storage rather than ordinary bag capacity.
Config: Exported provider/shared config inspected; not proof of runtime application.
- Exact asset domain is quiversandsheaths, NOT quiversfork; requires overhaulliblegacycompat and attributerenderinglibrary. Leather guide supplies materials. Colored variants remain visible in exported config.
Boundary: Config allow_stones_in_sling_pouch=false and allow_lanterns_in_tool_holster=true; tool holster asset supports lantern with -4 light offset. Equipment UI/hotkey activation not live-tested; do not infer arrow damage or ammo consumed in crafting (recipe marks arrow as zero-wear tool).
Related existing guides: `process-butchering-leather`, `process-ore-to-equipment`, `world-overland`, `craftinginfo-leathermaking`
Evidence keys: `quiver`, `quiver-slots`, `quiversfork:quiversandsheaths.yaml`

### `sawchiseling`
Metadata: Chisel wood blocks with a saw. Universal mod, required on both client and server; patches add sawing animation and replace the saw held-hit animation.
Vanilla delta: Adds saw-based wood chiseling, rather than requiring a newly crafted standalone station.
Config: No dedicated matching exported config found; do not infer unconfigurable or default runtime behavior.
- Link contextual note from Chisel Tools, not an invented tutorial.
Boundary: Precise activation, compatible wood blocks and durability behavior are code-driven; no dedicated matching exported config found. DLL not executed.
Related existing guides: Covered with the new page, not a separate process.
Evidence keys: `saw-meta`, `sawchiseling:assets/sawchiseling/patches/add-sawing-animation.json`, `sawchiseling:assets/sawchiseling/patches/replace-sawing-animation.json`

### `scaffolding`
Recipe produces four scaffolds from plank/stick/rope pattern; asset contains top/bottom/plain connectivity states.
Vanilla delta: Adds dedicated scaffolding; not a claim that ladders become obsolete.
Config: Exported provider/shared config inspected; not proof of runtime application.
- Material supply from woodworking and fibre processing.
Boundary: Export MaxStability=6, speed multipliers=1, ClimbDistance=0.5. No code trace proving a six-block safe horizontal reach or failure physics; omit such advice.
Related existing guides: `process-wood-to-workshop`, `process-textiles`
Evidence keys: `scaffold`, `scaffolding:scaffolding.json`

### `stoneharvesting`
Metadata explicitly supports harvesting full stone/rock blocks with hammer and chisel.
Vanilla delta: Alternative intact-rock acquisition; no drop bonus or resource yield inferred.
Config: Exported provider/shared config inspected; not proof of runtime application.
- Full rock supports masonry/chiseling; changes what mining advice should mention. Export ChiselDurability=true and PerBlockDamage=false implies durability is relevant but exact per-hit loss not established.
Boundary: No gameplay language guide / static recipe; exact click sequence and block eligibility must not be invented.
Related existing guides: `process-ore-to-equipment`
Evidence keys: `stone-meta`, `stoneharvesting:StoneHarvesting.json`

### `windchimes`
Wood, bamboo, copper, brass, crystal/bone wind chime recipes and door chime variants.
Vanilla delta: Adds decorative audio, not a progression machine or weather instrument.
Config: Exported provider/shared config inspected; not proof of runtime application.
- Wooden recipe uses rope/plank/five sticks; metallic recipes consume rods. Floral-region recipes are compatibility assets, not automatically active base recipes.
Boundary: Client export master=3, wood=2, indoor=0.1, range=16; no computed loudness formula claimed.
Related existing guides: `process-wood-to-workshop`, `process-ore-to-equipment`
Evidence keys: `chimes`, `windchimes:windchimes-client.json`, `windchimes:windchimes-server.json`

### `windowdisplay`
Craftable custom window from four planks; metadata advertises animated windows/free-placement display surfaces, using Universal Display Library.
Vanilla delta: Adds window/display objects; does not prove greenhouse/cellar room safety.
Config: Exported provider/shared config inspected; not proof of runtime application.
- Depends on universaldisplaylib >=1.5.0 and windowdisplaypatches >=1.0.7. Shared UDL config includes RoomSafeOpening=false, precise placement true, overlap false.
Boundary: Runtime selection/swap/animation controls not traced; do not claim glazing thermal benefit or room-seal behavior.
Related existing guides: `process-wood-to-workshop`
Evidence keys: `window`, `windowdisplay:universaldisplaylibconfig.json`

### `windowdisplaypatches`
Content-only patch collection; marks vanilla/third-party objects displayable and adjusts placement, including particle offsets for Pewter candlelamps/candlelabras.
Vanilla delta: Extends display placement compatibility, not a separate crafted station.
Config: No dedicated matching exported config found; do not infer unconfigurable or default runtime behavior.
- Depends on universaldisplaylib 1.5.0; Window Display depends on this addon >=1.0.7. Each conditional third-party patch has its own dependsOn, not proof the target mod is installed. Pewter-specific patches directly join the tableware and decor processes.
Boundary: No bundled DLL. Match each patch target against installed assets; do not treat all optional integrations as active.
Related existing guides: Covered with the new page, not a separate process.
Evidence keys: `windowpatch-meta`, `windowdisplaypatches:assets/windowdisplaypatches/patches/pewter.json`, `windowdisplaypatches:assets/windowdisplaypatches/patches/enable-conversions.json`, `windowdisplaypatches:assets/windowdisplaypatches/patches/vanilla/clay-molds.json`

## Validation

- 17 manifest-enabled ZIPs verified against SHA-256; exact metadata/dependencies/member inventory retained.
- 3 pages; 17 individually classified IDs; 22 unique item/block references statically derived from definitions, variant properties and addon patches.
- Every guide code resolves against existing research, native VS handbook JSON, or the native Chisel Tools guide. Every evidence key resolves.
- All archive JSON members parsed; three deeply nested shield models used stdlib JSON after JSON5 recursion limits, without invoking assemblies.
- Exact config snapshots and complete exported-config filename inventory retained in JSON.
- No live game test claimed; page interactions deliberately stop where static evidence ends.
