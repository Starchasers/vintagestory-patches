"""Deterministic asset-only handbook build; statically reads JSON, never loads mod DLLs."""
from pathlib import Path
import json,zipfile,itertools,fnmatch,re,hashlib,html
import json5
from content_rewrite import rewrite
from pack_audit import clean,audit
W=Path(__file__).parent
PACK=Path('/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted')
VS=Path('/home/brunette/.hermes/cache/scratch/woodworking-tutorial/vs')
TS=PACK/'mods/toolsmith_1.2.19.zip'
BASE=PACK/'mods/KuboczochModpackGuide-1.0.2.zip'
PREFIX='assets/kuboczochmodpackguide/'
def dump(x):return json.dumps(x,ensure_ascii=False,indent=2)+'\n'
def expand(a):
 gs=a.get('variantgroups',a.get('variantGroups',[])); combos=[{}]
 for g in gs:
  states=g.get('states',[])
  if 'loadFromProperties' in g:
   prop=g['loadFromProperties']; prop=prop.split(':',1)[-1]; f=VS/'assets/survival/worldproperties'/ (prop+'.json')
   if not f.exists():f=VS/'assets/game/worldproperties'/(prop+'.json')
   if not f.exists():raise ValueError('Unsupported world property '+str(g))
   states=[v['code'] for v in json5.loads(f.read_text())['variants']]
  if not states:raise ValueError('Unsupported variant group '+str(g))
  combos=[{**c,g['code']:s} for c in combos for s in states]
 out=[]
 for c in combos:
  code=a['code']+''.join('-'+str(v) for v in c.values())
  if any(fnmatch.fnmatchcase(code,p) for p in a.get('skipVariants',a.get('skipvariants',[]))):continue
  allowed=a.get('allowedVariants',a.get('allowedvariants'))
  if allowed and not any(fnmatch.fnmatchcase(code,p) for p in allowed):continue
  out.append(code)
 return out

