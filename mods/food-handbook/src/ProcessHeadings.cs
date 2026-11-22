using Vintagestory.API.Client;
using Vintagestory.API.Config;
using Vintagestory.GameContent;
namespace Kuboczoch.FoodHandbook;
// Exact 1.22.7 native AddHeading has FOUR parameters, contrary to the preliminary review.
// Keep its plain behavior, 14px separator, translated text, newline and bold small font.
internal static class ProcessHeadings
{
 internal static void AddHeading(List<RichTextComponentBase> nodes, ICoreClientAPI api,string key,ref bool haveText,string? link=null)
 {
  if(link==null) {CollectibleBehaviorHandbookTextAndExtraInfo.AddHeading(nodes,api,key,ref haveText);return;}
  if(haveText) nodes.Add(new ClearFloatTextComponent(api,14));
  haveText=true;
  nodes.Add(new LinkTextComponent(api,Lang.Get(key)+"\n",CairoFont.WhiteSmallText().WithWeight(Cairo.FontWeight.Bold),null){Href=link});
 }
}
