"""Create private install/source archives from the real build; no upload or installation."""
from pathlib import Path
import zipfile,shutil,hashlib,json
ROOT=Path(__file__).resolve().parent
PKG=ROOT/'package'; OUT=ROOT/'dist';OUT.mkdir(exist_ok=True)
dll=ROOT/'src/bin/Release/net10.0/KuboczochVisualMining.dll'
if not dll.is_file():raise SystemExit('Build the Release DLL first')
shutil.copy2(dll,PKG/dll.name)
for name in ['README.md','ROOT-CAUSE.md','TEST-EVIDENCE.txt','asset-audit.json','REVIEW.md']:
    if not (ROOT/name).is_file():raise SystemExit('Missing evidence/documentation: '+name)
    shutil.copy2(ROOT/name,PKG/name)
def writezip(path, entries):
    with zipfile.ZipFile(path,'w',compression=zipfile.ZIP_DEFLATED,compresslevel=9) as z:
        for relative,p in sorted(entries):
            i=zipfile.ZipInfo(relative,date_time=(2026,1,1,0,0,0));i.compress_type=zipfile.ZIP_DEFLATED;i.external_attr=0o644<<16
            z.writestr(i,p.read_bytes())
    with zipfile.ZipFile(path) as z:
        assert z.testzip() is None
        assert len(z.namelist())==len(set(z.namelist()))
    print(path, path.stat().st_size, 'bytes; ZIP CRC PASS')
install=OUT/'KuboczochVisualMining_1.0.4.zip'
writezip(install,[(p.relative_to(PKG).as_posix(),p) for p in PKG.rglob('*') if p.is_file()])
entries=[]
for directory in ['src','sound-src','voxel-src','tests','package']:
    for p in (ROOT/directory).rglob('*'):
        if not p.is_file() or any(x in {'bin','obj','__pycache__'} for x in p.relative_to(ROOT).parts) or p.suffix in {'.dll','.pdb'}:continue
        entries.append((p.relative_to(ROOT).as_posix(),p))
for name in ['README.md','ROOT-CAUSE.md','TEST-EVIDENCE.txt','asset-audit.json','prepare_assets.py','audit.py','package.py','REVIEW.md']:
    if (ROOT/name).is_file():entries.append((name,ROOT/name))
for p in (ROOT/'evidence').rglob('*'):
    if p.is_file() and p.suffix in {'.txt','.json'}:entries.append((p.relative_to(ROOT).as_posix(),p))
source=OUT/'KuboczochVisualMining_1.0.4_source.zip'
writezip(source,entries)
manifest=''.join(f'{hashlib.sha256(p.read_bytes()).hexdigest()}  {p.name}\n' for p in sorted(OUT.glob('*.zip')))
(OUT/'SHA256SUMS.txt').write_text(manifest)
print(manifest,end='')
with zipfile.ZipFile(install) as z:
    assert {'KuboczochVisualMining.dll','modinfo.json','README.md','TEST-EVIDENCE.txt'} <= set(z.namelist())
    info=json.loads(z.read('modinfo.json'));assert info['name']=='Kuboczoch Visual Mining'
    assert [n for n in z.namelist() if n.endswith('.dll')]==['KuboczochVisualMining.dll']
    assert z.read('KuboczochVisualMining.dll')==dll.read_bytes()
with zipfile.ZipFile(source) as z:
    assert all(not n.endswith('.dll') for n in z.namelist())
    assert 'src/KuboczochVisualMining.csproj' in z.namelist()
print('PASS final install/source archive contents verified; source ZIP excludes game/original/compiled DLLs')
