"""Auditable source/evidence bundle with immutable baseline inputs; no third-party DLLs."""
import hashlib,zipfile
from pathlib import Path
W=Path(__file__).parent;D=W/'dist';ROOT='pszczoly-process-guides/'
def put(z,n,b):
 i=zipfile.ZipInfo(n,(2026,11,17,0,0,0));i.compress_type=zipfile.ZIP_DEFLATED;i.external_attr=0o644<<16;z.writestr(i,b)
source=D/'KuboczochModpackGuide-1.4.0-source.zip'
with zipfile.ZipFile(source,'w',compression=zipfile.ZIP_DEFLATED,compresslevel=9) as z:
 for p in sorted(W.rglob('*')):
  if not p.is_file():continue
  rel=p.relative_to(W)
  if any(x in rel.parts for x in ('__pycache__','bin','obj','dist','.git','generated-addon')):continue
  if p.suffix.lower() in ('.dll','.pdb','.exe','.dmp') or p.name.startswith('core.'):continue
  put(z,ROOT+rel.as_posix(),p.read_bytes())
 for version in ('1.1.0','1.2.0','1.3.0','1.3.1'):
  name='KuboczochModpackGuide-'+version+'.zip';put(z,ROOT+'dist/'+name,(D/name).read_bytes())
 with zipfile.ZipFile(D/'KuboczochModpackGuide-1.4.0.zip') as install:
  for n in install.namelist():
   if not n.endswith('.dll'):put(z,ROOT+'generated-addon/'+n,install.read(n))
with zipfile.ZipFile(source) as audit:
 assert audit.testzip() is None
 for name in audit.namelist():
  rel=name.removeprefix(ROOT)
  if rel.startswith('generated-addon/'):
   with zipfile.ZipFile(D/'KuboczochModpackGuide-1.4.0.zip') as install:assert audit.read(name)==install.read(rel.removeprefix('generated-addon/')),name
  else:assert audit.read(name)==(W/rel).read_bytes(),name
print('PASS both ZIP CRCs; every archived source/input byte equals workspace; generated resources equal install ZIP')
rows=[]
for p in (D/'KuboczochModpackGuide-1.4.0.zip',source):
 with zipfile.ZipFile(p) as z:assert z.testzip() is None
 digest=hashlib.sha256(p.read_bytes()).hexdigest();rows.append(digest+'  '+p.name);print(p,p.stat().st_size,digest)
(D/'SHA256SUMS-1.4.0').write_text('\n'.join(rows)+'\n')
