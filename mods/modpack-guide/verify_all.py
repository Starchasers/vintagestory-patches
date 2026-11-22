"""Run offline acceptance suite and retain actual stdout/stderr, not synthetic results."""
from pathlib import Path
import subprocess,os,sys,hashlib,json
W=Path(__file__).parent
sdk='/home/brunette/.hermes/cache/scratch/woodworking-tutorial/dotnet'
commands=[[sys.executable,'research_inventory.py'],[sys.executable,'build.py'],[sys.executable,'-m','unittest','test_build','test_pack_audit','-v'],[sys.executable,'verify_attribute_inventory.py'],[sys.executable,'validate_content_rewrite.py'],[sdk+'/dotnet','run','--project','harness/RichtextHarness.csproj','-c','Release','--nologo']]
logs=[]
for cmd in commands:
 r=subprocess.run(cmd,cwd=W,env={**os.environ,'DOTNET_ROOT':sdk},capture_output=True,text=True);logs.append('$ '+' '.join(cmd)+'\n'+r.stdout+r.stderr+'\nEXIT '+str(r.returncode)+'\n')
 if r.returncode:
  (W/'test-results.txt').write_text('\n'.join(logs));print(logs[-1]);sys.exit(r.returncode)
zipfile=W/'dist/KuboczochModpackGuide-1.1.0.zip';first=hashlib.sha256(zipfile.read_bytes()).hexdigest()
r=subprocess.run([sys.executable,'build.py'],cwd=W,capture_output=True,text=True);second=hashlib.sha256(zipfile.read_bytes()).hexdigest();assert r.returncode==0 and first==second
logs.append('PASS: consecutive installation builds have identical SHA-256 '+first+'\n')
(W/'test-results.txt').write_text('\n'.join(logs))
print('\n'.join(logs))
