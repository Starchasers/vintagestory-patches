using System.Reflection;
using System.Text.Json.Nodes;
using System.IO.Compression;
using KuboczochModpackGuide.Client;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;

/// Actual release VTML and exact static registrations, trusted 1.22.7 font/layout/click API.
/// This is not a game-startup fixture and does not load installed mod assemblies.
public static class ProcessPages
{
 public static void Run()
 {
  var source=JsonNode.Parse(File.ReadAllText("process-reference-inventory.json"))!;
  var all=new Dictionary<string,CollectibleObject>();int id=0;
  foreach(var row in source["resolved"]!.AsArray())
  {
   var code=row!["code"]!.GetValue<string>();bool block=row["type"]!.GetValue<string>()=="block";
   CollectibleObject obj=block?new Block {BlockId=++id}:new Item {ItemId=++id};obj.Code=new AssetLocation(code);all[code]=obj;
  }
  var logger=Proxy.Make<ILogger>((m,a)=>null);
  var registry=Proxy.Make<IClassRegistryAPI>((m,a)=>m.ReturnType==typeof(Dictionary<string,Type>)?new Dictionary<string,Type>():null);
  var world=Proxy.Make<IClientWorldAccessor>((m,a)=>m.Name switch {"GetItem"=>all.TryGetValue(((AssetLocation)a![0]!).ToString(),out var i)?i as Item:null,"GetBlock"=>all.TryGetValue(((AssetLocation)a![0]!).ToString(),out var b)?b as Block:null,"get_Logger"=>logger,_=>null});
  string? routed=null;
  var protocols=new Dictionary<string,Action<LinkTextComponent>> { ["handbook"]=l=>routed=l.Href };
  var api=Proxy.Make<ICoreClientAPI>((m,a)=>m.Name switch {"get_Logger"=>logger,"get_ClassRegistry"=>registry,"get_World"=>world,"get_LinkProtocols"=>protocols,_=>null});
  var system=new ToolsmithItemReferences();system.StartClientSide(api);
  using var zip=ZipFile.OpenRead("dist/KuboczochModpackGuide-1.4.0.zip");
  using var reader=new StreamReader(zip.GetEntry("assets/kuboczochmodpackguide/lang/en.json")!.Open());
  var lang=JsonNode.Parse(reader.ReadToEnd())!.AsObject();
  var native=JsonNode.Parse(File.ReadAllText("native-registry.json"))!.AsObject().Select(x=>x.Key).ToHashSet();
  int pages=0,icons=0,anchors=0,checks=0;
  void Check(bool b,string why){if(!b)throw new Exception(why);checks++;}
  foreach(var (key,value) in lang.Where(x=>(x.Key.StartsWith("process-")||x.Key.StartsWith("world-"))&&x.Key.EndsWith("-text")))
  {
   pages++;
   foreach(double scale in new[]{0.75,1.0,1.5,2.0})foreach(double width in new[]{240.0,320.0,420.0,550.0})
   {
    RuntimeEnv.GUIScale=(float)scale;int clicked=0;
    var components=VtmlUtil.Richtextify(api,value!.GetValue<string>(),CairoFont.WhiteSmallText(),l=>{routed=l.Href;clicked++;});
    var rich=new GuiElementRichtext(api,components,ElementBounds.Fixed(0,0,width,1));rich.CalcHeightAndPositions();
    Check(double.IsFinite(rich.TotalHeight)&&rich.TotalHeight>0,key+" finite positive layout");
    foreach(var component in components)foreach(var b in component.BoundsPerLine.Where(b=>b.Width>0))
     Check(double.IsFinite(b.X)&&double.IsFinite(b.Y)&&b.X>=-0.1&&b.X+b.Width<=width*scale+1.1,$"{key} width={width} scale={scale} contained x={b.X} w={b.Width} text={(component as RichTextComponent)?.DisplayText}");
    foreach(var icon in components.OfType<SlideshowItemstackTextComponent>())
    {
     icons++;Check(icon.Itemstacks.Length>0,key+" native icon stacks");
     typeof(SlideshowItemstackTextComponent).GetField("slot",BindingFlags.Instance|BindingFlags.NonPublic)!.SetValue(icon,new DummySlot(icon.Itemstacks[0]));
     var b=icon.BoundsPerLine[0];routed=null;
     icon.OnMouseDown(new MouseEvent((int)(b.X+b.Width/2),(int)(b.Y+b.Height/2),EnumMouseButton.Left,0));
     Check(routed!=null&&native.Contains(routed!.Replace("handbook://","")),key+" actual item callback routes registered native page: "+routed);
    }
    foreach(var link in components.OfType<LinkTextComponent>())
    {
     anchors++;int before=clicked;var b=link.BoundsPerLine.First(x=>x.Width>0);routed=null;
     var down=new MouseEvent((int)Math.Ceiling(b.X+2),(int)Math.Ceiling(b.Y+2),EnumMouseButton.Left,0);link.OnMouseDown(down);
     var up=new MouseEvent(down.X,down.Y,EnumMouseButton.Left,0);link.OnMouseUp(up);
     Check(clicked==before+1&&routed==link.Href&&native.Contains(link.Href.Replace("handbook://","")),key+" actual anchor callback and exact destination "+link.Href);
    }
   }
   Console.WriteLine("PROCESS PAGE "+key+" geometry/callbacks PASS 4 widths x 4 scales");
  }
  RuntimeEnv.GUIScale=1;system.Dispose();
  Console.WriteLine($"PROCESS PASS pages={pages} layouts={pages*16} iconCallbacks={icons} anchorCallbacks={anchors} assertions={checks}; no live GUI or mod startup");
 }
}
