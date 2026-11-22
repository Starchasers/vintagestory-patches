"""Root-only 1.3.1 contract against the immutable 1.3.0 release."""
import json,re,unittest,zipfile
from pathlib import Path
from root_index import TITLE,TOPICS,PREFIX,root_content
W=Path(__file__).parent
P='assets/kuboczochmodpackguide/'
def links(text):return re.findall(r'href=[\"\']([^\"\']+)',text)
class RootIndex(unittest.TestCase):
 @classmethod
 def setUpClass(cls):
  cls.old=zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.3.0.zip')
  cls.new=zipfile.ZipFile(W/'dist/KuboczochModpackGuide-1.3.1.zip')
  cls.before=json.loads(cls.old.read(P+'lang/en.json'))
  cls.lang=json.loads(cls.new.read(P+'lang/en.json'))
 def test_root_directory_semantics_and_exact_topics(self):
  text=self.lang['root-text'];self.assertEqual(self.lang['root-title'],TITLE)
  self.assertTrue(text.startswith('Browse the mods in the Pszczoly Modpack by category.'))
  self.assertIn('<strong>Mod categories</strong>',text);self.assertIn('<strong>Our guides</strong>',text)
  expected=[PREFIX+'category-'+k for k,_,_ in TOPICS]
  self.assertEqual(links(text),expected+[PREFIX+'toolsmith-items'])
  self.assertLess(text.index(expected[-1]),text.index('Our guides'))
  for _,label,scope in TOPICS:self.assertIn(label,text);self.assertIn(scope,text)
  for forbidden in ('getting started','welcome','new here','first steps','mod-toolsmith','mod-expandedfoods','mod-immersivewoodworking'):
   self.assertNotIn(forbidden,(self.lang['root-title']+' '+text).lower())
 def test_root_and_all_other_pages_preserved(self):
  self.assertEqual(set(self.old.namelist()),set(self.new.namelist()))
  for name in self.old.namelist():
   if name not in ('modinfo.json',P+'lang/en.json'):self.assertEqual(self.old.read(name),self.new.read(name),name)
  changed={k for k in self.before if self.before[k]!=self.lang[k]}
  self.assertEqual(changed,{'root-title','root-text'})
  a=json.loads(self.old.read('modinfo.json'));b=json.loads(self.new.read('modinfo.json'))
  self.assertEqual(b.pop('version'),'1.3.1');a.pop('version');self.assertEqual(a,b)
 def test_every_category_mod_destination_retained(self):
  pages={json.loads(self.new.read(n))['pageCode'] for n in self.new.namelist() if '/config/handbook/mod-' in n}
  self.assertEqual(len(pages),75)
  destinations=set()
  for key,_,_ in TOPICS:
   loc='category-'+key+'-text'
   self.assertEqual(links(self.before[loc]),links(self.lang[loc]))
   destinations.update(h.removeprefix('handbook://') for h in links(self.lang[loc]) if h.startswith(PREFIX+'mod-'))
  self.assertEqual(destinations,pages)
 def test_all_internal_links_resolve(self):
  pages={json.loads(self.new.read(n))['pageCode'] for n in self.new.namelist() if '/config/handbook/' in n}
  for key,text in self.lang.items():
   for href in links(text):
    if href.startswith(PREFIX):self.assertIn(href.removeprefix('handbook://'),pages,(key,href))
 def test_regeneration_is_consistent(self):
  from content_rewrite import rewrite
  self.assertEqual(root_content(self.before['root-text']),self.lang['root-text'])
  rebuilt=rewrite(self.before)
  self.assertEqual(rebuilt['root-title'],TITLE);self.assertEqual(rebuilt['root-text'],self.lang['root-text'])
if __name__=='__main__':unittest.main()
