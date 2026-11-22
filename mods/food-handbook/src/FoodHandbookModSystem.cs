using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
namespace Kuboczoch.FoodHandbook;

/// <summary>Client-only presentation addon; redirects one ACA composition call.</summary>
public sealed class FoodHandbookModSystem : ModSystem
{
 internal const string HarmonyId = "kuboczoch.foodhandbook";
 private Harmony? harmony;
 public override bool ShouldLoad(EnumAppSide forSide) => forSide == EnumAppSide.Client;
 public override void StartClientSide(ICoreClientAPI api)
 {
  if (harmony is not null) return;
  try
  {
   var target = AccessTools.Method(typeof(ACulinaryArtillery.GetHandbookIngredientForPatch), "Postfix",
       new[] { typeof(bool).MakeByRefType(), typeof(ICoreClientAPI), typeof(ItemStack[]),
        typeof(ActionConsumable<string>), typeof(ItemStack), typeof(List<Vintagestory.API.Client.RichTextComponentBase>),
        typeof(float), typeof(List<ItemStack>), typeof(List<ItemStack>), typeof(List<ItemStack>), typeof(bool) });
   if (target is null)
   {
    api.Logger.Warning("Kuboczoch Food Handbook disabled: compatible ACA handbook composition method was not found.");
    return;
   }
   var acaAssembly=typeof(ACulinaryArtillery.GetHandbookIngredientForPatch).Assembly;
   var acaHash=Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(File.ReadAllBytes(acaAssembly.Location)));
   if(!NativeProcessPolicy.SupportsAcaAssembly(acaHash))
       throw new InvalidOperationException("Unknown ACA assembly: ingredient relocation requires the exact reviewed dev.29 binary");
   harmony = new Harmony(HarmonyId);
   harmony.Patch(target,
       transpiler: new HarmonyMethod(typeof(SectionCompositionPatch), nameof(SectionCompositionPatch.Transpiler)));
   var native=NativeRecipeSections.Target;
   if(Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(native.GetMethodBody()!.GetILAsByteArray()!))==NativeRecipeSections.ExpectedIL) {
    harmony.Patch(native,prefix:new HarmonyMethod(typeof(NativeRecipeSections),nameof(NativeRecipeSections.Prefix)),
     postfix:new HarmonyMethod(typeof(NativeRecipeSections),nameof(NativeRecipeSections.Postfix)),
     transpiler:new HarmonyMethod(typeof(NativeRecipeSections),nameof(NativeRecipeSections.Transpiler)),
     finalizer:new HarmonyMethod(typeof(NativeRecipeSections),nameof(NativeRecipeSections.Finalizer)));
   } else api.Logger.Warning("Food Handbook: unknown native ingredient IL, grid/barrel adapter disabled");
   api.Logger.Notification("Kuboczoch Food Handbook: dedicated client process sections installed.");
  }
  catch (Exception error)
  {
   try { harmony?.UnpatchAll(HarmonyId); }
   catch (Exception cleanupError)
   {
    api.Logger.Warning("Kuboczoch Food Handbook patch cleanup failed: {0}", cleanupError.Message);
   }
   harmony = null;
   api.Logger.Warning("Kuboczoch Food Handbook disabled: patch installation failed: {0}", error.Message);
  }
 }
 public override void Dispose() { harmony?.UnpatchAll(HarmonyId); harmony=null; }
}
