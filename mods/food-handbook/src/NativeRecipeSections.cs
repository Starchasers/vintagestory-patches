using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;
namespace Kuboczoch.FoodHandbook;

// Intercept recipe contributions BEFORE native deduplication; never delete shared output icons.
internal static class NativeRecipeSections
{
 internal const string ExpectedIL = NativeProcessPolicy.KnownNativeIL;
 internal static readonly MethodInfo Target=AccessTools.Method(typeof(CollectibleBehaviorHandbookTextAndExtraInfo),"addIngredientForInfo");
 internal static readonly MethodInfo Add=AccessTools.Method(typeof(CollectibleBehaviorHandbookTextAndExtraInfo),"addToListUniquely");
 [ThreadStatic] private static Scope? current;
 internal sealed class Scope {
  internal Scope? Parent;
  internal ICoreClientAPI Api=null!;
  internal ActionConsumable<string> Open=null!;
  internal Dictionary<string,List<RichTextComponentBase>> Sections=new();
  internal Dictionary<string,List<ItemStack>> Outputs=new();
 }
 internal static void Prefix(ICoreClientAPI capi, ActionConsumable<string> openDetailPageFor, out Scope __state) {
  __state=new Scope{Parent=current,Api=capi,Open=openDetailPageFor}; current=__state;
 }
 internal static void Postfix(List<RichTextComponentBase> components, ref bool __result, Scope __state) {
  foreach(var nodes in __state.Sections.Values) { components.AddRange(nodes); ProcessSectionLayout.EndRow(components,__state.Api); }
  if(__state.Sections.Count>0) __result=true;
 }
 internal static Exception? Finalizer(Exception? __exception,Scope? __state) { if(__state!=null) current=__state.Parent; return __exception; }
 internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
  if(!NativeProcessPolicy.AcceptsNativeIL(Convert.ToHexString(SHA256.HashData(Target.GetMethodBody()!.GetILAsByteArray()!))))
   throw new InvalidOperationException("Unknown native ingredient method IL; native recipe adapter disabled");
  var code=instructions.ToList();
  if(code.Count(i=>i.Calls(Add))!=12) throw new InvalidOperationException("Native contribution call count changed");
  var locals=Target.GetMethodBody()!.LocalVariables;
  var grid=locals.Single(l=>l.LocalType==typeof(GridRecipe)).LocalIndex;
  var barrel=locals.Single(l=>l.LocalType==typeof(BarrelRecipe)).LocalIndex;
  int call=0;
  foreach(var i in code) {
   if(!i.Calls(Add)) {yield return i;continue;}
   call++;
   // Source evidence exact-handbook.cs: first output is generated grid output; sixth is barrel output.
   // Second call is returnedStack (containers/byproducts), deliberately NEVER relocated.
   if(call!=1 && call!=6) {yield return i;continue;}
   var load=CodeInstruction.LoadLocal(call==1?grid:barrel);
   load.labels.AddRange(i.labels);load.blocks.AddRange(i.blocks);yield return load;
   yield return new CodeInstruction(OpCodes.Call,AccessTools.Method(typeof(NativeRecipeSections),nameof(Capture)));
  }
 }
 private static void Capture(CollectibleBehaviorHandbookTextAndExtraInfo owner,ICoreClientAPI api,List<ItemStack> native,ItemStack output,RecipeBase recipe) {
  try {
   var state=current;
   var family=recipe is GridRecipe?"grid":"barrel";
   var section=NativeProcessPolicy.Classify(family,recipe.Name?.Domain,recipe.Name?.Path,output?.Collectible.Code.ToString());
   if(state!=null && output!=null && section!=null && recipe.Enabled) {
    if(state.Outputs.TryGetValue(section,out var seen) && seen.Any(s=>s.Equals(api.World,output,Array.Empty<string>()))) return;
    // Prepare the entire first section before suppressing any native contribution.
    var icon=ProcessSectionLayout.Icon(api,output,
     clicked=>state.Open(IngredientComponentsPostfix.Destination(api,clicked)));
    if(!state.Sections.TryGetValue(section,out var nodes)) {
     nodes=new List<RichTextComponentBase>();var text=true;
     CollectibleBehaviorHandbookTextAndExtraInfo.AddHeading(nodes,api,"kuboczochfoodhandbook:native-"+section,ref text);
     ProcessSectionLayout.BeginRow(nodes,api);
     state.Sections.Add(section,nodes);state.Outputs.Add(section,new List<ItemStack>());
    }
    nodes.Add(icon);state.Outputs[section].Add(output);return;
   }
  } catch(Exception error) {api.Logger.Warning("Food Handbook native contribution retained: {0}",error.Message);}
  Add.Invoke(owner,new object?[]{api,native,output});
 }
}
