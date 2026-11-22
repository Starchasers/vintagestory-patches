"""Offline checks against the actual baseline archive; does not write build output."""
import json
from pathlib import Path
import re
import zipfile
from content_rewrite import rewrite, GROUPS, DESCRIPTIONS

BASELINE = Path('/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted/mods/KuboczochModpackGuide-1.0.2.zip')
HREF = re.compile(r'href="([^"]+)"')
BANNED = re.compile(r'Installed version|audited|\[Native guides\]|\[Mod DB\]|\[truncated\]|…|\.\.\.|Start here:|All in-game guides|No in-game guide available|does not reproduce upstream|not excluded as|independently documented|all four installed|source defaults', re.I)


def main():
    with zipfile.ZipFile(BASELINE) as archive:
        baseline = json.loads(archive.read('assets/kuboczochmodpackguide/lang/en.json').decode('utf-8-sig'))
    snapshot = dict(baseline)
    result = rewrite(baseline)
    assert baseline == snapshot, 'Input mutated'
    assert set(result) == set(baseline), 'Language keys changed'
    pages = [k for k in baseline if k.endswith('-text')]
    for key in pages:
        assert result[key] != baseline[key], ('Unrewritten page', key)
        assert set(HREF.findall(result[key])) == set(HREF.findall(baseline[key])), ('Href change', key)
        assert not BANNED.search(result[key]), ('Boilerplate', key, BANNED.search(result[key]).group())
        assert len(HREF.findall(result[key])) == len(set(HREF.findall(result[key]))), ('Repeated destination', key)
    mod_ids = {k[4:-5] for k in pages if k.startswith('mod-')}
    assert mod_ids == set(DESCRIPTIONS), ('Missing description', mod_ids ^ set(DESCRIPTIONS))
    for category, groups in GROUPS.items():
        members = [m for _, mods in groups for m in mods.split()]
        assert len(members) == len(set(members)), category
        baseline_members = {h.rsplit('mod-', 1)[1] for h in HREF.findall(baseline['category-' + category + '-text']) if '-mod-' in h}
        assert baseline_members == set(members), ('Category member change', category)
    for key, terms in {
        'mod-realsmoke-text': ['ventilate', 'smother'],
        'mod-rivers-text': ['swimmers', 'configuration', 'compatibility patch'],
        'mod-roughseas-text': ['bail', 'configuration', 'stroke power'],
        'mod-livestockwelfare-text': ['disables the breeding module', 'bedding', 'inactive'],
        'mod-fallingtree-text': ['impact damage', 'disables domino'],
        'mod-toolbelt-text': ['two', 'four', 'configuration'],
    }.items():
        for term in terms:
            assert term in result[key], (key, term)
    assert rewrite(result) == result, 'Rewrite is not idempotent'
    assert rewrite({}) == {}, 'Empty input handling'
    unrelated = {'game:example': 'Unrelated text'}
    assert rewrite(unrelated) == unrelated
    print(json.dumps({'pages_rewritten': len(pages), 'mod_pages': len(mod_ids), 'category_pages': len(GROUPS), 'root_pages': 1, 'distinct_page_href_pairs_preserved': sum(len(set(HREF.findall(baseline[k]))) for k in pages), 'boilerplate_matches': 0, 'input_unchanged': True, 'idempotent': True}, indent=2))


if __name__ == '__main__':
    main()
