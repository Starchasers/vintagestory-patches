#!/usr/bin/env python3
"""Exercise published providers with native VS methods; no SDK/provider redistribution."""
import argparse
import hashlib
import json
from pathlib import Path
import shutil
import subprocess
import sys
import zipfile

ROOT = Path(__file__).resolve().parents[1]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--dotnet', required=True)
    parser.add_argument('--game-path', type=Path, required=True)
    parser.add_argument('--provider', nargs=3, action='append', required=True,
                        metavar=('LABEL', 'DLL_DIRECTORY', 'ZIP'))
    parser.add_argument('--mod-assembly', type=Path, help='Exercise this release DLL instead of linked-source green')
    parser.add_argument('--baseline-assembly', type=Path, help='Optional 1.0.2 DLL for real startup/authority reds')
    parser.add_argument('--output', type=Path, required=True)
    args = parser.parse_args()
    if args.baseline_assembly and not args.mod_assembly:
        parser.error("--baseline-assembly requires --mod-assembly for the release under test")
    args.output.mkdir(parents=True, exist_ok=True)
    summary = []
    for label, directory, archive in args.provider:
        if not label.replace('-', '').isalnum():
            raise ValueError('Unsafe provider label')
        provider = Path(directory) / 'ACulinaryArtillery.dll'
        with zipfile.ZipFile(archive) as source:
            dlls = [n for n in source.namelist() if Path(n).name == 'ACulinaryArtillery.dll']
            if len(dlls) != 1 or source.read(dlls[0]) != provider.read_bytes():
                raise ValueError('Provider DLL directory does not match asset ZIP')
        manifest = args.output / f'stoppers-{label}.json'
        discovery = subprocess.run([sys.executable, str(ROOT / 'tests/discover_stoppers.py'), archive],
                                   check=True, capture_output=True, text=True)
        manifest.write_text(discovery.stdout)
        specs = json.loads(discovery.stdout)
        build = subprocess.run([args.dotnet, 'build', str(ROOT / 'tests/Tests.csproj'), '-t:Rebuild',
                                '-c', 'Release', f'-p:GamePath={args.game_path}', f'-p:AcaPath={directory}',
                                *([f'-p:TestModAssembly={args.baseline_assembly.resolve()}'] if args.baseline_assembly else [])],
                               capture_output=True, text=True)
        (args.output / f'build-{label}.log').write_text(build.stdout + build.stderr)
        build.check_returncode()
        copied = ROOT / 'tests/bin/Release/net10.0/ACulinaryArtillery.dll'
        provider = Path(directory) / 'ACulinaryArtillery.dll'
        assert copied.read_bytes() == provider.read_bytes(), 'Wrong test provider copied'
        mod_target = ROOT / 'tests/bin/Release/net10.0/KuboczochStopperReturn.dll'
        if args.mod_assembly:
            shutil.copyfile(args.mod_assembly, mod_target)
        release_bytes = mod_target.read_bytes()
        modes = [('red', ['--red'], 1), ('clone-only-red', ['--clone-only'], 1),
                 ('green', [], 0), ('flow-native-red', ['--native-flow', '--red'], 1),
                 ('flow-green', ['--native-flow'], 0), ('lifecycle-green', ['--lifecycle'], 0)]
        if args.baseline_assembly:
            modes += [('flow-102-direct-install', ['--native-flow', '--single-install'], None),
                      ('lifecycle-102-red', ['--lifecycle'], 1)]
        for mode, flags, expected in modes:
            if '102' in mode:
                shutil.copyfile(args.baseline_assembly, mod_target)
            else:
                mod_target.write_bytes(release_bytes)
            if mode == 'green' and args.mod_assembly:
                flags = [f'--mod-assembly={args.mod_assembly.resolve()}']
            result = subprocess.run([args.dotnet, str(ROOT / 'tests/bin/Release/net10.0/Tests.dll'),
                                     str(manifest), *flags], capture_output=True, text=True)
            text = result.stdout + result.stderr
            (args.output / f'{label}-{mode}.log').write_text(text)
            print(f'{label} {mode}: exit={result.returncode}\n{text}', end='')
            assert (result.returncode == expected if expected is not None else result.returncode in (0, 1)), 'Unexpected native exit'
            marker = next((line for line in reversed(text.splitlines()) if line.startswith(('RESULT', 'PASS lifecycle', 'FAIL lifecycle'))), None)
            assert marker is not None, 'Missing native result marker'
            summary.append({'provider': label, 'providerSha256': hashlib.sha256(provider.read_bytes()).hexdigest(),
                            'variants': len(specs), 'mode': mode, 'exitCode': result.returncode,
                            'modSha256': hashlib.sha256(mod_target.read_bytes()).hexdigest(), 'result': marker})
        mod_target.write_bytes(release_bytes)
    (args.output / 'native-summary.json').write_text(json.dumps(summary, indent=2) + '\n')


if __name__ == '__main__':
    main()
