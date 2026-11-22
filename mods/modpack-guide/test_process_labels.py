"""Player-visible label regressions, covering every new process and hub page."""
import hashlib
import json
import re
import unittest
import zipfile
from pathlib import Path
import json5
from process_content import drafts, pages, plain
from process_labels import item_label, guide_label, CURATED, catalog

W = Path(__file__).parent

class Labels(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.pages = pages()
        cls.drafts = drafts()

    def test_all_29_pages_keep_exact_original_icon_occurrences(self):
        baseline = json.loads((W / 'process-icon-baseline.json').read_text())
        self.assertEqual(len(self.pages), 29)
        self.assertEqual(len(baseline), 29)
        for code, (_, text) in self.pages.items():
            self.assertEqual(re.findall(r'<toolsmithitem\b[^>]*></toolsmithitem>', text), baseline[code + '-text'], code)

    def test_all_visible_labels_are_justified_not_transformed_codes(self):
        for page in self.drafts.values():
            for step in page['steps']:
                for item in step.get('items', []):
                    self.assertEqual(item['labelEvidence'], item_label(item['type'], item['code']))
                    self.assertEqual(item['label'], item['labelEvidence']['label'])
            for guide in page['guides']:
                if 'authoredTitle' not in guide['labelEvidence']:
                    self.assertEqual(guide['labelEvidence'], guide_label(guide['code']))
        for code, (_, text) in self.pages.items():
            self.assertNotIn('&amp;', text, code)
            for label in re.findall(r'</toolsmithitem> ([^<]+)|<a\b[^>]*>([^<]+)</a>', text):
                label = ''.join(label)
                self.assertNotRegex(label, r'\b(?:north|south|east|west|grainbundle|butcherhook|butchertable|primemeat|Animalhusbandry|Butcheringguide|Mealcooking)\b')
                self.assertNotRegex(label, r'\b(?:hide prepared small|fat rendered|seeds rye)\b')
                self.assertNotRegex(label, r'\{\d+\}|(?:gamemechanicinfo|craftinginfo|process-index)-')

    def test_source_language_evidence_reads_back_exactly(self):
        seen = set()
        decoded = {}
        for page in self.drafts.values():
            records = [i['labelEvidence'] for s in page['steps'] for i in s.get('items', [])]
            records += [g['labelEvidence'] for g in page['guides']]
            for record in records:
                if 'source' not in record:
                    self.assertTrue('curatedReason' in record or 'authoredTitle' in record)
                    continue
                ident = record['source'], record['languageKey']
                if ident in seen:
                    continue
                seen.add(ident)
                if '!' in record['source']:
                    archive, member = record['source'].split('!', 1)
                    with zipfile.ZipFile(archive) as z:
                        raw = z.read(member)
                else:
                    raw = Path(record['source']).read_bytes()
                self.assertEqual(hashlib.sha256(raw).hexdigest(), record['sha256'])
                if record['source'] not in decoded:
                    decoded[record['source']] = json5.loads(raw.decode('utf-8-sig').replace('\r', ''))
                values = decoded[record['source']]
                self.assertEqual(values[record['languageKey']], record['label'])

    def test_requested_problem_examples_have_real_names(self):
        self.assertEqual(item_label('block', 'butchering:butcherhook-flint-north')['label'], 'Primitive Skinning Hook')
        self.assertEqual(item_label('block', 'butchering:butchertable-primitive-north')['label'], 'Primitive Butchering Table')
        self.assertEqual(item_label('item', 'game:hide-prepared-small')['label'], 'Prepared hide (Small)')
        self.assertEqual(guide_label('gamemechanicinfo-animalhusbandry')['label'], 'Game Mechanic: Animal Husbandry')
        self.assertEqual(guide_label('butcheringguide')['label'], 'Butchering: Guide')
        self.assertEqual(guide_label('craftinginfo-mealcooking')['label'], 'Crafting Mechanic: Meal making')
        with self.assertRaises(ValueError):
            item_label('item', 'game:invented-raw-north')
        with self.assertRaises(ValueError):
            guide_label('gamemechanicinfo-madeup')

if __name__ == '__main__':
    unittest.main()
