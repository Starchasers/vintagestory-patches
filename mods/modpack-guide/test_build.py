import unittest,json,re,zipfile
from pathlib import Path
W=Path(__file__).parent
class BuildTests(unittest.TestCase):
 def setUp(self):
  self.z=zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.1.0.zip');self.l=json.loads(self.z.read('assets/kuboczochmodpackguide/lang/en.json'));self.i=json.loads((W/'inventory.json').read_text())
 def test_index(self):
  self.assertEqual(self.l['toolsmith-items-title'],'Pszczoly — Toolsmith Items')
  for k in ['root-text','mod-toolsmith-text']:self.assertIn('kuboczochmodpackguide-toolsmith-items',self.l[k])
 def test_coverage(self):
  from pack_audit import ANCHOR,_href
  rendered='\n'.join(v for k,v in self.l.items() if k.startswith('toolsmith-items'))
  destinations={_href(m) for m in ANCHOR.finditer(rendered)}
  attrs=json.loads((W/'attribute_inventory.json').read_text())
  for row in attrs['codes']:
   if row['internal']:self.assertIn('Recovered tool parts',rendered);continue
   if row['navigationDestination']:self.assertIn(row['navigationDestination']['href'],destinations,row['code'])
   for s in row['stacks']:self.assertIn(s['href'],destinations,row['code'])
  native={r['code'] for r in attrs['codes']}
  for r in self.i['parts']:
   for v in r['resolved']:
    if v['code'] not in native:self.assertIn('handbook://'+v['type']+'-'+v['code'].removeprefix('game:'),destinations)
  self.assertEqual(self.i['counts']['nativeItems'],15);self.assertEqual(self.i['counts']['nativeBlocks'],20)
  self.assertEqual(attrs['counts']['nativeHandbookStacks'],85)
  self.assertFalse(self.i['resolutionErrors'])
  handles=self.l['toolsmith-items-handles-text'];self.assertLess(handles.index('item-stick'),handles.index('toolsmith:crudehandle'));self.assertLess(handles.index('toolsmith:handle-'),handles.index('toolsmith:carpentedhandle-'))
 def test_boilerplate_duplicates(self):
  from pack_audit import ANCHOR,_href
  for k,t in self.l.items():
   h=[_href(m) for m in ANCHOR.finditer(t)];self.assertEqual(len(h),len(set(h)),k)
   self.assertNotRegex(t,r'Installed version|audited|generated index|source.policy|\[Native guides\]|\[Mod DB\]|…')
 def test_preservation(self):
  from pack_audit import clean
  old=clean(json.loads((W/'baseline_en.json').read_text()))
  for k,v in old.items():
   if k.endswith('-text'):self.assertTrue(set(re.findall('href="([^"]+)"',v))<=set(re.findall('href="([^"]+)"',self.l[k])),k)
 def test_compact_icons_and_metal_progression(self):
  for k,t in self.l.items():
   if not k.startswith('toolsmith-items'):continue
   self.assertNotIn('survival:',t)
   for size in re.findall('rsize="([^"]+)"',t):self.assertLessEqual(float(size),1.5)
  bindings=self.l['toolsmith-items-bindings-text']
  positions=[bindings.index('Metal fasteners — material tier '+str(t)) for t in [1,2,3,4]]
  self.assertEqual(positions,sorted(positions))
 def test_packages(self):
  names=self.z.namelist();self.assertFalse(any(n.endswith('.dll') for n in names));m=json.loads(self.z.read('modinfo.json'));self.assertEqual(m['modid'],'kuboczochmodpackguide');self.assertEqual(m['version'],'1.1.0')
  self.assertFalse(any('mod-immersivemining' in n for n in names))
  pages={json.loads(self.z.read(n))['pageCode'] for n in names if '/config/handbook/' in n}
  for v in self.l.values():
   for h in re.findall('href="handbook://(kuboczochmodpackguide-[^"]+)"',v):self.assertIn(h,pages)
  base=zipfile.ZipFile('/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/KuboczochModpackGuide-1.0.2.zip')
  for n in base.namelist():
   if n.endswith('.json') and '/config/handbook/' in n and 'immersivemining' not in n:self.assertEqual(base.read(n),self.z.read(n))
if __name__=='__main__':unittest.main()
