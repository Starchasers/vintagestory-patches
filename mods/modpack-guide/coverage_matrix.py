"""Reconcile every enabled ID to exact archive, source research and guide/support disposition."""
import json,hashlib,re,zipfile
from pathlib import Path
from build import PACK
from process_content import drafts,TOPICS,NS,key
W=Path(__file__).parent
GROUPS={c:t for t,(_,cs) in TOPICS.items() for c in cs}

def run():
 ds=drafts();research={};datasets={}
 for f in sorted((W/'research').glob('*.json')):
  d=json.loads(f.read_text());datasets[f.name]=d
  for row in d.get('classifications',d.get('coverage',d.get('evidenceClassification',[]))):
   mid=row.get('modid',row.get('modId',row.get('mod')));assert mid,(f,row)
   research.setdefault(mid,[]).append({'file':f.relative_to(W).as_posix(),'record':row})
 enabled=[m for m in json.loads((PACK/'evpack.json').read_text())['Mods'] if m['Enabled']]
 assert len({m['ModId'] for m in enabled})==len(enabled)
 assert {m['ModId'] for m in enabled}<=set(research),set(m['ModId'] for m in enabled)-set(research)
 matrix=[]
 for m in enabled:
  mid=m['ModId'];rs=research[mid];archive=PACK/'mods'/m['FileName']
  effective=next((r['record'] for r in rs if r['file']=='research/tweaks.json'),rs[-1]['record'])
  effectiveStatus=effective.get('status',effective.get('configStatus',effective.get('configurationOrModuleStatus','static asset/recipe research; module applicability detailed in cited research')))
  effective=dict(effective,status=effectiveStatus)
  with zipfile.ZipFile(archive) as z:
   info=next(n for n in z.namelist() if n.lower()=='modinfo.json');assets=[n for n in z.namelist() if n.endswith('.json') and n.startswith('assets/')]
   sources=[str(archive)+'!'+info]
   # This is a source inventory, not a claim that every asset affects gameplay.
   config=[str(p) for p in (PACK/'config/ModConfig').rglob('*') if p.is_file() and mid.lower() in p.name.lower()]
  guides={c for c,p in ds.items() if mid in p['mods']}
  for r in rs:
   row=r['record'];extra=row.get('coveredBy',row.get('relevantGuides',row.get('existingGuideCodes',[])))
   if isinstance(extra,str):extra=[extra]
   guides.update(key(c) for c in extra if isinstance(c,str) and key(c) in ds)
  cats=sorted({GROUPS[c] for c in guides})
  if not guides:
   # Explicit support/exclusion, never invent a progression instruction for a library.
   status='support/excluded from player process steps'
   disposition='See linked research classification for technical, UI, ambience, world-generation or uncertain runtime impact. No unsupported acquisition/upgrade gate is asserted.'
  else:status='player process coverage';disposition='Cross-mod steps use only the source-backed subset recorded below; configuration/runtime unknowns are retained in research.'
  matrix.append({'modid':mid,'name':m['Name'],'version':m['Version'],'archive':str(archive),'sha256':hashlib.sha256(archive.read_bytes()).hexdigest(),'classification':status,'effectiveResearch':effective,'disposition':disposition,'processPages':sorted(NS+c for c in guides),'categoryGuides':[NS+'process-index-'+t for t in (cats or sorted(TOPICS))],'researchRecords':rs,'metadataEvidence':sources,'nativeJsonAssetCount':len(assets),'matchingConfigFiles':config})
 steps=[]
 for c,p in ds.items():
  for n,s in enumerate(p['steps'],1):
   ev=s.get('evidence',[]);assert ev,(c,n)
   steps.append({'page':NS+c,'step':n,'label':s['label'],'claim':s['text'],'items':s.get('items',[]),'evidence':ev,'evidenceDocument':p['source'],'sourceRecord':f'pages[{list(x["code"] for x in datasets[Path(p["source"]).name]["pages"]).index(c)}].steps[{n-1}]' if c in [x['code'] for x in datasets[Path(p['source']).name]['pages']] else 'pages[].steps[]'})
 report={'manifest':str(PACK/'evpack.json'),'manifestSha256':hashlib.sha256((PACK/'evpack.json').read_bytes()).hexdigest(),'enabledCount':len(enabled),'classifiedCount':len(matrix),'playerProcessCount':sum(bool(r['processPages']) for r in matrix),'supportOrExcludedCount':sum(not r['processPages'] for r in matrix),'scope':'Exact exported enabled manifest; static source research. Config files alone are not proof that a module runs. No installed mod DLL executed and no GUI is available.','mods':matrix,'processSteps':steps}
 (W/'coverage-matrix.json').write_text(json.dumps(report,indent=2)+'\n')
 lines=['# Enabled-pack coverage matrix','',f'{len(matrix)}/{len(enabled)} enabled manifest IDs reconciled. All support/exclusion rows are explicit; no player page is fabricated for a library. Research records carry source-specific claims and limits.','', '| ID | Version | Disposition | Process pages | Research |','|---|---|---|---|---|']
 for row in matrix:lines.append('| '+ ' | '.join([row['modid'],row['version'],row['classification'],', '.join(c.removeprefix(NS) for c in row['processPages']) or 'Support / excluded: see evidence',', '.join(x['file'] for x in row['researchRecords'])])+' |')
 (W/'COVERAGE-MATRIX.md').write_text('\n'.join(lines)+'\n')
 print('PASS exhaustive manifest reconciliation:',len(matrix),'/',len(enabled),'enabled;',report['playerProcessCount'],'player process;',report['supportOrExcludedCount'],'support/excluded;',len(steps),'source-linked steps')
 return report
if __name__=='__main__':run()
