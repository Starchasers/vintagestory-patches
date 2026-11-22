"""Static invariant checks; does not execute game/mod DLLs."""
import json
from pathlib import Path
from attribute_inventory import page_code, tree_json

root = Path(__file__).resolve().parent
report = json.loads((root / 'attribute_inventory.json').read_text())
rows = report['codes']
by_code = {r['code']: r for r in rows}
assert len(by_code) == len(rows) == 35
assert report['counts'] == {'definedCodes': 35, 'handbookCodes': 19, 'nativeHandbookStacks': 85, 'generatedAttributeStacks': 72}
expected = {'cloth': 17, 'leather': 11, 'wool': 17, 'rawhide': 1, 'sturdy': 1}
for material, count in expected.items():
    assert by_code['toolsmith:fabricstrips-' + material]['stackCount'] == count
assert by_code['toolsmith:handle']['stackCount'] == 13
assert by_code['toolsmith:carpentedhandle']['stackCount'] == 13
assert by_code['toolsmith:crudehandle']['stackCount'] == 1
assert by_code['toolsmith:tinkertoolparts']['navigationDestination'] is None
pages = {s['pageCode'] for r in rows for s in r['stacks']}
assert len(pages) == 85
for r in rows:
    assert len(r['stacks']) == r['stackCount']
    for s in r['stacks']:
        assert s['pageCode'] == page_code(r['kind'], r['code'], s['attributes'])
        # Mimic VtmlParser ParseQuotedValue: quotes of the other kind are raw.
        link = json.loads(json.dumps(s['richtextLink']))
        href = link.split("href='", 1)[1].split("'", 1)[0]
        assert href == 'handbook://' + s['pageCode']
        assert '&quot;' not in link
        if r['generatedCreativeAttributes']:
            assert r['kind'] + '-' + r['code'] not in pages
    if r['navigationDestination']:
        assert r['navigationDestination']['pageCode'] in pages
assert all(by_code['toolsmith:workbench-' + direction]['navigationDestination']['pageCode'] == 'block-toolsmith:workbench-north' for direction in ['east', 'south', 'west'])
assert tree_json({'z': {}, 'a': {'y': 'v', 'b': 'w'}}) == '{ "a": { "b": "w", "y": "v" }, "z": {  } }'
assert {p['mod'] for p in report['appliedNativeCompatibilityPatches']} == {'wool', 'tailorsdelight'}
print('PASS: 35 defined codes, 19 native handbook codes, 85 unique pages, 72 attributed stacks; links/aliases/compatibility/internal exclusion verified statically.')
