# World, movement, survival and community research

## Scope and verification

- Source pack: `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted`; exact vanilla assets: `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs` (1.22.7).
- Loaded `vintage-story-handbook-audit`. Static metadata, assets and exported config only; no bundled DLL was executed, no runtime controls were inferred from DLL names.
- All 146 manifest IDs are enabled and all 146 ZIP SHA-256 values matched evpack.json.
- 90 enabled IDs are classified here. Remaining 56 IDs belong to food, materials, farming, crafting, building or equipment research; they are not silently classified as technical exclusions.
- Four draft pages, each below 400 words including titles/stage labels/native guide labels: world-overland=271, world-water=250, world-survival=261, world-trade=293.
- Archive evidence locators use `absolute.zip::member/path`; files use absolute paths. All source locators and all 11 native guide page codes checked present; exact schema and page word limits were checked programmatically.
- Native guides are references, not confirmation of every configured mechanic: the walking-stick guide generalizes speed bonuses incorrectly for the crude config; vanilla armour recipes are disabled by Combat Overhaul. Rough Seas/Quiet Rough Seas swimming-load behaviour was not verified and is omitted.
- Runtime asset registration, DLL patch order and in-game input bindings were not exercised. Statuses deliberately distinguish config/asset evidence from unresolved code workflows.

## Native item references

| Type | Exact code | Evidence basis |
|---|---|---|
| item | `walkingstick:walkingstick-crude` | ZIP recipe exact output and item definition |
| block | `kscartographytable:cartographytable-filled-north` | Recipe exact output; state/side variants |
| item | `kscartographytable:quill` | Native item definition |
| item | `game:rope` | VS 1.22.7 native item definition |
| item | `game:flaxtwine` | VS 1.22.7 native item definition |
| item | `game:bandage-clean` | Native type variant and enabled two-twine recipe |
| block | `coins:mint-east` | Native mint horizontalorientation variant, creative entry |
| item | `coins:wallet` | Native item definition |
| item | `coins:banknote-1` | Native denomination states 1/10/100; no wildcard/base-code preview |

## Plan an overland route and share it

Build a return route before taking a long expedition. This pack changes both terrain and movement; a single movement trick is not a substitute for supplies.

### 1. Make a starter walking stick

Follow the crude-stick recipe: three sticks on a diagonal, with a rope immediately left of the centre stick (pattern __S / RS_ / S__). Its pack config grants AutoStep and a hunger modifier of -5; unlike better sticks, it has no WalkingSpeed entry. Equip it as a walking aid; the native guide describes passive bonuses while equipped.

Items: `item walkingstick:walkingstick-crude`.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/adventurers-walking-stick-net10_3.0.12.zip::assets/walkingstick/recipes/grid/walkingstick-crude.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/adventurers-walking-stick-net10_3.0.12.zip::assets/walkingstick/lang/en.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/WalkingSticks/walkingstick-crude.json`

### 2. Choose a forgiving route

Scout a route back to shelter before trying cliffs. Terrain is affected by Watersheds, Rivers and Terra Prety. Mountain climbing is configured for Stone and Ore materials, with climb stamina cost 5 per second and jump stamina cost 20. Charged jumping has maxCharge 0.15 and sprint-jump disabled. Test movement on low ground; these pages do not prescribe unverified default keybinds.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/ClimbMountain.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/ChargedJumpConfig.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/TerraPrety.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/watersheds_7.0.6.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/rivers_5.0.3.zip::modinfo.json`

### 3. Build a shared waypoint station

Make a cartography table using its native recipe. Take the quill by right-clicking the ink-and-quill on the placed table. With the quill, right-click its map to write your changes; Ctrl + right-click reads the table changes into your map. Each table has its own shared waypoints; deletions are copied too.

