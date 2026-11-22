"""Read-only static source audit of new references and exhaustive manifest coverage.
Never loads installed mod DLLs. Registration fixtures are not a mod-startup claim.
"""
import copy,fnmatch,hashlib,itertools,json,re,zipfile
from pathlib import Path
import json5
from build import PACK,VS
from process_content import drafts,TOPICS,key,NS
W=Path(__file__).parent

def read_sources():
 assets={};origins={};mods=json.loads((PACK/'evpack.json').read_text())['Mods']
 for p in VS.glob('assets/**/*.json'):
  n=p.relative_to(VS).as_posix();a=n.split('/');a[1]='game';loc='/'.join(a)
  assets[loc]=p.read_bytes();origins[loc]=str(p)
 hashes=[]
 for m in mods:
  if not m['Enabled']:continue
  f=PACK/'mods'/m['FileName'];digest=hashlib.sha256(f.read_bytes()).hexdigest()
  expected=m.get('Sha256',m.get('SHA256',m.get('Hash')))
  if expected:assert digest.lower()==expected.lower(),f
  hashes.append({'modid':m['ModId'],'archive':str(f),'sha256':digest})
  with zipfile.ZipFile(f) as z:
   for n in z.namelist():
    if n.startswith('assets/') and n.endswith('.json'):
     assets[n]=z.read(n);origins[n]=str(f)+'!'+n
 return assets,origins,mods,hashes

def build():
 ds=drafts();targets={(i['type'],c) for p in ds.values() for s in p['steps'] for i in s.get('items',[]) for c in i['code'].split('|')}
 assets,origins,mods,hashes=read_sources();defs={};parse_errors=[]
 for n,b in assets.items():
  if not re.match(r'assets/[^/]+/(itemtypes|blocktypes)/',n):continue
  domain=n.split('/')[1]
  if not any(c.startswith(domain+':') for _,c in targets):continue
  try:a=json5.loads(b)
  except Exception as e:parse_errors.append({'source':origins[n],'error':str(e)});continue
  if not isinstance(a,dict) or not isinstance(a.get('code'),str):continue
  if any(c==domain+':'+a['code'] or c.startswith(domain+':'+a['code']+'-') for _,c in targets):defs[n]=a
 # World properties are also inputs to registration (e.g. pewter ingots).
 for n,b in assets.items():
  if '/worldproperties/' in n:
   try:defs[n]=json5.loads(b)
   except Exception:pass
 # Only normal root patch folders; compatibility/ is not automatically active.
 patches={n:[] for n in defs};patch_errors=[]
 enabled={m['ModId'] for m in mods if m['Enabled']}
 for n,b in assets.items():
  if not re.match(r'assets/[^/]+/patches/',n):continue
  try:records=json5.loads(b)
  except Exception:continue
  if isinstance(records,dict):records=[records]
  if not isinstance(records,list):continue
  for rec in records:
   if not isinstance(rec,dict) or not isinstance(rec.get('file'),str):continue
   if rec.get('enabled') is False:continue
   # Textures/behaviours do not establish a code and need not be emulated.
   if not any(rec.get('path','').lower().startswith('/'+x) for x in ('variantgroups','variants','allowedvariants','skipvariants')):continue
   deps=rec.get('dependsOn',[])
   if isinstance(deps,dict):deps=[deps]
   if any((d.get('modid',d.get('modId')) not in enabled) != bool(d.get('invert',False)) for d in deps if isinstance(d,dict)):continue
   f=rec['file'];domain,path=f.split(':',1) if ':' in f else (n.split('/')[1],f)
   pat='assets/'+domain+'/'+path
   for target,a in defs.items():
    if not (fnmatch.fnmatchcase(target,pat) or fnmatch.fnmatchcase(target,pat+'.json')):continue
    parts=[x.replace('~1','/').replace('~0','~') for x in rec.get('path','').strip('/').split('/')];node=a
    try:
     for x in parts[:-1]:node=node[int(x)] if isinstance(node,list) else node[x]
     x=parts[-1];op=rec.get('op')
     if op in ('add','addmerge','replace'):
      value=copy.deepcopy(rec['value'])
      if isinstance(node,list):
       if x=='-':node.append(value)
       elif op=='add':node.insert(int(x),value)
       else:node[int(x)]=value
      elif op=='addmerge' and isinstance(node.get(x),dict):node[x].update(value)
      elif op=='addmerge' and isinstance(node.get(x),list):node[x].extend(value if isinstance(value,list) else [value])
      else:node[x]=value
     elif op=='remove':
      if isinstance(node,list):node.pop(int(x))
      else:node.pop(x,None)
     else:raise ValueError('unsupported '+str(op))
     patches[target].append(origins[n])
    except Exception as e:patch_errors.append({'source':origins[n],'target':target,'path':rec.get('path'),'error':str(e)})
 def codes(a,domain):
  gs=a.get('variantgroups',a.get('variantGroups',[]));combinations=[([], {})]
  for g in gs:
   ss=list(g.get('states',[]))
   if 'loadFromProperties' in g:
    prop=g['loadFromProperties'];dom,prop=prop.split(':',1) if ':' in prop else (domain,prop)
    pp=f'assets/{dom}/worldproperties/{prop}.json'
    if pp not in defs:pp=f'assets/game/worldproperties/{prop}.json'
    ss.extend(v.get('code',v.get('Code')) for v in defs[pp]['variants'])
   if not ss:raise ValueError('no states '+str(g))
   combos=[]
   for vals,named in combinations:
    if g.get('combine','').lower()=='selectivemultiply' and named.get(g['onVariant'])!=g['code']:
     combos.append((vals,named));continue
    for s in ss:combos.append((vals+([str(s)] if s else []),dict(named,**{g['code']:s})))
   combinations=combos
  for vals,named in combinations:
   c=a['code']+''.join('-'+x for x in vals)
   if any(fnmatch.fnmatchcase(c,p) for p in a.get('skipVariants',a.get('skipvariants',[]))):continue
   allow=a.get('allowedVariants',a.get('allowedvariants'))
   if allow and not any(fnmatch.fnmatchcase(c,p) for p in allow):continue
   yield domain+':'+c
 resolved={};expansion_errors=[]
 for n,a in defs.items():
  if '/itemtypes/' not in n and '/blocktypes/' not in n:continue
  dom=n.split('/')[1];kind='block' if '/blocktypes/' in n else 'item'
  try:
   for c in codes(a,dom):
    if (kind,c) in targets:resolved[(kind,c)]={'type':kind,'code':c,'source':origins[n],'patches':patches[n],'basis':'static definition/variants and matching content patches; not live registry'}
  except Exception as e:expansion_errors.append({'source':origins[n],'error':str(e)})
 missing=sorted(targets-set(resolved))
 report={'count':len(targets),'resolved':list(resolved.values()),'missing':missing,'parseErrors':parse_errors,'patchErrors':patch_errors,'expansionErrors':expansion_errors,'archiveHashes':hashes,'runtimeModsExecuted':False}
 (W/'process-reference-inventory.json').write_text(json.dumps(report,indent=2)+'\n')
 print('PROCESS REFERENCE AUDIT',len(resolved),'/',len(targets),'missing:',missing,'expansion errors',expansion_errors)
 if missing:raise SystemExit(1)
 return report
if __name__=='__main__':build()
