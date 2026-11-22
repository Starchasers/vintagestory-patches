using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;
namespace Kuboczoch.FoodHandbook;
// Routing only; no longer installed as a helper postfix.
internal static class IngredientComponentsPostfix
{
 internal static string Destination(ICoreClientAPI capi, ItemStack target) => PageDestination.Resolve(
  () => target.Collectible.GetCollectibleInterface<IHandBookPageCodeProvider>()?.HandbookPageCodeForStack(capi.World,target),
  () => GuiHandbookItemStackPage.PageCodeForStack(target));
}
