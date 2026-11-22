#!/usr/bin/env python3
"""Build/test using external SDK and original pack; package only owned files."""
import argparse, hashlib, io, json, pathlib, subprocess, zipfile
ROOT = pathlib.Path(__file__).resolve().parents[1]
p = argparse.ArgumentParser()
p.add_argument('--vs', type=pathlib.Path, required=True)
p.add_argument('--pack', type=pathlib.Path, required=True)
p.add_argument('--dotnet', default='dotnet')
a = p.parse_args()
vs, pack = a.vs.resolve(), a.pack.resolve()
for f in ['VintagestoryAPI.dll','Lib/0Harmony.dll','Mods/VSSurvivalMod.dll','assets/survival/blocktypes/wood/firepit.json']:
    if not (vs/f).is_file(): raise SystemExit('Missing SDK input: '+f)
for folder in ['audit','evidence','dist']: (ROOT/folder).mkdir(exist_ok=True)
sha = lambda b: hashlib.sha256(b).hexdigest()
manifest = {'pack_sha256':sha(pack.read_bytes()),'inputs':{},'firepit_mentions':[]}
with zipfile.ZipFile(pack) as outer:
    name = next(n for n in outer.namelist() if n.endswith('/ArtOfGrowing_1.2.2.zip'))
    aogbytes=outer.read(name)
    manifest['aog_zip_sha256']=sha(aogbytes)
    with zipfile.ZipFile(io.BytesIO(aogbytes)) as aog:
        for source, target in [('ArtOfGrowing.dll','ArtOfGrowing.dll'),('assets/artofgrowing/blocktypes/wood/firepit.json','assets__artofgrowing__blocktypes__wood__firepit.json')]:
            data=aog.read(source); (ROOT/'audit'/target).write_bytes(data);manifest['inputs'][source]=sha(data)
        manifest['aog_modinfo']=json.loads(aog.read('modinfo.json'))
    # Audit every nested mod's JSON for competing firepit changes; keep metadata, not upstream source.
    for nested in outer.namelist():
        if not nested.endswith('.zip'): continue
        try: mod=zipfile.ZipFile(io.BytesIO(outer.read(nested)))
        except zipfile.BadZipFile: continue
        for asset in mod.namelist():
            if asset.endswith('.json') and '/lang/' not in asset:
                data=mod.read(asset)
                if b'firepit' in data.lower(): manifest['firepit_mentions'].append({'mod':nested,'asset':asset,'sha256':sha(data)})
for path in ['VintagestoryAPI.dll','Mods/VSSurvivalMod.dll','assets/survival/blocktypes/wood/firepit.json']:
    manifest['inputs'][path]=sha((vs/path).read_bytes())
(ROOT/'evidence'/'input-audit.json').write_text(json.dumps(manifest,indent=2)+'\n')
def run(cmd, log):
    result=subprocess.run(cmd,cwd=ROOT,text=True,stdout=subprocess.PIPE,stderr=subprocess.STDOUT)
    (ROOT/'evidence'/log).write_text(result.stdout)
    print(result.stdout,end='')
    if result.returncode: raise SystemExit(result.returncode)
run([a.dotnet,'build','src/FirepitTinderReturn.csproj','-c','Release',f'-p:VSPath={vs}'],'build.log')
run([a.dotnet,'run','--project','tests/Regression.csproj','-c','Release',f'-p:VSPath={vs}','--',str(vs/'assets/survival/blocktypes/wood/firepit.json'),str(ROOT/'audit/assets__artofgrowing__blocktypes__wood__firepit.json')],'regression.log')
def archive(name, mapping):
    target=ROOT/'dist'/name
    with zipfile.ZipFile(target,'w',compression=zipfile.ZIP_DEFLATED,compresslevel=9) as z:
        for dest,path in sorted(mapping.items()):
            part=pathlib.PurePosixPath(dest)
            if part.is_absolute() or '..' in part.parts or any(ord(c)<32 for c in dest) or path.is_symlink(): raise ValueError(dest)
            info=zipfile.ZipInfo(dest,(2026,1,1,0,0,0));info.external_attr=0o100644<<16;info.compress_type=zipfile.ZIP_DEFLATED
            z.writestr(info,path.read_bytes())
    with zipfile.ZipFile(target) as z:
        assert len(z.namelist())==len(mapping) and set(z.namelist())==set(mapping) and z.testzip() is None
        for dest,path in mapping.items(): assert z.read(dest)==path.read_bytes()
    return {'file':name,'sha256':sha(target.read_bytes()),'entries':sorted(mapping),'bytes':target.stat().st_size}
release={n:ROOT/n for n in ['modinfo.json','README.md','LICENSE']}
release['Kuboczoch.FirepitTinderReturn.dll']=ROOT/'src/bin/Release/net10.0/Kuboczoch.FirepitTinderReturn.dll'
source={n:ROOT/n for n in ['modinfo.json','README.md','LICENSE','src/FirepitTinderReturn.cs','src/FirepitTinderReturn.csproj','tests/Program.cs','tests/Regression.csproj','tools/build.py','tools/smoke.py']}
outputs=[archive('kuboczochfirepittinderreturn_1.0.0.zip',release),archive('kuboczochfirepittinderreturn_1.0.0_source.zip',source)]
(ROOT/'dist'/'manifest.json').write_text(json.dumps(outputs,indent=2)+'\n')
print(json.dumps(outputs,indent=2))
