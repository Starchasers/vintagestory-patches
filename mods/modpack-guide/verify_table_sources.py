"""Independent read-only completeness/stat/registration audit of authored table inventory.
Consumes enabled pack JSON and trusted base assets only; never loads a target assembly.
"""
import hashlib,itertools,json,zipfile,json5
from pathlib import Path
from build import PACK,VS
W=Path(__file__).parent
inv=json.loads((W/'table_inventory.json').read_text())
mods={}; sources={}
for m in json.loads((PACK/'evpack.json').read_text())['Mods']:
 if not m['Enabled']:continue
 z=zipfile.ZipFile(PACK/'mods'/m['FileName']);mods[m['FileName']]=z
 info=json5.loads(z.read('modinfo.json').decode('utf-8-sig'))
 sources[str(info.get('modid',info.get('ModID',''))).lower()]=m['FileName']
assert hashlib.sha256((PACK/'mods/toolsmith_1.2.19.zip').read_bytes()).hexdigest()==inv['toolsmithArchiveSha256']
def active(name):
 parts=name.split('/')
 return '/compatibility/' not in name or parts[parts.index('compatibility')+1] in sources
def read(source):
 archive,name=source.split('!',1)
 if archive=='VS1.22.7':return json5.loads((VS/name).read_text())
 return json5.loads(mods[archive].read(name).decode('utf-8-sig'))
# Re-enumerate every active Toolsmith parts file, not just files named by the fixture.
actual={};ts=mods['toolsmith_1.2.19.zip']
for name in ts.namelist():
 if '/config/toolsmith/parts/' not in name or not name.endswith('.json') or not active(name):continue
 category=name.split('/parts/')[1].split('/')[0]
 if category not in inv['groups']:continue
 for value in json5.loads(ts.read(name).decode('utf-8-sig')):
  key=category,value['id'];assert key not in actual,key
  actual[key]=(value,'toolsmith_1.2.19.zip!'+name)
expected={(x['category'],x['id']):x for x in inv['options']}
assert actual.keys()==expected.keys(),(actual.keys()-expected.keys(),expected.keys()-actual.keys())
for key,(mapping,source) in actual.items():
 x=expected[key];assert x['mapping']==mapping and x['mappingSource']==source,key
 stat=next(s for s in read(x['statsSource']) if s['id']==x['statTag'])
 assert stat==x['stats'],key
# Verify inactive records and real stat definitions, including neutral baselines.
for category,stats in inv['stats'].items():
 for statid,e in stats.items():assert e['values']==next(s for s in read(e['source']) if s['id']==statid)
for name in inv['inactiveMappingSources']:assert not active(name),name
# Resolve declared variants using their source JSON/worldproperties, rather than trusting code strings.
worldprops={}
for p in VS.glob('assets/*/worldproperties/**/*.json'):
 raw=json5.loads(p.read_text());worldprops[p.relative_to(VS).as_posix().split('/worldproperties/')[1][:-5]]=[s['code'] for s in raw.get('variants',[]) if isinstance(s,dict) and 'code' in s]
def expanded(source):
 raw=read(source);archive,name=source.split('!',1);domain='game' if archive=='VS1.22.7' else name.split('/')[1]
 base=raw['code'];domain,base=(base.split(':',1) if ':' in base else (domain,base))
 groups=[]
 for vg in raw.get('variantgroups',[]):
  if 'states' in vg:states=vg['states']
  elif 'loadFromProperties' in vg:states=worldprops[vg['loadFromProperties'].split(':')[-1]]
  else:raise AssertionError(('Unhandled variant declaration',source,vg))
  # The installed Wool patch appends exactly this Toolsmith variant.
  if source.endswith('!assets/toolsmith/itemtypes/fabricstrips.json') and vg.get('code')=='material':
   assert 'wool' in sources
   states=list(dict.fromkeys(states+['wool']))
  groups.append(states)
 return {domain+':'+base+('-'+'-'.join(values) if values else '') for values in itertools.product(*groups)}
resolved=0;assets={}
for x in inv['options']:
 for c in x['resolved']:
  codes=assets.setdefault(c['source'],expanded(c['source']))
  assert c['code'] in codes,(x['id'],c)
  assert c['code'].split(':',1)[1]==x['id'],(x['id'],c['code'])
  assert c['kind']==('block' if '/blocktypes/' in c['source'] else 'item')
  resolved+=1
assert not inv['resolutionErrors']
for entry in inv['maintenance']:
 assert sorted(entry['codes'])==sorted(expanded(entry['source']))
 raw=read(entry['source'])
 if entry['durability'] is not None:assert raw['durability']==entry['durability']
tagged=[]
for name in ts.namelist():
 if not name.endswith('.json') or not ('/itemtypes/' in name or '/blocktypes/' in name) or not active(name):continue
 raw=json5.loads(ts.read(name).decode('utf-8-sig'))
 if 'toolsmith-maintenance' in raw.get('tags',[]):tagged.append('toolsmith_1.2.19.zip!'+name)
assert set(tagged).issubset({x['source'] for x in inv['maintenance']})
# Independently search every enabled collectible declaration for ALL mapping IDs,
# including records the fixture reports absent. This catches silently omitted options.
allids={x['id'] for x in inv['options']};discovered={i:set() for i in allids}
candidates=[]
for p in VS.glob('assets/**/*.json'):
 name=p.relative_to(VS).as_posix()
 if '/itemtypes/' in name or '/blocktypes/' in name:candidates.append('VS1.22.7!'+name)
for archive,z in mods.items():
 for name in z.namelist():
  if name.endswith('.json') and ('/itemtypes/' in name or '/blocktypes/' in name) and active(name):candidates.append(archive+'!'+name)
for source in candidates:
 raw=read(source)
 if not isinstance(raw,dict) or not isinstance(raw.get('code'),str):continue
 base=raw['code'].split(':')[-1]
 wanted=[i for i in allids if i==base or i.startswith(base+'-')]
 if not wanted:continue
 for code in expanded(source):
  path=code.split(':',1)[1]
  if path in discovered:discovered[path].add(code)
for x in inv['options']:
 assert discovered[x['id']]=={c['code'] for c in x['resolved']},('resolution mismatch',x['id'],discovered[x['id']],x['resolved'])
# Family rows are exact partitions; mechanical equality is asserted, not inferred from colour.
from collections import Counter
for category,groups in inv['groups'].items():
 wanted=[x['id'] for x in inv['options'] if x['category']==category and x['resolved']]
 covered=[i for g in groups for i in g['optionIds']]
 assert Counter(wanted)==Counter(covered)
 for g in groups:
  records=[expected[category,i] for i in g['optionIds']]
  assert all(r['stats']==records[0]['stats'] for r in records)
  assert sorted(g['codes'])==sorted(c['code'] for r in records for c in r['resolved'])
print(f'SOURCE AUDIT PASS: {len(actual)} active mapping records; {sum(bool(x["resolved"]) for x in inv["options"])} resolved options / {resolved} registered collectible matches; {len(assets)} independently expanded source assets; all rows exact equal-stat partitions.')
