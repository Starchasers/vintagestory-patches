"""Player-facing English text for the baseline Pszczoly custom guide.

Pure stdlib transformation: no archive writes, upstream language overrides, or
runtime game hooks. Existing destinations are copied literally from each page.
"""
import html
import re

PREFIX = 'handbook://kuboczochmodpackguide-'
ANCHOR = re.compile(r'<a\s+href="([^"]+)"[^>]*>(.*?)</a>', re.S)

# Authored against the complete English baseline, not shortened category blurbs.
DESCRIPTIONS = {
    'aculinaryartillery': 'Prepare food with mixing bowls, kneading, simmering pans and cauldrons. Use tree spiles, bottles, stoppers, egg-processing equipment and meat hooks, and explore the oven and pie additions.',
    'walkingstick': 'Craft a walking stick to improve walking speed and hunger use. Choose from survivalist knife, lantern, shepherd, sling and spearstaff variants.',
    'watersheds': 'Plan exploration, settlements and water routes around watershed terrain, river valleys and navigable water.',
    'artofgrowingpatch': 'Dry grain bundles, then thresh them with a stick. Process flax by retting, washing, softening and combing; mow grass and build layered haystacks as part of the reworked crop system.',
    'betterhusbandry': 'Care for animals through feeding, body condition, petting and milking to develop trust and generation progression. Neglect carries penalties.',
    'bettertraders': 'Find merchants in furnished shops and workspaces. Ordinary, rich and poor merchant variants provide places to trade.',
    'bloodtrail': 'Follow blood trails left by wounded animals to track fleeing prey.',
    'bricklayers': 'Make colored clay, porcelain, glazed ceramics and glass. Process pigments and frit, and develop a workshop with a durable kiln, hot-glass equipment and industrial glassmaking chains.',
    'butchering': 'Transport carcasses, skin them on hooks and process them on butchery tables with knives and cleavers. The butchering system includes quality, fats, scraps, bladders and sausage skins.',
    'carryon': 'Lift and carry placed storage containers, including carrying a container on your back.',
    'cartwrightscaravan': 'Pull or push carts, sleds and market stalls as a seraph. Add storage and canopy options.',
    'catchledge': 'Grab ledges, hang from them, climb up, traverse corners and leap between holds. Empty-hand requirements and timing can make a catch risky.',
    'catchlivestock': 'Use Ctrl-right-click to carry tamed livestock above generation zero without basket traps. Move animals to relocate or manage your herd.',
    'chargedjump': 'Charge jumps while sneaking or sprinting to gain height or distance.',
    'chiseltools': 'Build with hand planers, wedges, pantographs and custom doors. Use the ladder maker for climbable chiseled surfaces and the path maker for faster travel paths.',
    'climbmountain': 'Climb stone and ore walls, traverse sideways, slide and arrest falling momentum. Combine climbing with ledge catches.',
    'coinage': 'Design a coin punch and make planchets from metal plates. Mint custom coins with a trussell, anvil die, coining pile and hammer; copy designs before the tools break.',
    'coins': 'Issue banknote currencies with mint blocks, denominations and change recipes. Use wallets, share minting permissions and connect currencies to Vinconomy shops.',
    'combatoverhaulfork': 'Learn weapon handling, stances, attacks, parrying and blocking. Damage, resistance and shield behavior are part of the combat system.',
    'armoryfork': 'Equip layered armor, helmets, shields, pauldrons, greaves and protective clothing for Combat Overhaul.',
    'containersbundle': 'Store supplies in wicker, wood, linen, metal and stone containers. Food cupboards and metal cabinets change spoilage, while reinforced strongboxes protect valuables.',
    'coreofartspatch': 'Mix liquids and solids in the world and form dough with the Core of Arts crafting systems.',
    'crawlanddive': 'Crawl through one-block gaps. Sprint-dive, slide or dive in the air to move through terrain and escape danger.',
    'dairyplus': 'Separate cream, churn butter and stretch mozzarella. Work with a cream separator, churns, cheese vats and a liquid thermometer.',
    'downed': 'A downed state allows revival before death. Learn bleeding-out and passing-out choices, execution, dragging, carrying casualties on your back and optional self-revival.',
    'dressmakers': 'Craft and trade common, class-specific and Nadiyan clothing with added recipes. The clothing chains use Tailor’s Delight and Expanded Matter, with optional wool alternatives.',
    'equusdestrier': 'Ride domestic medieval horse breeds and equip them with tack. These horses also connect to breeding and husbandry systems.',
    'equusferus': 'Domesticate and ride wild horses. Equip saddles, bridles, lead ropes, saddlebags and other tack for travel and animal handling.',
    'efchefstricks': 'Process salt, resin and eggs, including washing and crushing eggshells. Explore preservation helpers and breadcrumb animal feed.',
    'expandedfoods': 'Chop, dry and age ingredients for an expanded range of foods. Explore soy products, fermented sauces, broth, oils, syrup, pasta, sausages, sushi, breads and liquids.',
    'efmealsmodule': 'Cook rice, salads, noodles, pies, pasta and other varied meals with the Expanded Foods meal recipes.',
    'em': 'Develop material and chemical production with ores, minerals, acids, bitumen, coke and sulfur. Use grinding, leaching and soaking in chains for starch, binders, herbarium products, ink and paper.',
    'fallingtree': 'Fell trees with directional falls and impact damage. Keep clear of falling trees; the pack configuration disables domino felling.',
    'firestarters': 'Choose an ignition method suited to the weather, wind and exposure. Environmental conditions restrict fire starting.',
    'foodshelves': 'Store food on specialized shelves and in ice-cooled cabinets or freezers. Storage types affect freshness, drying and curing.',
    'fromgoldencombs': 'Keep bees in ceramic and Langstroth hives. Manage frames, harvest honey and wax, and use bees for crop pollination.',
    'immersivemining': 'Mine and chop with individual tool impacts. Account for reach penalties, tool damage, durability and stamina costs.',
    'immersiveorecrush': 'Place ore chunks on the ground and hit them with a tool to crush them.',
    'immersivewoodworking': 'Process wood in the world using chopping blocks, paired sawhorses and pitsaws. Develop powered sawmills and choppers, and process bark and tar.',
    'kscartographytable': 'Craft a cartography table to share explored map areas and waypoints with other players.',
    'kevinsfurniture': 'Furnish your home with a four-burner stove, storage, beds and a Galileo thermometer.',
    'livestockwelfare': 'Care for animal thirst, shelter, cleanliness, illness and young animals. The pack configuration disables the breeding module, so its bedding requirements and breeding timings are inactive; the linked guide also describes those features.',
    'manualdough': 'Knead flour into dough against a water source or container instead of relying on the grid recipe.',
    'manualfirestarter': 'Light tinder on the ground, then carry it to ignite a firepit or another target.',
    'manualharvesting': 'Pick individual visible fruit from fruit-tree foliage and branches.',
    'manualshingle': 'Knead held clay by hand into raw shingles or bowls.',
    'millwright': 'Build wind-powered workshops with reinforced rotors, improved sails and vertical-axis windmills. Use reinforced brakes and axle passthroughs that preserve rooms.',
    'naturalfertilizer': 'Collect animal droppings and compost manure with grass or rot. Apply the resulting fertilizer to support your crops.',
    'trailmodmaro': 'Repeated player and animal traffic develops trails, paths and roads. Use trail tools to work with these routes.',
    'noticeboard': 'Craft communal notice boards for notices and signed parchment. Attach map locations to help other players find places.',
    'pewter': 'Alloy tin, lead and copper into pewter, then cast and assemble tableware parts. Make usable bowls, six-serving crocks and five-litre jugs.',
    'pagoldsilver': 'Cast and assemble gold and silver versions of Pewter tableware.',
    'purposefulstorage': 'Craft specialized storage for equipment and resources. Apothecary cabinets prevent alcohol-bandage drying, while pelt racks speed curing.',
    'quiversfork': 'Wear belts, quivers, sheaths, slings, holsters and pouches with specialized ammunition and tool slots. Use quick-draw and return controls to manage equipment.',
    'realsmoke': 'Give fires clear exhaust paths and ventilate workshops with correctly arranged chimneys. Combustion produces temperature-dependent smoke, and obstructed exhaust can smother fires.',
    'rivers': 'River currents affect swimmers, so take care when crossing. Quiet Rough Seas defaults to disabling river pushing of boats; whether that protection works depends on configuration and the compatibility patch being active.',
    'roughseas': 'Build and maintain vessels, navigate weather, repair hulls, bail flooding, and use ropes and anchors. Quiet Rough Seas changes hull-hit protection and stroke power and defaults to suppressing Rivers boat drift; configuration and an active compatibility patch determine boat behavior.',
    'rusticcookery': 'Cook and preserve food with roasting dishes, meat slabs, smoke racks and hooks. Prepare pot roasts and braised dishes.',
    'scaffolding': 'Craft climbable scaffolding for building work. Account for structural support and stability; climbing movement is configurable.',
    'tseasonedfirewood': 'Prepare and cure firewood into seasoned and aged fuel. Fuel condition changes ignition suitability, temperature and burn duration.',
    'simpletea': 'Brew flowers and herbs into tea. Brewing temperature affects strength, and teas provide timed beneficial effects.',
    'slowtox': 'Alcohol is absorbed and cleared over time and builds tolerance. It can provide regeneration or combat benefits, but also movement penalties and damage from overintoxication.',
    'sprintmomentum': 'Allow for acceleration, deceleration and turning inertia when moving. Surface-dependent movement tuning affects players and creatures.',
    'spyglass': 'Craft a metal-and-glass spyglass to observe distant places. Adjust its magnification while scouting.',
    'stickroast': 'Use a roasting stick and a holder over a fire to cook and eat skewered food.',
    'stonebakeoven': 'Build a modular stone oven from baking-space, heat-distribution and heat-source components, then use it for baking.',
    'stoneharvesting': 'Harvest intact rock by working the connections around a rock face with a hammer and chisel.',
    'tabletopgames': 'Craft boards, customizable pieces, playing cards and shufflers for games with other players. Create custom boards and piece schematics.',
    'tailorsdelight': 'Make clothing components with spindles, stitching awls, prepared leather bundles, twine and buttons. Use these production chains to craft wearable equipment.',
    'tankardsandgoblets': 'Craft tankards, goblets, bottles and demijohns for drinking and liquid storage. The recipes also include alcohol-soaked bandages.',
    'temporalsymphony': 'Prepare for stronger bell enemies and bellhead shivers that summon hostile creatures. Temporal storms and rift weather have reworked spawns and audiovisual effects.',
    'toolbelt': 'Wear a sturdy belt for two accessible tool slots or a heavy tool belt for four. These capacities follow the pack configuration.',
    'toolsmith': 'Build multipart tools with interchangeable heads, handles and bindings. Manage sharpness and wear, sharpen tools on grindstones, assemble or deconstruct them at workbenches, and reforge worn metal heads.',
    'vaultandroll': 'Vault obstacles and time landing rolls to reduce fall damage. A roll can mitigate damage, not guarantee a safe landing.',
    'vinconomy': 'Build player-run shops and purchase stalls. Use registers and catalogs to trade stock through the configured currency economy.',
    'wool': 'Shear domesticated animals and wash fleece into fibers for wool twine and cloth. Mordant and dye wool, knit clothing, and craft carrying materials, storage materials and fertilizers.',
}

