"""Current 1.4.0 acceptance; records actual output, stops at first failing command."""
import hashlib,json,re,subprocess,sys
from pathlib import Path
W=Path(__file__).parent
DOTNET='/home/brunette/.hermes/cache/scratch/woodworking-tutorial/dotnet/dotnet'
commands=[[DOTNET,'build','client/ToolsmithFamilySlideshows.csproj','-c','Release'],[sys.executable,'verify_table_sources.py'],[sys.executable,'process_inventory.py'],[sys.executable,'process_labels.py'],[sys.executable,'verify_process_sources.py'],[sys.executable,'coverage_matrix.py'],[sys.executable,'release_build.py'],[sys.executable,'registry_input.py'],[DOTNET,'run','--project','harness/RichtextHarness.csproj','-c','Release'],[DOTNET,'run','--project','client/tests/FamilyTests.csproj','-c','Release','--','attribute_inventory.json','client/assets/kuboczochmodpackguide/config/toolsmith-families.json'],[DOTNET,'run','--project','client/geometry/TableGeometry.csproj','-c','Release'],[sys.executable,'-m','unittest','test_progression','test_tables','test_root_index','test_process_guides','test_process_labels','-v']]
with (W/'test-results-1.4.0.txt').open('w') as log:
 for cmd in commands:
  result=subprocess.run(cmd,cwd=W,text=True,stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
  text='$ '+' '.join(cmd)+'\n'+result.stdout+f'\nEXIT {result.returncode}\n\n';log.write(text);log.flush();print(text,end='')
  if result.returncode:raise SystemExit(result.returncode)
 artifact=W/'dist/KuboczochModpackGuide-1.4.0.zip'
 first=hashlib.sha256(artifact.read_bytes()).hexdigest()
 subprocess.run([sys.executable,'release_build.py'],cwd=W,check=True,stdout=subprocess.DEVNULL)
 assert first==hashlib.sha256(artifact.read_bytes()).hexdigest()
 log.write('PASS consecutive 1.4.0 install builds have identical SHA-256 '+first+'\n')
print('ALL CURRENT 1.4.0 ACCEPTANCE COMMANDS PASSED')
