using Kuboczoch.FoodHandbook;

int count = 0;
void Check(bool ok, string name) { if (!ok) throw new Exception(name); Console.WriteLine("PASS " + name); count++; }
Node Label(ProcessKind p) => new("label:" + p);
Node Icon(string s) => new("exact:" + s);
List<Node> Transform(params Node[] nodes) => ProcessPlan.Rewrite(nodes, n => n.Stacks, n => n.Meal,
    s => s.StartsWith("gelatin") || s == "broth" ? ProcessKind.Simmering : s == "both" ? ProcessKind.Simmering | ProcessKind.Kneading : s == "dough" ? ProcessKind.Kneading : ProcessKind.None,
    (a,b) => a == b, Icon, Label);
var meal = new Node("meal", Meal: true); var unknown = new Node("unknown", new[]{"alien"});
var unchanged = new Node("other");
var result = Transform(new Node("group", new[]{"gelatin-liquid", "gelatin-raw", "broth"}), meal, meal);
Check(result.Select(n=>n.Name).SequenceEqual(new[]{"label:Simmering","exact:gelatin-liquid","exact:gelatin-raw","exact:broth","label:Mixing","meal","meal"}), "separate concrete stages and one heading per run");
Check(ReferenceEquals(result[5],meal) && ReferenceEquals(result[6],meal), "meal identity and callback object preserved");
result = Transform(new Node("group", new[]{"both", "both"}), new Node("duplicate", new[]{"both"}));
Check(result.Count==2 && result[0].Name=="label:Simmering, Kneading", "mixed signature one icon not duplicate process lists");
result = Transform(unknown, unchanged);
Check(ReferenceEquals(result[0],unknown) && ReferenceEquals(result[1],unchanged), "unknown components retain identity");
var partial = new Node("partial", new[]{"broth","alien"});
Check(ReferenceEquals(Transform(partial).Single(),partial), "partially unknown slideshow left untouched");
Check(Transform().Count==0, "empty unchanged");
result = Transform(new Node("dough", new[]{"dough"}), unknown, new Node("dough2", new[]{"dough2"}), meal);
Check(result[0].Name=="label:Kneading" && ReferenceEquals(result[^1],meal), "mixed runs preserve ordering");
int fallbackCalls=0;
Check(PageDestination.Resolve(()=>"provider:exact", ()=>{fallbackCalls++; return "fallback";})=="provider:exact" && fallbackCalls==0,"provider wins without fallback evaluation");
Check(PageDestination.Resolve(()=>null, ()=>"fallback:attributes")=="fallback:attributes","null provider fallback preserves code");
Check(PageDestination.Resolve(()=>"", ()=>"fallback")=="", "empty provider result native null-coalescing semantics");
var source = new[]{new Node("stages",new[]{"gelatin-liquid","gelatin-raw"}),meal};
var snapshot = source.ToArray();
try { ProcessPlan.Rewrite(source,n=>n.Stacks,n=>n.Meal,_=>ProcessKind.Simmering,(a,b)=>a==b,Icon,_=>throw new InvalidOperationException("test projection failure")); throw new Exception("expected failure"); }
catch (InvalidOperationException) { Check(source.SequenceEqual(snapshot) && ReferenceEquals(source[1],meal),"projection exception leaves native source untouched"); }
for (int seed=0; seed<100; seed++) {
 var random=new Random(seed);
 var outputs=Enumerable.Range(0,30).Select(_=>new[]{"gelatin-liquid","gelatin-raw","broth","dough","both"}[random.Next(5)]).ToArray();
 var projected=Transform(new Node("group",outputs));
 var exact=projected.Where(n=>n.Name.StartsWith("exact:")).Select(n=>n.Name[6..]);
 if (!exact.SequenceEqual(outputs.Distinct())) throw new Exception("ordering/dedup seed "+seed);
}
Check(true,"100 deterministic duplicate and mixed-process output sequences");
var groups = ProcessPlan.Sections(new[]{new Node("a",new[]{"broth","dough","broth"}),meal,new Node("b",new[]{"gelatin-raw"})}, n=>n.Stacks,n=>n.Meal,
 s=>s=="dough"?ProcessKind.Kneading:ProcessKind.Simmering,(a,b)=>a==b,Icon);
