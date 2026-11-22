"""Saved inventory -> source policy evidence; never executes upstream code."""
import json,re
from pathlib import Path
from collections import Counter
ROOT=Path(__file__).resolve().parents[1]
REVIEW=ROOT.parent/'food-handbook-refinement-review/RECIPE-REVIEW.json'
INV=ROOT.parent/'food-handbook-audit/recipes.jsonl'
review=json.loads(REVIEW.read_text()); inventory=[json.loads(s) for s in INV.read_text().splitlines()]
rows=[]; grid=set(); barrels=set()
for s in review['recipe_source_mappings']:
 if s['mod_id']!='expandedfoods':continue
 if s['source_kind']!='recipe_declaration' or s['effective_family'] not in ('grid','barrel'):continue
 d=inventory[s['inventory_line']-1]['definition'];family=s['effective_family'];out=d.get('output',{}).get('code','');domain=s['source_path'].split('/')[1]
 full=out if ':' in out else domain+':'+out
 section='preparing'
 if family=='barrel':
  name=Path(s['source_path']).stem
  section='preserving' if name in ('limeegg','pickledegg','vinegarpickledvegetable') else 'soaking' if name=='soyprep' else 'fermenting'
 excluded='equipment (nonfood brazier)' if '/braziers/' in s['source_path'] else ('unrelated-mod output outside EF/vanilla scope' if full.split(':')[0] not in ('expandedfoods','game') else None)
 row={**s,'declared_output':full,'declared_enabled':d.get('enabled',True),'native_section':None if excluded else section,'adapter':'NativeRecipeSections.Capture recipe-provenance callsite','native_evidence':'exact-handbook.cs:1753-1840 (grid), 1890-1916 (barrel)','disposition':('EXCLUDED_UNRELATED_MOD' if excluded and 'unrelated-mod' in excluded else 'EXCLUDED_NONFOOD') if excluded else 'SOURCE_POLICY_MAPPED_RUNTIME_UNVERIFIED','exclusion':excluded,'runtime_gate':'exact recipe.Name domain+path and declared output pattern; RecipeLoader.cs122-125 assigns unnamed declarations assetLocation; unknown identity retains native','runtime_verified':False}
 rows.append(row)
 identity=d.get('name',s['source_path'].removeprefix('assets/'+domain+'/'))
 if ':' in identity: namedomain,identity=identity.split(':',1)
 else:namedomain=domain
 row.update(runtime_name_domain=namedomain,runtime_name_path=identity,sample_output=re.sub(r'\{[^}]+\}|\*','testvariant',full))
 if excluded:continue
 if family=='grid':grid.add((namedomain,identity,full))
 else:barrels.add((namedomain,identity,full,section))
def pattern(s):return '^'+re.sub(r'\\\{[^}]+\\\}', '[^:]+',re.escape(s)).replace(r'\*','[^:]*')+'$'
cs='''// Generated from saved EF declarations by tools/extend_native_ledger.py; source metadata, not runtime certification.
using System.Text.RegularExpressions;
namespace Kuboczoch.FoodHandbook;
internal static class NativeProcessPolicy {
 internal const string KnownNativeIL="E2ADD5DAADB951752CBFE9280897F4435734707CF612BEE8CFCD7BA053A46207";
 internal static bool AcceptsNativeIL(string hash)=>hash==KnownNativeIL;
 internal static string? Classify(string family,string? domain,string? name,string? output) {
  if(output==null) return null;
  if(family=="grid") return Grid.Any(p=>p.Domain==domain && p.Name==name && Regex.IsMatch(output,p.Pattern))?"preparing":null;
  if(family!="barrel") return null;
  return Barrel.FirstOrDefault(p=>p.Domain==domain && name==p.Name && Regex.IsMatch(output,p.Pattern)).Section;
 }
 private static readonly (string Domain,string Name,string Pattern)[] Grid={
'''+',\n'.join('  ("'+d+'","'+n+'",@"'+pattern(p)+'")' for d,n,p in sorted(grid))+'''
 };
 private static readonly (string Domain,string Name,string Pattern,string Section)[] Barrel={
'''+',\n'.join('  ("'+d+'","'+n+'",@"'+pattern(p)+'","'+section+'")' for d,n,p,section in sorted(barrels))+'''
 };
}
'''
(ROOT/'src/NativeProcessPolicy.cs').write_text(cs)
attrs=[]
for s in review['attribute_source_mappings']:
 if s['mod_id'] not in ('expandedfoods','aculinaryartillery','efchefstricks'):continue
 key=s['operation_key'];section=''
 if key.startswith('grindingProps'):section='Grinding (native addProcessesIntoInfo lines1166-1187)'
 elif key in ('pressedStack','juiceableProperties','juiceablePropertiesByType'):section='Juicing (native addProcessesIntoInfo lines1189-1241)'
 elif key.startswith('distill'):section='Distillation (native addProcessesIntoInfo lines1360-1377)'
 elif key.startswith('transitionableProps'):
  def types(v):
   if isinstance(v,dict):
    if 'transitionedStack' in v:yield str(v.get('type','unknown')).lower()
    for child in v.values():yield from types(child)
   elif isinstance(v,list):
    for child in v:yield from types(child)
  kinds=sorted(set(types(s['declared_value'])))
  labels={'perish':'Spoilage info (native, not a food preparation recipe)','cure':'Curing (native EnumTransitionType heading and duration)','ripen':'Ripening (native EnumTransitionType heading and duration)','harden':'Hardening (native EnumTransitionType heading and duration; nonfood unchanged)','dry':'Drying (native)','convert':'Converting (native)','melt':'Melting (native)'}
  section='; '.join(labels.get(k,'UNRESOLVED transition '+k) for k in kinds) or 'Empty transition array: not a recipe'

 elif key.startswith('combustibleProps') or key=='smeltedStack':section='Native smelting/baking process (addProcessesIntoInfo; preserve native machinery and container requirements)'
 elif key.lower().startswith('bakingpropert'):section='Baking properties consumed by native oven/combustible smeltedStack renderer; source pointer is not an extra recipe'
 else:section='EXCLUDED visual transform metadata for Ancient Tools curing rack: declared value has transform only, no ingredient/output/time' 
 attrs.append({**s,'disposition':'EXCLUDED_VISUAL_METADATA' if section.startswith('EXCLUDED') else 'NATIVE_NAMED_PROCESS_RETAINED','native_section_evidence':section,'runtime_verified':False,'notes':'No copy to Ingredient for, no duplicate process icons; nested output pointers are not standalone recipes. Nonfood oil/wax/hide/equipment remains native.'})
