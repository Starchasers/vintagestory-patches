#!/usr/bin/env python3
"""Discover usable ACA stoppers directly from a provider ZIP (stdlib only).

Usage: python discover_stoppers.py ACulinaryArtillery.zip > stoppers.json

Only provider itemtypes are read; compatibility patches (including optional
Wildcraft additions) are deliberately not applied. Recursive object inheritance
merges attributes, while child arrays replace parent arrays. Raw ceramic items
have no true isLiquidContainerStopper attribute and are therefore excluded.
The attributes output retains the provider's ByType maps, not invented defaults.
"""

import argparse
from copy import deepcopy
from fnmatch import fnmatchcase
from itertools import product
import json
from pathlib import PurePosixPath
import sys
from zipfile import ZipFile


def merge(parent, child):
    """Merge inherited JSON objects; replace scalars and arrays wholesale."""
    result = deepcopy(parent)
    for key, value in child.items():
        if isinstance(value, dict) and isinstance(result.get(key), dict):
            result[key] = merge(result[key], value)
        else:
            result[key] = deepcopy(value)
    return result


def discover(zip_path):
    """Return a stable manifest, resolving parents only from the given ZIP."""
    with ZipFile(zip_path) as archive:
        names = set(archive.namelist())
        cache = {}

        def resolve(name, ancestors=()):
            if name in ancestors:
                raise ValueError(f"Inheritance cycle: {' -> '.join((*ancestors, name))}")
            if name in cache:
                return deepcopy(cache[name])
            source = json.loads(archive.read(name).decode('utf-8-sig'))
            parent = source.get('inheritFrom')
            if parent:
                domain = PurePosixPath(name).parts[1]
                if ':' in parent:
                    domain, parent = parent.split(':', 1)
                parent_name = f"assets/{domain}/{parent.removesuffix('.json')}.json"
                if parent_name not in names:
                    raise ValueError(f"Missing provider parent {parent_name} for {name}")
                source = merge(resolve(parent_name, (*ancestors, name)), source)
            cache[name] = source
            return deepcopy(source)

        result = {}
        for name in sorted(names):
            path = PurePosixPath(name)
            if (len(path.parts) < 4 or path.parts[0] != 'assets'
                    or path.parts[2] != 'itemtypes'
                    or not path.match('stopper*.json')):
                continue
            source = resolve(name)
            groups = source.get('variantgroups', [])
            states = []
            for group in groups:
                values = group.get('states')
                if not isinstance(values, list) or not all(isinstance(v, str) for v in values):
                    raise ValueError(f"Unsupported variant states in {name}: {group!r}")
                states.append(values)
            for variant in product(*states):
                base_code = source['code']
                domain = path.parts[1]
                if ':' in base_code:
                    domain, base_code = base_code.split(':', 1)
                code_path = '-'.join((base_code, *variant))
                code = f'{domain}:{code_path}'
                if any(fnmatchcase(code_path, pattern) for pattern in source.get('skipVariants', [])):
                    continue
                allowed = source.get('allowedVariants')
                if allowed is not None and not any(fnmatchcase(code_path, pattern) for pattern in allowed):
                    continue
                attributes = deepcopy(source.get('attributes', {}))
                # Root attributesByType is an ordered first-match override. Ignore
                # malformed scalar entries such as raw ACA's misplaced false flag.
                for pattern, value in source.get('attributesByType', {}).items():
                    if isinstance(value, dict) and fnmatchcase(code_path, pattern):
                        attributes = merge(attributes, value)
                        break
                if attributes.get('isLiquidContainerStopper') is not True:
                    continue
                if code in result:
                    raise ValueError(f'Duplicate provider stopper code: {code}')
                result[code] = {
                    'code': code,
                    'maxStackSize': source.get('maxstacksize', source.get('maxStackSize')),
                    'storageFlags': source.get('storageFlags'),
                    'attributes': attributes,
                }
        return [result[code] for code in sorted(result)]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('aca_zip', help='ACA provider ZIP; no optional patches are applied')
    args = parser.parse_args()
    try:
        manifest = discover(args.aca_zip)
        if not manifest:
            raise ValueError('No usable provider stoppers found')
    except (OSError, ValueError, KeyError) as exc:
        parser.exit(1, f'{parser.prog}: {exc}\n')
    json.dump(manifest, sys.stdout, indent=2, sort_keys=True)
    sys.stdout.write('\n')


if __name__ == '__main__':
    main()
