#!/usr/bin/env python3
"""Compile owned code against an external SDK; package an exact allowlist."""
import argparse
import hashlib
from pathlib import Path
import re
import subprocess
import zipfile

ROOT = Path(__file__).resolve().parents[1]
DLL = "KuboczochStopperReturn.dll"
TEXT = ("modinfo.json", "README.md", "RESEARCH.md")


def package(root, assembly, output):
    root, assembly, output = Path(root), Path(assembly), Path(output)
    if assembly.is_symlink() or assembly.name != DLL:
        raise ValueError("Only the owned assembly may be packaged")
    data = assembly.read_bytes()
    if not data.startswith(b"MZ"):
        raise ValueError("Expected built managed assembly")
    files = {DLL: data}
    for name in TEXT:
        path = root / name
        if path.is_symlink():
            raise ValueError("Symlink payload")
        data = path.read_bytes()
        if data.startswith((b"MZ", b"\x7fELF", b"PK\x03\x04")):
            raise ValueError("Executable/archive disguised as text")
        text = data.decode("utf-8")
        if re.search(r"/home/|/Users/|[A-Za-z]:\\|(?:gh[pousr]_[A-Za-z0-9]{20,})|-----BEGIN .*PRIVATE KEY-----", text):
            raise ValueError("Private path or credential in text payload")
        files[name] = data
    output.parent.mkdir(parents=True, exist_ok=True)
    temporary = output.with_suffix(".tmp")
    try:
        with zipfile.ZipFile(temporary, "w", compression=zipfile.ZIP_DEFLATED, compresslevel=9) as archive:
            for name, data in sorted(files.items()):
                info = zipfile.ZipInfo(name, (2020, 1, 1, 0, 0, 0))
                info.create_system = 3
                info.external_attr = 0o100644 << 16
                info.compress_type = zipfile.ZIP_DEFLATED
                archive.writestr(info, data, compresslevel=9)
        with zipfile.ZipFile(temporary) as archive:
            if archive.namelist() != sorted(files) or archive.testzip() is not None:
                raise ValueError("Archive manifest/CRC mismatch")
            for name, data in files.items():
                if archive.read(name) != data:
                    raise ValueError("Archive byte mismatch")
        temporary.replace(output)
    finally:
        temporary.unlink(missing_ok=True)
    return hashlib.sha256(output.read_bytes()).hexdigest()


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--game-path", required=True, type=Path)
    parser.add_argument("--dotnet", default="dotnet")
    args = parser.parse_args()
    subprocess.run([args.dotnet, "build", str(ROOT / "KuboczochStopperReturn.csproj"),
                    "-c", "Release", "-p:GamePath=" + str(args.game_path.resolve())], check=True)
    output = ROOT / "dist/kuboczochstopperreturn-1.0.2.zip"
    checksum = package(ROOT, ROOT / "bin/Release/net10.0" / DLL, output)
    output.with_suffix(".zip.sha256").write_text(checksum + "  " + output.name + "\n")
    print(output)
    print("SHA256 " + checksum)


if __name__ == "__main__":
    main()