patches=[s for s in review['recipe_source_mappings'] if s['mod_id']=='expandedfoods' and s['source_kind']=='compatibility_patch_operation']
ledger={'schema_version':1,'overall_acceptance':'PARTIAL_RUNTIME_UNVERIFIED','native_recipe_sources':rows,'attribute_sources':attrs,'patch_operations':[{**s,'disposition':'PATCH_EFFECT_NOT_RECIPE','notes':'Adapter reads effective live native registries, never enumerates patch operations as recipes; config/patch execution unverified.'} for s in patches],'counts':{'native_recipe_sources':len(rows),'native_dispositions':dict(Counter(s['disposition'] for s in rows)),'attribute_sources':len(attrs),'patch_operations':len(patches)},'remaining_blockers':['RecipeLoader.cs lines122-125 establishes default Name=assetLocation; client synchronization/live identities unverified.','No client runtime/visual/click verification performed.']}
basepath=ROOT/'docs/recipe-ledger.json'
base=json.loads(basepath.read_text()); byid={s['source_id']:s for s in rows}; byattr={s['source_id']:s for s in attrs}
base.update(release='0.3.0',overall_acceptance='IMPLEMENTED_SOURCE_POLICY_RUNTIME_UNVERIFIED')
for entry in base['recipes']:
 if entry['source_id'] in byid:
  new=byid[entry['source_id']]; entry.update(relocation_policy=new['disposition'],process_section=new['native_section'],native_adapter_evidence=new['native_evidence'],ingredient_for_policy='intercept only selected primary recipe contribution before native output dedup; retain returned containers and independently legitimate native relations',helper_inclusion_condition=new['runtime_gate'])
 elif entry['mod_id']=='expandedfoods' and entry['source_kind']=='compatibility_patch_operation':entry.update(relocation_policy='PATCH_EFFECT_NOT_RECIPE',ingredient_for_policy='read effective existing registry, not source patch operations')
for entry in base['attribute_operations']:
 new=byattr[entry['source_id']];entry.update(relocation_policy=new['disposition'],process_section=new['native_section_evidence'])
base['notes']=['Source policy only, zero verified runtime identities.','ACA helper relocation plus exact native grid/barrel contribution interception; unknown identities retained.','Attribute pointers overlap: native named processes retained, visual-only metadata excluded.','EF patch operations are not recipes; effective registries used.','See native-operation-ledger.json for per-source evidence, exclusions and runtime blockers.']
basepath.write_text(json.dumps(base,indent=2,ensure_ascii=False)+'\n')
(ROOT/'docs/native-operation-ledger.json').write_text(json.dumps(ledger,indent=2)+'\n')
print(json.dumps(ledger['counts'],indent=2))
