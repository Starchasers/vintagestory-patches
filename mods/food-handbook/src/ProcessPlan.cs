namespace Kuboczoch.FoodHandbook;
[Flags] internal enum ProcessKind { None=0, Simmering=1, Kneading=2, Mixing=4 }
internal sealed record ProcessSection<T>(ProcessKind Kind, List<T> Nodes);
internal sealed record RelocationPlan<T>(List<ProcessSection<T>> Sections, List<T> IngredientFor);
internal static class ProcessPlan {
 internal static RelocationPlan<TNode> Relocate<TNode,TStack>(IEnumerable<TNode> source,
     Func<TNode,TStack[]?> stacks, Func<TNode,bool> meal, Func<TStack,ProcessKind> process,
     Func<TStack,TStack,bool> equal, Func<TStack,TNode> icon) =>
  new(Sections(source,stacks,meal,process,equal,icon), new List<TNode>());

 // A separate section is a composition unit, not an inline label inside Ingredient for.
 // Reject the entire relocation on unknown nodes, so ACA retains ownership of its fallback.
 internal static List<ProcessSection<TNode>> Sections<TNode,TStack>(IEnumerable<TNode> source,
     Func<TNode,TStack[]?> stacks, Func<TNode,bool> meal, Func<TStack,ProcessKind> process,
     Func<TStack,TStack,bool> equal, Func<TStack,TNode> icon) {
  var groups = new Dictionary<ProcessKind,List<TNode>>();
  var emitted = new List<TStack>();
  void Add(ProcessKind k,TNode n) { if (!groups.TryGetValue(k,out var nodes)) groups[k]=nodes=new(); nodes.Add(n); }
  foreach (var node in source) {
   if (meal(node)) { Add(ProcessKind.Mixing,node); continue; }
   var outputs=stacks(node);
   if (outputs is null || outputs.Length==0) throw new InvalidOperationException("Unknown ACA helper widget");
   foreach (var output in outputs) {
    var kind=process(output);
    if (kind==ProcessKind.None) throw new InvalidOperationException("Unclassified ACA output");
    if (emitted.Any(s=>equal(s,output))) continue;
    Add(kind,icon(output)); emitted.Add(output);
   }
  }
  return groups.OrderBy(g=>(int)g.Key).Select(g=>new ProcessSection<TNode>(g.Key,g.Value)).ToList();
 }

 // Transactional projection: never modify source or native nodes/callbacks.
 internal static List<TNode> Rewrite<TNode,TStack>(IEnumerable<TNode> source, Func<TNode,TStack[]?> stacks, Func<TNode,bool> meal, Func<TStack,ProcessKind> process, Func<TStack,TStack,bool> equal, Func<TStack,TNode> icon, Func<ProcessKind,TNode> label) {
  var result = new List<TNode>();
  var emitted = new List<TStack>();
  var previous = ProcessKind.None;
  void Heading(ProcessKind kind) { if (previous != kind) result.Add(label(kind)); previous = kind; }
  foreach (var node in source) {
   // Meal widgets are not generic slideshows. Preserve native recipe and callback identity.
   if (meal(node)) { Heading(ProcessKind.Mixing); result.Add(node); continue; }
   var outputs = stacks(node);
   if (outputs == null || outputs.Length == 0) { result.Add(node); previous=ProcessKind.None; continue; }
   var kinds = outputs.Select(process).ToArray();
   // Unknown extensions are not ours to rebuild; retain the whole original node.
   if (kinds.Any(k => k == ProcessKind.None)) { result.Add(node); previous=ProcessKind.None; continue; }
   for (int i=0; i<outputs.Length; i++) {
    var output = outputs[i];
    if (emitted.Any(s => equal(s,output))) continue;
    Heading(kinds[i]); result.Add(icon(output)); emitted.Add(output);
   }
  }
  return result;
 }
}
internal static class PageDestination { internal static string Resolve(Func<string?> provider, Func<string> fallback) => provider() ?? fallback(); }
