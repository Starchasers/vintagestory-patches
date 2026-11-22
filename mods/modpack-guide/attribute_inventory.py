"""Static Toolsmith creative-stack model; no managed assembly is executed.
Evidence: evidence/attributes/*.cs, decompiled with ilspycmd.
Called by research_inventory.py; importing this file has no side effects.
"""
import copy
import fnmatch
import itertools
import json


def effective_assets(assets, installed):
    """Apply installed compatibility patches targeting native definitions only.

    Scope intentionally excludes recipes, vanilla assets, and runtime part stats.
    Preserve unpatched sourceAssets as evidence in the original research report.
    """
    result = copy.deepcopy(assets)
    applied = []
    for source, patches in assets.items():
        if '/compatibility/' not in source or '/patches/' not in source:
            continue
        mod = source.split('/compatibility/')[1].split('/')[0]
        if mod not in installed:
            continue
        for patch in patches if isinstance(patches, list) else [patches]:
            target = patch.get('file', '')
            if not target.startswith('toolsmith:itemtypes/'):
                continue
            target = 'assets/toolsmith/' + target.split(':', 1)[1]
            if target not in result:
                raise ValueError('Unresolved native compatibility target: ' + target)
            tokens = patch['path'].strip('/').split('/')
            node = result[target]
            for token in tokens[:-1]:
                node = node[int(token)] if isinstance(node, list) else node[token]
            key = tokens[-1]
            value = copy.deepcopy(patch['value'])
            if key == '-':
                if patch['op'] == 'addeach':
                    node.extend(value)
                elif patch['op'] in ('add', 'addmerge'):
                    node.append(value)
                else:
                    raise ValueError(patch)
            elif patch['op'] in ('add', 'addmerge'):
                assert key not in node, ('Unexpected overwrite', source, key)
                node[key] = value
            else:
                raise ValueError(patch)
            applied.append({'mod': mod, 'source': source, 'target': target, 'patch': patch})
    return result, applied


def tree_json(value):
    """Exact TreeAttribute.ToJsonToken whitespace and recursive SortedCopy(true).

    StringAttribute.ToJsonToken is quoted raw text; all source texture/shape paths
    contain neither quotes nor control characters, so json.dumps is equivalent.
    """
    if isinstance(value, dict):
        return '{ ' + ', '.join(json.dumps(k) + ': ' + tree_json(value[k]) for k in sorted(value)) + ' }'
    return json.dumps(value, ensure_ascii=False)


def page_code(kind, code, attrs):
    return kind + '-' + code + ('-' + tree_json(attrs) if attrs else '')


