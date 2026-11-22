"""Authored Toolsmith reference pages for the exact installed pack.

Tables use three ordinary VTML-richtext cells per row. The client converter owns
column sizing, wrapping and hit testing; this module never simulates columns with
spaces or HTML. Native attributed-family rendering remains <toolsmithfamily>.
The inventory is checked-in static evidence, not fetched or inferred at runtime.
"""
from decimal import Decimal
from html import escape
import json
from pathlib import Path

NAMES = {
    'toolsmith-items': 'Toolsmith - Pszczoly Guide to Toolsmith',
    'toolsmith-items-handles': 'Toolsmith - Handles',
    'toolsmith-items-grips': 'Toolsmith - Grips',
    'toolsmith-items-bindings': 'Toolsmith - Bindings',
    'toolsmith-items-treatments': 'Toolsmith - Treatments',
    'toolsmith-items-maintenance': 'Toolsmith - Maintenance',
}
REF = {
    'crafting': 'craftinginfo-toolsmithcraftingmethods',
    'wear': 'craftinginfo-toolsmithdurabilityandwear',
    'reforging': 'craftinginfo-toolsmithreforging',
}


def family(codes):
    return '<toolsmithfamily code="' + escape(codes, quote=True) + '"></toolsmithfamily>'


def para(text):
    return text + '\n\n'


def row(*cells):
    if len(cells) != 3:
        raise ValueError('Toolsmith table rows must contain exactly three cells')
    return '<toolsmithrow>' + ''.join('<toolsmithcell>' + str(c) + '</toolsmithcell>' for c in cells) + '</toolsmithrow>'


def table(headers, rows):
    return '<toolsmithtable>' + row(*(f'<strong>{escape(h)}</strong>' for h in headers)) + ''.join(row(*r) for r in rows) + '</toolsmithtable>\n\n'


def heading(title):
    return '<font size="17"><strong>' + escape(title) + '</strong></font>\n\n'


def d(value):
    return Decimal(str(value))


def number(value):
    return format(d(value).normalize(), 'f')


def percent(value):
    return number(d(value) * 100) + '%'


def signed_percent(value):
    return ('+' if d(value) >= 0 else '') + percent(value)


def option(g):
    """Attributed icons keep the existing native converter; plain ones get an explicit recipe link."""
    codes = g['codes']
    if all(c.startswith('toolsmith:') for c in codes):
        return family('|'.join(codes)) + '\n<strong>' + escape(g['label'], quote=False) + '</strong>'
    return item('|'.join(codes), g['kind']) + '\n<strong>' + escape(g['label'], quote=False) + '</strong>'


def a(target, label):
    if target in NAMES:
        target = 'kuboczochmodpackguide-' + target
    href = target if target.startswith(('https://', 'http://', 'handbook://')) else 'handbook://' + target
    return '<a href="' + escape(href, quote=True) + '">' + escape(label) + '</a>'


def item(code, kind='item'):
    return '<toolsmithitem type="' + escape(kind, quote=True) + '" code="' + escape(code, quote=True) + '"></toolsmithitem>'


def navigation(current):
    return para('Browse: ' + ' · '.join(a(k, v) for k, v in NAMES.items() if k != current))


def inventory():
    return json.loads(Path(__file__).with_name('table_inventory.json').read_text())


