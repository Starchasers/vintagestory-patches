"""English display names, resolved independently of collectible / page identifiers.

Keep native language spelling and case. Fail closed instead of displaying a slug.
The audit records the exact language key, installed source and source-byte hash.
"""
import fnmatch
import hashlib
import json
import re
import zipfile
from functools import lru_cache
from pathlib import Path
import json5
from build import PACK, VS

W = Path(__file__).parent

def parse(raw):
    text = raw.decode('utf-8-sig').replace('\r', '')
    try:
        return json.loads(text)
    except ValueError:
        return json5.loads(text)

@lru_cache(maxsize=1)
def catalog():
    names = {}
    pages = {}
    def ingest(path, raw, origin, native=False):
        domain = 'game' if native else path.split('/')[1]
        if path.endswith('/lang/en.json'):
            try:
                values = parse(raw)
            except ValueError:
                # A broken third-party language file must not fabricate names.
                return
            for key, value in values.items():
                full = key if ':' in key else domain + ':' + key
                names[full] = {'label': value, 'source': origin, 'languageKey': key,
                               'sha256': hashlib.sha256(raw).hexdigest()}
        elif '/config/handbook/' in path and path.endswith('.json'):
            try:
                page = json5.loads(raw)
            except ValueError:
                return
            if isinstance(page, dict) and 'pageCode' in page:
                key = page.get('title', '')
                key = key if ':' in key else 'game:' + key
                pages[page['pageCode']] = (key, origin)
    for p in sorted(VS.glob('assets/*/lang/en.json')):
        ingest(p.relative_to(VS).as_posix(), p.read_bytes(), str(p), True)
    for p in sorted(VS.glob('assets/*/config/handbook/*.json')):
        ingest(p.relative_to(VS).as_posix(), p.read_bytes(), str(p), True)
    for mod in json.loads((PACK / 'evpack.json').read_text())['Mods']:
        if not mod['Enabled']:
            continue
        archive = PACK / 'mods' / mod['FileName']
        with zipfile.ZipFile(archive) as z:
            for path in z.namelist():
                if path.endswith('/lang/en.json') or ('/config/handbook/' in path and path.endswith('.json')):
                    ingest(path, z.read(path), str(archive) + '!' + path)
    return names, pages

# Only for assets whose name is assembled at runtime or whose source language
# is malformed. These are editorial descriptions, explicitly not native titles.
CURATED = {
    'containersbundle:foodcupboard-east': ('Food cupboard', 'Food cupboard block definition; east is placement orientation, not part of its name.'),
    'firestarters-environment-guide': ('Firestarting and the environment', 'Installed firestarters environment handbook page; its English language JSON is malformed.'),
}

def _curated(code):
    label, reason = CURATED[code]
    return {'label': label, 'curatedReason': reason}

@lru_cache(maxsize=None)
def item_label(kind, code):
    names, _ = catalog()
    target = code.split('|')[0]
    domain, path = target.split(':', 1)
    key = domain + ':' + kind + '-' + path
    if key in names:
        return dict(names[key])
    matches = [(k, v) for k, v in names.items() if '*' in k and fnmatch.fnmatchcase(key, k)]
    if matches:
        return dict(max(matches, key=lambda kv: len(kv[0].replace('*', '')))[1])
    if target in CURATED:
        return _curated(target)
    raise ValueError('No justified English item label: ' + key)

@lru_cache(maxsize=None)
def guide_label(code):
    names, pages = catalog()
    if code in pages:
        title, source = pages[code]
        if title in names:
            return dict(names[title], handbookSource=source, titleKey=title)
    # Some mods register pages in code or aggregate handbook files. Their
    # exact language title still supplies a source-backed display name.
    domain, page = code.split(':', 1) if ':' in code else ('game', code)
    title = domain + ':' + page + '-title'
    if title in names:
        return dict(names[title])
    candidates = [v for k, v in names.items() if k.endswith(':' + page + '-title')]
    if len(candidates) == 1:
        return dict(candidates[0])
    if code in CURATED:
        return _curated(code)
    raise ValueError('No justified English handbook title: ' + code)


def annotate(pages):
    """Annotate effective evidence definitions after editorial reference guards."""
    for page in pages.values():
        for step in page['steps']:
            for item in step.get('items', []):
                evidence = item_label(item['type'], item['code'])
                item['label'] = evidence['label']
                item['labelEvidence'] = evidence
        for guide in page['guides']:
            if not guide['code'].startswith('kuboczochmodpackguide-'):
                evidence = guide_label(guide['code'])
                guide['label'] = evidence['label']
                guide['labelEvidence'] = evidence
            else:
                target = guide['code'].removeprefix('kuboczochmodpackguide-')
                guide['label'] = pages[target]['title'].removeprefix('Kitchen - ')
                guide['labelEvidence'] = {'label': guide['label'], 'authoredTitle': target}
    return pages


def audit():
    from process_content import drafts
    records = []
    for code, page in drafts().items():
        for step in page['steps']:
            records.extend({'page': code, 'kind': item['type'], 'code': item['code'], **item['labelEvidence']}
                           for item in step.get('items', []))
        records.extend({'page': code, 'kind': 'guide', 'code': guide['code'], **guide['labelEvidence']}
                       for guide in page['guides'] if 'labelEvidence' in guide)
    (W / 'process-label-audit.json').write_text(json.dumps(records, indent=2, ensure_ascii=False) + '\n')
    print('PASS justified English display labels:', len(records), 'occurrences')
    return records

if __name__ == '__main__':
    audit()
