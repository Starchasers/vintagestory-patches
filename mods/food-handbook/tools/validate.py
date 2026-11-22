"""Original stdlib-only package/source contract checks, not client integration tests."""
import json
from pathlib import Path
import sys
import zipfile
ROOT = Path(__file__).resolve().parents[1]
def check(ok, message):
    if not ok: raise AssertionError(message)
    print("PASS " + message)
info = json.loads((ROOT / "modinfo.json").read_text())
check(info["modid"] == "kuboczochfoodhandbook" and info["name"] == "Kuboczoch Food Handbook" and info["authors"] == ["Kuboczoch"], "exact mod identity")
check(info["type"] == "code" and info["side"] == "client" and info["requiredOnServer"] is False and info["requiredOnClient"] is False, "client-only optional presentation addon")
check(info["dependencies"] == {"game":"1.22.7", "aculinaryartillery":"2.0.0-dev.29", "expandedfoods":"2.0.0-dev.17"}, "explicit verified dependency minima")
lang = json.loads((ROOT / "assets/kuboczochfoodhandbook/lang/en.json").read_text())
check(set(lang) == {"process-1","process-2","process-3","process-4","native-preparing","native-fermenting","native-preserving","native-soaking"}, "all reachable process signatures localized")
for path in (ROOT / "assets").rglob("*.json"):
    data = json.loads(path.read_text())
    check(path.parent.name == "lang" and set(data)==set(lang), "language-only assets: " + path.name)
csproj = (ROOT / "KuboczochFoodHandbook.csproj").read_text()
check(csproj.count("<Private>false</Private>") == 5, "all upstream references non-copying")
hook = (ROOT / "src/IngredientComponentsPostfix.cs").read_text()
mod = (ROOT / "src/FoodHandbookModSystem.cs").read_text()
section = (ROOT / "src/SectionCompositionPatch.cs").read_text()
check(mod.count("harmony.Patch(")==2 and "typeof(ACulinaryArtillery.GetHandbookIngredientForPatch)" in mod and "transpiler:" in mod, "ACA composition plus IL-gated native recipe patch; existing native named attribute sections retained, no behavior shadow")
check("Calls(Helper)" in section and "matches.Length!=1" in section and "OpCodes.Ldarg_S,(byte)5" in section and "OpCodes.Ldarg_0" in section, "single validated helper call redirects component list and ref result")
check("return relocation.IngredientFor;" in section and "ProcessPlan.Relocate(original," in section, "migrated process contribution removed from generic Ingredient for")
check("DisplayText" not in section and "components.Remove" not in section and "components.Insert" not in section, "no translated-label matching or native list surgery")
check("AddHeading(addition,capi" in section and "components.AddRange(addition); haveText=text;" in section, "dedicated headings compose transactionally before commit")
layout = (ROOT / "src/ProcessSectionLayout.cs").read_text()
native = (ROOT / "src/NativeRecipeSections.cs").read_text()
check("OutputFloat = EnumFloat.Inline" in layout and "ProcessSectionLayout.Icon(" in section and "ProcessSectionLayout.Icon(" in native and "EnumFloat.Left" not in section+native, "both output paths use shared inline geometry and callbacks")
check("ProcessSectionLayout.BeginRow(" in section and "ProcessSectionLayout.EndRow(" in section and "ProcessSectionLayout.BeginRow(" in native and "ProcessSectionLayout.EndRow(" in native, "each process row has shared leading/trailing clear spacers")
check("new[]{output},40" in layout and "GetCollectibleInterface<IHandBookPageCodeProvider>()" in hook, "explicit concrete output constructor and provider-first routing")
check("SatisfiesAsIngredient(candidate,true)" in section and "GetMatch(candidate)!=null" in section and "GlobalConstants.IgnoredStackAttributes" in section, "native predicates and native matching attribute policy retained")
check("SupportsAcaAssembly(acaHash)" in mod and mod.index("SupportsAcaAssembly(acaHash)") < mod.index("harmony = new Harmony"), "exact ACA semantic fingerprint gated before any patch")
headings=(ROOT/'src/ProcessHeadingPolicy.cs').read_text()
import re
check(set(re.findall(r'handbook://([a-z]+)',headings))=={'simmeringguide','kneadingmixingguide'}, 'only two verified guide destinations; no guessed guides')
check('ProcessHeadingPolicy.For(section.Kind)' in section and 'ref text,heading.Link' in section, 'all combined heading destinations composed through optional link helper')
check(section.count('ProcessSectionLayout.BeginRow(addition,capi)')==1, 'combined headings retain one output row')
check('ref text);' in native and 'handbook://' not in native, 'Preparing Fermenting Preserving Soaking remain plain')
check(not (ROOT/'assets/game/lang').exists(), 'no guessed locale overrides')
heading_impl=(ROOT/'src/ProcessHeadings.cs').read_text()
check('Href=link' in heading_impl and 'Lang.Get(key)+"\\n"' in heading_impl and 'ClearFloatTextComponent(api,14)' in heading_impl, 'linked headings retain exact destination, translated newline and native spacing')
for locale in ('en','pl'):
 check(all(k in json.loads((ROOT/f'assets/kuboczochfoodhandbook/lang/{locale}.json').read_text()) for k in ('process-1','process-2','process-3','process-4')), 'combined parent and subheadings localized '+locale)
from collections import Counter
coverage=json.loads((ROOT / "docs/coverage-status.json").read_text())
providers=coverage["providers"]
manifest=coverage["manifest_inventory"]
check(len(providers)==63 and len({p["mod_id"] for p in providers})==63, "all 63 selected provider rows unique")
check(len(manifest)==141 and len({p["mod_id"] for p in manifest})==141, "all 141 manifest provider classifications unique")
check({p["mod_id"] for p in providers}=={p["mod_id"] for p in manifest if p["classification"]=="food-related"}, "selected provider IDs match manifest food classification")
check(dict(Counter(p["status"] for p in providers))==coverage["status_counts"], "provider status totals reconciled")
check(all(not p["runtime_verified"] and not p["full_provider_validated"] for p in providers), "no provider claimed runtime-validated")
for arg in sys.argv[1:]:
    with zipfile.ZipFile(arg) as z:
        names = z.namelist()
        check(not any(n.startswith("/") or ".." in Path(n).parts for n in names), "safe archive names: " + Path(arg).name)
        check(len(names)==len(set(names)) and z.testzip() is None, "archive uniqueness and CRC: " + Path(arg).name)
        dlls = [n for n in names if n.lower().endswith(".dll")]
        if arg.endswith("-source.zip"):
            check(not dlls and "src/IngredientComponentsPostfix.cs" in names and "tests/Program.cs" in names, "source archive original code, tests, no binaries")
        else:
            check(dlls==["KuboczochFoodHandbook.dll"], "runtime archive contains only own DLL")
            check(z.read("KuboczochFoodHandbook.dll")== (ROOT/"bin/Release/net10.0/KuboczochFoodHandbook.dll").read_bytes(), "runtime archive DLL equals compiled production artifact")
            check(json.loads(z.read("modinfo.json"))==info, "runtime archive manifest matches source")
print("Static contracts passed; GUI layout, Harmony execution, routing and live recipe registries NOT tested.")
