"""Offline manifest/ZIP handbook audit; clean() returns a new language dict.

Build integration: report = audit(); lang = clean(lang). The builder MUST also
remove report['stale_page_assets'] from its output archive. No input is written.
Duplicate anchors retain their visible label but lose the repeated link wrapper.
External HTTP statuses are deliberately unknown, never a pruning signal.
"""
from functools import lru_cache
from pathlib import Path
import hashlib
import html
import json
import re
import zipfile

import json5

ROOT = Path('/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted')
EVPACK = ROOT / 'evpack.json'
BASELINE = ROOT / 'mods/KuboczochModpackGuide-1.0.2.zip'
DOMAIN = 'kuboczochmodpackguide'
ANCHOR = re.compile(r'<a\b(?P<attrs>[^>]*)>(?P<label>.*?)</a\s*>', re.I | re.S)
HREF = re.compile(r'\bhref\s*=\s*(?:"([^"]*)"|\'([^\']*)\'|([^\s>]+))', re.I)


def _href(match):
    found = HREF.search(match['attrs'])
    return html.unescape(next(x for x in found.groups() if x is not None)) if found else None


def _sha(path):
    digest = hashlib.sha256()
    with path.open('rb') as stream:
        for chunk in iter(lambda: stream.read(1024 * 1024), b''):
            digest.update(chunk)
    return digest.hexdigest()


def _load(raw):
    return json5.loads(raw.decode('utf-8-sig'))


@lru_cache(maxsize=1)
def audit():
    """Return exact baseline coverage, manifest/metadata evidence and stale IDs.

    Cached for one build process; call audit.cache_clear() after input changes.
    Aliases are accepted only from explicit modinfo aliases/provides declarations;
    website slugs and asset domains are recorded but do not prove mod identity.
    """
    manifest = json.loads(EVPACK.read_text())
    records = manifest['Mods']
    with zipfile.ZipFile(BASELINE) as archive:
        pages = []
        for name in archive.namelist():
            if '/config/handbook/mod-' in name and name.endswith('.json'):
                page = _load(archive.read(name))
                page_id = page['pageCode'].removeprefix(DOMAIN + '-mod-')
                pages.append({'id': page_id, 'pageCode': page['pageCode'], 'asset': name,
                              'title_key': page.get('title'), 'text_key': page.get('text')})
        baseline_lang = _load(archive.read(f'assets/{DOMAIN}/lang/en.json'))
    metadata = []
    issues = []
    enabled = {m['ModId']: m for m in records if m.get('Enabled') is True}
    all_records = {m['ModId']: m for m in records}
    for record in records:
        evidence = {'manifest_id': record['ModId'], 'enabled': record.get('Enabled'),
                    'bundled': record.get('Bundled'), 'filename': record.get('FileName'),
                    'manifest_version': record.get('Version'), 'manifest_sha256': record.get('Sha256'),
                    'aliases': []}
        path = ROOT / 'mods' / record['FileName']
        try:
            evidence['sha256'] = _sha(path)
            evidence['hash_matches'] = evidence['sha256'].lower() == str(record.get('Sha256', '')).lower()
            with zipfile.ZipFile(path) as archive:
                info = {k.lower(): v for k, v in _load(archive.read('modinfo.json')).items()}
                evidence['metadata'] = info
                evidence['asset_domains'] = sorted({n.split('/')[1] for n in archive.namelist()
                                                   if n.startswith('assets/') and len(n.split('/')) > 2})
            evidence['metadata_id_matches'] = info.get('modid') == record['ModId']
            evidence['metadata_version_matches'] = info.get('version') == record.get('Version')
            for field in ('aliases', 'provides'):
                values = info.get(field, [])
                if isinstance(values, str):
                    values = [values]
                if isinstance(values, dict):
                    values = list(values)
                for value in values:
                    alias = value if isinstance(value, str) else value.get('modid') if isinstance(value, dict) else None
                    if alias:
                        evidence['aliases'].append({'id': alias, 'source': 'modinfo.json:' + field})
            for field in ('hash_matches', 'metadata_id_matches', 'metadata_version_matches'):
                if not evidence[field]:
                    issues.append({'filename': record['FileName'], 'issue': field, 'evidence': evidence.copy()})
        except (OSError, KeyError, ValueError, zipfile.BadZipFile) as error:
            evidence['error'] = str(error)
            issues.append({'filename': record['FileName'], 'issue': str(error)})
        metadata.append(evidence)
    coverage = []
    for page in sorted(pages, key=lambda p: p['id']):
        page_id = page['id']
        matches = [m for m in metadata if m['enabled'] is True and
                   (m['manifest_id'] == page_id or any(a['id'] == page_id for a in m['aliases']))]
        row = {**page, 'manifest_path': str(EVPACK), 'enabled_exact_match': page_id in enabled,
               'bundled_matches': matches}
        if matches:
            row['status'] = 'installed' if page_id in enabled else 'installed-alias'
        elif page_id in all_records:
            row['status'] = 'stale'
            row['reason'] = 'Manifest entry exists but Enabled is not true.'
            row['manifest_entry'] = all_records[page_id]
        else:
            row['status'] = 'stale'
            row['reason'] = 'No evpack ModId and no explicit alias in any enabled bundled modinfo.json.'
            row['manifest_entry'] = None
        row['external_urls'] = [{'url': _href(m), 'status': 'unknown', 'checked': False}
                                for m in ANCHOR.finditer(baseline_lang.get('mod-' + page_id + '-text', ''))
                                if (_href(m) or '').startswith(('http://', 'https://'))]
        coverage.append(row)
    stale = [p for p in coverage if p['status'] == 'stale']
    # Missing/corrupt metadata makes a negative alias assertion unsafe.
    prune_safe = not any(m.get('error') for m in metadata if m['enabled'] is True)
    return {'baseline': str(BASELINE), 'baseline_sha256': _sha(BASELINE),
            'manifest': str(EVPACK), 'manifest_sha256': _sha(EVPACK),
            'baseline_mod_page_count': len(pages), 'manifest_mod_count': len(records),
            'enabled_mod_count': len(enabled), 'coverage': coverage,
            'stale_ids': [p['id'] for p in stale], 'stale': stale,
            'stale_page_assets': [p['asset'] for p in stale], 'metadata': metadata,
            'metadata_issues': issues, 'prune_safe': prune_safe,
            'external_url_policy': 'Unknown offline; never remove installed mods because of external link status.'}


