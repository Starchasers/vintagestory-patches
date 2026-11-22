using System.Reflection;
using System.Reflection.Emit;
using ACulinaryArtillery;
using ACulinaryArtillery.Util;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Util;
using Vintagestory.GameContent;
namespace Kuboczoch.FoodHandbook;

// Sole patch target: ACA's Ingredient-for postfix, NOT the vanilla behavior or helper.
// Replace its one helper call at the point BEFORE ACA creates/inserts its generic heading.
// All pie repair/capping code before that call and native Created by remain unmodified.
internal static class SectionCompositionPatch
{
 internal static readonly MethodInfo Helper = AccessTools.Method(typeof(HandbookInfoExtensions),
  nameof(HandbookInfoExtensions.ACAHandbookIngredientForComponents),
  new[]{typeof(ICoreClientAPI),typeof(ItemStack[]),typeof(ActionConsumable<string>),typeof(ItemStack)});
 internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
 {
  var code=instructions.ToList();
  var matches=code.Where(i=>i.Calls(Helper)).ToArray();
  if (matches.Length!=1) throw new InvalidOperationException("Expected exactly one ACA ingredient helper call; refusing unknown composition");
  foreach(var instruction in code) {
   if (!instruction.Calls(Helper)) { yield return instruction; continue; }
   // ACA static Postfix arguments: 0 ref result, 1 capi, 2 allStacks, 3 callback,
   // 4 stack, 5 components. Keep branch labels on the first replacement instruction.
   var list=new CodeInstruction(OpCodes.Ldarg_S,(byte)5);
   list.labels.AddRange(instruction.labels); list.blocks.AddRange(instruction.blocks);
   yield return list;
   yield return new CodeInstruction(OpCodes.Ldarg_0);
   yield return new CodeInstruction(OpCodes.Call,AccessTools.Method(typeof(SectionCompositionPatch),nameof(Compose)));
  }
 }
 private static bool warned;
 internal static List<RichTextComponentBase> Compose(ICoreClientAPI capi, ItemStack[] allStacks,
  ActionConsumable<string> openDetailPageFor, ItemStack stack, List<RichTextComponentBase> components, ref bool haveText)
 {
  var original=HandbookInfoExtensions.ACAHandbookIngredientForComponents(capi,allStacks,openDetailPageFor,stack);
  if (original.Count==0) return original;
  try {
   var candidate=stack.Clone(); candidate.StackSize=candidate.Collectible.MaxStackSize*10;
   var simmer=capi.GetSimmerRecipes().Where(r=>r.Ingredients.Any(i=>i.SatisfiesAsIngredient(candidate,true)))
    .Select(r=>r.Simmering.SmeltedStack.ResolvedItemStack).Where(s=>s!=null).ToArray();
   var knead=capi.GetKneadingRecipes().Where(r=>r.Ingredients.Any(i=>i.GetMatch(candidate)!=null))
    .Select(r=>r.Output.ResolvedItemStack).Where(s=>s!=null).ToArray();
   ProcessKind Kind(ItemStack output) {
    var kind=ProcessKind.None;
    if(simmer.Any(s=>output.Equals(capi.World,s,GlobalConstants.IgnoredStackAttributes))) kind|=ProcessKind.Simmering;
    if(knead.Any(s=>output.Equals(capi.World,s,GlobalConstants.IgnoredStackAttributes))) kind|=ProcessKind.Kneading;
    return kind;
   }
   RichTextComponentBase Icon(ItemStack output)=>ProcessSectionLayout.Icon(capi,output,
    clicked=>openDetailPageFor(IngredientComponentsPostfix.Destination(capi,clicked)));
   var relocation=ProcessPlan.Relocate(original,
    n=>n.GetType()==typeof(SlideshowItemstackTextComponent)?((SlideshowItemstackTextComponent)n).Itemstacks:null,
    n=>n.GetType()==typeof(MealstackTextComponent),Kind,
    (a,b)=>a.Equals(capi.World,b,Array.Empty<string>()),Icon);
   // Compose off-page and commit once; never search translated labels or remove native nodes.
   var addition=new List<RichTextComponentBase>(); var text=haveText;
   foreach(var section in relocation.Sections) {
    foreach(var heading in ProcessHeadingPolicy.For(section.Kind))
     ProcessHeadings.AddHeading(addition,capi,"kuboczochfoodhandbook:"+heading.Key,ref text,heading.Link);
    ProcessSectionLayout.BeginRow(addition,capi);
    addition.AddRange(section.Nodes);
    ProcessSectionLayout.EndRow(addition,capi);
   }
   if(addition.Count==0) return original;
   components.AddRange(addition); haveText=text;
   // ACA now takes its existing empty-helper early-return: no generic heading/insertion/spacing.
   return relocation.IngredientFor;
  } catch(Exception error) {
   if(!warned) { capi.Logger.Warning("Kuboczoch Food Handbook: section relocation failed; native Ingredient for retained: {0}",error); warned=true; }
   return original;
  }
 }
}