def pages():
    inv = inventory()
    groups = inv['groups']
    options = {(r['category'], r['id']): r for r in inv['options']}
    p = {}
    key = 'toolsmith-items'
    p[key] = (NAMES[key],
        heading('Make your first tool')
        + para('Example first-tool kit: ' + item('game:axehead-flint') + ' flint axe head, ' + item('game:stick') + ' stick, ' + item('game:drygrass') + ' dry grass. Click each icon for its native recipe page.')
        + para('1. Start with a tool head, a stick or bone, and grass or reeds for a binding. Hold the head in your main hand and handle in your offhand; hold right-click to pair them. Keep the pair in your main hand, put the binding in your offhand and hold right-click again. A workbench is an alternative, not a requirement. Grid tool assembly is disabled in this pack.')
        + para('2. Start with what you have. Upgrade the handle and binding later; a grip and a dried treatment improve the handle before assembly.')
        + para('3. Maintain the edge before it goes blunt. Sharpening restores sharpness, not lost head HP; replace worn components at the bench.')
        + navigation(key)
        + para('Native instructions and recipes: ' + a(REF['crafting'], 'Assembly and crafting') + ' · ' + a(REF['wear'], 'Durability and wear') + ' · ' + a(REF['reforging'], 'Reforging') + ' · ' + a('kuboczochmodpackguide-mod-immersivewoodworking', 'Woodworking workshop guide')))

    key = 'toolsmith-items-handles'
    rows = []
    for g in groups['handles']:
        s = g['stats']
        factor = d(s['baseHPfactor']) * (1 + d(s['selfHPBonus']))
        hp = '<strong>' + number(factor) + ' × B HP</strong>\nBase ' + number(s['baseHPfactor']) + ' × B; own HP bonus ' + signed_percent(s['selfHPBonus']) + '.'
        use = 'Binding HP ' + signed_percent(s['bindingHPBonus']) + '; speed ' + signed_percent(s['speedBonus']) + '.\n' + escape(g['note'])
        rows.append((option(g), hp, use))
    p[key] = (NAMES[key], heading('Choose a handle')
        + para('<strong>B means the tool’s base maximum durability before Toolsmith’s head multiplier.</strong> HP is the amount of wear a part can take. The HP column already includes the handle’s own bonus, but not a treatment or binding support. Binding HP support strengthens the binding, not the handle. Speed is the handle’s tool-use speed contribution.')
        + table(('Handle / recipe', 'Handle HP, before extras', 'Support, speed and first use'), rows)
        + para('Stick takes neither grip nor treatment. Bone and crude handles take grips only. Carved and carpented handles accept both. Carved and carpented handles have these 13 native wood appearances: aged, birch, oak, maple, pine, acacia, kapok, bald cypress, larch, redwood, ebony, walnut and purpleheart. They use the same stat tag within each handle type: oak is not a secret durability upgrade over pine. Crude handles have no separate wood-appearance attribute.')
        + para('Click a family icon to browse its native attributed handles and recipes. A log does not automatically become an installed handle species: only the listed native appearances are claimed here.')
        + navigation(key))

    key = 'toolsmith-items-grips'
    rows = [('No grip', 'Speed +0%', '100% handle-wear chance when handle wear is eligible.')]
    for g in groups['grips']:
        s = g['stats']
        rows.append((option(g), '<strong>Speed ' + signed_percent(s['speedBonus']) + '</strong>', '<strong>' + percent(s['chanceToDamage']) + ' handle-wear chance</strong>\nWhen a damage event is eligible to wear the handle.'))
    p[key] = (NAMES[key], heading('Wrap a crafted handle')
        + para('Combine an unassembled handle with a grip ingredient in the grid. Bone and crude handles take grips, as do carved and carpented handles; sticks cannot. Each handle carries one grip, not a stack of grip bonuses. Click an ingredient icon for its native recipe references.')
        + para('<strong>Lower wear chance is better.</strong> A leather grip’s 80% means an eligible handle-damage unit has an 80% chance to remove handle HP; it is not 80% damage reduction and does not protect the binding or head. Handle wear is normally eligible below 95% sharpness. Speed bonuses from handle and grip add; a carved handle (+5%) with sturdy leather (+30%) contributes +35% before sharpness modifiers.')
        + table(('Grip ingredient / recipe', 'Tool-use speed', 'Handle wear'), rows)
        + para('Separate material/acquisition rows remain separate even where the stats match. Only equivalent cosmetic colours or linen patterns share a slideshow. Wool twine and wool cloth each include plain, mordant, eleven standard colours and four dark shades. Tailor’s Delight adds dark blue, dark brown, dark green and dark red linen cloth. Diamond, normal, offset and square linen sheets are one pattern family. Leather grips include all native leather colours without changing these numbers.')
        + navigation(key))

    key = 'toolsmith-items-bindings'
    rows = [('No binding (where allowed)', '0.5 × B HP; own bonus +0%', 'Handle HP +0%; recovery needs 100% HP.')]
    for g in groups['bindings']:
        s = g['stats']
        f = d(s['baseHPfactor']) * (1 + d(s['selfHPBonus']))
        tier = 0 if f <= 1 else 1 if f < d('1.75') else 2 if f < d('2.5') else 3 if f < 3 else 4
        hp = '<strong>' + number(f) + ' × B HP</strong>\nBase ' + number(s['baseHPfactor']) + ' × B; own bonus ' + signed_percent(s['selfHPBonus']) + '.\nBinding tier ' + str(tier) + '.'
        support = 'Handle HP ' + signed_percent(s['handleHPBonus']) + '.\nOn breakage: recover binding at ≥' + percent(s['recoveryPercent']) + ' remaining binding HP.'
        if s['isMetal']:
            support += '\nBelow that threshold: 2–3 ' + escape(s['metalType']) + ' metal bits instead.'
        if g['note']:
            support += '\n' + escape(g['note'])
        rows.append((option(g), hp, support))
    p[key] = (NAMES[key], heading('Choose a binding')
        + para('B is the tool’s base maximum durability. Binding HP below includes its base factor and own bonus only. Multiply it again by the handle’s binding-support factor: leather is 1.95 × B alone, or <strong>2.34 × B</strong> with a carved handle (+20%). The binding’s separate handle-HP bonus strengthens the handle; it does not increase its own HP twice.')
        + para('<strong>Recovery is an HP threshold, not a random drop chance.</strong> On breakage, leather requires at least 60% binding HP left to return the binding. Metal bindings below their recovery threshold return 2–3 matching metal bits. Deliberate disassembly is stricter: only a pristine binding returns intact; a worn metal binding returns bits. Binding tier is the native strength ranking calculated from the binding’s own HP factor, not the head’s metal tier. Keep native recipe quantities and temperatures: hot pitch, cold sinew glue and hot hide glue are different inputs.')
        + table(('Binding ingredient / recipe', 'Binding HP, before handle support', 'Handle support and recovery'), rows)
        + para('The coloured flax-twine family is from Tailor’s Delight; all seventeen Wool twines are a separate material row. Wool strips are installed through the active Wool compatibility patches. Metal parts deliberately remain separate from nails: they use cupronickel stats. Meteoritic steel and uranium-steel compatibility options are not installed and are not listed.')
        + navigation(key))

    key = 'toolsmith-items-treatments'
    rows = [('Untreated', 'Handle HP +0% (×1)', 'No drying or ingredient needed.')]
    for g in groups['treatments']:
        r = options['treatments', g['optionIds'][0]]
        m, s = r['mapping'], r['stats']
        dose = number(m['litersUsed']) + ' L in a suitable liquid container' if m['isLiquid'] else '1 solid ingredient in the crafting grid'
        rows.append((option(g), '<strong>Handle HP ' + signed_percent(s['handleHPbonus']) + '</strong>\nMultiply handle HP by ' + number(1 + d(s['handleHPbonus'])) + '.', 'Drying hours: carved: ' + number(m['dryingHours']) + '; carpented: ' + number(d(m['dryingHours']) * 2) + '.\n' + dose + '.'))
    p[key] = (NAMES[key], heading('Treat the handle before assembly')
        + para('Only carved and carpented handles accept treatment; sticks, bones and crude handles do not. Treat before adding a grip: gripped handles cannot accept treatment. Use the native handle recipe with fat, wax or an oil container, then leave the handle out of a container to dry. A wet treatment must finish drying before assembly. Carpented (professional) handles take twice as long as carved handles; the table shows both in-game times. One handle has one treatment; oils do not stack with wax or fat.')
        + table(('Treatment / ingredient recipe', 'What it improves', 'Drying and amount'), rows)
        + para('Treatment raises handle HP only. It does not refill a worn head, strengthen the binding or add speed. A carved handle has 1.26 × B HP before extras; oil makes that 2.079 × B before the binding’s handle support and integer rounding. Fat and rendered fat are separate installed ingredients; all four installed oils are shown, including both Expanded Foods portions.')
        + navigation(key))

    key = 'toolsmith-items-maintenance'
    whet, grind, bench, sandpaper = inv['maintenance']
    rows = [
        (option(whet), 'Portable sharpening. Put the whetstone in the offhand and the tool or metal head in the main hand; hold right-click.', 'Chert, granite, andesite, basalt, peridotite. Each has 300 durability; a sharpening tick uses 1. All share the same sharpening rate.'),
        (option(grind), 'Stationary sharpening. Hold a tool or metal head and hold right-click on the placed grindstone.', 'Granite, sandstone, shale, andesite; every orientation. Same sharpening routine as whetstones. These are appearance/placement choices, not sharpening tiers.'),
        (option(bench), 'Repair by component replacement. Place a tool on the bench with the replacement part in its marked slot, then hold right-click.', 'Keep the still-usable parts; a fresh handle or binding does not heal the head. The four bench directions are placement variants.'),
        (option(sandpaper), 'Handle-making consumable: one sandpaper plus a knife and support beam makes a carpented handle in the grid.', 'Listed because the native item has the maintenance tag. It does not sharpen a tool, restore sharpness or heal HP; use whetstones or grindstones for the edge.'),
        ('Reforge a damaged metal head', a(REF['reforging'], 'Open native reforging instructions'), 'Rework a suitable metal head at the anvil. Follow the actual recipe and remaining-metal constraints; sharpening alone cannot restore lost head HP.'),
    ]
    p[key] = (NAMES[key], heading('Maintain the edge and replace worn parts')
        + para('Watch four separate values: head HP, handle HP, binding HP and sharpness. HP means remaining component condition. Sharpness means how keen the edge is, not another HP bar; it affects speed and wear. There is no single whole-tool durability percentage. A sharp tool keeps its other parts healthier; do not wait for a part to break. Blunt-headed tools are a separate wear case and do not become cutting tools by sharpening.')
        + table(('Installed option / recipe', 'How to use it', 'What changes (and what does not)'), rows)
        + para('<strong>Installed settings:</strong> head maximum HP is 5 × B; maximum sharpness is 1.5 × B, subject to integer rounding. Each sharpening tick restores ceil(1% of maximum sharpness). The first honing is free of head-HP cost; later honing can remove head HP. Sharpening does not restore lost head HP. These rates are shared by all five whetstones and all four grindstone stones; no extra geology-mod stones are installed.')
        + para('At ≥98% sharpness, ordinary use avoids head/handle wear and only nicks a pristine binding once. Below 98%, head wear may occur; below 95%, handle wear also becomes eligible, using the grip’s chance. A cutting tool at ≥90% sharpness has a +5% sharpness speed modifier; at ≤33%, −10%. Its handle-plus-grip speed contribution is then applied. Head-wear rolls use 15% SharpWear and 2% BluntWear in their separate eligible branches: they are not a universal loss rate. A fully blunt edge loses extra head HP.')
        + para('On separation, sticks and bones need more than 25% handle HP to survive; crafted handles need more than 5%. Binding recovery uses its own threshold on the Bindings page. More detail: ' + a(REF['wear'], 'Native durability and wear') + ' · ' + a(REF['crafting'], 'Native assembly and replacement') + '.')
        + navigation(key))
    return p


def apply(lang):
    for page_code, (title, text) in pages().items():
        lang[page_code + '-title'] = title
        lang[page_code + '-text'] = text
    lang['mod-toolsmith-text'] = (
        '<strong>Toolsmith</strong> adds modular heads, handles and bindings, grips, treatments and maintenance.\n\n'
        + a('toolsmith-items', NAMES['toolsmith-items']) + '\n\n'
        + a('https://mods.vintagestory.at/toolsmith', 'Official mod page')
    )
    return lang
