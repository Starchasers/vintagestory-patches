#!/usr/bin/env python3
"""Build with an external game SDK, then archive only explicitly owned files."""
import argparse
import hashlib
from pathlib import Path
import subprocess
import zipfile

ROOT = Path(__file__).resolve().parents[1]

def package(root, assembly, output):
    files = {"KuboczochInventoryHoverCleanup.dll": Path(assembly).read_bytes()}
    for name in ("modinfo.json", "README.md", "RESEARCH.md"):
        path = root / name
        if path.is_symlink():
            raise ValueError("Symlink payload")
        files[name] = path.read_bytes()
    output.parent.mkdir(parents=True, exist_ok=True)
    temporary = output.with_suffix(".tmp")
    try:
        with zipfile.ZipFile(temporary, "w", compression=zipfile.ZIP_DEFLATED, compresslevel=9) as z:
            for name, data in sorted(files.items()):
                info = zipfile.ZipInfo(name, (2020, 1, 1, 0, 0, 0))
                info.create_system = 3
                info.external_attr = 0o100644 << 16
                info.compress_type = zipfile.ZIP_DEFLATED
                z.writestr(info, data, compresslevel=9)
        with zipfile.ZipFile(temporary) as z:
            assert z.namelist() == sorted(files)
            assert z.testzip() is None
            for name, data in files.items():
                assert z.read(name) == data
        temporary.replace(output)
    finally:
        temporary.unlink(missing_ok=True)
    return hashlib.sha256(output.read_bytes()).hexdigest()

def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--game-path", required=True, type=Path)
    parser.add_argument("--dotnet", default="dotnet")
    args = parser.parse_args()
    subprocess.run([args.dotnet, "build", str(ROOT / "KuboczochInventoryHoverCleanup.csproj"), "-c", "Release", "-p:GamePath=" + str(args.game_path.resolve())], check=True)
    assembly = ROOT / "bin/Release/net10.0/KuboczochInventoryHoverCleanup.dll"
    if assembly.is_symlink() or not assembly.read_bytes().startswith(b"MZ"):
        raise ValueError("Expected built managed DLL")
    output = ROOT / "dist/kuboczochinventoryhovercleanup-1.0.0.zip"
    print(output)
    print("SHA256 " + package(ROOT, assembly, output))

if __name__ == "__main__":
    main()
