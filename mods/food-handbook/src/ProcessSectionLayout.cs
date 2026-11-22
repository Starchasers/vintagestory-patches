using Vintagestory.API.Client;
using Vintagestory.API.Common;
namespace Kuboczoch.FoodHandbook;

// Shared by ACA concrete outputs and captured native grid/barrel outputs.
internal static class ProcessSectionLayout
{
 // Inline advances GuiElementRichtext's cursor; Left only reserves a float area.
 // Native handbook output rows likewise use EnumFloat.Inline (numeric 1).
 internal const EnumFloat OutputFloat = EnumFloat.Inline;
 internal static SlideshowItemstackTextComponent Icon(ICoreClientAPI api, ItemStack output, Action<ItemStack> open)
  => new(api,new[]{output},40,OutputFloat,open);
 internal static void BeginRow(List<RichTextComponentBase> nodes,ICoreClientAPI api)
  => nodes.Add(new ClearFloatTextComponent(api,2));
 internal static void EndRow(List<RichTextComponentBase> nodes,ICoreClientAPI api)
  => nodes.Add(new ClearFloatTextComponent(api,3));
}
