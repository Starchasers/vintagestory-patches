import pathlib, zipfile, json, json5, itertools, hashlib, collections
ROOT=pathlib.Path('/home/brunette/.hermes/game-context/vintage-story/1571480359365771304/prizC/extracted')
OUT=pathlib.Path('/home/brunette/.hermes/cache/scratch/toolsmith-handbook-index')
archive=ROOT/'mods/toolsmith_1.2.19.zip'
z=zipfile.ZipFile(archive)
assets={}; errors={}
for n in z.namelist():
    if n.endswith('.json') and not any(x in n for x in ['/shapes/','/lang/']):
        try: assets[n]=json5.loads(z.read(n).decode('utf-8-sig'))
        except Exception as e:errors[n]=str(e)
lang=json5.loads(z.read('assets/toolsmith/lang/en.json').decode('utf-8-sig'))
configs={str(ROOT/'config/ModConfig'/n):json.loads((ROOT/'config/ModConfig'/n).read_text()) for n in ['Toolsmith.json','ToolsmithClient.json','ToolsmithPartsStats.json']}
installed={}
for p in (ROOT/'mods').glob('*.zip'):
    try:
        with zipfile.ZipFile(p) as a:
            m=json5.loads(a.read('modinfo.json').decode('utf-8-sig'));installed[m['modid']]={'archive':str(p),'version':m.get('version')}
    except Exception:pass
from attribute_inventory import effective_assets, generate
active_assets, applied_native_patches = effective_assets(assets, installed)
inventory=[]
for n,a in active_assets.items():
    kind='item' if n.startswith('assets/toolsmith/itemtypes/') else 'block' if n.startswith('assets/toolsmith/blocktypes/') else None
    if not kind:continue
    groups=[]
    for g in a.get('variantgroups',[]):
        groups.append({'group':g['code'],'states':g.get('states',['north','east','south','west'] if g.get('loadFromProperties')=='abstract/horizontalorientation' else []),'source':g})
    codes=['toolsmith:'+a['code']+(';' if False else '')+('~' if False else '')+('' if not v else '-'+ '-'.join(v)) for v in itertools.product(*(g['states'] for g in groups))]
    render=[]
    for b in a.get('behaviors',[]):
        if 'ModularPartRendering' in b['name']:render.append(b.get('properties',{}))
    for typ,bs in a.get('behaviorsByType',{}).items():
        for b in bs:
            if 'ModularPartRendering' in b['name']:render.append({'typePattern':typ,**b.get('properties',{})})
    inventory.append({'source':n,'kind':kind,'baseCode':'toolsmith:'+a['code'],'codeVariants':codes,'variantGroups':groups,'attributeRenderExpansion':render,'creativeInventory':a.get('creativeinventory',{}),'handbook':a.get('attributes',{}).get('handbook',{}),'tags':a.get('tags',[])})
compat={}
for n,a in assets.items():
    if '/compatibility/' in n:
        mod=n.split('/compatibility/')[1].split('/')[0]
        compat.setdefault(mod,{'installedExactModId':mod in installed,'sources':[]})['sources'].append(n)
recipes={n:a for n,a in assets.items() if '/recipes/' in n}
report={'archive':str(archive),'sha256':hashlib.sha256(archive.read_bytes()).hexdigest(),'scope':'Static JSON assets/configs only; DLL never executed. Conditional folders are candidate integrations, not proof of runtime loader behavior. Installed ZIP metadata is not proof a mod is enabled.','nativeInventory':inventory,'compatibilityFolders':compat,'installedZipMetadata':installed,'configs':configs,'sourceAssets':assets,'englishLanguage':lang,'parseErrors':errors}
OUT.mkdir(parents=True,exist_ok=True)
attribute_report=generate(active_assets, inventory, applied_native_patches)
(OUT/'attribute_inventory.json').write_text(json.dumps(attribute_report, indent=2, ensure_ascii=False)+'\n')
report['attributeInventory']={'file':'attribute_inventory.json','counts':attribute_report['counts'],'appliedNativeCompatibilityPatches':applied_native_patches}
(OUT/'research-inventory.json').write_text(json.dumps(report,indent=2,ensure_ascii=False)+'\n')
lines=['# Toolsmith 1.2.19 static inventory and progression research','',f'Archive: `{archive}`',f'SHA-256: `{report["sha256"]}`','',report['scope'],'','## Native collectible codes and exact expansion','', 'Only itemtypes/blocktypes create native collectible codes. Shape files, language names, and tool classification records are NOT new item IDs. Attribute wood/color/treatment/grip states are stack attributes, not hyphenated code variants.','']
for i in inventory:
    lines += [f'### {i["baseCode"]} ({i["kind"]})',f'Source: `{i["source"]}`',f'Code variants ({len(i["codeVariants"])}): '+', '.join('`'+x+'`' for x in i['codeVariants']),f'Handbook grouping: `{json.dumps(i["handbook"])}`',f'Tags: `{json.dumps(i["tags"])}`']
    for r in i['attributeRenderExpansion']:
        for t in r.get('textures',[]):lines += [f'Attribute texture `{t["code"]}`, pattern `{r.get("typePattern","*")}`, {len(t.get("values",[]))} values: '+', '.join('`'+x+'`' for x in t.get('values',[]))]
    lines += ['']