GROUPS = {
    'food-cooking': [
        ('Prepare ingredients and equip a kitchen', 'aculinaryartillery coreofartspatch manualdough dairyplus stonebakeoven kevinsfurniture'),
        ('Cook meals and make drinks', 'expandedfoods efmealsmodule stickroast simpletea tankardsandgoblets'),
        ('Preserve food and choose storage', 'efchefstricks rusticcookery foodshelves'),
    ],
    'crafting-workshops': [
        ('Make and maintain tools', 'toolsmith'),
        ('Fell trees and work wood', 'fallingtree immersivewoodworking millwright'),
        ('Mine and process materials', 'immersivemining immersiveorecrush em bricklayers manualshingle pewter pagoldsilver'),
        ('Make clothing', 'tailorsdelight dressmakers'),
    ],
    'gear-tools': [
        ('Equip for travel and carry tools', 'walkingstick quiversfork spyglass'),
        ('Build and shape blocks', 'chiseltools scaffolding stoneharvesting'),
    ],
    'farming-husbandry': [
        ('Grow and harvest crops', 'artofgrowingpatch manualharvesting naturalfertilizer fromgoldencombs'),
        ('Care for and move animals', 'betterhusbandry livestockwelfare catchlivestock equusferus equusdestrier'),
        ('Process fleece', 'wool'),
    ],
    'movement-transport': [
        ('Learn movement controls', 'sprintmomentum chargedjump crawlanddive catchledge climbmountain vaultandroll'),
        ('Carry supplies and organize storage', 'carryon cartwrightscaravan toolbelt containersbundle purposefulstorage'),
        ('Plan travel and water routes', 'kscartographytable trailmodmaro watersheds rivers roughseas'),
    ],
    'combat-survival': [
        ('Hunt and process animals', 'bloodtrail butchering'),
        ('Fight, protect yourself and help casualties', 'combatoverhaulfork armoryfork downed temporalsymphony'),
        ('Light fires and manage smoke', 'firestarters manualfirestarter tseasonedfirewood realsmoke'),
        ('Understand alcohol risks', 'slowtox'),
    ],
    'economy-community': [
        ('Trade and run shops', 'bettertraders vinconomy'),
        ('Make coins and issue currency', 'coinage coins'),
        ('Share notices and play games', 'noticeboard tabletopgames'),
    ],
}


