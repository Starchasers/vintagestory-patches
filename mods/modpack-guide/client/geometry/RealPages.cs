using System.IO.Compression;
using System.Reflection;
using System.Text.Json;
using KuboczochModpackGuide.Client;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.GameContent;

public static class RealPages
{
 public static void Run()
 {
  var logger=Proxy.Make<ILogger>((m,a)=>null);
  var registry=Proxy.Make<IClassRegistryAPI>((m,a)=>m.ReturnType==typeof(Dictionary<string,Type>)?new Dictionary<string,Type>():null);
  using var fixture=JsonDocument.Parse(File.ReadAllText("registration-input.json"));
  var items=new Dictionary<string,Item>();var blocks=new Dictionary<string,Block>();int id=1;
  foreach(var s in fixture.RootElement.GetProperty("stacks").EnumerateArray())
  {
   string code=s.GetProperty("code").GetString()!;
   if(s.GetProperty("kind").GetString()=="block")blocks.TryAdd(code,new Block{Code=new AssetLocation(code),BlockId=id++});
   else items.TryAdd(code,new Item{Code=new AssetLocation(code),ItemId=id++});
  }
  var world=Proxy.Make<IClientWorldAccessor>((m,a)=>m.Name switch {"GetItem"=>items.GetValueOrDefault(a![0]!.ToString()!),"GetBlock"=>blocks.GetValueOrDefault(a![0]!.ToString()!),"get_Logger"=>logger,_=>null});
  string? routed=null;
  var protocols=new Dictionary<string,Action<LinkTextComponent>>{["handbook"]=l=>routed=l.Href};
  var api=Proxy.Make<ICoreClientAPI>((m,a)=>m.Name switch {"get_Logger"=>logger,"get_ClassRegistry"=>registry,"get_World"=>world,"get_LinkProtocols"=>protocols,_=>null});
  var family=new ToolsmithFamilySlideshows(File.ReadAllText("client/assets/kuboczochmodpackguide/config/toolsmith-families.json"));
  VtmlUtil.TagConverters[ToolsmithFamilySlideshows.TagName]=family.Convert;
  VtmlUtil.TagConverters[ToolsmithTable.TagName]=ToolsmithTable.Convert;
  VtmlUtil.TagConverters["toolsmithitem"]=ToolsmithItemReferences.Convert;
  using var zip=ZipFile.OpenRead("dist/KuboczochModpackGuide-1.3.0.zip");
  using var stream=zip.GetEntry("assets/kuboczochmodpackguide/lang/en.json")!.Open();
  using var lang=JsonDocument.Parse(stream);
  var nativeCodes=JsonDocument.Parse(File.ReadAllText("native-registry.json")).RootElement.EnumerateObject().Select(x=>x.Name).ToHashSet();
  int layouts=0,cellChecks=0,clicks=0;
  foreach(var page in lang.RootElement.EnumerateObject().Where(x=>x.Name.StartsWith("toolsmith-items")&&x.Name.EndsWith("-text")))
  foreach(float scale in new[]{0.75f,1f,1.5f,2f})foreach(double width in new[]{240.0,320.0,420.0,550.0})
  {
   RuntimeEnv.GUIScale=scale;
   var components=VtmlUtil.Richtextify(api,page.Value.GetString(),CairoFont.WhiteSmallText(),_=>{});
   var outer=new GuiElementRichtext(api,components,ElementBounds.Fixed(0,0,width,1));outer.CalcHeightAndPositions();layouts++;
   foreach(var icon in components.OfType<SlideshowItemstackTextComponent>())foreach(var stack in icon.Itemstacks)
   {
    string dest=GuiHandbookItemStackPage.PageCodeForStack(stack);
    if(!nativeCodes.Contains(dest))throw new Exception("Unregistered entry icon "+dest);
    typeof(SlideshowItemstackTextComponent).GetField("slot",BindingFlags.Instance|BindingFlags.NonPublic)!.SetValue(icon,new DummySlot(stack));
    var b=icon.BoundsPerLine[0];routed=null;
    icon.OnMouseDown(new MouseEvent((int)(b.X+b.Width/2),(int)(b.Y+b.Height/2),EnumMouseButton.Left,0));
    if(routed!="handbook://"+dest)throw new Exception("Wrong entry icon click "+dest);
    clicks++;
   }
   foreach(var table in components.OfType<ToolsmithTable>())foreach(var cell in table.Cells)
   {
    if(cell.ContentWidth<=0||cell.X<0||cell.X+cell.Width>width*scale+1)throw new Exception("cell geometry "+page.Name);
    foreach(var c in cell.Components)foreach(var b in c.BoundsPerLine)
     if(b.X<-.1||b.X+b.Width>cell.ContentWidth+1.1)throw new Exception($"Overflow {page.Name} width={width} scale={scale} component={c.GetType().Name} x={b.X} w={b.Width} available={cell.ContentWidth}");
    foreach(var icon in cell.Components.OfType<SlideshowItemstackTextComponent>())foreach(var stack in icon.Itemstacks)
    {
     string dest=GuiHandbookItemStackPage.PageCodeForStack(stack);
     if(!nativeCodes.Contains(dest))throw new Exception("Unregistered icon "+dest);
     typeof(SlideshowItemstackTextComponent).GetField("slot",BindingFlags.Instance|BindingFlags.NonPublic)!.SetValue(icon,new DummySlot(stack));
     var b=icon.BoundsPerLine[0];routed=null;
     table.OnMouseDown(new MouseEvent((int)(cell.ContentX+b.X+b.Width/2),(int)(cell.ContentY+b.Y+b.Height/2),EnumMouseButton.Left,0));
     if(routed!="handbook://"+dest)throw new Exception("Wrong icon click "+dest+" => "+routed);
     clicks++;
    }
    cellChecks++;
   }
  }
  RuntimeEnv.GUIScale=1;
  Console.WriteLine($"REAL PAGES PASS: {layouts} layouts; {cellChecks} cell bounds; {clicks} displayed stack callbacks. Trusted API/font geometry; no game GUI.");
 }
}