Items: `block kscartographytable:cartographytable-filled-north`, `item kscartographytable:quill`.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/KsCartographyTable_v2.0.4.zip::assets/kscartographytable/blocktypes/cartographytable.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/KsCartographyTable_v2.0.4.zip::assets/kscartographytable/itemtypes/tool/quill.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/KsCartographyTable_v2.0.4.zip::assets/kscartographytable/lang/en.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/KsCartographyTable_v2.0.4.zip::assets/kscartographytable/recipes/grid/cartographytable.json`

### 4. Make repeat journeys easier

Use the same path repeatedly to develop a trail. Only players create trails here; TrailDevolveDays is 60 and TrampledSoilDevolveDays is 4, so paths are not permanent. For group exploration, the advanced cartography table also shares explored areas; the ordinary table shares waypoints.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/TrailModConfig.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/NaturalTrailsMod_1.3.21.zip::assets/trailmod/config/handbook/25-trails.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/KsCartographyTable_v2.0.4.zip::assets/kscartographytable/lang/en.json`

Native guides:
- `walkingstick-guide` — Walking sticks
- `sharingwaypointsguide` — Cartography and sharing waypoints
- `gamemechanicinfo-trails` — Trails

Mods: `walkingstick`, `watersheds`, `rivers`, `terraprety`, `climbmountain`, `chargedjump`, `kscartographytable`, `trailmodmaro`.

## Prepare, cross, and secure a vessel

Use the native Rough Seas guides for vessel-specific repairs and bailing. Quiet Rough Seas changes some behaviour, so these are not unmodified vanilla boats.

### 1. Check the water before launching

Watch the wind, treetops, darkening sky and swell. Plan a short crossing with a sheltered landing, and turn back as storms approach. Rough Seas describes lightning and dangerous surf; a boat is not a safe place to wait out a storm.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/roughseas_4.4.0.zip::assets/roughseas/lang/en.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/roughseas_4.4.0.zip::assets/roughseas/config/handbook/01-weather.json`

### 2. Pack for the actual vessel

Read Vessels and Their Repair, inspect the hull, and take its repair materials plus a bowl or bucket for bailing. Different vessels have different repair requirements; do not assume one repair material works for every hull.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/roughseas_4.4.0.zip::assets/roughseas/config/handbook/02-vessels.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/roughseas_4.4.0.zip::assets/roughseas/config/handbook/03-surf.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/roughseas_4.4.0.zip::assets/roughseas/lang/en.json`

### 3. Do not depend on river propulsion

Quiet Rough Seas sets RiversPushBoats to false and both paddling/sculling stroke multipliers to 8. Its metadata also restricts boat hit damage to axes. That does not make a hull stormproof or replace repairs. Swimming/armour-load advice and patch-specific keys are deliberately omitted pending code-level verification.

Additional pack config: `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/quietroughseas.json`.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/quietroughseas_1.5.0.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/roughseas_4.4.0.zip::assets/roughseas/lang/en.json`

### 4. Moor before stepping ashore

With a rope, right-click the vessel and then a fence post ashore. Use the rope tool-mode control to adjust its length; a longer rope permits a wider swing. A sailboat can instead lower its anchor by right-clicking it with an empty hand, and raise it the same way. Storms can drag or break moorings: leave valuables ashore.

Items: `item game:rope`.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/roughseas_4.4.0.zip::assets/roughseas/config/handbook/04-mooring.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/roughseas_4.4.0.zip::assets/roughseas/lang/en.json`
- `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets/survival/itemtypes/resource/rope.json`

Native guides:
- `roughseas-goingtosea` — Going to Sea
- `roughseas-weather` — Wind, Storms and Currents
- `roughseas-vessels` — Vessels and Their Repair
- `roughseas-surf` — Surf, Swamping and Bailing
- `roughseas-mooring` — Mooring and Anchoring

Mods: `roughseas`, `quietroughseas`, `rivers`.

## Prepare a recovery kit before a fight

Combat Overhaul, Armory and Downed change the risk of travel. Prepare healing and a retreat before hunting or fighting; do not treat surviving defeat as free healing.

### 1. Make a small healing reserve

Place two flax twine vertically in the crafting grid to make two clean bandages. Keep them accessible before leaving shelter. The clean bandage is a real healing item, not an ingredient-only preview.