def inventory():
 z=zipfile.ZipFile(TS);native=[];parts=[];definitions=[]
 for n in z.namelist():
  if n.endswith('.json') and re.match(r'assets/toolsmith/(itemtypes|blocktypes)/',n):
   a=json5.loads(z.read(n));kind='block' if '/blocktypes/' in n else 'item';definitions.append((n,a))
   for c in expand(a):native.append({'code':'toolsmith:'+c,'type':kind,'source':n,'hidden':c=='tinkertoolparts'})
 # Source inventory applies the active compatibility patches before expansion.
 attrs=json.loads((W/'attribute_inventory.json').read_text())
 native=[{'code':r['code'],'type':r['kind'],'source':r['source'],'hidden':r['internal']} for r in attrs['codes']]
 for n in z.namelist():
  if '/config/toolsmith/parts/' not in n or not n.endswith('.json') or '/compatibility/' in n:continue
  category=n.split('/parts/')[1].split('/')[0]; branch=n.rsplit('-',1)[-1].removesuffix('.json')
  for r in json5.loads(z.read(n)):parts.append({'category':category,'branch':branch,'id':r['id'],'source':n,'record':r,'resolved':[]})
 # Restrict resolution to enabled pack's actual JSON definitions, retaining every unresolved entry as evidence.
 targets={r['id'] for r in parts}; candidates=[]
 def consider(n,raw,origin):
  if not re.match(r'assets/[^/]+/(itemtypes|blocktypes)/.*\.json$',n):return
  try:a=json5.loads(raw)
  except Exception:return
  if not isinstance(a,dict) or not isinstance(a.get('code'),str):return
  prefix=a['code']; wanted={t for t in targets if t==prefix or t.startswith(prefix+'-')}
  if not wanted:return
  try:codes=expand(a)
  except Exception as e:
   candidates.append({'source':origin+'!'+n,'error':str(e)});return
  domain=n.split('/')[1];domain='game' if origin=='VS1.22.7' else domain;kind='block' if '/blocktypes/' in n else 'item'
  for c in codes:
   if c in wanted:candidates.append({'id':c,'code':domain+':'+c,'type':kind,'source':origin+'!'+n})
 for p in sorted(VS.glob('assets/*/itemtypes/**/*.json')):consider(p.relative_to(VS).as_posix(),p.read_bytes(),'VS1.22.7')
 for p in sorted(VS.glob('assets/*/blocktypes/**/*.json')):consider(p.relative_to(VS).as_posix(),p.read_bytes(),'VS1.22.7')
 manifest=json.loads((PACK/'evpack.json').read_text())
 for m in manifest['Mods']:
  if not m['Enabled']:continue
  with zipfile.ZipFile(PACK/'mods'/m['FileName']) as q:
   for n in q.namelist():
    if n.endswith('.json') and ('/itemtypes/' in n or '/blocktypes/' in n):
     # cheap code prefilter avoids parsing irrelevant thousands of assets
     raw=q.read(n)
     if any(('"'+t.split('-')[0]).encode() in raw for t in targets):consider(n,raw,m['FileName'])
 for r in parts:
  r['resolved']=[{k:v for k,v in c.items() if k!='id'} for c in candidates if c.get('id')==r['id']]
  for c in native:
   if c['code']=='toolsmith:'+r['id'] and not any(x['code']==c['code'] for x in r['resolved']):r['resolved'].append({'code':c['code'],'type':c['type'],'source':'toolsmith_1.2.19.zip!'+c['source']})
 result={'native':native,'parts':parts,'resolutionErrors':[c for c in candidates if 'error' in c], 'counts':{'nativeItems':sum(r['type']=='item' for r in native),'nativeBlocks':sum(r['type']=='block' for r in native),'partRecords':len(parts),'resolvedPartRecords':sum(bool(r['resolved']) for r in parts)},'source':{'pack':'Pszczoly 1.2.0','created':manifest['Created'],'game':'1.22.7','toolsmith':'1.2.19','toolsmithSha256':hashlib.sha256(TS.read_bytes()).hexdigest()},'ordering':'Handles: recipe prerequisites. Others: config record order within named material branch; alternatives are not an enforced total order.'}
 (W/'inventory.json').write_text(dump(result));return result

def link(code,label=None,kind='item',destination=None):
 path=code.removeprefix('game:');label=label or {'toolsmith:crudehandle':'Crude handle','toolsmith:handle':'Carved handle','toolsmith:carpentedhandle':'Professional handle','toolsmith:fabricstrips-cloth':'Cloth strips','toolsmith:fabricstrips-wool':'Wool strips','toolsmith:fabricstrips-leather':'Leather strips','toolsmith:fabricstrips-sturdyleather':'Sturdy leather strips'}.get(code,path.split(':')[-1].replace('-',' ').title())
 native=next((r for r in json.loads((W/'attribute_inventory.json').read_text())['codes'] if r['code']==code),None)
 if native:
  d=native['navigationDestination']
  if d is None:return html.escape(label)
  icon='' if d['attributes'] else f'<itemstack type="{kind}" code="{code}" rsize="0.9"/> '
  return icon+f"<a href='{d['href']}'>{html.escape(label)}</a>"
 return f'<itemstack type="{kind}" code="{code}" rsize="0.9"/> <a href="handbook://{kind}-{path}">{html.escape(label)}</a>'
