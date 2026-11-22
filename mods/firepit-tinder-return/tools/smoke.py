#!/usr/bin/env python3
"""Fresh loopback-only baseline/addon native lifecycle smoke, never a live deployment."""
import argparse, collections, hashlib, json, pathlib, subprocess, time, zipfile
r=pathlib.Path(__file__).resolve().parents[1]
p=argparse.ArgumentParser();p.add_argument('--vs',type=pathlib.Path,required=True);p.add_argument('--pack',type=pathlib.Path,required=True);p.add_argument('--dotnet',default='dotnet');a=p.parse_args()
vs=a.vs.resolve(); run=r/'smoke'/('run-'+str(time.time_ns()));run.mkdir(parents=True)
subprocess.run([a.dotnet,str(vs/'VintagestoryServer.dll'),'--dataPath',str(run/'template'),'--genconfig'],cwd=vs,check=True,stdout=subprocess.DEVNULL)
with zipfile.ZipFile(a.pack) as z:
    dependencies={pathlib.Path(n).name:z.read(n) for n in z.namelist() if n.endswith(('/ArtOfGrowing_1.2.2.zip','/CoreOfArts_1.2.2.zip'))}
assert len(dependencies)==2, 'Expected exact retained AoG/CoreOfArts dependencies'
summary={}; release=r/'dist/kuboczochfirepittinderreturn_1.0.0.zip'
for name,port in [('baseline',43571),('addon',43572)]:
    d=run/name;(d/'Mods').mkdir(parents=True)
    for filename,data in dependencies.items():(d/'Mods'/filename).write_bytes(data)
    if name=='addon':(d/'Mods'/release.name).write_bytes(release.read_bytes())
    cfg=json.loads((run/'template/serverconfig.json').read_text())
    cfg.update({'Ip':'127.0.0.1','Port':port,'Upnp':False,'AdvertiseServer':False,'MaxClients':1,'MapSizeX':1024000,'MapSizeZ':1024000,'MapSizeY':256,'MaxChunkRadius':2,'ModPaths':['Mods',str(d/'Mods')]})
    cfg['WorldConfig']['Seed']='kuboczoch-firepit-regression'
    cfg['WorldConfig']['SaveFileLocation']=str(d/'Saves/default.vcdbs')
    (d/'serverconfig.json').write_text(json.dumps(cfg))
    logpath=r/'evidence'/f'server-{name}.log';start=time.monotonic();ready=False
    with logpath.open('w') as log:
        proc=subprocess.Popen([a.dotnet,str(vs/'VintagestoryServer.dll'),'--dataPath',str(d),'--ip','127.0.0.1','--port',str(port),'--reducedthreads'],cwd=vs,stdin=subprocess.PIPE,stdout=log,stderr=subprocess.STDOUT,text=True)
        while proc.poll() is None and time.monotonic()-start<110:
            text=logpath.read_text(errors='replace')
            if 'Dedicated Server now running' in text:ready=True;break
            time.sleep(.5)
        if proc.poll() is None:proc.stdin.write('/stop\n');proc.stdin.flush()
        try:proc.wait(timeout=25)
        except subprocess.TimeoutExpired:proc.terminate();proc.wait(timeout=10)
    text=logpath.read_text(errors='replace');lines=text.splitlines()
    issues=[s.split('] ',1)[-1] for s in lines if 'error]' in s.lower() or 'warning]' in s.lower() or 'exception' in s.lower()]
    loaded='artofgrowingpatch' in text and (name!='addon' or '[Kuboczoch Firepit Tinder Return] Dry grass uses game firepits' in text)
    summary[name]={'ready':ready,'exit_code':proc.returncode,'loaded_expected_mods':loaded,'issues':issues,'seconds':round(time.monotonic()-start,1),'data_path':str(d)}
    print(name,json.dumps(summary[name],indent=2))
summary['release_sha256']=hashlib.sha256(release.read_bytes()).hexdigest()
summary['new_addon_issues']=list((collections.Counter(summary['addon']['issues'])-collections.Counter(summary['baseline']['issues'])).elements())
(r/'evidence/server-smoke.json').write_text(json.dumps(summary,indent=2)+'\n')
assert all(summary[k]['ready'] and summary[k]['loaded_expected_mods'] and summary[k]['exit_code']==0 for k in ['baseline','addon']), 'Lifecycle smoke did not load/run cleanly; inspect evidence'
assert not summary['new_addon_issues'], 'New warnings/errors; inspect evidence'
print('SUCCESS native baseline/addon server lifecycle; zero new warnings/errors')
