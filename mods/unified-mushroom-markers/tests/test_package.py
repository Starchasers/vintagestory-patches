"""Package mechanics only; synthetic PE-marker fixtures are not runtime evidence."""
import hashlib
import importlib.util
import json
import os
from pathlib import Path
import subprocess
import tempfile
import unittest
from unittest.mock import patch
import zipfile

SPEC = importlib.util.spec_from_file_location('mushroom_package', Path(__file__).resolve().parents[1] / 'tools/package.py')
assert SPEC is not None and SPEC.loader is not None
MODULE = importlib.util.module_from_spec(SPEC)
SPEC.loader.exec_module(MODULE)


class PackageTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        self.addon_patch = patch.object(MODULE, 'ADDON', self.root)
        self.addon_patch.start()
        self.addCleanup(self.addon_patch.stop)
        self.readme = self.root / 'README.md'
        self.readme.write_text('Synthetic fixture, not runtime evidence.')
        self.metadata = self.root / 'modinfo.json'
        self.valid_metadata = {'modid': 'unifiedmushroommarkers', 'side': 'client', 'version': '1.0.0'}
        self.metadata.write_text(json.dumps(self.valid_metadata))
        self.dll = self.root / 'UnifiedMushroomMarkers.dll'
        self.dll.write_bytes(b'MZ synthetic fixture, NOT a managed assembly')
        self.output = self.root / 'release.zip'
        self.output.write_bytes(b'existing release must survive errors')
        self.previous = self.output.read_bytes()

    def assert_rejected(self, message):
        with self.assertRaisesRegex(ValueError, message):
            MODULE.package(self.dll, self.output)
        self.assertEqual(self.output.read_bytes(), self.previous)
        self.assertFalse(self.output.with_suffix('.zip.tmp').exists())

    def test_deterministic_allowlist(self):
        for name in ('AutoMapMarkers.dll', 'VintagestoryAPI.dll', '0Harmony.dll', 'VSSurvivalMod.dll'):
            (self.root / name).write_bytes(b'NEVER INCLUDE')
        a, b = self.root / 'a.zip', self.root / 'b.zip'
        digest = MODULE.package(self.dll, a)
        for source in (self.readme, self.metadata, self.dll):
            os.utime(source, (1234567890, 1234567890))
            source.chmod(0o600)
        self.assertEqual(digest, MODULE.package(self.dll, b))
        self.assertEqual(a.read_bytes(), b.read_bytes())
        self.assertEqual(digest, hashlib.sha256(a.read_bytes()).hexdigest())
        with zipfile.ZipFile(a) as archive:
            self.assertEqual(archive.namelist(), ['README.md', 'UnifiedMushroomMarkers.dll', 'modinfo.json'])
            self.assertIsNone(archive.testzip())
            for info in archive.infolist():
                self.assertEqual(info.date_time, (1980, 1, 1, 0, 0, 0))
                self.assertEqual(info.create_system, 3)
                self.assertEqual(info.external_attr >> 16, 0o100644)
                self.assertEqual(info.compress_type, zipfile.ZIP_DEFLATED)
                self.assertEqual(archive.read(info.filename), (self.root / info.filename).read_bytes())

    def test_empty_inputs_preserve_release(self):
        for source in (self.readme, self.metadata, self.dll):
            with self.subTest(source=source.name):
                original = source.read_bytes()
                source.write_bytes(b'')
                self.assert_rejected('Empty package input: ' + source.name)
                source.write_bytes(original)

    def test_missing_inputs_preserve_release(self):
        for source in (self.readme, self.metadata, self.dll):
            with self.subTest(source=source.name):
                original = source.read_bytes()
                source.unlink()
                self.assert_rejected('Invalid package input: ' + source.name)
                source.write_bytes(original)

    def test_symlink_inputs_preserve_release(self):
        for source in (self.readme, self.metadata, self.dll):
            with self.subTest(source=source.name):
                original = source.read_bytes()
                target = self.root / 'external'
                target.write_bytes(original)
                source.unlink()
                source.symlink_to(target)
                self.assert_rejected('Invalid package input: ' + source.name)
                source.unlink()
                source.write_bytes(original)

    def test_non_pe_preserves_release(self):
        self.dll.write_bytes(b'not a DLL')
        self.assert_rejected('Expected compiled managed PE assembly')

    def test_executable_documents_preserve_release(self):
        for source in (self.readme, self.metadata):
            original = source.read_bytes()
            for signature in (b'MZ', b'\x7fELF'):
                with self.subTest(source=source.name, signature=signature):
                    source.write_bytes(signature + b'executable')
                    self.assert_rejected('Unexpected executable/private path')
            source.write_bytes(original)

    def test_private_paths_preserve_release(self):
        for source in (self.readme, self.metadata):
            original = source.read_bytes()
            source.write_bytes(b'/home/private/path')
            self.assert_rejected('Unexpected executable/private path')
            source.write_bytes(original)

    def test_metadata_identity_preserves_release(self):
        for key, value in (('modid', 'wrong'), ('side', 'universal'), ('version', '2.0.0')):
            for missing in (False, True):
                with self.subTest(key=key, missing=missing):
                    metadata = dict(self.valid_metadata)
                    if missing:
                        metadata.pop(key)
                    else:
                        metadata[key] = value
                    self.metadata.write_text(json.dumps(metadata))
                    self.assert_rejected('Unexpected mod metadata')

    def test_malformed_metadata_preserves_release(self):
        for data in (b'{', b'\xff'):
            self.metadata.write_bytes(data)
            self.assert_rejected('Invalid mod metadata JSON')
        for data in ('[]', 'null', '1', '"text"'):
            self.metadata.write_text(data)
            self.assert_rejected('Unexpected mod metadata')

    def test_write_error_preserves_release_and_cleans_temporary(self):
        with patch.object(zipfile.ZipFile, 'writestr', side_effect=OSError('disk failure')):
            with self.assertRaisesRegex(OSError, 'disk failure'):
                MODULE.package(self.dll, self.output)
        self.assertEqual(self.output.read_bytes(), self.previous)
        self.assertFalse(self.output.with_suffix('.zip.tmp').exists())

    def test_byte_verification_failure_preserves_release(self):
        with patch.object(zipfile.ZipFile, 'read', return_value=b'corrupted'):
            self.assert_rejected('Package byte verification failed')

    def test_cli_build_passes_both_external_paths_and_queries_target(self):
        game = self.root / 'game'
        for name in ('VintagestoryAPI.dll', 'Mods/VSSurvivalMod.dll', 'Lib/0Harmony.dll'):
            target = game / name
            target.parent.mkdir(parents=True, exist_ok=True)
            target.write_bytes(b'external fixture')
        automap = self.root / 'AutoMapMarkers.dll'
        automap.write_bytes(b'external fixture')
        argv = ['package.py', '--game-path', str(game), '--automap-markers-path', str(automap),
                '--dotnet', 'external-dotnet', '--output', str(self.output)]
        with patch('sys.argv', argv), patch.object(MODULE.subprocess, 'run') as run:
            run.side_effect = [subprocess.CompletedProcess([], 0),
                               subprocess.CompletedProcess([], 0, stdout=str(self.dll) + '\n')]
            MODULE.main()
        build, query = [call.args[0] for call in run.call_args_list]
        for command in (build, query):
            self.assertIn(f'-p:GamePath={game}', command)
            self.assertIn(f'-p:AutoMapMarkersPath={automap}', command)
            self.assertIn(str(self.root / 'UnifiedMushroomMarkers.csproj'), command)
        self.assertEqual(build[:2], ['external-dotnet', 'build'])
        self.assertIn('-getProperty:TargetPath', query)
        digest = hashlib.sha256(self.output.read_bytes()).hexdigest()
        self.assertEqual(self.output.with_suffix('.zip.sha256').read_text(), f'{digest}  release.zip\n')


if __name__ == '__main__':
    unittest.main()