def dedupe_hrefs(text):
    """Keep the first anchor per exact decoded href in this one page only."""
    seen = set()
    def replace(match):
        href = _href(match)
        if href is None:
            return match[0]
        if href in seen:
            return match['label']
        seen.add(href)
        return match[0]
    return ANCHOR.sub(replace, text)


def clean(lang):
    """Copy language dict, prune proven stale keys/inbound anchors, dedupe links.

    Bullet directory rows containing only a stale destination are removed with
    their descriptions; other prose keeps surrounding text, without dead anchors.
    All string values are processed, so both en/pl and future languages work.
    """
    report = audit()
    if report['stale_ids'] and not report['prune_safe']:
        raise ValueError('Cannot safely prune: enabled archive metadata could not be read')
    stale = set(report['stale_ids'])
    destinations = {'handbook://' + p['pageCode'] for p in report['stale']}
    output = {}
    for key, value in lang.items():
        if any(key == 'mod-' + mod or key.startswith('mod-' + mod + '-') for mod in stale):
            continue
        if isinstance(value, str):
            pieces = re.split(r'(<br\s*/?>\s*<br\s*/?>)', value, flags=re.I)
            for index in range(0, len(pieces), 2):
                segment = pieces[index]
                links = [m for m in ANCHOR.finditer(segment) if _href(m)]
                if re.match(r'^\s*•', segment) and links and all(_href(m) in destinations for m in links):
                    pieces[index] = ''
                    if index + 1 < len(pieces):
                        pieces[index + 1] = ''
                else:
                    pieces[index] = ANCHOR.sub(lambda m: '' if _href(m) in destinations else m[0], segment)
            value = dedupe_hrefs(''.join(pieces))
        output[key] = value
    return output


if __name__ == '__main__':
    print(json.dumps(audit(), indent=2, ensure_ascii=False))
