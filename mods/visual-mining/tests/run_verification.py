"""Run real engine CPU tests. Requires owned VS 1.22.7 assets/DLLs and .NET 10.
No graphical/gameplay session is claimed. Optional --legacy-install exercises actual 1.0.2 DLL.
"""
from pathlib import Path
import argparse, os, subprocess, json, re, hashlib, zipfile
root = Path(__file__).resolve().parents[1]
p = argparse.ArgumentParser()
p.add_argument('--dotnet', default='dotnet')
p.add_argument('--game', required=True)
p.add_argument('--legacy-install', type=Path)
args = p.parse_args()
evidence = root / 'evidence/1.0.4'
evidence.mkdir(parents=True, exist_ok=True)
env = dict(os.environ, VS_GAME_DIR=str(Path(args.game).resolve()))
for key in ['KVM_REPRO_DISPATCH_LEGACY','KVM_REPRO_TOPSOIL_LEGACY','KVM_REPRO_LEGACY']:
    env.pop(key, None)

def run(name, argv, expected=0):
    result = subprocess.run(argv, cwd=root, env=env, capture_output=True, text=True)
    text = '$ ' + ' '.join(argv) + '\nexit=' + str(result.returncode) + '\n' + result.stdout + result.stderr
    (evidence / name).write_text(text)
    if result.returncode != expected:
        raise RuntimeError(name + ': unexpected exit ' + str(result.returncode) + '\n' + text[-4000:])
    print(name + ': PASS (exit=' + str(result.returncode) + ')')
    return text

for project, label in [('tests/IntegrationTests.csproj','integration-green.txt'),('voxel-src/tests/VoxelTests.csproj','voxel.txt'),('sound-src/SoundTests.csproj','sound.txt')]:
    text = run(label, [args.dotnet,'run','--project',project,'-c','Release','-p:GameDir='+str(Path(args.game).resolve())])
    if label == 'integration-green.txt':
        green = text
rows = []
for line in green.splitlines():
    m = re.match(r'dispatch fixture: (.*?), (.*?) drawtype=(.*?) → (\d+) → (.*?);',line)
    if m:
        rows.append(dict(zip(['block','asset','drawtype','drawtype_id','renderer'],m.groups())))
assert rows and len(rows)==len({r['block'] for r in rows})
assert green.count('PASS dispatcher CPU mesh:')==len(rows)
assert green.count('PASS topsoil secondary asset=')==sum(r['renderer']=='TopsoilTesselator' for r in rows)
report={'game':'1.22.7','headless':True,'fixture_count':len(rows),'shovel_fixtures':sum(not r['block'].startswith('game:log-') for r in rows),'fixtures':rows}
files = ['assets/survival/blocktypes/'+name for name in sorted({r['asset'] for r in rows})]
files += ['VintagestoryAPI.dll','VintagestoryLib.dll','Mods/VSSurvivalMod.dll','assets/survival/itemtypes/tool/shovel.json','assets/game/shapes/block/basic/cube.json']
report['reference_sha256']={name:hashlib.sha256((Path(args.game)/name).read_bytes()).hexdigest() for name in files}
(evidence/'coverage.json').write_text(json.dumps(report,indent=2)+'\n')
print('Verified fixture count:',len(rows))
if args.legacy_install:
    # Compile the SAME test sources against the actual old DLL's assembly identity.
    # Swapping a version-2 DLL into a version-3 test build would fail at the loader,
    # not reproduce the renderer bug.
    import xml.etree.ElementTree as ET
    legacy=root/'tests/obj/legacy-1.0.2'
    legacy.mkdir(parents=True,exist_ok=True)
    old_dll=legacy/'KuboczochVisualMining.dll'
    with zipfile.ZipFile(args.legacy_install) as archive:
        assert json.loads(archive.read('modinfo.json'))['version']=='1.0.2'
        old_dll.write_bytes(archive.read('KuboczochVisualMining.dll'))
    project=ET.parse(root/'tests/IntegrationTests.csproj')
    ET.SubElement(project.getroot().find('PropertyGroup'),'EnableDefaultCompileItems').text='false'
    for group in project.getroot().findall('ItemGroup'):
        for reference in group.findall('ProjectReference'):
            group.remove(reference)
    group=ET.SubElement(project.getroot(),'ItemGroup')
    reference=ET.SubElement(group,'Reference',Include='KuboczochVisualMining')
    ET.SubElement(reference,'HintPath').text=str(old_dll)
    ET.SubElement(group,'Compile',Include=str(root/'tests/*.cs'))
    project_path=legacy/'LegacyRegression.csproj'
    project.write(project_path)
    argv=[args.dotnet,'run','--project',str(project_path),'-c','Release','-p:GameDir='+str(Path(args.game).resolve())]
    result=subprocess.run(argv,cwd=root,env=env,capture_output=True,text=True)
    text='ACTUAL 1.0.2 packaged DLL, same test sources; exit='+str(result.returncode)+'\n'+result.stdout+result.stderr
    (evidence/'integration-red-actual-1.0.2.txt').write_text(text)
    assert result.returncode!=0 and 'TopsoilTesselator replacement missing' in text
    assert 'hook/light/source/emission deltas=0/0/0/0' in text
    print('Actual 1.0.2 DLL regression: expected RED verified')
    run('integration-green-restored.txt',[args.dotnet,str(root/'tests/bin/Release/net10.0/IntegrationTests.dll')])
print('PASS real tests; artifacts are CPU observations, not rendered gameplay evidence')
