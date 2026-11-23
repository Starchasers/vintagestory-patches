import importlib.util
import os
from pathlib import Path
import tempfile
import unittest
import zipfile

spec = importlib.util.spec_from_file_location("package", Path(__file__).resolve().parents[1] / "tools/package.py")
assert spec is not None and spec.loader is not None
package = importlib.util.module_from_spec(spec)
spec.loader.exec_module(package)


class PackagingTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory(dir=os.environ.get("TMPDIR"))
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        # Synthetic packaging-mechanics fixture, NOT a compiled/tested mod DLL.
        self.assembly = self.root / package.DLL
        self.assembly.write_bytes(b"MZ synthetic package-test fixture")
        for name in package.TEXT:
            (self.root / name).write_text("owned text")
        (self.root / "VintagestoryAPI.dll").write_bytes(b"MZ foreign")
        (self.root / "ACulinaryArtillery.dll").write_bytes(b"MZ foreign")
        self.output = self.root / "out.zip"

    def test_deterministic_allowlist(self):
        first = package.package(self.root, self.assembly, self.output)
        second = package.package(self.root, self.assembly, self.root / "second.zip")
        self.assertEqual(first, second)
        with zipfile.ZipFile(self.output) as archive:
            self.assertEqual(archive.namelist(), sorted([package.DLL, *package.TEXT]))
            for info in archive.infolist():
                self.assertEqual(info.date_time, (2020, 1, 1, 0, 0, 0))
                self.assertEqual(info.external_attr >> 16, 0o100644)

    def test_foreign_assembly_refused(self):
        with self.assertRaises(ValueError):
            package.package(self.root, self.root / "ACulinaryArtillery.dll", self.output)

    def test_symlink_refused(self):
        path = self.root / "README.md"
        path.unlink()
        path.symlink_to(self.root / "RESEARCH.md")
        with self.assertRaises(ValueError):
            package.package(self.root, self.assembly, self.output)

    def test_disguised_executable_refused(self):
        (self.root / "README.md").write_bytes(b"MZ executable")
        with self.assertRaises(ValueError):
            package.package(self.root, self.assembly, self.output)

    def test_private_path_refused(self):
        (self.root / "README.md").write_text("/home/example/private")
        with self.assertRaises(ValueError):
            package.package(self.root, self.assembly, self.output)

    def test_nonassembly_refused(self):
        self.assembly.write_bytes(b"not a DLL")
        with self.assertRaises(ValueError):
            package.package(self.root, self.assembly, self.output)


if __name__ == "__main__":
    unittest.main()