Items: `item game:flaxtwine`, `item game:bandage-clean`.

Evidence:
- `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets/survival/recipes/grid/bandage.json`
- `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets/survival/itemtypes/bandage.json`
- `/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs/assets/survival/itemtypes/resource/flaxtwine.json`

### 2. Check the combat settings

This pack enables melee swings stopping on terrain, while spear directional attacks are disabled. Spear aiming difficulty and thrown-distance settings are both 1. Vanilla armour grid recipes are disabled, so use the current item recipes rather than treating the vanilla armour guide as authoritative. Practise in open ground and use the current weapon controls shown by the game rather than a guessed attack key.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/combatoverhaul.yaml`

### 3. Retreat with a recovery plan

Downed duration is 300 seconds here, self-revive is disabled and the map is hidden while downed. Passed-out recovery is also disabled. Agree where companions should regroup before splitting up. Do not promise automatic recovery: the exact rescue interaction is not established by this config and is not invented here.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/downed.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/downed_2.9.0.zip::modinfo.json`

### 4. Track and recover cautiously

Blood Trail adds blood trails to animals wounded by a player. Follow tracks only when the return route and your remaining healing are adequate. Immersive Corpse Drop makes harvested corpse contents drop without a loot UI, so check the ground after harvesting rather than waiting for a container window.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/BloodTrail-v1.2.13.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/Immersive Corpse Drop 1.0.4 (Vintagestory 1.22).zip::modinfo.json`

Native guides:
- `craftinginfo-armor` — Native armour guide: crafting reference, not proof of current combat controls

Mods: `combatoverhaulfork`, `armoryfork`, `downed`, `bloodtrail`, `immersivecorpsedrop`.

## Agree a currency and make a small trade

Coins and Coinage are separate systems. Player-issued notes are not automatically metal coins or trader gears. Agree on the issuer and denomination before posting a price.

### 1. Found a note currency

Craft and place a Coins mint using its native recipe, then right-click it. Choose a name, a 2–4 letter symbol and a colour. Your first personal currency is free. Mint a small quantity of 1-notes first; unlimited minting does not guarantee anyone will accept them. This pack permits at most three currencies per player.

Additional pack config: `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/coins.json`.

Items: `block coins:mint-east`.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/coins_0.1.0.zip::assets/coins/blocktypes/mint.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/coins_0.1.0.zip::assets/coins/lang/en.json`

### 2. Store money without hiding your payment

Deposit notes by dragging them onto a wallet, or open the wallet and choose Deposit all notes. Withdraw before buying at a note-accepting shop: the Coins guide says shops take loose notes, not wallet balances. Losing the wallet loses its contents.

Items: `item coins:wallet`, `item coins:banknote-1`.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/coins_0.1.0.zip::assets/coins/itemtypes/wallet.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/coins_0.1.0.zip::assets/coins/itemtypes/banknote.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/coins_0.1.0.zip::assets/coins/lang/en.json`

### 3. Make the exact change

A shop asking for five 1-notes will not take a 10-note. Craft one 10-note into ten 1-notes, or one 100-note into ten 10-notes. Different currencies never mix. Use the Coinage guide separately for metal coin-making; no exchange rate between the two mods is established. Coinage requires hammer tool tier 3. Food still decays in Vinconomy shops here (stall perish rate 0.015).

Additional pack config: `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/coinage.json`, `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/vinconomy.json`.

Items: `item coins:banknote-1`.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/coins_0.1.0.zip::assets/coins/lang/en.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/coinage-2.4.0.zip::assets/game/config/handbook/56-guide-coins.json`

### 4. Coordinate a group

At a bound mint the founder can add players under Shared with. They can mint that currency, but cannot rename or transfer it. Use a notice board to advertise issuer, denomination and meeting place; its UI provides Compose notice. Ownership and posting permissions still apply. Tabletop Games is optional social play, not a prerequisite for trading. For Hand to Hand, the config requires the receiver to hold R; one-sided forced handoff is disabled.