def generate(assets, inventory, applied):
    rows = []
    handles = {v['id']: v for v in assets['assets/toolsmith/config/toolsmith/parts/handles/handles-vanilla.json']}
    for definition in inventory:
        asset = assets[definition['source']]
        for code in definition['codeVariants']:
            path = code.split(':', 1)[1]
            behaviors = asset.get('behaviors', [])
            for pattern, choices in asset.get('behaviorsByType', {}).items():
                if fnmatch.fnmatchcase(path, pattern):
                    behaviors = choices
                    break
            props = next((b.get('properties', {}) for b in behaviors if b['name'].endswith(':ModularPartRenderingFromAttributes')), None)
            generated = props is not None and not props.get('skipCreativeInventoryAdditions', True)
            original_tabs = [tab for tab, patterns in asset.get('creativeinventory', {}).items() if any(fnmatch.fnmatchcase(path, p) for p in patterns)]
            combinations = []
            if generated:
                textures = props.get('textures', [])
                # AddAllToCreativeInventory creates this tree before recursion even
                # when there are no texture dimensions (notably crudehandle).
                initial = {'modularPartRenderData': {'partTextures': {}}}
                if path in handles:
                    # CollectibleBehaviorToolHandle.InitializeRenderTree supplies shape.
                    initial['modularPartRenderData']['partShapeIndex'] = handles[path]['handleShapePath']
                for choices in itertools.product(*(t.get('values', []) for t in textures)):
                    attrs = copy.deepcopy(initial)
                    for texture, value in zip(textures, choices):
                        attrs['modularPartRenderData']['partTextures'][texture['code']] = value
                    combinations.append(attrs)
            else:
                combinations = [{}]
            handbook = definition['handbook']
            included = not handbook.get('exclude', False) and (generated or bool(original_tabs) or handbook.get('include', False))
            if not included:
                combinations = []
            stacks = []
            for attrs in combinations:
                pc = page_code(definition['kind'], code, attrs)
                assert "'" not in pc and '<' not in pc and '>' not in pc
                stacks.append({'attributes': attrs, 'pageCode': pc, 'href': 'handbook://' + pc,
                               'richtextLink': "<a href='handbook://" + pc + "'>" + code + '</a>'})
            # Prefer configured defaults where present; defaults are NOT added by
            # the recursion and must themselves occur in values to select a page.
            textures = props.get('textures', []) if generated else []
            default = next((s for s in stacks if all(s['attributes']['modularPartRenderData']['partTextures'].get(t['code']) == t.get('Default') for t in textures)), None)
            representative = default or (stacks[0] if stacks else None)
            rows.append({'code': code, 'kind': definition['kind'], 'source': definition['source'],
                         'generatedCreativeAttributes': generated,
                         'creativeTabs': props.get('creativeTabs', []) if generated else original_tabs,
                         'originalCreativeTabs': original_tabs,
                         'textureDimensions': textures,
                         'stackCount': len(stacks), 'handbookIncluded': bool(included),
                         'representative': representative, 'stacks': stacks,
                         'internal': path == 'tinkertoolparts',
                         'destinationNote': None if included else 'Defined collectible but no creative tabs/stacks or handbook.include: no native handbook destination. Link to a custom explanation page instead.'})
    by_code = {r['code']: r for r in rows}
    for row in rows:
        destination = row['representative']
        if destination is None and row['kind'] == 'block':
            north = row['code'].rsplit('-', 1)[0] + '-north'
            destination = by_code.get(north, {}).get('representative')
            if destination is not None:
                row['destinationNote'] = 'This orientation has no native handbook page. Navigate to the same block material in its creative north orientation.'
        row['navigationDestination'] = destination
    return {'scope': 'Static reconstruction of installed native Toolsmith creative/handbook stacks, using decompiled methods; assemblies never executed. Installed ZIP mods assumed active as supplied by parent. No arbitrary crafted/recovered runtime attributes invented.',
            'evidence': ['evidence/attributes/ModularPartRenderingFromAttributes.cs', 'evidence/attributes/CollectibleBehaviorToolHandle.cs', 'evidence/attributes/MultiPartRenderingHelpers.cs', 'evidence/attributes/TreeAttribute.cs', 'evidence/attributes/CollectibleObject.cs', 'evidence/richtext/GuiHandbookItemStackPage.cs', 'evidence/richtext/VtmlUtil.cs', 'evidence/richtext/VtmlParser.cs'],
            'appliedNativeCompatibilityPatches': applied,
            'linkStrategy': 'VtmlUtil itemstack accepts codes only, not JsonItemStack attributes. Use single-quoted VTML href with raw JSON double quotes; escape the outer language/config JSON with a JSON serializer. Do NOT HTML-encode JSON quotes as &quot;: VtmlParser does not decode entities in attribute values. Use representative.richtextLink as real navigation; unattributed itemstack icons are decorative and may navigate to a missing bare page.',
            'counts': {'definedCodes': len(rows), 'handbookCodes': sum(r['handbookIncluded'] for r in rows), 'nativeHandbookStacks': sum(r['stackCount'] for r in rows), 'generatedAttributeStacks': sum(r['stackCount'] for r in rows if r['generatedCreativeAttributes'])},
            'codes': rows}
