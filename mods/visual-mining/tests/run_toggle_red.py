"""Run the SAME integration sources against the actual archived 1.0.3 DLL (expected RED)."""
from pathlib import Path
import argparse, subprocess, zipfile, json, xml.etree.ElementTree as ET, os
p=argparse.ArgumentParser()
p.add_argument('--dotnet',default='dotnet');p.add_argument('--game',required=True)
p.add_argument('--legacy-install',type=Path,required=True)
a=p.parse_args();root=Path(__file__).resolve().parents[1]
legacy=root/'tests/obj/legacy-1.0.3';legacy.mkdir(parents=True,exist_ok=True)
with zipfile.ZipFile(a.legacy_install) as z:
    assert json.loads(z.read('modinfo.json'))['version']=='1.0.3'
    (legacy/'KuboczochVisualMining.dll').write_bytes(z.read('KuboczochVisualMining.dll'))
tree=ET.parse(root/'tests/IntegrationTests.csproj');project=tree.getroot()
ET.SubElement(project.find('PropertyGroup'),'EnableDefaultCompileItems').text='false'
for group in project.findall('ItemGroup'):
    for reference in group.findall('ProjectReference'): group.remove(reference)
group=ET.SubElement(project,'ItemGroup')
reference=ET.SubElement(group,'Reference',Include='KuboczochVisualMining')
ET.SubElement(reference,'HintPath').text=str(legacy/'KuboczochVisualMining.dll')
ET.SubElement(group,'Compile',Include=str(root/'tests/*.cs'))
path=legacy/'LegacyRegression.csproj';tree.write(path)
cmd=[a.dotnet,'run','--project',str(path),'-c','Release','-p:GameDir='+str(Path(a.game).resolve())]
r=subprocess.run(cmd,cwd=root,env=dict(os.environ,VS_GAME_DIR=str(Path(a.game).resolve())),text=True,capture_output=True)
text='$ '+' '.join(cmd)+'\nexit='+str(r.returncode)+'\n'+r.stdout+r.stderr
out=root/'evidence/1.0.4/integration-red-actual-1.0.3.txt';out.parent.mkdir(parents=True,exist_ok=True);out.write_text(text)
assert r.returncode!=0 and 'Tool toggle regressions:' in text,text[-4000:]
assert 'existing config is NEVER overwritten on startup' in text
assert 'actual tool switch stops owned animations in both managers' in text
print('PASS expected RED: actual archived 1.0.3 fails existing-config preservation and owned FP/TP animation cleanup; log:',out)
