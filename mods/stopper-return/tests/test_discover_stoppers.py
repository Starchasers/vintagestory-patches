import importlib.util
import json
import os
from pathlib import Path
import tempfile
import unittest
import zipfile

spec = importlib.util.spec_from_file_location('discover', Path(__file__).with_name('discover_stoppers.py'))
assert spec is not None and spec.loader is not None
discover = importlib.util.module_from_spec(spec)
spec.loader.exec_module(discover)


class DiscoveryTests(unittest.TestCase):
    def archive(self, assets):
        temporary = tempfile.TemporaryDirectory(dir=os.environ.get('TMPDIR'))
        self.addCleanup(temporary.cleanup)
        path = Path(temporary.name) / 'provider.zip'
        with zipfile.ZipFile(path, 'w') as archive:
            for name, value in assets.items():
                archive.writestr('assets/aca/' + name + '.json', json.dumps(value))
        return path

    def test_recursive_inheritance_variants_and_raw_exclusion(self):
        path = self.archive({
            'itemtypes/base': {'maxstacksize': 64, 'storageFlags': 257,
                               'attributes': {'isLiquidContainerStopper': True, 'nested': {'a': 1}}},
            'itemtypes/middle': {'inheritFrom': 'itemtypes/base', 'attributes': {'nested': {'b': 2}}},
            'itemtypes/stopper-test': {'inheritFrom': 'itemtypes/middle', 'code': 'stopper-test',
                'variantgroups': [{'code': 'material', 'states': ['wood', 'metal']},
                                  {'code': 'color', 'states': ['a', 'b']}]},
            'itemtypes/stopper-raw': {'inheritFrom': 'itemtypes/base', 'code': 'stopper-raw',
                                    'attributes': {'isLiquidContainerStopper': False}},
            'patches/stopper-extra': {'code': 'stopper-extra', 'attributes': {'isLiquidContainerStopper': True}},
        })
        result = discover.discover(path)
        self.assertEqual([s['code'] for s in result], [f'aca:stopper-test-{m}-{c}'
                          for m in ['metal', 'wood'] for c in ['a', 'b']])
        for stopper in result:
            self.assertEqual(stopper['attributes']['nested'], {'a': 1, 'b': 2})
            self.assertEqual(stopper['maxStackSize'], 64)
            self.assertEqual(stopper['storageFlags'], 257)

    def test_cycle_rejected(self):
        path = self.archive({'itemtypes/stopper-loop': {'inheritFrom': 'itemtypes/stopper-loop'}})
        with self.assertRaisesRegex(ValueError, 'cycle'):
            discover.discover(path)

    def test_missing_parent_rejected(self):
        path = self.archive({'itemtypes/stopper-broken': {'inheritFrom': 'itemtypes/missing'}})
        with self.assertRaisesRegex(ValueError, 'Missing provider parent'):
            discover.discover(path)

    def test_filter_variants(self):
        path = self.archive({'itemtypes/stopper-test': {
            'code': 'stopper-test', 'attributes': {'isLiquidContainerStopper': True},
            'variantgroups': [{'code': 'kind', 'states': ['one', 'two', 'three']}],
            'allowedVariants': ['stopper-test-t*'], 'skipVariants': ['*-three']}})
        self.assertEqual([s['code'] for s in discover.discover(path)], ['aca:stopper-test-two'])


if __name__ == '__main__':
    unittest.main()
