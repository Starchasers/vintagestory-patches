"""Build a static pack registration fixture; no mod DLLs are loaded."""
import json,zipfile,json5
from pathlib import Path
from build import PACK,VS
W=Path(__file__).parent
pages=[]
def add(name,raw):
 if '/config/handbook/' not in '/'+name or not name.endswith('.json'):return
 try:
  d=json5.loads(raw)
  if isinstance(d,dict) and 'pageCode' in d:pages.append(d)
 except Exception:pass
for p in VS.glob('assets/*/config/handbook/**/*.json'):add(p.as_posix(),p.read_text())
for m in json.loads((PACK/'evpack.json').read_text())['Mods']:
 if m['Enabled']:
  with zipfile.ZipFile(PACK/'mods'/m['FileName']) as z:
   for n in z.namelist():
    if '/config/handbook/' in n:add(n,z.read(n))
with zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.4.0.zip') as z:
 for n in z.namelist():
  if '/config/handbook/' in n:add(n,z.read(n))
# Preserve the selected release when replacing the older installed guide assets.
pages=list({p['pageCode']:p for p in pages}.values())
codes=json.loads((W/'vanilla-codes.json').read_text())
# Only referenced plain vanilla collectibles are needed; item/block kind is explicit.
import re
from progression_content import pages as authored
stacks=[]
resolved={c['code']:c for x in json.loads((W/'table_inventory.json').read_text())['options'] for c in x['resolved']}
for _,text in authored().values():
 for kind,group in re.findall(r'<(?:itemstack|toolsmithitem) type="(item|block)" code="([^"]+)"',text):
  for c in group.split('|'):
   assert c in codes or c in resolved,c
   if c in resolved: assert resolved[c]['kind']==kind,c
   stacks.append({'code':c,'kind':kind,'attributes':{}})
for entry in json.loads((W/'attribute_inventory.json').read_text())['codes']:
 if entry['internal']:continue
 for s in entry['stacks']:stacks.append({'code':entry['code'],'kind':entry['kind'],'attributes':s['attributes']})
from process_content import pages as process_pages
for row in json.loads((W/'process-reference-inventory.json').read_text())['resolved']:
 stacks.append({'code':row['code'],'kind':row['type'],'attributes':{}})
# Code-native recipe guides are not JSON pages. Their exact keys come from
# static IL/pack language evidence, recorded by the kitchen/workshop audits.
for code in ('aculinaryartillery:recipeindex','aculinaryartillery:recipelist-broth','aculinaryartillery:recipelist-gelatin','aculinaryartillery:recipelist-pastanoodle','immersivewoodworking:craftinginfo-barkbasket','craftinginfo-woodworking'):
 pages.append({'pageCode':code,'title':'Static code-native registration fixture','text':''})
(W/'registration-input.json').write_text(json.dumps({'customPages':pages,'stacks':stacks},indent=2))
print(len(pages),'custom pages;',len(stacks),'stacks')
