"""Snapshot-preservation tests for the real Nb7Rd-based optional config."""
import hashlib
import json
from pathlib import Path
import unittest

CONFIG = Path(__file__).resolve().parents[1] / 'config/AutoMapMarkers/settings.json'
LABELS = ('egocarib-mapmarkers:safe-mushrooms', 'egocarib-mapmarkers:unsafe-mushrooms')

class ConfigTests(unittest.TestCase):
    def test_tracking_enabled_and_neutral(self):
        data = json.loads(CONFIG.read_text())
        self.assertFalse(data['DisableAllModFeatures'])
        for label in LABELS:
            override = data['MarkerOverrides'][label]
            self.assertTrue(override['Enabled'])
            self.assertEqual(('Mushrooms', 'mushroom', '#A89880'), (override['Title'], override['Icon'], override['Color']))
            self.assertFalse(data['ExpandStates'][label])

    def test_all_unrelated_source_values_preserved(self):
        data = json.loads(CONFIG.read_text())
        for label in LABELS:
            data['MarkerOverrides'].pop(label, None)
            data['ExpandStates'].pop(label, None)
        canonical = json.dumps(data, sort_keys=True, separators=(',', ':')).encode()
        # SHA256 of actual Nb7Rd settings.json with only both mushroom keys removed.
        self.assertEqual('ffe7b039fcd233cf1968f7a3212161e978b0753b1d10377cd8279a27dd2cc744', hashlib.sha256(canonical).hexdigest())
