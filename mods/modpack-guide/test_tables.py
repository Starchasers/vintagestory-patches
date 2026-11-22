"""1.3.1 complete-table regression contract; run against generated install ZIP."""
import collections, html, json, re, unittest, zipfile
from pathlib import Path
import xml.etree.ElementTree as ET
from progression_content import pages, inventory, NAMES
from release_build import normalize_display_text
W=Path(__file__).parent
P='assets/kuboczochmodpackguide/'
class Tables(unittest.TestCase):
 def test_complete_one_row_per_resolved_option(self):
  inv=inventory()
  self.assertEqual(inv['resolutionErrors'],[])
  for category,groups in inv['groups'].items():
   resolved=[x['id'] for x in inv['options'] if x['category']==category and x['resolved']]
   covered=[x for g in groups for x in g['optionIds']]
   self.assertEqual(collections.Counter(resolved),collections.Counter(covered))
   self.assertTrue(all(v==1 for v in collections.Counter(covered).values()))
   for g in groups:
    entries=[x for x in inv['options'] if x['category']==category and x['id'] in g['optionIds']]
    self.assertTrue(all(x['stats']==entries[0]['stats'] for x in entries))
   text=pages()['toolsmith-items-'+category][1]
   tree=ET.fromstring('<page>'+text+'</page>')
   table=tree.find('toolsmithtable'); self.assertIsNotNone(table)
   self.assertEqual(len(table),len(groups)+1+(category!='handles'))
   self.assertTrue(all(len(row)==3 for row in table))
   for g in groups:
    for code in g['codes']: self.assertIn(code,text)
 def test_unique_item_references_and_no_family_overlap(self):
  attrs={x['code']:x['stacks'] for x in json.loads((W/'attribute_inventory.json').read_text())['codes']}
  for key,(_,text) in pages().items():
   seen=set()
   tree=ET.fromstring('<page>'+text+'</page>')
   for widget in tree.iter():
    if widget.tag not in ('toolsmithfamily','toolsmithitem','itemstack'):continue
    for code in widget.attrib['code'].split('|'):
     variants=attrs[code] if widget.tag=='toolsmithfamily' else [{'attributes':{}}]
     for s in variants:
      semantic=(code,json.dumps(s['attributes'],sort_keys=True))
      self.assertNotIn(semantic,seen,(key,code));seen.add(semantic)
 def test_titles_navigation_and_entities(self):
  p=pages()
  self.assertEqual(NAMES['toolsmith-items'],'Toolsmith - Pszczoly Guide to Toolsmith')
  for key,(title,text) in p.items():
   self.assertTrue(title.startswith('Toolsmith - '));self.assertNotIn('&amp;',title)
   self.assertNotIn('&amp;amp;',text);self.assertNotIn('Grips &amp; Bindings',text)
   for dest,label in re.findall(r'<a href="handbook://kuboczochmodpackguide-(toolsmith-items[^"]*)">([^<]+)</a>',text):
    self.assertEqual(html.unescape(label),NAMES[dest])
  entry=p['toolsmith-items'][1]
  for key in NAMES:
   if key!='toolsmith-items':self.assertEqual(entry.count('handbook://kuboczochmodpackguide-'+key+'"'),1)
 def test_eligibility_and_drying(self):
  p=pages()
  self.assertIn('Bone and crude handles take grips only',p['toolsmith-items-handles'][1])
  self.assertIn('Bone and crude handles',p['toolsmith-items-grips'][1])
  text=p['toolsmith-items-treatments'][1]
  self.assertIn('carved and carpented',text)
  self.assertIn('carpented: 24',text);self.assertIn('carpented: 48',text)
  self.assertNotIn('Stick and bone cannot take a grip',p['toolsmith-items-grips'][1])
 def test_only_scoped_install_files_changed(self):
  allowed={'modinfo.json','KuboczochModpackGuide.Client.dll',P+'lang/en.json',P+'config/toolsmith-families.json'}
  allowed.update(P+'config/handbook/'+key+'.json' for key in NAMES)
  with zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.2.0.zip') as old, zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.3.1.zip') as new:
   self.assertEqual(set(old.namelist()),set(new.namelist()))
   for name in old.namelist():
    if name not in allowed:self.assertEqual(old.read(name),new.read(name),name)
 def test_pack_integration_and_preservation(self):
  with zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.3.1.zip') as z:
   info=json.loads(z.read('modinfo.json'));self.assertEqual(info['modid'],'kuboczochmodpackguide');self.assertEqual(info['version'],'1.3.1')
   lang=json.loads(z.read(P+'lang/en.json'))
   for key,(title,text) in pages().items():
    page=json.loads(z.read(P+'config/handbook/'+key+'.json'))
    self.assertEqual(page['categoryCode'],'kuboczochmodpackguide-index')
    self.assertEqual(page['pageCode'],'kuboczochmodpackguide-'+key)
    self.assertEqual(lang[key+'-title'],title);self.assertEqual(lang[key+'-text'],text)
   for key in ('root','mod-toolsmith'):
    self.assertIn('handbook://kuboczochmodpackguide-toolsmith-items',lang[key+'-text'])
    self.assertIn(NAMES['toolsmith-items'],lang[key+'-text'])
   for k,v in lang.items():
    if k.endswith('-title'):self.assertNotIn('&amp;',v,k)
   with zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.2.0.zip') as old:
    before=json.loads(old.read(P+'lang/en.json'))
    for k,v in before.items():
     if not k.startswith('toolsmith-items') and k not in ('root-title','root-text','mod-toolsmith-text'):self.assertEqual(lang[k],normalize_display_text(v),k)
 def test_entity_normalization_only_changes_display_text(self):
  self.assertEqual(normalize_display_text('<a href="https://example.invalid/?x=1&amp;y=2">A &amp; B</a>'),'<a href="https://example.invalid/?x=1&amp;y=2">A and B</a>')
  self.assertEqual(normalize_display_text('A &lt; B &gt; C &nbsp; D'),'A &lt; B &gt; C &nbsp; D')
  with zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.3.1.zip') as z:
   lang=json.loads(z.read(P+'lang/en.json'))
   for key,value in lang.items():
    self.assertNotIn('&amp;',''.join(re.split(r'<[^>]*>',value)),key)
if __name__=='__main__':unittest.main()
