"""Offline integration/regression tests; run python -m unittest test_pack_audit."""
import json
from pathlib import Path
import unittest
from unittest.mock import patch
import pack_audit as p


class PackAuditTests(unittest.TestCase):
    def test_complete_pack_evidence(self):
        report = p.audit()
        self.assertEqual(report['baseline_mod_page_count'], len(report['coverage']))
        self.assertEqual(76, len(report['coverage']))
        self.assertEqual(['immersivemining'], report['stale_ids'])
        self.assertEqual([], report['metadata_issues'])
        self.assertEqual(146, len(report['metadata']))
        self.assertTrue(report['prune_safe'])
        self.assertTrue(all(m['hash_matches'] and m['metadata_id_matches'] and
                            m['metadata_version_matches'] for m in report['metadata']))

    def test_real_language(self):
        original = json.loads((Path(__file__).parent / 'baseline_en.json').read_text())
        result = p.clean(original)
        self.assertIn('mod-immersivemining-text', original)
        self.assertNotIn('mod-immersivemining-text', result)
        self.assertEqual(result, p.clean(result))
        for text in result.values():
            if not isinstance(text, str):
                continue
            self.assertNotIn('handbook://kuboczochmodpackguide-mod-immersivemining', text)
            links = [p._href(m) for m in p.ANCHOR.finditer(text) if p._href(m)]
            self.assertEqual(len(links), len(set(links)))

    def test_duplicate_links_keep_first_original_and_text(self):
        text = '<a href="x?a=1&amp;b=2">First</a> <a href=\'x?a=1&b=2\'><strong>Second</strong></a>'
        self.assertEqual('<a href="x?a=1&amp;b=2">First</a> <strong>Second</strong>', p.dedupe_hrefs(text))

    def test_stale_directory_row_and_unrelated_dead_external(self):
        destination = 'handbook://kuboczochmodpackguide-mod-immersivemining'
        source = {'category-text': 'Heading<br><br>• <a href="' + destination + '">Old</a> — obsolete description<br><br>• <a href="https://dead.invalid">Installed</a><br><br>',
                  'mod-toolsmith-text': '<a href="https://dead.invalid">Keep</a>',
                  'mod-immersivemining-text': 'Old',
                  'mod-immersivemining2-text': 'Unrelated prefix'}
        result = p.clean(source)
        self.assertNotIn('obsolete description', result['category-text'])
        self.assertIn('https://dead.invalid', result['mod-toolsmith-text'])
        self.assertEqual('Unrelated prefix', result['mod-immersivemining2-text'])

    def test_unreadable_metadata_blocks_prune(self):
        with patch.object(p, 'audit', return_value={'stale_ids': ['x'], 'prune_safe': False}):
            with self.assertRaises(ValueError):
                p.clean({})


if __name__ == '__main__':
    unittest.main()
