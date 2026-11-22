import pathlib,json,zipfile,io,re,collections,json5,csv
import argparse
parser=argparse.ArgumentParser(description="Bounded source reference audit; requires json5 (pip install json5). Never loads upstream assemblies.")
parser.add_argument('--inventory',type=pathlib.Path,required=True,help='Saved exact-pack native_guides_all_providers.json source inventory')
parser.add_argument('--evpack',type=pathlib.Path,required=True)
parser.add_argument('--game-assets',type=pathlib.Path,required=True,help='Exact-version assets directory containing game/lang')
parser.add_argument('--output',type=pathlib.Path,default=pathlib.Path(__file__).resolve().parents[1]/'docs/missing-reference-ledger.json')
args=parser.parse_args()
A=json.loads(args.inventory.read_text())
Z=zipfile.ZipFile(args.evpack)
langs={x:collections.defaultdict(list) for x in ['en','pl']}; errors=[]; installed=set()
def addlang(domain,locale,data,source):
 for k,v in data.items():
  key=k[5:] if k.startswith('game:') else (k if ':' in k or domain=='game' else domain+':'+k)
  langs[locale][key].append({'value':v,'source':source,'literal_key':k})
for l in langs:
 f=args.game_assets/f'game/lang/{l}.json';addlang('game',l,json5.loads(f.read_text()),str(f))
for n in Z.namelist():
 if not n.endswith('.zip'):continue
 z=zipfile.ZipFile(io.BytesIO(Z.read(n)))
 for f in z.namelist():
  if f.lower()=='modinfo.json':
   try:installed.add(json5.loads(z.read(f).decode('utf-8-sig'))['modid'])
   except Exception:pass
  m=re.fullmatch(r'assets/([^/]+)/lang/(en|pl)\.json',f)
  if m:
   try:addlang(m[1],m[2],json5.loads(z.read(f).decode('utf-8-sig')),n+'!'+f)
   except Exception as e:errors.append({'source':n+'!'+f,'error':str(e)})
supported={'expandedfoods','aculinaryartillery','efmealsmodule','efchefstricks'}
rows=[]
for r in A['recipe_inventory']:
 if r['mod_id'] not in supported:continue
 if r['recipe_family'] not in ['cooking','mixing','meals']:continue
 defs=r['definition'];defs=defs if isinstance(defs,list) else [defs]
 for idx,d in enumerate(defs):
  if not isinstance(d,dict):continue
  code=d.get('code',d.get('Code')); cooks=d.get('cooksInto',d.get('CooksInto'))
  row={'mod_id':r['mod_id'],'source':r['source'],'definition_index':idx,'family':r['recipe_family'],'literal_code':code,'conditions':r.get('conditions',[]),'cooksInto':cooks,'runtime_registration':'unverified; asset patches/config/ingredient resolution may change registration'}
  row['route_status']='item output; no assumed meal page' if cooks else 'source candidate only; page registration not runtime verified'
  row['pageCode']=None if cooks or not code else 'handbook-mealrecipe-'+code
  row['expected_title_key']=None if cooks or not code else 'mealrecipe-name-'+code
  row['translations']={}
  if row['expected_title_key']:
   key=row['expected_title_key']
   for loc in langs:
    matches=langs[loc].get(key,[]); fallback=langs['en'].get(key,[])
    row['translations'][loc]={'status':('present' if matches else 'english_fallback_candidate' if loc=='pl' and fallback else 'missing_raw_label'),'candidates':matches,'fallback_candidates':fallback if not matches and loc=='pl' else [],'effective_value':matches[0]['value'] if len(matches)==1 else None,'ambiguity':'multiple candidates require load-order verification' if len(matches)>1 else None}
   row['unused_alternative_spellings']={prefix+code:{l:langs[l].get(prefix+code,[]) for l in langs} for prefix in ['meal-recipe-','meat-recipe-']}
  rows.append(row)
counts={'source_definitions':len(rows),'unique_meal_codes':len({r['literal_code'] for r in rows if r['expected_title_key']}),'item_output_definitions':sum(bool(r['cooksInto']) for r in rows),'unresolved_runtime_registrations':len(rows),'translations':{l:dict(collections.Counter(r['translations'][l]['status'] for r in rows if r['translations'])) for l in langs}}
result={'scope':'Complete only for saved supported cooking/mixing source definitions, NOT runtime registration, all item routing or arbitrary generated meal IDs. No invented fallback translations.','counts':counts,'parse_errors':errors,'rows':rows,'guide_pageCodes':[{'pageCode':g['pageCode'],'source':g['source'],'title_key':g['title_key']} for g in A['guides'] if g['mod_id'] in supported]}
args.output.parent.mkdir(parents=True,exist_ok=True)
args.output.write_text(json.dumps(result,ensure_ascii=False,indent=2))
print(json.dumps(counts));print('PARSE_ERRORS',errors);print('MISSING',[(r['literal_code'],{l:t['status'] for l,t in r['translations'].items()}) for r in rows if any(t['status']!='present' for t in r['translations'].values())]); print('COMPOTE',json.dumps([r for r in rows if r['literal_code']=='compote'],ensure_ascii=False));print('LANGCOUNT',{l:len(v) for l,v in langs.items()})