counts=collections.Counter()
for i in inventory:counts[i['kind']]+=len(i['codeVariants'])
lines += ['## Counts','',f'Baseline native codes: {counts["item"]} items, {counts["block"]} orientation-expanded blocks; {len(inventory)} source definitions. Blocks include four workbench orientations and four grindstone stones times four orientations. Creative entries expose only north-facing station blocks. `tinkertoolparts` is a hidden intermediate, not a final tool.','', '## Progression and configuration caveats','', '- Starter: stick/bone handles and grass/reeds bindings; knife + firewood -> crudehandle. Knife + supportbeam -> handle (Carved Tool Handle); knife + supportbeam + sandpaper -> carpentedhandle (Professional Tool Handle). Exact recipes below.', '- Treatments/grips are optional handle attributes: stick cannot have either; bone/crude can have grip but not treatment; carved/professional accept both. Professional drying multiplier 2 versus 1 for others. Source `assets/toolsmith/config/toolsmith/parts/handles/handles-vanilla.json`.', '- Bindings progress through reeds, twine/rawhide/rope/cloth/leather/sturdy leather/glue and metal nails/parts. They are not a simple enforced tech tree. Exact records and stats below.', '- Knapped whetstones provide early portable honing (durability 300). Grindstone is a permanent workshop block requiring rock and planks; workbench needs planks and saw, supports assembly/deconstruction/reforge preparation per bundled handbook.', '- Actual server config has `EnableGridRecipesForToolCrafting=false`, so do not teach generated grid TOOL ASSEMBLY as currently enabled. Component grid recipes are separate assets. `HeadDurabilityMult=5`, `SharpnessMult=1.5`, `SharpWear=0.15`, `BluntWear=0.02`, `UseBitsForSmithing=true`.', '- `ToolsmithPartsStats.json` has `EnableEdits=false`: persisted values are configuration evidence, not independently proven overrides. `EnableEditsForRegex=false` likewise means regex lists are not a verified editable override. Persisted regex differs from archive: includes barkspud/pitsaw/maul and custom blacklist sentinel `__armoryfork_toolsmith_allow__`; do not discard or silently repair it.', '- Handbook says Drawbacks are not implemented in basics but elsewhere describes them as guaranteed. Treat that as inconsistent bundled documentation, not verified runtime behavior.', '- `treatments-vanilla.json` actually includes oilportion-flax and oilportion-olive; handbook claim oil only via cross-compatibility is stale.', '- Copper cold-working patch also sets tin/lead metalplate workable temperature to zero; see `assets/toolsmith/patches/coldforgemetals.json`. Supportbeam output patch sets 4, with an indappledgroves-dependent disabling patch under `assets/toolsmith/patches/compatability/in-dappled-groves-disable-supportbeam-patch.json`.','', '## Conditional compatibility folders','']
for mod,d in sorted(compat.items()):
    lines += [f'### {mod}: exact installed ZIP mod ID present = {d["installedExactModId"]}']
    for n in d['sources']:
        lines += [f'`{n}`','```json',json.dumps(assets[n],ensure_ascii=False,indent=2),'```']
lines += ['## Exact attributed native destinations', '', 'See `attribute_inventory.json` for every defined concrete code, full creative attribute combinations, exact recursively sorted TreeAttribute page strings, and valid representative links. Active wool and Tailor’s Delight native patches are applied before expansion.', '', json.dumps(attribute_report['counts']), '', attribute_report['linkStrategy'], '', 'Internal `toolsmith:tinkertoolparts` is defined but has no native creative/handbook page; use a custom explanation destination. Non-north block orientations navigate to the corresponding north creative page.', '']
lines += ['## Exact recipes (native and conditional)','']
for n,a in recipes.items():lines += [f'### `{n}`','```json',json.dumps(a,ensure_ascii=False,indent=2),'```']
lines += ['## Part definitions, stat tables, classification, patches','']
for n,a in assets.items():
    if ('/config/toolsmith/' in n or '/patches/' in n) and '/compatibility/' not in n:
        lines += [f'### `{n}`','```json',json.dumps(a,ensure_ascii=False,indent=2),'```']
lines += ['## Exact supplied configs','']
for n,a in configs.items():lines += [f'### `{n}`','```json',json.dumps(a,ensure_ascii=False,indent=2),'```']
lines += ['## Bundled English handbook evidence','Source: `assets/toolsmith/lang/en.json`; page definitions: `assets/toolsmith/config/handbook/*.json`.','']
for k,v in lang.items():
    if 'craftinginfo' in k:lines += [f'### `{k}`',v,'']
(OUT/'research-inventory.md').write_text('\n'.join(lines)+'\n')
print(json.dumps({'counts':dict(counts),'definitions':len(inventory),'parsed_assets':len(assets),'parse_errors':errors,'conditional_folders':{m:d['installedExactModId'] for m,d in compat.items()},'attribute_counts':attribute_report['counts'],'artifacts':[str(OUT/'research-inventory.json'),str(OUT/'research-inventory.md'),str(OUT/'attribute_inventory.json')]},indent=2))
