import importlib.util
from pathlib import Path
import tempfile
import unittest
import zipfile

spec = importlib.util.spec_from_file_location("package", Path(__file__).resolve().parents[1] / "tools/package.py")
assert spec is not None and spec.loader is not None
module = importlib.util.module_from_spec(spec)
spec.loader.exec_module(module)

class PackageTests(unittest.TestCase):
    def test_allowlist_determinism_and_bytes(self):
        with tempfile.TemporaryDirectory() as tmp:
            root = Path(tmp)
            for name in ("modinfo.json", "README.md", "RESEARCH.md"):
                (root / name).write_text("owned " + name)
            dll = root / "fixture.dll"
            dll.write_bytes(b"synthetic fixture, not a production assembly")
            (root / "VintagestoryAPI.dll").write_bytes(b"forbidden SDK")
            first, second = root / "a.zip", root / "b.zip"
            self.assertEqual(module.package(root, dll, first), module.package(root, dll, second))
            with zipfile.ZipFile(first) as z:
                self.assertEqual(z.namelist(), ["InventoryHoverCleanup.dll", "README.md", "RESEARCH.md", "modinfo.json"])
                self.assertEqual(z.read("InventoryHoverCleanup.dll"), dll.read_bytes())
                self.assertIsNone(z.testzip())
    def test_symlink_rejected(self):
        with tempfile.TemporaryDirectory() as tmp:
            root = Path(tmp)
            (root / "fixture.dll").write_bytes(b"fixture")
            (root / "target").write_text("not owned")
            (root / "modinfo.json").symlink_to(root / "target")
            with self.assertRaises(ValueError):
                module.package(root, root / "fixture.dll", root / "a.zip")

if __name__ == "__main__":
    unittest.main()
