"""Deterministic packages containing only explicitly owned files."""
from pathlib import Path, PurePosixPath
import hashlib
import json
import os
import zipfile

ROOT = Path(__file__).resolve().parent
OUT = ROOT / 'release'
ASSETS = [
    'assets/kuboczochpitchgluecompat/lang/en.json',
    'assets/kuboczochpitchgluecompat/lang/pl.json',
    'assets/kuboczochpitchgluecompat/patches/barkcanoe-resin-handbook.json',
]
SOURCE = ['Directory.Build.props', 'mod/KuboczochPitchGlueCompat.csproj',
          'mod/ResinConstructionMod.cs', 'tests/Tests.csproj',
          'tests/Program.cs', 'tests/Proxy.cs', 'modinfo.json', 'README.md', 'package.py'] + ASSETS

def package(name, entries):
    OUT.mkdir(exist_ok=True)
    target = OUT / name
    tmp = target.with_suffix('.zip.partial')
    with zipfile.ZipFile(tmp, 'w', compression=zipfile.ZIP_DEFLATED, compresslevel=9) as z:
        for name, path in sorted(entries.items()):
            relative = PurePosixPath(name)
            assert not relative.is_absolute() and '..' not in relative.parts
            assert not any(ord(c) < 32 for c in name)
            assert path.is_file() and not path.is_symlink()
            data = path.read_bytes()
            if name.endswith('.dll'):
                assert name == 'KuboczochPitchGlueCompat.dll' and data.startswith(b'MZ')
            else:
                assert not data.startswith((b'MZ', b'\x7fELF'))
                assert (str(Path.home()) + '/').encode() not in data
            if name.endswith('.json'):
                json.loads(data)
            entry = zipfile.ZipInfo(name, (2020, 1, 1, 0, 0, 0))
            entry.create_system = 3
            entry.external_attr = 0o100644 << 16
            entry.compress_type = zipfile.ZIP_DEFLATED
            z.writestr(entry, data, compresslevel=9)
    with zipfile.ZipFile(tmp) as z:
        assert z.testzip() is None and z.namelist() == sorted(entries)
        assert len(z.namelist()) == len(set(z.namelist()))
        for name, path in entries.items():
            assert z.read(name) == path.read_bytes()
    os.replace(tmp, target)
    digest = hashlib.sha256(target.read_bytes()).hexdigest()
    print('VERIFIED', target.name, target.stat().st_size, 'bytes SHA256', digest)
    return digest + '  ' + target.name

if __name__ == '__main__':
    info = json.loads((ROOT / 'modinfo.json').read_text())
    assert info['version'] == '1.3.0' and info['side'] == 'Universal'
    assert info['requiredOnClient'] and info['requiredOnServer']
    binary = {n: ROOT / n for n in ['modinfo.json', 'README.md'] + ASSETS}
    binary['KuboczochPitchGlueCompat.dll'] = ROOT / 'mod/bin/Release/net10.0/KuboczochPitchGlueCompat.dll'
    sums = [package('KuboczochBoatResinConstruction-1.3.0.zip', binary),
            package('KuboczochBoatResinConstruction-1.3.0-source.zip', {n: ROOT/n for n in SOURCE})]
    (OUT / 'SHA256SUMS.txt').write_text('\n'.join(sums) + '\n')