Evidence:
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/coins_0.1.0.zip::assets/coins/lang/en.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/NoticeBoard_3.0.4.zip::assets/noticeboard/lang/en.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/TabletopGames-v4.0.1.zip::modinfo.json`
- `/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/config/ModConfig/HandToHandConfig.json`

Native guides:
- `coins-money` — Player-issued money and wallets
- `gamemechanicinfo-coinage` — Metal coin-making: separate currency system

Mods: `coins`, `coinage`, `noticeboard`, `vinconomy`, `commercially`, `handtohand`, `tabletopgames`.

## Classification register

All statuses describe this static audit; an exclusion is an exclusion from player progression pages, not a request to disable the mod.

| ID | Group | Status | Impact / vanilla delta |
|---|---|---|---|
| `walkingstick` | travel | covered-static | Equipable walking aids; crude stick has no speed config, AutoStep true, Hunger -5. |
| `watersheds` | world | covered-static | World-generation overhaul: route-finding context, not a craft unlock. |
| `rivers` | world | covered-static | Adds rivers; do not promise boat current transport with Quiet Rough Seas. |
| `terraprety` | world | covered-static | Natural-terrain world-generation redesign. |
| `kscartographytable` | travel | covered-static | Table-local waypoint exchange; advanced table also exchanges explored map areas. |
| `trailmodmaro` | travel | covered-static | Only players create trails; trail and trampled-soil devolution are configured, not disabled. |
| `roughseas` | travel | covered-static | Weather, vessel damage, repair, swamping and mooring systems. |
| `coins` | trade-community | covered-static | Player-issued notes, personal/shared currencies, mint and wallet; denominations 1/10/100. |
| `coinage` | trade-community | covered-static | Metal coin-making with a separate native guide; not proven interchangeable with Coins notes. |
| `downed` | combat-survival | covered-static | 300-second downed state; no self-revive, no passed-out recovery, hidden map. |
| `bloodtrail` | combat-survival | covered-static | Player-wounded animals leave blood trails. |
| `immersivecorpsedrop` | combat-survival | covered-static | Harvested corpses drop loot without a container UI. |
| `noticeboard` | trade-community | covered-static | Player notices, parchment and board permissions; posting is community coordination. |
| `quietroughseas` | travel-compatibility | covered-with-code-verification-limit | Declares quiet logs, axe-only boat hit damage, faster paddled boats, suppression of Rivers pushing boats. |
| `catchledge` | movement-logistics | covered-config-no-invented-controls | Ledge grab/hang requires both main hand and offhand empty; exact activation key not verified. |
| `chargedjump` | movement-logistics | covered-config-no-invented-controls | Charge max 0.15, charge rate 0.003, sprint jump disabled; metadata says sneak and hold space. |
| `climbmountain` | movement-logistics | covered-config-no-invented-controls | Only Stone/Ore materials configured climbable; stamina costs jump 20 and climb 5/sec. |
| `crawlanddive` | movement-logistics | covered-config-no-invented-controls | Crawling plus dive/slide; DisableDive false, CrawlSpeedDelta -0.6; no invented activation key. |
| `vaultandroll` | movement-logistics | covered-config-no-invented-controls | RollDamageScale 0.5 and vault cooldown 1000ms; this is not immunity to falls. |
| `sprintmomentum` | movement-logistics | covered-config-no-invented-controls | Material-specific acceleration, stopping and turning replaces instant-response movement. |
| `carryon` | movement-logistics | covered-config-no-invented-controls | Allows selected containers; no sprint while carrying, carry penalty retained; crates cannot go on back. |
| `combatoverhaulfork` | combat-survival | covered-config-no-invented-controls | Melee stops on terrain; spear directional attacks off; vanilla armour grid recipes off; Toolsmith integration on. |
| `armoryfork` | combat-survival | covered-config-no-invented-controls | Weapon/shield/armour content, not a client cosmetic library; complete recipes/controls not reconstructed. |
| `handtohand` | trade-community | covered-config-no-invented-controls | Direct item handoff; AllowGiveWithoutReceiverHolding false, comment identifies receiver holding R as required. |
| `cartwrightscaravan` | movement-logistics | deferred-native-workflow | Seraph-pulled/pushed carts, sleds and market stalls; mounting/storage workflow deferred, not invented. |
| `cartcarryonpatch` | movement-logistics | classified-no-standalone-progression | Compatibility for Cartwright carts transporting Carry On barrels and crates. |
| `equusdestrier` | movement-logistics | deferred-native-workflow | Enabled horse addon; metadata is insufficient to establish its practical stages. |
| `equusferus` | movement-logistics | deferred-native-workflow | Tameable wild horses and coat genetics; taming and riding controls deferred to dedicated evidence. |
| `bettertraders` | trade-community | classified-no-standalone-progression | Adds trader structures in addition to vanilla traders; no new-currency claim. |
| `remotetradercheckmod` | trade-community | classified-no-standalone-progression | Trader-phone stock check UI; rejects uncharted lands; reports gears and stock arrival, not remote buying. |
| `vinconomy` | trade-community | classified-no-standalone-progression | Player-owned shops; Coins native guide describes note purchases from loose notes, not wallet balances. |
| `commercially` | trade-community | classified-no-standalone-progression | Ownership, commercial blocks and map-waypoint UI; exact shop setup not proven by generic metadata. |
| `tabletopgames` | trade-community | classified-no-standalone-progression | Tabletop simulator/social games; no progression gate. |
| `egocaribautomapmarkers` | navigation-helper | classified-no-standalone-progression | Creates configurable map markers on interaction; do not imply every object is automatically marked. |
| `prospecttogether` | navigation-helper | classified-no-standalone-progression | Stores and shares prospecting readings; does not itself prospect or create ore. |
| `spyglass` | navigation-helper | deferred-native-workflow | Adds a spyglass; item recipe/controls deferred to native entry.  |
| `footprints` | tracking-ambience | classified-no-standalone-progression | Animals and players leave footprints; potentially useful tracking, not a crafting chain. |
| `clickuptorches` | survival-qol | classified-no-standalone-progression | Right-click pickup of torches; gameplay interaction tweak, not a library. |
| `slowtox` | survival | classified-no-standalone-progression | Changes intoxication with benefits; exact effects deferred to food/drink research. |
| `toolbelt` | logistics | classified-no-standalone-progression | Belts can hold tools; recipe and use belong to equipment/storage research. |
| `placeonslabs` | building-qol | classified-no-standalone-progression | Allows placement on slabs; not a staged technology unlock. |
| `terrainslabs` | world-building | deferred-native-workflow | Slab-related terrain/building content; no unsupported creation/placement instructions. |
| `sortablestorage` | storage-qol | classified-no-standalone-progression | Adds a storage sort button; QoL, not new material progression. |
| `radialcrafting` | crafting-ui | classified-no-standalone-progression | Radial crafting menu; do not replace native recipe constraints with imagined recipes. |
| `danatweaks` | mixed-tweaks | needs-config-or-code-verification | Mixed tweaks must be evaluated from server and client configs, not treated as all enabled. |
| `configureeverything` | mixed-tweaks | needs-config-or-code-verification | Configuration-driven vanilla overrides; do not assume every potential override is active. |
| `cbr` | unresolved-enabled | needs-config-or-code-verification | Enabled ID with empty description; insufficient evidence to declare cosmetic, library or progression. |
| `yangcoifix` | compatibility | needs-config-or-code-verification | Enabled fix ID with empty description; no player progression inferred. |
| `attributerenderinglibrary` | technical-library | excluded-as-standalone-progression | Attribute-based variant system for rendering things and much more. |
| `commonlib` | technical-library | excluded-as-standalone-progression | Infrastructure/support ID; no standalone player process established from metadata. |
| `configlib` | technical-library | excluded-as-standalone-progression | A universal place to configure your mods. Makes it possible for content mods to have configs too. |
| `coreofartspatch` | technical-library | excluded-as-standalone-progression | The main mod-library for other mods of the Arts series. |
| `vsimgui` | technical-library | excluded-as-standalone-progression | Brings famous immediate mode GUI to Vintage Story |
| `genelib` | technical-library | excluded-as-standalone-progression | Infrastructure/support ID; no standalone player process established from metadata. |
| `handbookoptimizer` | technical-library | excluded-as-standalone-progression | Heavy mod-pack handbook and crafting-grid optimizer: ingredient and recipe indexes, paced previews, progressive descriptions and diagnostics for Vintage Story 1.22.7. |
| `jaunt` | technical-library | excluded-as-standalone-progression | Rideable/stamina infrastructure; indirectly changes mounts but is not itself a rideable crafting chain. |
| `jsonpatcheslib` | technical-library | excluded-as-standalone-progression | Implements more convenient syntax and more functionality for patching JSON assets. |
| `overhaullib` | technical-library | excluded-as-standalone-progression | Infrastructure/support ID; no standalone player process established from metadata. |
| `overhaulliblegacycompat` | technical-library | excluded-as-standalone-progression | Infrastructure/support ID; no standalone player process established from metadata. |
| `playermodellib` | technical-library | excluded-as-standalone-progression | Infrastructure/support ID; no standalone player process established from metadata. |
| `universaldisplaylib` | technical-library | excluded-as-standalone-progression | Library for blocks with free-placement display surfaces - any block, not just windows. Adds surfaces defined in the shape file, item stacking, rotation, precise placement, lighting, particles and block swapping. |
| `shearlib` | technical-library | excluded-as-standalone-progression | Animal-harvest support; downstream animal/wool interactions need their own evidence. |
| `infinitumcore` | technical-library | excluded-as-standalone-progression | A shared backbone mod for Infinitum mods that handles networking, pickup tracking, collection matching, server-side progress storage, and common client/server communication. |
| `cavesymphony` | client-ambience-rendering | excluded-as-standalone-progression | Gives caves life. |
| `farseer` | client-ambience-rendering | excluded-as-standalone-progression | Draws silhouettes of the terrain outside of your view distance |
| `footsteps` | client-ambience-rendering | no-player-stages-established-code-verification-needed | Presentation/compatibility candidate; metadata alone does not establish a standalone process. |
| `immersivefoliage` | client-ambience-rendering | excluded-as-standalone-progression | Dynamic foliage that reacts to entities, both physically and audibly. |
| `immersivelanterns` | client-ambience-rendering | excluded-as-standalone-progression | Lanterns react to movement and make noise |
| `immersivelight` | client-ambience-rendering | excluded-as-standalone-progression | Light that respects walls! |
| `juicyores` | client-ambience-rendering | excluded-as-standalone-progression | Makes ores much more visible |
| `mistsofstability` | client-ambience-rendering | excluded-as-standalone-progression | Visual effects for stable/unstable areas. |
| `realsmoke` | client-ambience-rendering | no-player-stages-established-code-verification-needed | Physics-based smoke |
| `realsmokeartofgrowingfirepit` | client-ambience-rendering | no-player-stages-established-code-verification-needed | Presentation/compatibility candidate; metadata alone does not establish a standalone process. |
| `stickstepsounds` | client-ambience-rendering | excluded-as-standalone-progression | Plays stick cracking sound when walking on loose sticks |
| `sungaze` | client-ambience-rendering | excluded-as-standalone-progression | Little animation for looking at the sun |
| `temporalsymphony` | client-ambience-rendering | no-player-stages-established-code-verification-needed | Presentation/compatibility candidate; metadata alone does not establish a standalone process. |
| `kuboczochvisualmining` | client-ambience-rendering | excluded-as-standalone-progression | Private standalone client-side cosmetic mining animations and voxel chips. Original visual assets by SaltyWater and GamingToast. No gameplay changes. |
| `extrainfo` | ui-information | excluded-as-standalone-progression | Useful information for handbook, blocks, items and entities |
| `itempickupnotifier` | ui-information | excluded-as-standalone-progression | Small hud overlay that shows pickup items |
| `playerlistrevived` | ui-information | excluded-as-standalone-progression | View the player list on Vintage Story servers |
| `scrollbarcreativeinventorytabs` | ui-information | excluded-as-standalone-progression | Inventory tabs, but for people with too many mods. |
| `statushudcont` | ui-information | excluded-as-standalone-progression | Displays status information on the HUD. |
| `infinitumpickuphud` | ui-information | excluded-as-standalone-progression | A lightweight pickup HUD that shows which items you just received, including items gained from panning, hand gathering, and other non-standard pickup sources. |
| `infinitumprospectinghud` | ui-information | excluded-as-standalone-progression | A client-side prospecting HUD with reading history, ore icons, trend indicators and a draggable window, configurable through Infinitum Core without switching your selected chat tab. |
| `kuboczochfoodhandbook` | documentation-tutorial | excluded-as-standalone-progression | Client-only independent process sections for native ACA handbook recipe contributions. No recipe or tooltip changes. |
| `kuboczochhandbookrefresh` | documentation-tutorial | excluded-as-standalone-progression | Updates existing English survival guides for the Pszczoly 1.0 modpack. Documentation only; no gameplay changes. |
| `kuboczochmodpackguide` | documentation-tutorial | excluded-as-standalone-progression | Additive client handbook index for the Pszczoly 1.0 modpack. |
| `kuboczochtutorialskip` | documentation-tutorial | excluded-as-standalone-progression | Stuck on a tutorial step? Skip it and continue the tutorial. Saves progress immediately. |
| `kuboczochwoodworkingtutorial` | documentation-tutorial | excluded-as-standalone-progression | Two compact First Steps stages with native item previews and handbook links: prepare a chopping block, then make firewood. |
| `manualfirestartertutorial` | documentation-tutorial | excluded-as-standalone-progression | Replaces only the First Steps firepit instructions with short stages for Manual Firestarter. |

## Held or omitted workflows

- Cartwright carts, sleds, carry loading, horses, exact climb/vault/crawl activation and Downed rescue require native/source control verification before drafting detailed actions. Carry On explicitly disables sprint and crate back-carry; Catch Ledge requires both hands empty.
- Trader phones expose stock checks, not verified remote purchase; Commercially ownership UI is not a proven shop-creation walkthrough. Do not invent an exchange rate between notes, metal coins and trader gears.
- `cbr` and `yangcoifix` have insufficient metadata for a strong mechanical classification; Dana Tweaks and Configure Everything need per-setting/code review, not blanket cosmetic exclusion.
- Client rendering/audio, HUDs, libraries, handbook and tutorial patches are not fake technology tiers. Real Smoke and empty-description ambience/patch IDs remain explicitly unverified for side effects.
- No assertion that bundled worldgen mods have a known combined precedence, or that pack configs prove the actual running save/world settings.

## Other-domain enabled IDs (not excluded)

`aculinaryartillery`, `artofgrowingpatch`, `barkbeetle`, `betterhusbandry`, `bricklayers`, `butchering`, `capes`, `catchlivestock`, `chiseltools`, `containersbundle`, `dairyplus`, `dressmakers`, `efchefstricks`, `efmealsmodule`, `em`, `expandedfoods`, `fallingtree`, `firestarters`, `foodshelves`, `fromgoldencombs`, `heraldry`, `heraldrybanners`, `immersiveorecrush`, `immersivewoodworking`, `kevinsfurniture`, `livestockwelfare`, `manualdishes`, `manualdough`, `manualfirestarter`, `manualharvesting`, `manualquenching`, `manualshingle`, `millwright`, `morepiles`, `naturalfertilizer`, `oneroof`, `pagoldsilver`, `pewter`, `purposefulstorage`, `quiversfork`, `rusticcookery`, `sawchiseling`, `scaffolding`, `shelfobsessed`, `simpletea`, `stickroast`, `stonebakeoven`, `stoneharvesting`, `tailorsdelight`, `tankardsandgoblets`, `toolsmith`, `tseasonedfirewood`, `windchimes`, `windowdisplay`, `windowdisplaypatches`, `wool`
