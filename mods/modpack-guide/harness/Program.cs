using System.Reflection;
using System.Text.Json;
using System.IO.Compression;
using Vintagestory.API.Datastructures;
using System.Runtime.CompilerServices;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.GameContent;

static class Program {
 static int checks;
 static void Check(bool ok,string name) { if(!ok) throw new Exception(name); checks++; Console.WriteLine("PASS "+name); }
 static void Field(object o,string name,object? value) { for(Type? t=o.GetType();t!=null;t=t.BaseType) { var f=t.GetField(name,BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.DeclaredOnly); if(f!=null){f.SetValue(o,value);return;} } throw new Exception("No field "+name); }
 static T Bare<T>() => (T)RuntimeHelpers.GetUninitializedObject(typeof(T));
 static TreeAttribute Attr(JsonElement e) { var t=new TreeAttribute();foreach(var p in e.EnumerateObject()){if(p.Value.ValueKind==JsonValueKind.Object)t.SetAttribute(p.Name,Attr(p.Value));else t.SetString(p.Name,p.Value.GetString()!);}return t; }
 static void Main() {
 Check(GameVersion.ShortGameVersion=="1.22.7","exact API version 1.22.7");
 var logger=DispatchProxy.Create<ILogger,NullProxy>();
 Check(((VtmlTextToken)VtmlParser.Tokenize(logger,"A &amp; B")[0]).Text=="A &amp; B","native parser preserves amp entity literally; display-node normalization required");
 var tag=(VtmlTagToken)VtmlParser.Tokenize(logger,"<itemstack type=\"item\" code=\"game:stick|game:flint\"/>")[0];
 Check(tag.Name=="itemstack" && tag.Attributes["type"]=="item" && tag.Attributes["code"]=="game:stick|game:flint","native parser: compact pipe slideshow");
 var linked=VtmlParser.Tokenize(logger,"<itemstack type=\"item\" code=\"game:stick\"/> <a href=\"handbook://item-stick\">Stick</a>");
 Check(linked.Length==3 && ((VtmlTagToken)linked[2]).Attributes["href"]=="handbook://item-stick","native parser: adjacent icon and anchor");
 var stack=new ItemStack(new Item { Code=new AssetLocation("game:stick"), ItemId=1 });
 Check(GuiHandbookItemStackPage.PageCodeForStack(stack)=="item-stick","native item page omits game domain");
 var custom=new ItemStack(new Item { Code=new AssetLocation("example:widget"), ItemId=2 });
 Check(GuiHandbookItemStackPage.PageCodeForStack(custom)=="item-example:widget","native item page preserves custom domain");
 var block=new ItemStack(new Block { Code=new AssetLocation("game:stone-granite"), BlockId=1 });
 Check(GuiHandbookItemStackPage.PageCodeForStack(block)=="block-stone-granite","native block page prefix");
 stack.Attributes.SetInt("durability",17);
 Check(GuiHandbookItemStackPage.PageCodeForStack(stack)=="item-stick","native page ignores durability");
 stack.Attributes.SetString("material","oak");
 var attributed=GuiHandbookItemStackPage.PageCodeForStack(stack);
 Check(attributed.StartsWith("item-stick-")&&attributed.Contains("material")&&attributed.Contains("oak"),"native page retains material attribute suffix: "+attributed);
 var icon=Bare<SlideshowItemstackTextComponent>();
 typeof(RichTextComponentBase).GetProperty("BoundsPerLine")!.SetValue(icon,new[]{new LineRectangled(0,0,20,20)});
 Field(icon,"slot",new DummySlot(custom));
 int clicks=0; ItemStack? received=null;
 Field(icon,"onStackClicked",(Action<ItemStack>)(s=>{clicks++;received=s;}));
 icon.OnMouseDown(new MouseEvent(10,10,EnumMouseButton.Left,0));
 Check(clicks==1 && ReferenceEquals(received,custom),"real OnMouseDown calls supplied callback with displayed slot");
 icon.OnMouseDown(new MouseEvent(50,50,EnumMouseButton.Left,0));
 Check(clicks==1,"real OnMouseDown excludes outside bounds");
 Field(icon,"onStackClicked",null);
 icon.OnMouseDown(new MouseEvent(10,10,EnumMouseButton.Left,0));
 Check(clicks==1,"real OnMouseDown with null callback performs no navigation (VTML default)");
 var api=DispatchProxy.Create<ICoreClientAPI,NullProxy>();
 var proxy=(NullProxy)(object)api;
 string? routed=null; proxy.Protocols["handbook"]=c=>routed=c.Href;
 var link=Bare<LinkTextComponent>(); Field(link,"api",api); link.Href="handbook://item-example:widget";
 link.Trigger();
 Check(routed==link.Href,"real LinkTextComponent.Trigger dispatches handbook protocol");
 routed=null;
 Field(link,"onLinkClicked",(Action<LinkTextComponent>)(c=>routed="callback:"+c.Href));
 link.Trigger();
 Check(routed=="callback:"+link.Href,"explicit link callback overrides default protocol routing");
 using var inv=JsonDocument.Parse(File.ReadAllText("attribute_inventory.json")); int cases=0;
 foreach(var entry in inv.RootElement.GetProperty("codes").EnumerateArray()) foreach(var s in entry.GetProperty("stacks").EnumerateArray()) {
 var code=entry.GetProperty("code").GetString()!; bool isBlock=entry.GetProperty("kind").GetString()=="block";
 var sample=isBlock?new ItemStack(new Block{Code=new AssetLocation(code),BlockId=9}):new ItemStack(new Item{Code=new AssetLocation(code),ItemId=9});sample.Attributes=Attr(s.GetProperty("attributes"));
 var actual=GuiHandbookItemStackPage.PageCodeForStack(sample);var expected=s.GetProperty("pageCode").GetString()!;
 if(actual!=expected)throw new Exception("ATTR PAGE MISMATCH "+actual+" vs "+expected);
 var parsed=(VtmlTagToken)VtmlParser.Tokenize(logger,s.GetProperty("richtextLink").GetString()!)[0];
 if(parsed.Attributes["href"]!="handbook://"+actual)throw new Exception("VTML href mismatch "+code);cases++;
 }
 Check(cases==85,"all 85 native attributed stack page codes and VTML hrefs match exact game methods");
 using var zip=ZipFile.OpenRead("dist/KuboczochModpackGuide-1.4.0.zip");using var stream=zip.GetEntry("assets/kuboczochmodpackguide/lang/en.json")!.Open();using var language=JsonDocument.Parse(stream);int pages=0;
 foreach(var prop in language.RootElement.EnumerateObject())if(prop.Name.EndsWith("-text")){VtmlParser.Tokenize(logger,prop.Value.GetString()!);pages++;}
 Check(pages==118,"exact VTML parser tokenizes all 118 shipped custom pages");
 using var fixture=JsonDocument.Parse(File.ReadAllText("registration-input.json"));
 var registered=new List<GuiHandbookPage>();
 foreach(var p in fixture.RootElement.GetProperty("customPages").EnumerateArray()) registered.Add(new GuiHandbookTextPage{pageCode=p.GetProperty("pageCode").GetString()!,Title=p.GetProperty("title").GetString()!,Text=p.GetProperty("text").GetString()!});
 foreach(var s in fixture.RootElement.GetProperty("stacks").EnumerateArray()) {
 var c=s.GetProperty("code").GetString()!;var st=s.GetProperty("kind").GetString()=="block"?new ItemStack(new Block{Code=new AssetLocation(c),BlockId=10}):new ItemStack(new Item{Code=new AssetLocation(c),ItemId=10});st.Attributes=Attr(s.GetProperty("attributes"));var pg=Bare<GuiHandbookItemStackPage>();pg.Stack=st;registered.Add(pg);
 }
 var dialog=Bare<GuiDialogHandbook>();var registry=new Dictionary<string,int>();
 Field(dialog,"allHandbookPages",registered);Field(dialog,"pageNumberByPageCode",registry);
 Field(dialog,"createPageHandlerAsync",(OnCreatePagesDelegate)(()=>new List<GuiHandbookPage>()));
 typeof(GuiDialogHandbook).GetMethod("LoadPages_Async",BindingFlags.NonPublic|BindingFlags.Instance)!.Invoke(dialog,null);
 Check(!registry.ContainsKey("item-toolsmith:handle")&&!registry.ContainsKey("item-toolsmith:fabricstrips-leather")&&!registry.ContainsKey("item-game:flaxtwine"),"regression: plain attributed aliases and game-domain href are not registered");
 Check(registry.Count>100,"real GuiDialogHandbook.LoadPages_Async registers fixture custom and stack pages");
 int destinations=0;var absent=new HashSet<string>();
 void Walk(IEnumerable<VtmlToken> tokens) { foreach(var token in tokens) if(token is VtmlTagToken t){if(t.Name=="a"&&t.Attributes.TryGetValue("href",out var href)&&href.StartsWith("handbook://")){destinations++;if(!registry.ContainsKey(href.Substring(11)))absent.Add(href);} Walk(t.ChildElements);} }
 foreach(var prop in language.RootElement.EnumerateObject())if((prop.Name.StartsWith("toolsmith-items")||prop.Name.StartsWith("process-")||prop.Name.StartsWith("world-"))&&prop.Name.EndsWith("-text"))Walk(VtmlParser.Tokenize(logger,prop.Value.GetString()!));
 Check(absent.Count==0,"all "+destinations+" progression anchors resolve after native registration; missing: "+string.Join(",",absent));
 File.WriteAllText("native-registry.json",JsonSerializer.Serialize(registry,new JsonSerializerOptions{WriteIndented=true}));
 Console.WriteLine($"RESULT {checks} checks passed; no client GUI, network, modloader or downloaded mod assemblies executed.");
 }
}
public class NullProxy : DispatchProxy {
 public Dictionary<string,Action<LinkTextComponent>> Protocols=new();
 protected override object? Invoke(MethodInfo? m,object?[]? args) {
 if(m!.Name=="get_LinkProtocols") return Protocols;
 if(m.ReturnType==typeof(void)) return null;
 return m.ReturnType.IsValueType?Activator.CreateInstance(m.ReturnType):null;
 }
}
