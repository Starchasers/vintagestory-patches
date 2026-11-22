"""Shared root directory content for cleanup and release regeneration."""
import re
TITLE = 'Pszczoly Modpack - Mod Index'
PREFIX = 'handbook://kuboczochmodpackguide-'
TOPICS = (
 ('food-cooking', 'Food and Cooking', 'Ingredients, meals, drinks and food preservation.'),
 ('crafting-workshops', 'Crafting and Workshops', 'Toolmaking, woodworking, mineral processing and clothing crafts.'),
 ('gear-tools', 'Gear and Tools', 'Travel equipment and building tools.'),
 ('farming-husbandry', 'Farming and Husbandry', 'Crops, beekeeping and livestock.'),
 ('movement-transport', 'Movement and Transport', 'Movement, hauling and land or water transport.'),
 ('combat-survival', 'Combat and Survival', 'Combat, hunting, fire safety and player assistance.'),
 ('economy-community', 'Economy and Community', 'Trading, currency, notices and games.'),
)
def root_content(previous):
    parts = ['Browse the mods in the Pszczoly Modpack by category.', '<strong>Mod categories</strong><br>' + '<br><br>'.join(
        f'• <a href="{PREFIX}category-{key}">{label}</a> — {scope}' for key,label,scope in TOPICS)]
    # Preserve genuine authored root-guide links, never the handpicked mod overviews.
    guides = dict(re.findall(r'<a href="(handbook://kuboczochmodpackguide-(?!mod-|category-)[^"]+)">([^<]+)</a>', previous))
    guides[PREFIX+'toolsmith-items'] = 'Toolsmith - Pszczoly Guide to Toolsmith'
    parts.append('<strong>Our guides</strong><br>' + '<br>'.join(f'<a href="{href}">{label}</a>' for href,label in guides.items()))
    return '<br><br>'.join(parts)
