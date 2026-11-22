"""Join saved exhaustive review/inventory; never reopen/re-audit pack archives.
Usage: python tools/build_recipe_ledger.py REVIEW.json recipes.jsonl
No runtime destination or enabled state is inferred from a source declaration.
"""
import collections, hashlib, json, sys
from pathlib import Path
ROOT=Path(__file__).resolve().parents[1]
review_path, inventory_path=map(Path,sys.argv[1:])
review=json.loads(review_path.read_text())
inventory=[json.loads(line) for line in inventory_path.read_text().splitlines()]
def strings(value):
    if isinstance(value,str): yield value
    elif isinstance(value,dict):
        for k,v in value.items(): yield str(k); yield from strings(v)
    elif isinstance(value,list):
        for v in value: yield from strings(v)
rows=[]
for source in review["recipe_source_mappings"]:
    supported=source["existing_aca_registry_integration"]
    if not supported and source["mod_id"]!="expandedfoods": continue
    raw=inventory[source["inventory_line"]-1]
    definition=raw.get("definition",{})
    family=source["effective_family"]
    encoded=json.dumps(definition)
    patch=source["source_kind"]!="recipe_declaration"
    process={"simmering":"Simmering","kneading":"Kneading","mixing":"Mixing"}.get(family)
    formula=("provider-first HandbookPageCodeForStack(world, recipe.Simmering.SmeltedStack.ResolvedItemStack) ?? GuiHandbookItemStackPage.PageCodeForStack(resolvedOutput)" if family=="simmering" else
      "provider-first HandbookPageCodeForStack(world, recipe.Output.ResolvedItemStack) ?? GuiHandbookItemStackPage.PageCodeForStack(resolvedOutput)" if family=="kneading" else
      "native MealstackTextComponent callback: handbook-mealrecipe- + recipe.Code" if family=="mixing" else source["native"].get("entry_formula"))
    enabled=source.get("enabled_literal")
    cooks=definition.get("cooksInto",definition.get("CooksInto")) if isinstance(definition,dict) else None
    status=("NOT_COMPLETE_PATCH_EFFECT_UNRESOLVED" if patch else
        "SUPPORTED_POLICY_RUNTIME_UNRESOLVED" if supported else "NOT_COMPLETE_NO_FAMILY_RELOCATION")
    rows.append({"source_id":source["source_id"],"mod_id":source["mod_id"],
      "source_path":source["source_path"],"source_sha256":source["source_sha256"],
      "inventory_line":source["inventory_line"],"declaration_index":source["declaration_index"],
      "source_kind":source["source_kind"],"family":family,"process_section":process if supported else None,
      "existing_aca_registry_integration":supported,"relocation_policy":status,
      "ingredient_for_policy":"remove only ACA helper contribution; retain independently legitimate native relations" if supported else "NOT_COMPLETE; unchanged, not certified correctly classified",
      "declared_recipe_identity":definition.get("code",definition.get("name")) if isinstance(definition,dict) else None,
      "native_entry_literal_candidate":source["native"].get("entry_literal_candidate"),
      "declared_output":source["native"].get("output_declaration"),"native_destination_formula":formula,
      "resolved_destination":None,"runtime_status":"UNRESOLVED_NOT_EXECUTED",
      "enabled_literal":enabled,"activation":"source-disabled" if enabled is False else "unresolved; active config, dependencies, patches and registry load not executed",
      "wildcards_or_variant_templates":any(any(mark in text for mark in ("*","{","ByType")) for text in strings(definition)),
      "mixing_cooks_into_declaration":cooks,
      "helper_inclusion_condition":"resolved matching ingredient; output in allStacks; mixing CooksInto must be null" if supported else "no addon family adapter",
      "source_to_runtime_multiplicity":"unresolved: disabled/patch/wildcard expansion and same-output recipes are not inferred from icon count"})
assert len({r["source_id"] for r in rows})==len(rows)
assert sum(r["existing_aca_registry_integration"] for r in rows)==626
attrs=[{k:s.get(k) for k in ("source_id","mod_id","source_path","source_sha256","inventory_line","json_path","operation_key")} |
 {"process_section":None,"runtime_status":"UNRESOLVED_NOT_EXECUTED","relocation_policy":"NOT_COMPLETE_ATTRIBUTE_DRIVEN_OPERATION_NO_SAFE_ADAPTER"}
 for s in review["attribute_source_mappings"] if s["mod_id"] in ("expandedfoods","aculinaryartillery","efchefstricks")]
result={"schema_version":1,"release":"0.2.0","overall_acceptance":"NOT_COMPLETE_EXPANDED_EF_SCOPE",
 "sources":[{"path":str(p),"sha256":hashlib.sha256(p.read_bytes()).hexdigest()} for p in (review_path,inventory_path)],
 "notes":["Source declarations are not active runtime recipes; zero verified resolved page identities.",
 "All 626 supported ACA-registry source declarations mapped, including three EF Meals mixing declarations.",
 "Additional EF declarations and patch operations explicitly retain NOT_COMPLETE dispositions; no blanket classification of grid/barrel as pure processing.",
 "Attribute nodes overlap and are not unique recipes; code-generated registrations have no exhaustive runtime snapshot."],
 "counts":{"recipe_rows":len(rows),"supported_declarations":626,"ef_source_declarations":sum(r["mod_id"]=="expandedfoods" and r["source_kind"]=="recipe_declaration" for r in rows),
 "ef_patch_operations":sum(r["mod_id"]=="expandedfoods" and r["source_kind"]!="recipe_declaration" for r in rows),"attribute_nodes":len(attrs),
 "supported_family_counts":dict(collections.Counter(r["family"] for r in rows if r["existing_aca_registry_integration"]))},
 "recipes":rows,"attribute_operations":attrs,"code_generated":review["code_driven_blockers"]}
(ROOT/"docs/recipe-ledger.json").write_text(json.dumps(result,indent=2,ensure_ascii=False)+"\n")
print(json.dumps(result["counts"],indent=2))
