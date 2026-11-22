"""Package mechanics only: synthetic PE-marker bytes are not a runnable assembly."""
import importlib.util
from pathlib import Path
import tempfile
import unittest
import zipfile

SPEC = importlib.util.spec_from_file_location('package', Path(__file__).resolve().parents[1] / 'tools/package.py')
MODULE = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(MODULE)


class PackageTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.root = Path(self.temp.name)
        self.original_addon = MODULE.ADDON
        MODULE.ADDON = self.root
        (self.root / 'README.md').write_text('Synthetic packaging fixture, not runtime evidence.')
        (self.root / 'modinfo.json').write_text('{"modid":"sawcontainerguard","side":"universal"}')
        self.dll = self.root / 'SawContainerGuard.dll'
        self.dll.write_bytes(b'MZ synthetic fixture, NOT a managed assembly')

    def tearDown(self):
        MODULE.ADDON = self.original_addon
        self.temp.cleanup()

    def test_deterministic_allowlist(self):
        (self.root / 'SawChiseling.dll').write_bytes(b'NEVER INCLUDE')
        (self.root / 'VintagestoryAPI.dll').write_bytes(b'NEVER INCLUDE')
        a, b = self.root / 'a.zip', self.root / 'b.zip'
        self.assertEqual(MODULE.package(self.dll, a), MODULE.package(self.dll, b))
        self.assertEqual(a.read_bytes(), b.read_bytes())
        with zipfile.ZipFile(a) as archive:
            self.assertEqual(archive.namelist(), list(MODULE.NAMES))
            self.assertEqual(archive.read('SawContainerGuard.dll'), self.dll.read_bytes())
            self.assertIsNone(archive.testzip())
            self.assertTrue(all(info.date_time == (1980, 1, 1, 0, 0, 0) for info in archive.infolist()))

    def test_empty_assembly_rejected(self):
        self.dll.write_bytes(b'')
        with self.assertRaises(ValueError): MODULE.package(self.dll, self.root / 'bad.zip')

    def test_symlink_rejected(self):
        other = self.root / 'other.dll'
        other.write_bytes(self.dll.read_bytes())
        self.dll.unlink()
        self.dll.symlink_to(other)
        with self.assertRaises(ValueError): MODULE.package(self.dll, self.root / 'bad.zip')

    def test_non_pe_rejected(self):
        self.dll.write_bytes(b'not a dll')
        with self.assertRaises(ValueError): MODULE.package(self.dll, self.root / 'bad.zip')

    def test_executable_document_rejected(self):
        (self.root / 'README.md').write_bytes(b'\x7fELF executable')
        with self.assertRaises(ValueError): MODULE.package(self.dll, self.root / 'bad.zip')

    def test_private_path_rejected(self):
        (self.root / 'README.md').write_text('/home/private/path')
        with self.assertRaises(ValueError): MODULE.package(self.dll, self.root / 'bad.zip')

    def test_missing_input_rejected(self):
        (self.root / 'modinfo.json').unlink()
        with self.assertRaises(ValueError): MODULE.package(self.dll, self.root / 'bad.zip')

    def test_unexpected_metadata_rejected(self):
        (self.root / 'modinfo.json').write_text('{"modid":"wrong","side":"universal"}')
        with self.assertRaises(ValueError): MODULE.package(self.dll, self.root / 'bad.zip')


if __name__ == '__main__': unittest.main()
