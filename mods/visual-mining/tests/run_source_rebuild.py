"""Extract source ZIP into a fresh scratch directory, build and run all suites."""
from pathlib import Path
import argparse, tempfile, zipfile, subprocess, os, hashlib, json
p=argparse.ArgumentParser();p.add_argument('--dotnet',default='dotnet');p.add_argument('--game',required=True);a=p.parse_args()
root=Path(__file__).resolve().parents[1]
fresh=Path(tempfile.mkdtemp(prefix='kvm104-source-',dir=os.environ.get('TMPDIR',str(root.parent))))
with zipfile.ZipFile(root/'dist/KuboczochVisualMining_1.0.4_source.zip') as z:
    assert z.testzip() is None;assert not any(n.endswith('.dll') for n in z.namelist());z.extractall(fresh)
logs=[]
for cmd in [[a.dotnet,'build','src/KuboczochVisualMining.csproj','-c','Release','-p:GameDir='+a.game],['python','tests/run_verification.py','--dotnet',a.dotnet,'--game',a.game],['python','audit.py']]:
    r=subprocess.run(cmd,cwd=fresh,text=True,capture_output=True)
    logs.append('$ '+' '.join(cmd)+'\nexit='+str(r.returncode)+'\n'+r.stdout+r.stderr)
    if r.returncode:raise RuntimeError(logs[-1])
original=(root/'src/bin/Release/net10.0/KuboczochVisualMining.dll').read_bytes()
rebuilt=(fresh/'src/bin/Release/net10.0/KuboczochVisualMining.dll').read_bytes()
logs.append('Original DLL SHA256 '+hashlib.sha256(original).hexdigest()+'\nFresh DLL SHA256 '+hashlib.sha256(rebuilt).hexdigest()+'\nByte-identical DLL: '+str(original==rebuilt)+'\nFresh directory: '+str(fresh))
out=root/'evidence/1.0.4/fresh-source-rebuild.txt';out.write_text('\n'.join(logs))
print('\n'.join(logs));print('PASS fresh source ZIP rebuild + complete integration/voxel/sound/audit; evidence:',out)
