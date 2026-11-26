#!/usr/bin/env python3
"""Build with an external game SDK and package only explicitly owned files.

The package() helper is exercised with synthetic test bytes; only the CLI builds
production source. It deliberately has no --assembly or --skip-build escape hatch.
"""
import argparse
import hashlib
import json
import os
import re
from pathlib import Path
import subprocess
import sys
import tempfile
import zipfile

VERSION = '0.1.0'
MOD_ID = 'kuboczochssafersurface'
ASSEMBLY = 'KuboczochsSaferSurface.dll'
ALLOWLIST = (ASSEMBLY, 'modinfo.json', 'README.md', 'LICENSE')
ARCHIVE_NAME = f'{MOD_ID}-{VERSION}.zip'


class PackageError(ValueError):
    """A build/package boundary failed validation."""


def safe_path(path):
    """Reject links in the path itself and all parent components."""
    path = Path(os.path.abspath(path))
    if any(part.is_symlink() for part in (path, *path.parents)):
        raise PackageError(f'Symlink path is not permitted: {path}')
    return path


def validate_names(names):
    names = list(names)
    if len(names) != len(set(names)):
        raise PackageError('Duplicate archive entries')
    for name in names:
        if name not in ALLOWLIST or Path(name).name != name:
            raise PackageError(f'Archive entry is not allowlisted: {name!r}')


def read_owned(path):
    path = safe_path(path)
    if not path.is_file():
        raise PackageError(f'Missing regular input: {path}')
    data = path.read_bytes()
    if not data:
        raise PackageError(f'Empty input: {path}')
    return data


def unique_object(pairs):
    result = {}
    for key, value in pairs:
        if key in result:
            raise PackageError(f'Duplicate metadata key: {key}')
        result[key] = value
    return result


def validate_metadata(data):
    try:
        metadata = json.loads(data, object_pairs_hook=unique_object)
    except (ValueError, UnicodeError) as error:
        raise PackageError(f'Invalid modinfo.json: {error}') from error
    expected = {'type': 'code', 'modid': MOD_ID, 'version': VERSION,
                'side': 'Server', 'requiredOnClient': False, 'requiredOnServer': True}
    if not isinstance(metadata, dict):
        raise PackageError('modinfo.json must be an object')
    for key, value in expected.items():
        if type(metadata.get(key)) is not type(value) or metadata[key] != value:
            raise PackageError(f'modinfo.json requires {key}={value!r}')


def verify_archive(path, payload):
    with zipfile.ZipFile(path) as archive:
        names = archive.namelist()
        validate_names(names)
        if names != sorted(payload) or archive.testzip() is not None:
            raise PackageError('Archive manifest/CRC verification failed')
        for name, data in payload.items():
            if archive.read(name) != data:
                raise PackageError(f'Archive byte mismatch: {name}')


def package(root, assembly):
    """Package explicit inputs; callers of this helper must establish provenance.

    This is a mechanical integrity check, not managed-assembly authentication or
    a claim of SDK/native compatibility. CLI provenance comes from dotnet build.
    """
    root = safe_path(root)
    assembly = safe_path(assembly)
    if assembly.name != ASSEMBLY:
        raise PackageError(f'Only the owned runtime assembly {ASSEMBLY} is permitted')
    payload = {name: read_owned(assembly if name == ASSEMBLY else root / name)
               for name in ALLOWLIST}
    validate_names(payload)
    if not payload[ASSEMBLY].startswith(b'MZ'):
        raise PackageError('Runtime output lacks PE signature')
    validate_metadata(payload['modinfo.json'])
    for name in ('README.md', 'LICENSE', 'modinfo.json'):
        if payload[name].startswith((b'MZ', b'\x7fELF', b'PK\x03\x04')):
            raise PackageError(f'Executable/archive masquerading as text: {name}')
        try:
            text = payload[name].decode('utf-8')
        except UnicodeError as error:
            raise PackageError(f'Non-UTF-8 text: {name}') from error
        sensitive = (r'-----BEGIN (?:[A-Z]+ )?PRIVATE KEY-----',
                     r'/(?:home|Users)/[^/\s]+/',
                     r'\bgh[pousr]_[A-Za-z0-9]{20,}',
                     r'\bAKIA[A-Z0-9]{16}\b')
        if any(re.search(pattern, text) for pattern in sensitive):
            raise PackageError(f'Sensitive credential/home-path pattern in {name}')
    dist = safe_path(root / 'dist')
    target = safe_path(dist / ARCHIVE_NAME)
    checksum = safe_path(target.with_suffix('.zip.sha256'))
    dist.mkdir(parents=True, exist_ok=True)
    # Stage on the destination filesystem, verify, then atomically replace.
    with tempfile.TemporaryDirectory(prefix='.package-', dir=dist) as staging:
        candidate = Path(staging) / ARCHIVE_NAME
        with zipfile.ZipFile(candidate, 'w', compression=zipfile.ZIP_DEFLATED,
                             compresslevel=9) as archive:
            for name in sorted(payload):
                info = zipfile.ZipInfo(name, date_time=(1980, 1, 1, 0, 0, 0))
                info.create_system = 3
                info.external_attr = 0o100644 << 16
                info.compress_type = zipfile.ZIP_DEFLATED
                archive.writestr(info, payload[name], compresslevel=9)
        verify_archive(candidate, payload)
        digest = hashlib.sha256(candidate.read_bytes()).hexdigest()
        staged_checksum = Path(staging) / checksum.name
        staged_checksum.write_text(f'{digest}  {target.name}\n', encoding='ascii')
        os.replace(candidate, target)
        os.replace(staged_checksum, checksum)
    return target


def build_and_package(root, game_dir, dotnet='dotnet'):
    root = safe_path(root)
    game_dir = safe_path(game_dir)
    if not game_dir.is_dir():
        raise PackageError(f'GameDir is not a directory: {game_dir}')
    # The actual project remains authoritative about all other SDK dependencies.
    read_owned(game_dir / 'VintagestoryAPI.dll')
    project = safe_path(root / 'src' / 'SaferSurface.csproj')
    if not project.is_file():
        raise PackageError(f'Missing production project: {project}')
    # Clean output prevents a previous assembly from satisfying a failed build.
    with tempfile.TemporaryDirectory(prefix='safer-surface-build-') as staging:
        output = Path(staging)
        subprocess.run([dotnet, 'build', str(project), '--configuration', 'Release',
                        '--framework', 'net10.0', '--output', str(output),
                        f'-p:GameDir={game_dir}', '-p:Deterministic=true',
                        '-p:ContinuousIntegrationBuild=true'], cwd=root, check=True)
        return package(root, output / ASSEMBLY)


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--game-dir', required=True, type=Path,
                        help='External licensed Vintage Story SDK/game directory')
    parser.add_argument('--dotnet', default='dotnet', help='dotnet executable (default: dotnet)')
    args = parser.parse_args(argv)
    try:
        archive = build_and_package(Path(__file__).resolve().parents[1], args.game_dir, args.dotnet)
    except (PackageError, OSError, subprocess.CalledProcessError) as error:
        print(f'Package failed: {error}', file=sys.stderr)
        return 1
    print(archive)
    print(archive.with_suffix('.zip.sha256').read_text(), end='')
    return 0


if __name__ == '__main__':
    raise SystemExit(main())
