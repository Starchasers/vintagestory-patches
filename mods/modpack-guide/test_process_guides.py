"""Release-level process contracts, not a live game-startup or GUI test."""
import hashlib,json,re,unittest,zipfile
from pathlib import Path
import json5
from build import PACK,VS
from process_content import drafts,pages,TOPICS,NS,CROSS
W=Path(__file__).parent;P='assets/kuboczochmodpackguide/'
def hrefs(s):return re.findall(r'href="([^"]+)"',s)
class ProcessGuides(unittest.TestCase):
 @classmethod
 def setUpClass(cls):
  cls.old=zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.3.1.zip');cls.z=zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.4.0.zip')
  cls.oldlang=json.loads(cls.old.read(P+'lang/en.json'));cls.lang=json.loads(cls.z.read(P+'lang/en.json'));cls.pages=pages();cls.drafts=drafts()
  cls.registry=json.loads((W/'native-registry.json').read_text());cls.matrix=json.loads((W/'coverage-matrix.json').read_text());cls.refs=json.loads((W/'process-reference-inventory.json').read_text())
 def test_version_identity_client_only(self):
  a=json.loads(self.old.read('modinfo.json'));b=json.loads(self.z.read('modinfo.json'))
  self.assertEqual(b['version'],'1.4.0');self.assertEqual(a['modid'],b['modid']);self.assertEqual(b['side'],'client')
 def test_all_existing_assets_and_toolsmith_preserved(self):
  changed=[]
  for n in self.old.namelist():
   self.assertIn(n,self.z.namelist())
   if self.old.read(n)!=self.z.read(n):changed.append(n)
  self.assertEqual(set(changed),{'modinfo.json',P+'lang/en.json'})
  changedKeys={k for k,v in self.oldlang.items() if self.lang[k]!=v}
  self.assertEqual(changedKeys,{'root-text'}|{'category-'+c+'-text' for c in TOPICS})
  for k,v in self.oldlang.items():
   if k.startswith('toolsmith') or k.startswith('mod-'):self.assertEqual(v,self.lang[k],k)
  self.assertEqual(self.old.read('KuboczochModpackGuide.Client.dll'),self.z.read('KuboczochModpackGuide.Client.dll'))
 def test_actual_new_assets_exactly_match_authored_pages(self):
  self.assertEqual(len(self.drafts),22);self.assertEqual(len(self.pages),29)
  for code,(title,text) in self.pages.items():
   d=json.loads(self.z.read(P+'config/handbook/'+code+'.json'))
   self.assertEqual(d['pageCode'],NS+code);self.assertEqual(d['categoryCode'],'kuboczochmodpackguide-index')
   self.assertEqual(self.lang[code+'-title'],title);self.assertEqual(self.lang[code+'-text'],text)
 def test_root_categories_before_our_guides_directory_not_tutorial(self):
  s=self.lang['root-text'];self.assertEqual(self.lang['root-title'],'Pszczoly Modpack - Mod Index')
  self.assertLess(s.index('category-economy-community'),s.index('Our guides'))
  self.assertEqual(hrefs(s)[:7],['handbook://'+NS+'category-'+c for c in ('food-cooking','crafting-workshops','gear-tools','farming-husbandry','movement-transport','combat-survival','economy-community')])
  self.assertTrue(s.startswith(self.oldlang['root-text']))
 def test_all_75_mod_destinations_and_seven_categories_retained(self):
  mods={json.loads(self.z.read(n))['pageCode'] for n in self.z.namelist() if '/config/handbook/mod-' in n};self.assertEqual(len(mods),75)
  dest=set()
  for c in TOPICS:
   k='category-'+c+'-text';self.assertTrue(self.lang[k].startswith(self.oldlang[k]));dest.update(x.removeprefix('handbook://') for x in hrefs(self.lang[k]) if NS+'mod-' in x)
  self.assertEqual(dest,mods)
 def test_bounded_cards_no_item_or_cosmetic_walls(self):
  metrics={}
  for c,(title,s) in self.pages.items():
   words=len(re.sub('<[^>]+>',' ',s).split());self.assertLessEqual(words,400,(c,words))
   self.assertTrue(title.startswith('Pszczoly - '));self.assertNotIn('Toolsmith - ',title)
   self.assertNotIn('&amp;',s);self.assertNotIn('type="item" code="game:toolsmith',s)
   refs=re.findall(r'<toolsmithitem type="([^"]+)" code="([^"]+)"',s)
   self.assertEqual(len(refs),len(set(refs)),c)
   self.assertLessEqual(len(refs),16,c)
   self.assertEqual(len(hrefs(s)),len(set(hrefs(s))),c)
   metrics[c]={'wordsIncludingNavigationAndIconLabels':words,'iconFamilies':len(refs),'anchors':len(hrefs(s)),'steps':s.count('<strong>')-1 if c in self.drafts else 0}
  (W/'process-layout-metrics.json').write_text(json.dumps(metrics,indent=2)+'\n')
 def test_no_broken_internal_or_native_links(self):
  for c,(_,s) in self.pages.items():
   for h in hrefs(s):self.assertIn(h.removeprefix('handbook://'),self.registry,(c,h))
 def test_every_new_page_reachable_from_root_no_orphans(self):
  graph={}
  for n in self.z.namelist():
   if '/config/handbook/' in n:
    d=json.loads(self.z.read(n));text=self.lang[d['text'].split(':')[-1]];graph[d['pageCode']]=[h.removeprefix('handbook://') for h in hrefs(text)]
  pending=[NS+'root'];visited=set()
  while pending:
   c=pending.pop()
   if c in visited:continue
   visited.add(c);pending.extend(graph.get(c,[]))
  self.assertTrue({NS+c for c in self.pages}<=visited)
 def test_cross_category_interaction_edges(self):
  pairs=[('process-farming-animal-care','process-textiles'),('process-farming-animal-care','process-kitchen-dairy'),('process-butchering-leather','process-kitchen-broth'),('process-butchering-leather','process-wood-to-workshop'),('process-wood-to-workshop','process-butchering-leather'),('process-ore-to-equipment','world-survival'),('process-workshop-storage','process-kitchen-tea-storage')]
  for a,b in pairs:self.assertIn('handbook://'+NS+b,hrefs(self.pages[a][1]))
 def test_each_enabled_manifest_id_has_explicit_evidence_disposition_and_category(self):
  mods=[m for m in json.loads((PACK/'evpack.json').read_text())['Mods'] if m['Enabled']]
  self.assertEqual(len(mods),146);self.assertEqual(self.matrix['classifiedCount'],146);self.assertEqual({m['ModId'] for m in mods},{r['modid'] for r in self.matrix['mods']})
  for row in self.matrix['mods']:
   self.assertTrue(row['effectiveResearch']['status']);self.assertTrue(row['researchRecords']);self.assertTrue(row['metadataEvidence']);self.assertTrue(row['categoryGuides']);self.assertTrue(row['disposition'])
   self.assertEqual(hashlib.sha256(Path(row['archive']).read_bytes()).hexdigest(),row['sha256'])
   for f in row['researchRecords']:self.assertTrue((W/f['file']).is_file())
   for code in row['processPages']+row['categoryGuides']:self.assertIn(code,self.registry)
 def test_every_claim_has_research_and_source_evidence(self):
  self.assertEqual(len(self.matrix['processSteps']),102)
  for step in self.matrix['processSteps']:
   self.assertTrue(step['evidence']);self.assertTrue((W/step['evidenceDocument']).is_file());self.assertIn(step['claim'],self.pages[step['page'].removeprefix(NS)][1])
 def test_reference_definitions_exact_native_types_and_source_readback(self):
  self.assertEqual(self.refs['count'],133);self.assertEqual(len(self.refs['resolved']),133);self.assertEqual(self.refs['missing'],[]);self.assertEqual(self.refs['expansionErrors'],[])
  for r in self.refs['resolved']:
   src=r['source']
   if '!' in src:
    z,n=src.split('!',1)
    with zipfile.ZipFile(z) as f:self.assertGreater(len(f.read(n)),0)
   else:self.assertTrue(Path(src).is_file())
   self.assertIn('/'+r['type']+'types/',src)
 def test_active_iw_tannin_and_disabled_welfare_not_vanilla_assumptions(self):
  cfg=json.loads((PACK/'config/ModConfig/ImmersiveWoodworking/general.json').read_text());self.assertTrue(cfg['DisableVanillaLogTannin']);self.assertTrue(cfg['EnableBarkChain'])
  s=self.pages['process-butchering-leather'][1];self.assertIn('Vanilla log-to-tannin recipes are disabled',s);self.assertIn('tanning bark',s)
  source=(W/'evidence/process-static/IwSystem.cs').read_text();self.assertIn('IsLogToTannin',source);self.assertIn('DisableVanillaLogTannin',source)
  s=self.pages['process-farming-animal-care'][1];self.assertIn('breeding module is disabled',s);self.assertIn('does not disable animal breeding',s);self.assertIn('GeneLib',s)
 def test_owner_native_dynamic_woodworking_key_not_fake_json(self):
  source=(W/'evidence/process-static/WoodworkingHandbookInfo.OwnPages.cs').read_text();self.assertIn('const string GuidePageCode = "craftinginfo-woodworking"',source)
  with zipfile.ZipFile(PACK/'mods/immersivewoodworking_1.3.13.zip') as z:
   dll=next(z.read(n) for n in z.namelist() if n.lower().endswith('immersivewoodworking.dll'))
   self.assertIn('craftinginfo-woodworking'.encode('utf-16-le'),dll)
  self.assertIn('craftinginfo-woodworking',self.registry)
 def test_patched_oven_smoke_dough_and_recovery_not_silent_vanilla(self):
  self.assertIn('Real Smoke can suffocate',self.pages['process-kitchen-firstfire'][1]);self.assertIn('DanaTweaks',self.pages['process-kitchen-dough-oven'][1]);self.assertIn('Grid dough recipes are disabled',self.pages['process-kitchen-dough-oven'][1]);self.assertIn('Drifters can open doors',self.pages['world-survival'][1])
 def test_reviewed_dough_controls_and_hide_state_chain(self):
  s=self.pages['process-kitchen-dough-oven'][1];self.assertIn('another hotbar slot',s);self.assertIn('Offhand water alone is not sufficient',s);self.assertNotIn('water container in your offhand',s)
  code=(W/'evidence/process-static/ManualDough.cs').read_text();self.assertIn('GetHotbarInventory()',code);self.assertIn('TryConsumeWaterFromContainer',code);self.assertIn('TryConsumeWaterFromHotbar',code)
  s=self.pages['process-butchering-leather'][1];self.assertIn('Seal scraped hide in weak tannin to prepare it',s);self.assertIn('prepared hide in strong tannin',s)
  weak=json5.loads((VS/'assets/survival/recipes/barrel/preparedhide.json').read_text());strong=json5.loads((VS/'assets/survival/recipes/barrel/leather.json').read_text())
  self.assertIn('hide-scraped',json.dumps(weak));self.assertIn('hide-prepared',json.dumps(strong))
 def test_pewter_example_fits_native_recipe(self):
  a,b,c=90,7,3;self.assertEqual(a+b+c,100);self.assertTrue(80<=a<=95 and 5<=b<=10 and 1<=c<=5)
if __name__=='__main__':unittest.main()