Check(groups.Select(g=>g.Kind).SequenceEqual(new[]{ProcessKind.Simmering,ProcessKind.Kneading,ProcessKind.Mixing}),"dedicated section order independent of interleaved helper contributions");
Check(groups[0].Nodes.Select(n=>n.Name).SequenceEqual(new[]{"exact:broth","exact:gelatin-raw"}),"one simmering section, no duplicate concrete icons");
Check(ReferenceEquals(groups[2].Nodes.Single(),meal),"relocated mixing keeps exact native callback widget");
Check(ProcessPlan.Sections(Array.Empty<Node>(),n=>n.Stacks,n=>n.Meal,_=>ProcessKind.None,(a,b)=>a==b,Icon).Count==0,"empty helper emits no process sections or spacing");
try { ProcessPlan.Sections(new[]{partial},n=>n.Stacks,n=>n.Meal,s=>s=="broth"?ProcessKind.Simmering:ProcessKind.None,(a,b)=>a==b,Icon);throw new Exception("expected rejection"); }
catch (InvalidOperationException) { Check(true,"partially unknown helper rejects relocation transaction, preserving original Ingredient for fallback"); }
var native = new List<Node>{unknown,unchanged}; var nativeSnapshot=native.ToArray();
var sections=ProcessPlan.Sections(new[]{meal},n=>n.Stacks,n=>n.Meal,_=>ProcessKind.None,(a,b)=>a==b,Icon);
Check(native.SequenceEqual(nativeSnapshot) && ReferenceEquals(native[0],unknown),"section planning cannot remove native/unrelated Ingredient for or Created by nodes");
var relocation=ProcessPlan.Relocate(new[]{new Node("aca",new[]{"broth","gelatin-raw","broth"})},n=>n.Stacks,n=>n.Meal,_=>ProcessKind.Simmering,(a,b)=>a==b,Icon);
Check(relocation.IngredientFor.Count==0 && relocation.Sections.Single().Nodes.Count==2,"migrated broth and pre-gelatin absent Ingredient for, present once Simmering");
// Emulates ACA's exact Count==0 early return and native append contract, not translated text matching.
var independentlyLegitimate=new Node("native crafting broth",new[]{"broth"});
var nativeIngredientFor=new List<Node>{independentlyLegitimate,unknown};
if(relocation.IngredientFor.Count>0) nativeIngredientFor.AddRange(relocation.IngredientFor);
Check(nativeIngredientFor.SequenceEqual(new[]{independentlyLegitimate,unknown}),"mixed vanilla+ACA preserves independently legitimate same-output crafting and unrelated Ingredient for");
var acaOnlyGenericHeading=relocation.IngredientFor.Count>0;
Check(!acaOnlyGenericHeading && relocation.Sections.Count==1,"ACA-only page has dedicated process section and no orphan Ingredient for heading");
using var ledger=System.Text.Json.JsonDocument.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory,"recipe-ledger.json")));
var recipeRows=ledger.RootElement.GetProperty("recipes").EnumerateArray().ToArray();
var supportedRows=recipeRows.Where(r=>r.GetProperty("existing_aca_registry_integration").GetBoolean()).ToArray();
Check(supportedRows.Length==626 && supportedRows.Select(r=>r.GetProperty("source_id").GetString()).Distinct().Count()==626,"all supported source declarations represented once, not inferred from icon count");
var mappedFamilies=new Dictionary<string,int>();
foreach(var row in supportedRows) {
 var family=row.GetProperty("family").GetString()!;
 var kind=Enum.Parse<ProcessKind>(row.GetProperty("process_section").GetString()!);
 mappedFamilies[family]=mappedFamilies.GetValueOrDefault(family)+1;
 var id=row.GetProperty("source_id").GetString()!;
 var sourceNode=family=="mixing"?new Node(id,Meal:true):new Node(id,new[]{id});
 var plan=ProcessPlan.Relocate(new[]{sourceNode},n=>n.Stacks,n=>n.Meal,_=>kind,(a,b)=>a==b,Icon);
 if(plan.IngredientFor.Count!=0 || plan.Sections.Count!=1 || plan.Sections[0].Kind!=kind || plan.Sections[0].Nodes.Count!=1)
  throw new Exception("source policy relocation failed: "+id);
 if(row.GetProperty("runtime_status").GetString()!="UNRESOLVED_NOT_EXECUTED" || row.GetProperty("resolved_destination").ValueKind!=System.Text.Json.JsonValueKind.Null)
  throw new Exception("unexecuted source incorrectly certified: "+id);
}
Check(mappedFamilies["simmering"]==203 && mappedFamilies["kneading"]==420 && mappedFamilies["mixing"]==3,"per-source policy projection for 203 simmering, 420 kneading, 3 mixing declarations (synthetic nodes, NOT registry verification)");
var incompleteEf=recipeRows.Where(r=>r.GetProperty("mod_id").GetString()=="expandedfoods" && r.GetProperty("source_kind").GetString()=="recipe_declaration" && !r.GetProperty("existing_aca_registry_integration").GetBoolean()).ToArray();
Check(incompleteEf.Count(r=>r.GetProperty("family").GetString()=="grid")==205 && incompleteEf.Count(r=>r.GetProperty("family").GetString()=="barrel")==30 && incompleteEf.All(r=>r.GetProperty("relocation_policy").GetString() is "SOURCE_POLICY_MAPPED_RUNTIME_UNVERIFIED" or "EXCLUDED_NONFOOD" or "EXCLUDED_UNRELATED_MOD"),"205 EF grid and 30 barrel declarations each classified or explicitly excluded, never runtime-certified");
Check(ledger.RootElement.GetProperty("attribute_operations").EnumerateArray().Count()==429 && ledger.RootElement.GetProperty("overall_acceptance").GetString()=="IMPLEMENTED_SOURCE_POLICY_RUNTIME_UNVERIFIED","attribute pointers and runtime acceptance remain explicit");
using var operations=System.Text.Json.JsonDocument.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory,"native-operation-ledger.json")));
var nativeRows=operations.RootElement.GetProperty("native_recipe_sources").EnumerateArray().ToArray();
Check(nativeRows.Length==235 && nativeRows.Select(n=>n.GetProperty("source_id").GetString()).Distinct().Count()==235,"all 235 native source identities unique");
foreach(var row in nativeRows) {
 var actual=NativeProcessPolicy.Classify(row.GetProperty("effective_family").GetString()!,row.GetProperty("runtime_name_domain").GetString(),row.GetProperty("runtime_name_path").GetString(),row.GetProperty("sample_output").GetString());
 var expected=row.GetProperty("native_section").GetString();
 if(actual!=expected) throw new Exception("Native source policy failed: "+row.GetProperty("source_id").GetString()+" expected "+expected+" actual "+actual);
 if(row.GetProperty("runtime_verified").GetBoolean()) throw new Exception("False runtime certification");
}
Check(true,"235 source-declaration synthetic output projections match exact native identity policy (NOT runtime verification)");
Check(NativeProcessPolicy.Classify("grid","game","recipes/grid/unrelated.json","expandedfoods:pasta-cut")==null,"same output independent vanilla crafting relation retained");
Check(NativeProcessPolicy.Classify("grid","expandedfoods","recipes/grid/unrelated.json","expandedfoods:pasta-cut")==null,"unknown EF recipe name retained even for known output");
Check(NativeProcessPolicy.Classify("barrel","game","recipes/barrel/hide.json","game:hide-small")==null,"nonfood hide, oil and wax not blanket classified");
Check(NativeProcessPolicy.Classify("barrel","unrelatedmod","recipes/barrel/yogurtplain.json","expandedfoods:yogurt-plain")==null,"unknown provider cannot acquire food sections");
Check(NativeProcessPolicy.SupportsAcaAssembly("8F6CB3C873F39CC2E3B898AA538015370608547B6F4D34A0FBDAE51EC255406B"),"exact trusted ACA semantic fingerprint accepted");
Check(!NativeProcessPolicy.SupportsAcaAssembly(null) && !NativeProcessPolicy.SupportsAcaAssembly(new string('0',64)),"unknown or missing ACA assembly rejected even with unchanged ABI");
Check(NativeProcessPolicy.AcceptsNativeIL(NativeProcessPolicy.KnownNativeIL) && !NativeProcessPolicy.AcceptsNativeIL(new string('0',64)),"unknown-version native IL fails closed");
Check(operations.RootElement.GetProperty("patch_operations").GetArrayLength()==330,"330 patch operations distinguished from recipe declarations");
Check(operations.RootElement.GetProperty("attribute_sources").EnumerateArray().Count(r=>r.GetProperty("disposition").GetString()=="EXCLUDED_VISUAL_METADATA")==1,"Ancient Tools rack transform is visual metadata, not a curing recipe or blocker");
Check(ProcessHeadingPolicy.For(ProcessKind.Simmering).Single() == new HeadingPart("process-1","handbook://simmeringguide"),"simmering uses verified bare guide code");
Check(ProcessHeadingPolicy.For(ProcessKind.Kneading).Single().Link == "handbook://kneadingmixingguide" && ProcessHeadingPolicy.For(ProcessKind.Mixing).Single().Link == "handbook://kneadingmixingguide","kneading and mixing share authoritative guide");
var combined=ProcessHeadingPolicy.For(ProcessKind.Simmering | ProcessKind.Kneading);
Check(combined.Select(h=>h.Key).SequenceEqual(new[]{"process-3","process-1","process-2"}) && combined[0].Link==null,"combined localized parent retains one output grouping with two clear subheadings");
var destinations=new List<string>();
foreach(var heading in combined) if(heading.Link!=null) destinations.Add(heading.Link);
Check(destinations.SequenceEqual(new[]{"handbook://simmeringguide","handbook://kneadingmixingguide"}),"combined routes independently to BOTH verified destinations, never one ambiguous link");
Check(new[]{ProcessKind.None,(ProcessKind)5,(ProcessKind)7}.All(k=>ProcessHeadingPolicy.For(k).Count==0),"unknown signatures never guess a guide");
Console.WriteLine($"{count} tests passed; own-code only, no ACA/game assemblies loaded.");
record Node(string Name, string[]? Stacks=null, bool Meal=false);