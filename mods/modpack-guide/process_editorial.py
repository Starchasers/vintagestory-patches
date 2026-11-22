"""Reviewed source corrections applied before emission; original research remains audit history."""
import json
from pathlib import Path
W=Path(__file__).parent

def apply(ds):
 corrections=json.loads((W/'research/tweaks.json').read_text())['editorialCorrections']
 byid={c['id']:c for c in corrections}
 def edit(page,index,text,ids=()):
  s=ds[page]['steps'][index];s['text']=text
  for ident in ids:
   c=byid[ident];s['evidence'].extend(c['evidence']);ds[page]['mods'].extend(c['modIds'])
  if ids:s['evidence'].append('research/tweaks.json#editorialCorrections:'+','.join(ids))
 def add(page,index,text,ids=()):edit(page,index,ds[page]['steps'][index]['text']+' '+text,ids)
 # Independent review: native log-tannin recipes are actively stripped by IW.
 p=ds['process-butchering-leather'];t=next(i for i,s in enumerate(p['steps']) if s['label']=='Tan twice')
 edit('process-butchering-leather',t,'Make weak tannin from tanning bark and water, then strong tannin from tanning bark and weak tannin. Seal scraped hide in weak tannin to prepare it, then seal prepared hide in strong tannin. Vanilla log-to-tannin recipes are disabled here; use the bark recipes for quantities and sealing times.')
 p['steps'][t]['evidence'].extend(['config/ModConfig/ImmersiveWoodworking/general.json','mods/immersivewoodworking_1.3.13.zip!assets/immersivewoodworking/recipes/barrel/weaktannin-bark.json','mods/immersivewoodworking_1.3.13.zip!assets/immersivewoodworking/recipes/barrel/strongtannin-bark.json','/home/brunette/.hermes/cache/scratch/woodworking-tutorial/IwSystem.cs'])
 p['mods'].append('immersivewoodworking')
 edit('process-wood-to-workshop',2,'From an end, hold right-click with a saw; select planks or beams in tool mode. Normal saws are allowed; keep matching logs for continuous feeding. For tannin instead, strip the side using a bark spud or axe with an offhand hammer. Use fresh tanning bark, or dry and quern it for the alternative barrel recipes.')
 ds['process-wood-to-workshop']['steps'][2]['evidence'].extend(p['steps'][t]['evidence'])
 # Woodworking overview is code-native (verified owner source and installed IL string), not a JSON page.
 add('process-wood-to-workshop',0,'Dropped tree seeds can self-plant after a delay; resin can regrow. Neither is instant.',('forestry-seed-resin',))
 # Short useful player text, not evidence-review notes or guessed controls.
 edit('world-water',2,'Rivers do not push boats in this pack. Paddling and sculling are faster, but that does not make a hull stormproof. Keep repair materials aboard and avoid damaging the vessel with an axe.')
 edit('world-overland',1,'Scout a route home before trying cliffs. Watersheds, Rivers and Terra Prety alter terrain. Climbing is limited to stone and ore; sprint-jumping is disabled. Test the movement controls on low ground before committing to a drop.')
 add('world-overland',3,'For hauling, follow the native cart/sled construction recipes; horses have species-specific saddlery. Carry On portability is not vehicle construction.',('transport-native-branches',))
 edit('world-survival',1,'Melee swings stop on terrain; spear directional attacks and vanilla armour grid recipes are disabled. Practise in open ground and follow the current equipment recipes. Bellhead alarms can summon enemies; do not expect vanilla enemy behaviour.',('temporal-enemy-caveat',))
 edit('world-survival',2,'Self-revive and passed-out recovery are disabled; the map is hidden while downed. Agree a regroup and rescue plan before splitting up. Drifters can open doors here: an auto-closing door is not a secure recovery shelter.',('shelter-drfter-door',))
 edit('process-kitchen-firstfire',0,'Work outdoors or vent smoke to open air: Real Smoke can suffocate players. Gather dry grass and firewood; make a firestarter from two diagonal sticks. Sneak-right-click grass onto solid ground, then add four firewood. Save separate tinder for ignition.',('indoor-fire-smoke',))
 edit('process-kitchen-dough-oven',0,'Select flour in the active hotbar slot. Put a suitable water-containing item in another hotbar slot, or target a suitable placed water container; hold right-click to knead. Offhand water alone is not sufficient. Grid dough recipes are disabled, including modded dough outputs. Expanded Foods kneading and Core of Arts liquid mixing are separate branches.',('dough-grid-scope',))
 ds['process-kitchen-dough-oven']['steps'][0]['evidence'].append('evidence/process-static/ManualDough.cs#TransformFlourToDough,TryConsumeWaterFromHotbar,TryConsumeWaterFromContainer')
 ds['process-kitchen-dough-oven']['guides'].append({'code':'artsguides-liquidmixing','label':'Liquid mixing'})
 edit('process-kitchen-dough-oven',2,'Craft Heat Source, Heat Distribution and Baking Space. Put Distribution beside Source, then Baking Space in front of Distribution. This is the Stone Bake Oven layout. A vanilla clay oven is a different branch: DanaTweaks lets a burning firepit directly below heat it.',('oven-dana-heating',))
 edit('process-kitchen-dough-oven',3,'Fuel and torch-light Heat Source; place dough on Baking Space. Heat falls with distance. Vent indoor firepits and smoke-producing heat sources to open air; custom sources may differ. Do not assume a decorative chimney makes a kitchen safe.',('oven-smoke',))
 edit('process-kitchen-dairy',0,'Seal fresh milk in a barrel in multiples of 10 L. After 24 in-game hours it becomes separating milk. Make a cream scoop from a fired bowl and stick. Pasteurization is separate: cook vanilla milk in a container for Expanded Foods pasteurized milk.',('milk-separate-paths','milk-config-scope'))
 add('process-kitchen-dairy',1,'Skim milk and whey also enter Expanded Foods kneading recipes; that does not make milk interchangeable with water in Manual Dough.',('milk-dough-ingredient-links',))
 add('process-farming-animal-care',0,'Rain collection fills compatible exposed containers; it is not automatic field irrigation.',('farming-collection-not-irrigation','farming-config-not-examples'))
 edit('process-farming-animal-care',4,'Keep animals clean, sheltered and watered; welfare can affect survival, milk and fibre. To milk an eligible animal, target it directly with a compatible liquid container rather than the adjacent block.',('milking-interaction-order',))
 edit('process-farming-animal-care',5,'Livestock Welfare’s breeding module is disabled: its bedding and pregnancy settings are not extra requirements. This does not disable animal breeding. GeneLib and species behaviours still matter; do not apply one species’ genetics or manure timing to every animal.',('genetics-disabled-welfare-scope',))
 add('process-ore-to-equipment',3,'Armour-quenching compatibility is item-specific; do not assume every armour piece accepts the same treatment.',('legacy-equipment-not-vanilla',))
 add('process-building-materials',3,'Chiseled-block side solidity does not guarantee a sealed, warm or smoke-safe room. Terrain Slabs are a separate recipe branch, not full-block equivalents.',('shelter-chiseling','terrain-slab-scope'))
 # Keep no implementation assertions in player steps.
 edit('process-butchering-leather',0,'Right-click a supported dead animal to carry it in a free bag slot; small bodies fit ordinary inventory slots. Bring it home promptly: field harvesting sacrifices yield.')
 edit('process-butchering-leather',3,'Cook two raw primemeat with two litres of water in a pot to obtain one rendered fat. Keep rendered fat for the recipes that ask for it; it is not interchangeable with every other fat ingredient.')
 edit('process-farming-animal-care',3,'Fill a trough with feed suitable for the animal species. Rye seeds can go into the large trough; check each animal’s diet rather than assuming every animal eats them.')
 edit('process-farming-animal-care',6,'Turn appropriate manure into slurry and compost in barrels, then use the listed fertilizer recipes to restore field nutrients. Different animals need not supply the same ingredients.')
 edit('process-textiles',4,'Spin the washed fibres into wool twine with the spindle recipe. Twine plus a stick makes portable wool cloth; the square-twine recipe makes a wool block. Use the form named by your garment recipe.')
 for p in ds.values():
  p['mods']=sorted(set(p['mods']))
  for s in p['steps']:
   s['text']=s['text'].replace('The shown blue raw/fired molds are actual native codes; clayforming is not an ordinary crafting-grid recipe.','Clayforming is not a crafting-grid recipe.').replace('Exact harvesting gestures and wear costs remain unverified.','').replace('This is a source-backed branch, not a fixed universal tier.','')
 return ds
