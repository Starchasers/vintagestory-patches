"""Package source + generated addon assets, excluding runtime binaries and caches."""
from pathlib import Path
import zipfile,hashlib,json
W=Path(__file__).parent
install=W/'dist/KuboczochModpackGuide-1.1.0.zip'
files={}
for p in sorted(W.rglob('*')):
 if not p.is_file() or any(x in p.relative_to(W).parts for x in ['dist','__pycache__','bin','obj']):continue
 if p.suffix.lower() in ['.dll','.pdb','.so','.exe','.pyc']:continue
 files[p.relative_to(W).as_posix()]=p.read_bytes()
with zipfile.ZipFile(install) as z:
 for n in z.namelist():files['addon/'+n]=z.read(n)
out=W/'dist/KuboczochModpackGuide-1.1.0-source.zip'
with zipfile.ZipFile(out,'w',compression=zipfile.ZIP_DEFLATED,compresslevel=9) as z:
 for n,b in sorted(files.items()):
  info=zipfile.ZipInfo(n,(2026,11,16,0,0,0));info.compress_type=zipfile.ZIP_DEFLATED;info.external_attr=0o644<<16;z.writestr(info,b)
with zipfile.ZipFile(out) as z:
 assert z.testzip() is None
 assert z.read('addon/modinfo.json')==zipfile.ZipFile(install).read('modinfo.json')
 assert 'build.py' in z.namelist() and 'test_build.py' in z.namelist() and 'inventory.json' in z.namelist()
 hashes={p.name:hashlib.sha256(p.read_bytes()).hexdigest() for p in [install,out]}
(W/'dist/SHA256SUMS').write_text(''.join(v+'  '+k+'\n' for k,v in hashes.items()))
print(json.dumps({'source':str(out),'files':len(files),'hashes':hashes},indent=2))