def _links(text):
    """Preserve literal destinations, deduplicating only within one page."""
    return list(dict.fromkeys(ANCHOR.findall(text)))


def _unique_links(text):
    seen = set()
    result = []
    for href, label in _links(text):
        if href not in seen:
            seen.add(href)
            result.append((href, label))
    return result


def _a(href, label):
    return '<a href="' + href + '">' + label + '</a>'


def _heading(text):
    return '<strong>' + text + '</strong>'


def _name(lang, page):
    return html.escape(lang[page + '-title'].removeprefix('Pszczoly — '), quote=False)


def rewrite(lang: dict) -> dict:
    """Return rewritten custom guide text, preserving unrelated language keys.

    The baseline supplies links and names only. All prose is authored here.
    Unknown added pages are left alone rather than assigned invented mechanics.
    """
    result = dict(lang)
    for mod, description in DESCRIPTIONS.items():
        page = 'mod-' + mod
        key = page + '-text'
        if key not in lang:
            continue
        links = _unique_links(lang[key])
        navigation = [(h, l) for h, l in links if h == PREFIX + 'root' or h.startswith(PREFIX + 'category-')]
        native = [(h, l) for h, l in links if h.startswith('handbook://') and not h.startswith(PREFIX)]
        related = [(h, l) for h, l in links if h.startswith(PREFIX + 'mod-')]
        external = [(h, l) for h, l in links if not h.startswith('handbook://')]
        parts = [' | '.join(_a(h, l) for h, l in navigation), _heading(_name(lang, page)), description]
        for heading, entries in [('Learn the steps', native), ('Related food preparation', related), ('More information online', external)]:
            if entries:
                parts.append(_heading(heading) + '<br>' + '<br>'.join('• ' + _a(h, l.replace(' — Mod DB', '').removesuffix(' (Mod DB website)') + (' (Mod DB website)' if heading == 'More information online' else '')) for h, l in entries))
        result[key] = '<br><br>'.join(parts)
    for category, groups in GROUPS.items():
        page = 'category-' + category
        key = page + '-text'
        if key not in lang:
            continue
        links = dict(_unique_links(lang[key]))
        parts = [_a(PREFIX + 'root', 'Back to Pszczoly guide'), _heading(_name(lang, page))]
        used = {PREFIX + 'root'}
        for heading, mod_ids in groups:
            rows = []
            for mod in mod_ids.split():
                href = PREFIX + 'mod-' + mod
                if href not in links:
                    continue
                # Full sentences replace truncated audit-oriented category blurbs.
                summary = DESCRIPTIONS[mod]
                rows.append('• ' + _a(href, _name(lang, 'mod-' + mod)) + ' — ' + summary)
                used.add(href)
            if rows:
                parts.append(_heading(heading) + '<br>' + '<br><br>'.join(rows))
        remainder = [(h, l) for h, l in links.items() if h not in used]
        if remainder:
            parts.append(_heading('More topics') + '<br>' + '<br>'.join(_a(h, l) for h, l in remainder))
        result[key] = '<br><br>'.join(parts)
    if 'root-text' in lang:
        from root_index import TITLE, root_content
        result['root-title'] = TITLE
        result['root-text'] = root_content(lang['root-text'])
    return result
