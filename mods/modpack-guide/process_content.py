"""Small source-backed process cards, rendered with the unchanged native callback helper."""
import json,re
from pathlib import Path
W=Path(__file__).parent
NS='kuboczochmodpackguide-'
TOPICS={
 'food-cooking':('Kitchen', ['process-kitchen-firstfire','process-kitchen-dough-oven','process-kitchen-pasta','process-kitchen-broth','process-kitchen-dairy','process-kitchen-bottles','process-kitchen-tea-storage']),
 'farming-husbandry':('Fields and animals',['process-farming-animal-care','process-butchering-leather','process-bees']),
 'crafting-workshops':('Workshops and materials',['process-wood-to-workshop','process-ore-to-equipment','process-powders-to-ceramics','process-textiles','process-workshop-storage','process-building-materials','process-heraldic-decor']),
 'gear-tools':('Equipment',['process-tableware-equipment']),
 'movement-transport':('Travel',['world-overland','world-water']),
 'combat-survival':('Survival',['world-survival']),
 'economy-community':('Trade and community',['world-trade'])}
CROSS={
 'process-kitchen-firstfire':['process-wood-to-workshop','process-kitchen-dough-oven','process-kitchen-pasta'],
 'process-kitchen-dough-oven':['process-farming-animal-care','process-powders-to-ceramics','process-kitchen-tea-storage'],
 'process-kitchen-pasta':['process-farming-animal-care','process-kitchen-broth','process-kitchen-tea-storage'],
 'process-kitchen-broth':['process-butchering-leather','process-kitchen-pasta'],
 'process-kitchen-dairy':['process-farming-animal-care','process-kitchen-tea-storage'],
 'process-kitchen-bottles':['process-bees','process-wood-to-workshop','process-workshop-storage'],
 'process-kitchen-tea-storage':['process-workshop-storage','world-water'],
 'process-farming-animal-care':['process-kitchen-dough-oven','process-textiles','process-butchering-leather','process-kitchen-dairy'],
 'process-butchering-leather':['process-wood-to-workshop','process-kitchen-broth','process-kitchen-firstfire','process-tableware-equipment'],
 'process-bees':['process-farming-animal-care','process-kitchen-bottles'],
 'process-wood-to-workshop':['process-butchering-leather','process-ore-to-equipment','process-kitchen-firstfire','process-workshop-storage'],
 'process-ore-to-equipment':['process-wood-to-workshop','process-tableware-equipment','world-survival'],
 'process-powders-to-ceramics':['process-wood-to-workshop','process-kitchen-bottles','process-building-materials'],
 'process-textiles':['process-farming-animal-care','process-heraldic-decor','process-tableware-equipment'],
 'process-workshop-storage':['process-kitchen-tea-storage','world-water'],
 'process-building-materials':['process-wood-to-workshop','process-powders-to-ceramics','process-heraldic-decor'],
 'process-heraldic-decor':['process-textiles','process-building-materials'],
 'process-tableware-equipment':['process-ore-to-equipment','process-butchering-leather','world-survival'],
 'world-overland':['world-water','world-survival','world-trade'],
 'world-water':['process-wood-to-workshop','process-workshop-storage','world-overland'],
 'world-survival':['process-ore-to-equipment','process-butchering-leather','process-kitchen-firstfire'],
 'world-trade':['world-overland','process-heraldic-decor','process-workshop-storage']}

def key(code):return code.removeprefix(NS)
def link(code,label):return f'<a href="handbook://{code}">{label}</a>'
def plain(s):return s.replace('&amp;','and').replace('&','and').replace('<','&lt;').replace('>','&gt;')
def drafts():
 result={}
 for source in sorted((W/'research').glob('*.json')):
  d=json.loads(source.read_text())
  for p in d.get('pages',[]):
   p=dict(p);p['code']=key(p['code']);p['source']=source.relative_to(W).as_posix()
   gs=p.get('guides',p.get('relatedNativeGuides',[]))
   p['guides']=[g if isinstance(g,dict) else {'code':NS+g if g.startswith(('process-','world-')) else g} for g in gs]
   p['mods']=p.get('mods',p.get('modIdsCovered',[]))
   assert p['code'] not in result,p['code'];result[p['code']]=p
 from process_editorial import apply
 from process_labels import annotate
 return annotate(apply(result))

def pages():
 ds=drafts();out={};groups={c:t for t,(_,cs) in TOPICS.items() for c in cs}
 assert set(ds)==set(groups),(set(ds)-set(groups),set(groups)-set(ds))
 for c,p in ds.items():
  topic=groups[c];title='Pszczoly - '+p['title'].removeprefix('Kitchen - ')
  body=[plain(p['intro'])];seen=set()
  for i,s in enumerate(p['steps'],1):
   refs=[]
   for item in s.get('items',[]):
    codes=item['code'].split('|');ident=(item['type'],item['code'])
    if ident in seen:continue
    seen.add(ident)
    label=item['label']
    refs.append(f'<toolsmithitem type="{item["type"]}" code="{item["code"]}"></toolsmithitem> '+plain(label))
   label=re.sub(r'^\d+[.)]\s*','',s['label'])
   body.append(f'<strong>{i}. {plain(label)}</strong><br>'+(' · '.join(refs)+'<br>' if refs else '')+plain(s['text']))
  links={g['code']:g['label'] for g in p['guides']}
  for x in CROSS[c]:links[NS+x]=ds[x]['title'].removeprefix('Kitchen - ')
  body.append('<strong>Recipes and next steps</strong><br>'+'<br>'.join(link(x,plain(label)) for x,label in links.items()))
  body.append(link(NS+'process-index-'+topic,'All '+TOPICS[topic][0].lower()+' guides')+' · '+link(NS+'category-'+topic,'Mod directory'))
  out[c]=(title,'<br><br>'.join(body))
 for t,(label,cs) in TOPICS.items():
  body=['Choose a short process. Click item pictures to open the native item handbook and its recipes. Representative materials are examples, not forced upgrade tiers.']
  body.extend(link(NS+c,plain(ds[c]['title'])) for c in cs)
  if t in ('crafting-workshops','gear-tools'):body.append(link(NS+'toolsmith-items','Toolsmith - Pszczoly Guide to Toolsmith'))
  body.append(link(NS+'category-'+t,'Related mods')+' · '+link(NS+'root','Mod index'))
  out['process-index-'+t]=('Pszczoly - '+label+' guides','<br><br>'.join(body))
 return out

if __name__=='__main__':
 for c,(title,text) in pages().items():print(c,len(re.sub('<[^>]+>',' ',text).split()),title)
