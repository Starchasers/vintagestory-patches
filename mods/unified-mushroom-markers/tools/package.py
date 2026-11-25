#!/usr/bin/env python3
"""Build/package the owned client mod using external game and Auto Map Markers DLLs."""
import argparse
import hashlib
import json
from pathlib import Path
import subprocess
import zipfile

ROOT = Path(__file__).resolve().parents[1]
ADDON = ROOT
NAMES = ('README.md', 'UnifiedMushroomMarkers.dll', 'modinfo.json')


def package(assembly, output):
    """Package a fixed allowlist, validating inputs before touching a release."""
    sources = {'README.md': ADDON / 'README.md',
               'UnifiedMushroomMarkers.dll': Path(assembly),
               'modinfo.json': ADDON / 'modinfo.json'}
    payload = {}
    for name in NAMES:
        source = sources[name]
        if source.is_symlink() or not source.is_file():
            raise ValueError(f'Invalid package input: {name}')
        data = source.read_bytes()
        if not data:
            raise ValueError(f'Empty package input: {name}')
        if name.endswith('.dll'):
            if not data.startswith(b'MZ'):
                raise ValueError('Expected compiled managed PE assembly')
        elif data.startswith((b'MZ', b'\x7fELF')) or b'/home/' in data:
            raise ValueError(f'Unexpected executable/private path in {name}')
        payload[name] = data
    try:
        metadata = json.loads(payload['modinfo.json'])
    except (ValueError, UnicodeError) as error:
        raise ValueError('Invalid mod metadata JSON') from error
    if not isinstance(metadata, dict) or any(
        metadata.get(key) != value for key, value in
        {'modid': 'unifiedmushroommarkers', 'side': 'client', 'version': '1.0.0'}.items()
    ):
        raise ValueError('Unexpected mod metadata')
    output = Path(output)
    output.parent.mkdir(parents=True, exist_ok=True)
    temporary = output.with_suffix('.zip.tmp')
    try:
        with zipfile.ZipFile(temporary, 'w', compression=zipfile.ZIP_DEFLATED, compresslevel=9) as archive:
            for name in NAMES:
                info = zipfile.ZipInfo(name, date_time=(1980, 1, 1, 0, 0, 0))
                info.create_system = 3
                info.external_attr = 0o100644 << 16
                info.compress_type = zipfile.ZIP_DEFLATED
                archive.writestr(info, payload[name], compresslevel=9)
        with zipfile.ZipFile(temporary) as archive:
            if archive.namelist() != list(NAMES) or archive.testzip() is not None:
                raise ValueError('Package archive verification failed')
            for name, data in payload.items():
                if archive.read(name) != data:
                    raise ValueError(f'Package byte verification failed: {name}')
        temporary.replace(output)
    finally:
        temporary.unlink(missing_ok=True)
    return hashlib.sha256(output.read_bytes()).hexdigest()


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--game-path', type=Path, required=True)
    parser.add_argument('--automap-markers-path', type=Path, required=True,
                        help='Path to the external AutoMapMarkers.dll (not vendored)')
    parser.add_argument('--dotnet', default='dotnet')
    parser.add_argument('--output', type=Path,
                        default=ROOT / 'artifacts/KuboczochUnifiedMushroomMarkers-1.0.0.zip')
    args = parser.parse_args()
    game = args.game_path.resolve()
    for required in ('VintagestoryAPI.dll', 'Mods/VSSurvivalMod.dll', 'Lib/0Harmony.dll'):
        if not (game / required).is_file():
            parser.error(f'Missing external game SDK input: {required}')
    automap = args.automap_markers_path.resolve()
    if not automap.is_file():
        parser.error('Missing external Auto Map Markers assembly')
    properties = [f'-p:GamePath={game}', f'-p:AutoMapMarkersPath={automap}']
    project = str(ADDON / 'UnifiedMushroomMarkers.csproj')
    subprocess.run([args.dotnet, 'build', project, '-c', 'Release', *properties],
                   check=True, cwd=ROOT)
    result = subprocess.run([args.dotnet, 'msbuild', project, '-p:Configuration=Release',
                             *properties, '-getProperty:TargetPath'],
                            check=True, cwd=ROOT, capture_output=True, text=True)
    assembly = Path(result.stdout.strip())
    if assembly.name != 'UnifiedMushroomMarkers.dll':
        raise ValueError('Unexpected build TargetPath')
    checksum = package(assembly, args.output)
    args.output.with_suffix('.zip.sha256').write_text(f'{checksum}  {args.output.name}\n')
    print(f'Package verified: {args.output}\nSHA256: {checksum}')


if __name__ == '__main__':
    main()