def page(key,title,text):return (PREFIX+'config/handbook/'+key+'.json',dump({'pageCode':'kuboczochmodpackguide-'+key,'title':'kuboczochmodpackguide:'+key+'-title','text':'kuboczochmodpackguide:'+key+'-text','categoryCode':'kuboczochmodpackguide-index'}).encode())
def build():
 i=inventory();z=zipfile.ZipFile(BASE);files={n:z.read(n) for n in z.namelist() if not n.endswith('/') and 'mod-immersivemining.json' not in n}
 lang=clean(rewrite(json.loads(files[PREFIX+'lang/en.json'])))
 pages={}
 root=['Choose a component group, then open an item name for recipes. Wood species, fabric colours and equal metal tiers are alternatives, not a single upgrade path.','<strong>Components</strong>']
 for cat,title in [('handles','Handles'),('grips','Grips'),('bindings','Bindings'),('treatments','Treatments'),('maintenance','Maintenance & stations')]:
  key='toolsmith-items-'+cat;root.append(f'<a href="handbook://kuboczochmodpackguide-{key}">{title}</a>')
  body=['<a href="handbook://kuboczochmodpackguide-toolsmith-items">All Toolsmith items</a>']
  if cat in ['handles','grips','bindings','treatments']:
   intro={'handles':'Start with a stick or bone; craft crude, carved, then professional handles as your workshop grows. Stick: no grip or treatment. Bone and crude: grips only. Carved and professional: grips and treatments. Professional handles take twice as long to dry.', 'grips':'Choose twine, cloth, leather or sturdy leather. Cloth patterns and colours are alternatives. A grip is optional; not every handle accepts one.', 'bindings':'Start with grass or reeds. Twine, hide, rope, cloth, leather, glue and metal fasteners are different material branches. Bronze alloys are alternatives; later metals require their own workshop progression.', 'treatments':'Carved and professional handles accept treatments. Fat and wax need 12 hours; oils need 24 hours before the handle drying multiplier. Oils are alternatives, not a mandatory upgrade over wax.'}[cat];body.append(intro)
   rows=[r for r in i['parts'] if r['category']==cat]
   rows.sort(key=lambda r:(r['branch']!='vanilla',r['branch']))
   if cat=='handles':
    order={'stick':0,'bone':1,'crudehandle':2,'handle':3,'carpentedhandle':4};rows.sort(key=lambda r:order.get(r['id'],99))
   for branch in dict.fromkeys(r['branch'] for r in rows):
    rs=[r for r in rows if r['branch']==branch and r['resolved']]
    if not rs:continue
    body.append('<strong>'+('Core materials' if branch=='vanilla' else branch.title()+' materials')+'</strong>')
    metals={v['code']:v['tier'] for v in json5.loads((VS/'assets/survival/worldproperties/block/metal.json').read_text())['variants']}
    def section(r):
     ident=r['id'];metal=ident.rsplit('-',1)[-1]
     if cat=='handles':return (0,'Starter → crude → carved → professional')
     if metal in metals and cat=='bindings':return (10+metals[metal],'Metal fasteners — material tier '+str(metals[metal]))
     if cat=='treatments':return (1,'Fat & wax') if any(x in ident for x in ['fat','wax']) else (2,'Oils — alternatives')
     if 'sturdy' in ident:return (4,'Sturdy leather')
     if 'rawhide' in ident:return (1,'Plant fibres, rope & hide')
     if 'leather' in ident:return (3,'Leather')
     if any(x in ident for x in ['cloth','wool','fabric']):return (2,'Fabric — alternative colours and materials')
     if 'glue' in ident:return (5,'Adhesives')
     return (1,'Plant fibres, rope & hide')
    sections=sorted(set(section(r) for r in rs))
    for sec in sections:
     body.append('<strong>'+sec[1]+'</strong>')
     entries=[]
     for r in rs:
      if section(r)!=sec:continue
      for c in r['resolved']:entries.append(link(c['code'],kind=c['type']))
     body.append(' · '.join(entries))
   if cat=='handles':body.append('Carved: knife + support beam. Professional: add sandpaper. Wood species change the appearance; they are not separate recipe tiers.')
  else:
   body.append('Portable honing first; add a grindstone and workbench for a permanent workshop. Whetstone rocks are alternatives.')
   # Native grouped pages use first-stack representative; rotations are one placed block.
   for r in i['native']:
    if any(s in r['code'] for s in ['whetstone','sandpaper','grindstone','workbench']):
     if r['type']=='block' and not r['code'].endswith('-north'):continue
     dest=None
     if 'whetstone-' in r['code']:dest='item-toolsmith:whetstone-chert'
     if 'grindstone-' in r['code']:dest='block-toolsmith:grindstone-granite-north'
     body.append(link(r['code'],kind=r['type'],destination=dest))
   body.append('<strong>Recovered parts</strong>\nRecovered tool parts are an internal intermediate, not a craftable handle tier.\n<a href="handbook://craftinginfo-toolsmithreforging">Reforging recovered parts</a>')
   body.append('North, east, south and west are placement directions of the same station; each station link opens its north-facing recipe page.')
  if cat in ['handles','bindings']:
   groups=json.loads((W/'attribute_inventory.json').read_text())['codes']
   groups.sort(key=lambda r:{'toolsmith:crudehandle':0,'toolsmith:handle':1,'toolsmith:carpentedhandle':2}.get(r['code'],3))
   for g in groups:
    want=('handle' in g['code']) if cat=='handles' else ('fabricstrips' in g['code'])
    if not want or len(g['stacks'])<=1:continue
    title={'toolsmith:handle':'Carved handles','toolsmith:carpentedhandle':'Professional handles','toolsmith:crudehandle':'Crude handle'}.get(g['code'],g['code'].split(':')[1].replace('fabricstrips-','').title()+' strips')
    body.append('<strong>'+title+' — alternatives</strong>')
    links=[]
    for s in g['stacks']:
     tex=s['attributes'].get('modularPartRenderData',{}).get('partTextures',{})
     suffix=next(iter(tex.values()),'Default').split('/')[-1];label=suffix.removeprefix('tool').replace('baldcypress','bald cypress').replace('darkblue','dark blue').replace('darkbrown','dark brown').replace('darkgreen','dark green').replace('darkred','dark red').title()
     links.append(f"<a href='{s['href']}'>{label}</a>")
    body.append(' · '.join(links))
  pages[key]=(title if cat=='maintenance' else dict(handles='Handles',grips='Grips',bindings='Bindings',treatments='Treatments')[cat],'<br><br>'.join(body))
 root+=['<strong>Learn the workflow</strong>','<a href="handbook://craftinginfo-toolsmithingbasics">Toolsmith basics</a>','Use the workbench for tool assembly in this pack; grid tool assembly is disabled. Component recipes are still available.']
 pages['toolsmith-items']=('Pszczoly — Toolsmith Items','<br><br>'.join(root))
 for k,(title,text) in pages.items():
  lang[k+'-title']=title;lang[k+'-text']=text;f,b=page(k,title,text);files[f]=b
 index_anchor='<a href="handbook://kuboczochmodpackguide-toolsmith-items">Toolsmith Items — components and workshop</a>'
 lang['root-text']+='<br><br><strong>Item indexes</strong><br>'+index_anchor
 lang['mod-toolsmith-text']=index_anchor+'<br><br>'+lang['mod-toolsmith-text']
 # Last-pass dedupe handles grouped native-page aliases without repeating destinations.
 from pack_audit import dedupe_hrefs
 lang={k:dedupe_hrefs(v) for k,v in lang.items()}
 files[PREFIX+'lang/en.json']=dump(lang).encode();m=json.loads(files['modinfo.json']);m.update(version='1.1.0',description='Player handbook and Toolsmith component index for Pszczoly 1.2.0.');files['modinfo.json']=dump(m).encode()
 out=W/'dist';out.mkdir(exist_ok=True)
 with zipfile.ZipFile(out/'KuboczochModpackGuide-1.1.0.zip','w',compression=zipfile.ZIP_DEFLATED,compresslevel=9) as q:
  for n,b in sorted(files.items()):info=zipfile.ZipInfo(n,(2026,11,16,0,0,0));info.compress_type=zipfile.ZIP_DEFLATED;info.external_attr=0o644<<16;q.writestr(info,b)
 print(dump(i['counts']));print('Built',out/'KuboczochModpackGuide-1.1.0.zip')
if __name__=='__main__':build()
