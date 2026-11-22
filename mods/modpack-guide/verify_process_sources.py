"""Resolve per-step evidence to local source bytes and annotate recipe applicability."""
import hashlib,json,re,zipfile
from pathlib import Path
import json5
from build import PACK,VS
from process_content import drafts
W=Path(__file__).parent

def run():
 out=[];errors=[];aliases={}
 for f in (W/'research').glob('*.json'):
  d=json.loads(f.read_text());ev=d.get('evidence')
  if isinstance(ev,dict):aliases[f.relative_to(W).as_posix()]=ev
 def resolve(ref,doc):
  if ref in aliases.get(doc,{}):
   a=aliases[doc][ref]
   if 'archive' in a and 'member' in a:
    rr=read(str(PACK/'mods'/a['archive'])+'!'+a['member'])
    if 'data' in a:
     actual=json5.loads(rr.pop('_bytes'))
     def subset(x,y):
      if isinstance(x,dict):return isinstance(y,dict) and all(k in y and subset(v,y[k]) for k,v in x.items())
      if isinstance(x,list):return isinstance(y,list) and all(any(subset(v,z) for z in y) for v in x)
      return x==y
     rr['researchExcerptSubsetMatchesReadback']=subset(a['data'],actual)
     # Source bytes, not a reduced researcher excerpt, are authoritative.
    return rr
   if 'path' in a:return read(a['path'])
   raise ValueError('unknown alias '+ref)
  return read(ref)
 def read(ref):
  original=ref;ref=ref.split('#',1)[0]
  if '::' in ref:
   a,b=ref.split('::',1)
   ref=a+'!'+b if a.endswith('.zip') else a
  if '!' in ref:
   a,n=ref.split('!',1);p=Path(a);n=n.removeprefix('/') # zip!/ is an archive-root locator, not a collectible alias
   if not p.is_absolute():p=PACK/p
   with zipfile.ZipFile(p) as z:b=z.read(n)
   loc=str(p)+'!'+n
  else:
   if ref.startswith(('vs/','vs:')):p=VS/ref[3:]
   else:
    p=Path(ref)
    if not p.is_absolute():p=(PACK/p if ref.startswith(('mods/','config/')) or ref=='evpack.json' else W/p)
   if p.is_dir():
    records=[{'file':str(x.relative_to(p)),'sha256':hashlib.sha256(x.read_bytes()).hexdigest()} for x in sorted(p.rglob('*')) if x.is_file()]
    b=json.dumps(records,sort_keys=True).encode();loc=str(p)+'#directory-inventory-only'
   else:b=p.read_bytes();loc=str(p)
  status='source evidence, not recipe'
  if '/recipes/' in loc:status='recipe from enabled source; static applicability only'
  if any('/recipes/barrel/'+x+'.json' in loc for x in ('weaktannin','strongtannin')) and 'immersivewoodworking' not in loc:status='INACTIVE log recipe: removed by enabled IW DisableVanillaLogTannin; cited for delta only'
  return {'citation':original,'resolved':loc,'sha256':hashlib.sha256(b).hexdigest(),'bytes':len(b),'recipeApplicability':status,'_bytes':b}
 for code,p in drafts().items():
  for i,s in enumerate(p['steps'],1):
   records=[]
   for ref in s['evidence']:
    try:
     r=resolve(ref,p['source']);r.pop('_bytes',None);records.append(r)
    except Exception as e:errors.append({'page':code,'step':i,'citation':ref,'error':str(e)})
   out.append({'page':code,'step':i,'claim':s['text'],'sourceRecords':records})
 report={'steps':len(out),'resolvedCitations':sum(len(s['sourceRecords']) for s in out),'errors':errors,'processEvidence':out,'boundary':'Static readback and source bytes, not full mod startup. Original drafts are retained as superseded research; process_editorial.py applies independent-review fixes. IW log-tannin source recipes are explicitly inactive. Current native handbook recipes remain the player authority.'}
 (W/'process-source-readback.json').write_text(json.dumps(report,indent=2)+'\n')
 print('SOURCE READBACK',report['steps'],'steps;',report['resolvedCitations'],'citations;',len(errors),'errors')
 for e in errors:print(e)
 if errors:raise SystemExit(1)
 return report
if __name__=='__main__':run()
