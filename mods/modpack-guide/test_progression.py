"""Player-facing acceptance checks, run against both regression and release ZIPs."""
import json, os, re, unittest, zipfile
from release_build import normalize_display_text
from pathlib import Path
W=Path(__file__).parent
ZIP=Path(os.environ.get('GUIDE_ZIP',str(W/'dist/KuboczochModpackGuide-1.3.1.zip')))
P='assets/kuboczochmodpackguide/'
class ProgressionTests(unittest.TestCase):
 @classmethod
 def setUpClass(cls):
  cls.z=zipfile.ZipFile(ZIP);cls.lang=json.loads(cls.z.read(P+'lang/en.json'))
 def test_entry_title(self):self.assertEqual(self.lang['toolsmith-items-title'],'Toolsmith - Pszczoly Guide to Toolsmith')
 def test_real_progression(self):
  s=self.lang['toolsmith-items-text'];self.assertIn('first tool',s);self.assertIn('sharpness',s)
 def test_bounded_pages(self):
  for k,s in self.lang.items():
   if k.startswith('toolsmith-items') and k.endswith('-text'):
    with self.subTest(page=k):
     if k=='toolsmith-items-text':self.assertLessEqual(len(re.sub('<[^>]+>',' ',s).split()),330)
     else:
      self.assertEqual(s.count('<toolsmithtable>'),1)
      for cell in re.findall(r'<toolsmithcell>(.*?)</toolsmithcell>',s,re.S):self.assertLessEqual(len(re.sub('<[^>]+>',' ',cell).split()),65)
     links=re.findall(r'href=[\"\']([^\"\']+)',s);self.assertEqual(len(links),len(set(links)))
 def test_no_attributed_synthetic_links(self):
  for k,s in self.lang.items():
   if k.startswith('toolsmith-items') and k.endswith('-text'):self.assertNotIn('modularPartRenderData',s)
 def test_numeric_comparisons(self):
  self.assertIn('1.26',self.lang['toolsmith-items-handles-text']);self.assertIn('2.34',self.lang['toolsmith-items-bindings-text'])
 def test_family_slideshow(self):self.assertRegex(self.lang['toolsmith-items-grips-text'],r'code="[^\"]+\|[^\"]+"')
 def test_registry_for_family_clicks(self):
  registry=json.loads((W/'native-registry.json').read_text())
  inv=json.loads((W/'attribute_inventory.json').read_text())
  bycode={x['code']:x for x in inv['codes']}
  for k,s in self.lang.items():
   if k.startswith('toolsmith-items') and k.endswith('-text'):
    for group in re.findall(r'<toolsmithfamily code="([^"]+)"',s):
     for code in group.split('|'):
      for stack in bycode[code]['stacks']:self.assertIn(stack['pageCode'],registry)
 def test_numeric_source_and_active_defaults(self):
  from build import PACK
  import json5
  from decimal import Decimal as D
  cfg=json.loads((PACK/'config/ModConfig/ToolsmithPartsStats.json').read_text());self.assertIs(cfg['EnableEdits'],False)
  with zipfile.ZipFile(PACK/'mods/toolsmith_1.2.19.zip') as z:
   def stats(kind):return {x['id']:x for x in json5.loads(z.read(f'assets/toolsmith/config/toolsmith/stats/{kind}/{kind}-vanilla.json').decode(),parse_float=D)}
   h=stats('handles');b=stats('bindings');g=stats('grips');t=stats('treatments')
   self.assertEqual(h['handle']['baseHPfactor']*(1+h['handle']['selfHPBonus']),D('1.26'))
   self.assertEqual(h['professional']['baseHPfactor']*(1+h['professional']['selfHPBonus']),D('1.65'))
   self.assertEqual(b['leather']['baseHPfactor']*(1+b['leather']['selfHPBonus'])*(1+h['handle']['bindingHPBonus']),D('2.34'))
   self.assertEqual(g['leather']['chanceToDamage'],D('.8'));self.assertEqual(g['sturdy']['speedBonus'],D('.3'))
   self.assertEqual(t['oil']['handleHPbonus'],D('.65'))
 def test_cleanup_preserved(self):
  with zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.1.0.zip') as z:old=json.loads(z.read(P+'lang/en.json'))
  for k,s in old.items():
   if not k.startswith('toolsmith-items') and k not in ('root-title','root-text','mod-toolsmith-text'):self.assertEqual(self.lang[k],normalize_display_text(s),k)
if __name__=='__main__':unittest.main()
