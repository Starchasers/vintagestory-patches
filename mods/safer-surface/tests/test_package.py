"""SDK-free packaging tests. SYNTHETIC bytes are not a release assembly."""
import hashlib
import importlib.util
import json
import os
from pathlib import Path
import subprocess
import sys
import tempfile
import unittest
from unittest import mock
import zipfile

TOOL = Path(__file__).resolve().parents[1] / 'tools' / 'package.py'
spec = importlib.util.spec_from_file_location('mod_package', TOOL)
assert spec is not None and spec.loader is not None
pkg = importlib.util.module_from_spec(spec)
spec.loader.exec_module(pkg)


class PackageTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        self.metadata = dict(type='code', modid='kuboczochssafersurface', version='0.1.0',
                             side='Server', requiredOnClient=False, requiredOnServer=True)
        self.write_metadata()
        (self.root / 'README.md').write_text('Synthetic test fixture, NOT A RELEASE.\n')
        (self.root / 'LICENSE').write_text('Synthetic license fixture\n')
        self.assembly = self.root / 'build' / 'KuboczochsSaferSurface.dll'
        self.assembly.parent.mkdir()
        self.assembly.write_bytes(b'MZ\x00SYNTHETIC TEST FIXTURE - NOT A MANAGED ASSEMBLY OR RELEASE')

    def write_metadata(self):
        (self.root / 'modinfo.json').write_text(json.dumps(self.metadata))

    def package(self):
        return pkg.package(self.root, self.assembly)

    def test_repeat_is_byte_identical_with_fixed_zip_metadata(self):
        archive = self.package()
        original = archive.read_bytes()
        os.utime(self.assembly, (1700000000, 1700000000))
        self.assertEqual(self.package().read_bytes(), original)
        with zipfile.ZipFile(archive) as z:
            self.assertEqual(z.namelist(), sorted(pkg.ALLOWLIST))
            self.assertIsNone(z.testzip())
            for info in z.infolist():
                self.assertEqual(info.date_time, (1980, 1, 1, 0, 0, 0))
                self.assertEqual(info.external_attr >> 16, 0o100644)
            self.assertEqual(z.read(self.assembly.name), self.assembly.read_bytes())
        self.assertEqual(archive.with_suffix('.zip.sha256').read_text(),
                         hashlib.sha256(original).hexdigest() + '  ' + archive.name + '\n')

    def test_unlisted_sdk_and_private_files_never_enter_zip(self):
        (self.root / 'build' / 'VintagestoryAPI.dll').write_bytes(b'proprietary')
        (self.root / '.env').write_text('SECRET=not-for-shipping')
        with zipfile.ZipFile(self.package()) as z:
            self.assertEqual(set(z.namelist()), set(pkg.ALLOWLIST))

    def test_wrong_assembly_name_cannot_inject_sdk(self):
        bad = self.assembly.with_name('VintagestoryAPI.dll')
        bad.write_bytes(b'MZsdk')
        with self.assertRaises(pkg.PackageError):
            pkg.package(self.root, bad)

    def test_invalid_metadata_rejected(self):
        for field, value in [('side', 'Universal'), ('requiredOnClient', True),
                             ('requiredOnServer', False), ('requiredOnClient', 0),
                             ('requiredOnServer', 1), ('version', '0.2.0'),
                             ('modid', '../escape'), ('type', 'content')]:
            with self.subTest(field=field, value=value):
                old = self.metadata[field]
                self.metadata[field] = value
                self.write_metadata()
                with self.assertRaises(pkg.PackageError):
                    self.package()
                self.metadata[field] = old

    def test_duplicate_json_keys_rejected(self):
        text = json.dumps(self.metadata)
        (self.root / 'modinfo.json').write_text(text[:-1] + ', "side": "Server"}')
        with self.assertRaises(pkg.PackageError):
            self.package()

    def test_symlink_inputs_and_output_rejected(self):
        for name in ['README.md', 'LICENSE', 'modinfo.json', 'build/KuboczochsSaferSurface.dll']:
            with self.subTest(name=name):
                path = self.root / name
                data = path.read_bytes()
                target = self.root / 'target'
                target.write_bytes(data)
                path.unlink()
                path.symlink_to(target)
                with self.assertRaises(pkg.PackageError):
                    self.package()
                path.unlink()
                path.write_bytes(data)
        destination = self.root / 'elsewhere'
        destination.mkdir()
        (self.root / 'dist').symlink_to(destination, target_is_directory=True)
        with self.assertRaises(pkg.PackageError):
            self.package()

    def test_missing_or_empty_inputs_rejected_without_archive(self):
        self.assembly.write_bytes(b'')
        with self.assertRaises(pkg.PackageError):
            self.package()
        self.assertFalse((self.root / 'dist').exists())

    def test_traversal_absolute_control_and_duplicate_names_rejected(self):
        for name in ['../evil.dll', '/evil.dll', 'a/../../evil', 'a\\evil', 'a\nevil',
                     'C:evil', './LICENSE', 'private/file', 'VintagestoryAPI.dll']:
            with self.subTest(name=name):
                with self.assertRaises(pkg.PackageError):
                    pkg.validate_names([name])
        with self.assertRaises(pkg.PackageError):
            pkg.validate_names(['LICENSE', 'LICENSE'])

    def test_executable_disguised_as_document_rejected(self):
        (self.root / 'README.md').write_bytes(b'MZ executable')
        with self.assertRaises(pkg.PackageError):
            self.package()

    def test_sensitive_text_rejected(self):
        for data in [b'-----BEGIN PRIVATE KEY-----', b'/home/alice/private',
                     b'/Users/alice/private', b'ghp_' + b'A' * 36]:
            with self.subTest(data=data):
                (self.root / 'README.md').write_bytes(data)
                with self.assertRaises(pkg.PackageError):
                    self.package()

    def test_build_passes_explicit_external_sdk_and_determinism(self):
        sdk = self.root / 'external-sdk'
        sdk.mkdir()
        (sdk / 'VintagestoryAPI.dll').write_bytes(b'fixture')
        (self.root / 'src').mkdir()
        (self.root / 'src' / 'SaferSurface.csproj').write_text('<Project />')
        def fake_run(command, **kwargs):
            self.assertIn('-p:GameDir=' + str(sdk), command)
            self.assertIn('-p:Deterministic=true', command)
            self.assertIn('-p:ContinuousIntegrationBuild=true', command)
            self.assertEqual(command[0], 'custom-dotnet')
            output = Path(command[command.index('--output') + 1])
            (output / self.assembly.name).write_bytes(self.assembly.read_bytes())
            (output / 'VintagestoryAPI.dll').write_bytes(b'not-shipped')
            return subprocess.CompletedProcess(command, 0)
        with mock.patch.object(pkg.subprocess, 'run', side_effect=fake_run) as run:
            archive = pkg.build_and_package(self.root, sdk, 'custom-dotnet')
            self.assertTrue(archive.is_file())
            run.assert_called_once()

    def test_failed_build_does_not_replace_previous_release(self):
        previous = self.package().read_bytes()
        sdk = self.root / 'sdk'
        sdk.mkdir()
        (sdk / 'VintagestoryAPI.dll').write_bytes(b'synthetic SDK')
        (self.root / 'src').mkdir()
        (self.root / 'src' / 'SaferSurface.csproj').write_text('<Project />')
        with mock.patch.object(pkg.subprocess, 'run',
                               side_effect=subprocess.CalledProcessError(1, 'dotnet')):
            with self.assertRaises(subprocess.CalledProcessError):
                pkg.build_and_package(self.root, sdk)
        self.assertEqual((self.root / 'dist' / pkg.ARCHIVE_NAME).read_bytes(), previous)

    def test_symlink_output_files_rejected_without_overwriting_target(self):
        archive = self.package()
        for destination in [archive, archive.with_suffix('.zip.sha256')]:
            with self.subTest(destination=destination.name):
                original = destination.read_bytes()
                target = self.root / 'protected'
                target.write_bytes(b'do not overwrite')
                destination.unlink()
                destination.symlink_to(target)
                with self.assertRaises(pkg.PackageError):
                    self.package()
                self.assertEqual(target.read_bytes(), b'do not overwrite')
                destination.unlink()
                destination.write_bytes(original)

    def test_archive_byte_mismatch_is_rejected(self):
        archive = self.package()
        with self.assertRaises(pkg.PackageError):
            pkg.verify_archive(archive, {name: b'wrong' for name in pkg.ALLOWLIST})

    def test_missing_sdk_fails_before_build(self):
        with mock.patch.object(pkg.subprocess, 'run') as run:
            with self.assertRaises(pkg.PackageError):
                pkg.build_and_package(self.root, self.root / 'missing', 'dotnet')
            run.assert_not_called()


if __name__ == '__main__':
    unittest.main()
