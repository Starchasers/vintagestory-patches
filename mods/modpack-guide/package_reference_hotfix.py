"""Package the checked-in guide snapshot and locally built client DLL, without game DLLs."""
import argparse
import hashlib
import json
from pathlib import Path
import zipfile

ROOT = Path(__file__).resolve().parent


def build(output: Path) -> Path:
    snapshot = ROOT / "generated-addon"
    files = {p.relative_to(snapshot).as_posix(): p.read_bytes()
             for p in snapshot.rglob("*") if p.is_file()}
    info = json.loads(files["modinfo.json"])
    assert info["modid"] == "kuboczochmodpackguide" and info["version"] == "1.4.1"
    dll_name = "KuboczochModpackGuide.Client.dll"
    files[dll_name] = (ROOT / "client/bin/Release/net10.0" / dll_name).read_bytes()
    assert {n for n in files if n.lower().endswith(".dll")} == {dll_name}
    output.mkdir(parents=True, exist_ok=True)
    target = output / "KuboczochModpackGuide-1.4.1.zip"
    with zipfile.ZipFile(target, "w", compression=zipfile.ZIP_DEFLATED, compresslevel=9) as archive:
        for name, data in sorted(files.items()):
            entry = zipfile.ZipInfo(name, (2026, 11, 23, 0, 0, 0))
            entry.compress_type = zipfile.ZIP_DEFLATED
            entry.external_attr = 0o644 << 16
            archive.writestr(entry, data)
    with zipfile.ZipFile(target) as archive:
        assert archive.testzip() is None
        assert set(archive.namelist()) == set(files)
        for name, data in files.items():
            assert archive.read(name) == data, name
    digest = hashlib.sha256(target.read_bytes()).hexdigest()
    (output / "SHA256SUMS-1.4.1").write_text(f"{digest}  {target.name}\n")
    print(f"PASS archive CRC, exact resource/DLL readback, no game DLLs: {target}")
    print(digest)
    return target


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--output", type=Path, default=ROOT / "dist")
    build(parser.parse_args().output)
