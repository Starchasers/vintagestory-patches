using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using HarmonyLib;
using KuboczochWoodworkingTutorial;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

class Program
{
    static int passed;
    static string root="";
    static void Main()
    {
        root=Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,"../../../.."));
        var game=Path.Combine(root,"vs");
        AssemblyLoadContext.Default.Resolving+=(ctx,name)=>{
            var file=new[]{game,Path.Combine(game,"Lib"),Path.Combine(game,"Mods")}.Select(dir=>Path.Combine(dir,name.Name+".dll")).FirstOrDefault(File.Exists);
            return file==null?null:ctx.LoadFromAssemblyPath(file);
        };
        var cairo=AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(game,"Lib/cairo-sharp.dll"));
        System.Runtime.InteropServices.NativeLibrary.SetDllImportResolver(cairo,(name,assembly,path)=>
            name=="libcairo-2"?System.Runtime.InteropServices.NativeLibrary.Load("libcairo.so.2"):IntPtr.Zero);
        Run();
    }
    static void Check(bool ok,string name) {if(!ok) throw new Exception("FAIL: "+name); Console.WriteLine("PASS: "+name); passed++;}
    [MethodImpl(MethodImplOptions.NoInlining)]
    static void Run()
    {
        Check(typeof(WoodworkingTutorialMod).Assembly.GetName().Version?.ToString()=="1.3.0.0","assembly version matches release manifest");
        var init=AccessTools.DeclaredMethod(typeof(FirstStepsTutorial),"initTutorialSteps");
        var field=AccessTools.Field(typeof(TutorialBase),"steps");
        var code=AccessTools.Field(typeof(TutorialStepBase),"code");
        List<TutorialStepBase> Fresh() {var t=new FirstStepsTutorial(null!); init.Invoke(t,null); return (List<TutorialStepBase>)field.GetValue(t)!;}
        var steps=Fresh(); var original=steps.ToArray();
        int pos=steps.FindIndex(s=>(string?)code.GetValue(s)=="craftfirewood");
        Check(steps.Count==21,"exact vanilla tutorial has 21 steps");
        Check(TutorialPatch.Replace(steps,null!) && steps.Count==22,"firewood replaced with exactly TWO tracked stages");
        Check(steps.Where((s,i)=>i<pos || i>pos+1).SequenceEqual(original.Where((s,i)=>i!=pos)),"other 20 vanilla tasks unchanged and in order");
        Check(steps.Select((s,i)=>s.index==i).All(x=>x),"indices renumbered");
        Check(steps.Select(s=>(string?)code.GetValue(s)).Distinct().Count()==22,"save IDs unique");
        var stage=steps.Skip(pos).Take(2).ToArray();
        Check(stage[0].text==TutorialPatch.Prefix+"setup" && stage[1].text==TutorialPatch.TextKey,"correct combined setup and splitting text keys");
        Check((string?)code.GetValue(stage[1])=="craftfirewood","final count preserves original save ID");
        Check(!TutorialPatch.Replace(steps,null!) && steps.Count==22,"duplicate installation does not add stages");
        var block=new Block{Code=new AssetLocation("immersivewoodworking:choppingblock")};
        var half=new ItemStack(new Item{Code=new AssetLocation("immersivewoodworking:halflog")},1);
        var firewood=new ItemStack(new ItemFirewood(),2);
        Check(!stage[0].OnItemStackReceived(new ItemStack(block),"onitemcrafted") && !stage[0].Complete,"crafting alone does not finish combined setup stage");
        Check(!stage[0].OnBlockPlaced(null!,new Block{Code=new AssetLocation("game:choppingblock")},null!),"wrong-domain block rejected");
        Check(stage[0].OnBlockPlaced(null!,block,null!) && stage[0].Complete,"placement completes combined craft-and-place stage");
        Check(!stage[1].OnItemStackReceived(half,"onitemcollected") && !stage[1].Complete,"half-log collection does not finish combined splitting stage");
        Check(!stage[1].OnItemStackReceived(firewood,"onitemcrafted"),"disabled vanilla craft event ignored");
        Check(stage[1].OnItemStackReceived(firewood,"onitemcollected") && !stage[1].Complete,"two collected firewood remain partial");
        var saved=new JsonObject(new JObject()); foreach(var s in stage) s.ToJson(saved);
        var restored=Fresh(); TutorialPatch.Replace(restored,null!); var restoredStages=restored.Skip(pos).Take(2).ToArray();
        foreach(var s in restoredStages) s.FromJson(saved);
        Check(restoredStages[0].Complete && !restoredStages[1].Complete,"save/load preserves stage boundary");
        Check(restoredStages[1].OnItemStackReceived(firewood,"onitemcollected") && restoredStages[1].Complete,"saved partial firewood completes at four");
        var legacy=new JsonObject(JObject.Parse("{\"kuboczoch_iw_placeblock\":{\"achieved\":1},\"kuboczoch_iw_makeblock\":{\"achieved\":1},\"kuboczoch_iw_halflog\":{\"achieved\":1},\"craftfirewood\":{\"achieved\":2}}"));
        foreach(var s in stage) {s.Restart(); s.FromJson(legacy);}
        Check(stage[0].Complete && !stage[1].Complete,"v1.1.0 four-stage save migrates to two-stage flow");
        Check(stage[1].OnItemStackReceived(firewood,"onitemcollected") && stage[1].Complete,"v1.1.0 partial count retained");
        var oldSave=new JsonObject(new JObject()); original[pos].Skip(); original[pos].ToJson(oldSave);
        foreach(var s in stage) {s.Restart(); s.FromJson(oldSave);}
        Check(stage.All(s=>s.Complete),"old completed firewood saves satisfy both stages");
        foreach(var s in stage) s.Restart();
        Check(stage.All(s=>!s.Complete),"restart clears both stages");
        foreach(var s in stage) {s.Skip(); Check(s.Complete,"each combined stage independently skippable: "+s.text);}
        var inv=new InventoryGeneric(2,"test-1",null!); inv[0].Itemstack=new ItemStack(block);
        var manager=DispatchProxy.Create<IPlayerInventoryManager,InventoryProxy>();
        ((InventoryProxy)(object)manager).Inventories=new Dictionary<string,IInventory>{{"hotbar",inv}};
        stage[0].Restart(); Check(!stage[0].DoCheckPlayerInventory(manager) && !stage[0].Complete,"unplaced block in inventory does not finish setup");
        inv[0].Itemstack=new ItemStack(new ItemFirewood(),4); stage[1].Restart();
        Check(stage[1].DoCheckPlayerInventory(manager) && stage[1].Complete,"existing four firewood count");
        Check(!TutorialPatch.Replace(new List<TutorialStepBase>(),null!),"missing target is safe");
        Check(!TutorialPatch.Replace(new List<TutorialStepBase>{original[pos],original[pos]},null!),"duplicate target is safe");
        var flow=new FirstStepsTutorial(null!); init.Invoke(flow,null);
        var flowSteps=(List<TutorialStepBase>)field.GetValue(flow)!; TutorialPatch.Replace(flowSteps,null!);
        foreach(var s in flowSteps.Take(pos)) {s.Skip(); s.ToJson(flow.StepDataForSaving);}
        for(int i=0;i<2;i++) {
            var visible=flow.GetTutorialSteps(true);
            Check(visible.Count==1 && ReferenceEquals(visible[0],flowSteps[pos+i]),"HUD shows only combined stage "+(i+1));
            visible[0].Skip(); visible[0].ToJson(flow.StepDataForSaving);
        }
        Check(ReferenceEquals(flow.GetTutorialSteps(true).Single(),flowSteps[pos+2]),"two stages return to next vanilla task");
        var harmony=new Harmony("kuboczoch.tests");
        harmony.Patch(init,postfix:new HarmonyMethod(typeof(TutorialPatch),nameof(TutorialPatch.Postfix)));
        Check(Fresh().Count==22,"real Harmony hook adds exactly two stages");
        var skipAssembly=AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(root,"skip/src/bin/Release/net10.0/KuboczochTutorialSkip.dll"));
        var hud=AccessTools.Method(typeof(HudTutorial),"loadHud");
        harmony.Patch(hud,transpiler:new HarmonyMethod(skipAssembly.GetType("KuboczochTutorialSkip.HudPatch")!,"Transpile"));
        Check(Fresh().Count==22 && Harmony.GetPatchInfo(hud)!.Transpilers.Count>0,"separate skip patch coexists");
        harmony.UnpatchAll("kuboczoch.tests"); Check(Fresh().Count==21,"unpatch restores vanilla");
        Vintagestory.API.Config.RuntimeEnv.GUIScale=1;
        typeof(Vintagestory.API.Config.Lang).GetProperty("CurrentLocale")!.SetValue(null,"en");
        var languages=(System.Collections.IDictionary)Vintagestory.API.Config.Lang.AvailableLanguages;
        languages["en"]=DispatchProxy.Create(languages.GetType().GenericTypeArguments[1],typeof(NullProxy));
        var layoutApi=DispatchProxy.Create<ICoreClientAPI,NullProxy>();
        foreach(var locale in new[]{"en","pl"}) {
            var path=Path.Combine(root,"package/assets/kuboczochwoodworkingtutorial/lang",locale+".json");
            var texts=System.Text.Json.JsonSerializer.Deserialize<Dictionary<string,string>>(File.ReadAllText(path))!;
            Check(texts.Count==2 && texts.ContainsKey("setup") && texts.ContainsKey("firewood"),"exactly two localized stage texts: "+locale);
            foreach(var pair in texts) {
                var font=CairoFont.WhiteSmallText().WithLineHeightMultiplier(1.5).WithFontSize(18);
                foreach(var width in new[]{260,300}) {
                    var components=VtmlUtil.Richtextify(layoutApi,pair.Value.Replace("{0}","4"),font);
                    Check(components.OfType<SlideshowItemstackTextComponent>().Count()==3,"three native item previews: "+locale+"/"+pair.Key+"/"+width);
                    Check(components.OfType<LinkTextComponent>().Count()==3,"three clickable handbook references: "+locale+"/"+pair.Key+"/"+width);
                    var rich=new GuiElementRichtext(null!,components,ElementBounds.Fixed(0,0,width,200));
                    rich.CalcHeightAndPositions();
                    Console.WriteLine($"LAYOUT {locale}/{pair.Key} width={width}: {rich.Bounds.fixedHeight:F1} UI units");
                    Check(double.IsFinite(rich.Bounds.fixedHeight) && rich.Bounds.fixedHeight<=240,"compact native layout: "+locale+"/"+pair.Key+"/"+width);
                }
                foreach(System.Text.RegularExpressions.Match match in System.Text.RegularExpressions.Regex.Matches(pair.Value,@"<itemstack type=""(item|block)"">([^<]+)</itemstack>")) {
                    var asset=new AssetLocation(match.Groups[2].Value);
                    ItemStack stack=match.Groups[1].Value=="item"?new ItemStack(new Item{Code=asset}):new ItemStack(new Block{Code=asset});
                    var target=GuiHandbookItemStackPage.PageCodeForStack(stack);
                    Check(pair.Value.Contains("handbook://"+target+"\""),"handbook target matches native page code: "+target);
                }
                var visible=System.Text.RegularExpressions.Regex.Replace(pair.Value,@"<itemstack[^>]*>.*?</itemstack>","");
                visible=System.Text.RegularExpressions.Regex.Replace(visible,"<[^>]+>","");
                Check(!System.Text.RegularExpressions.Regex.IsMatch(visible,"immersive|modpack|settings|already|inventory",System.Text.RegularExpressions.RegexOptions.IgnoreCase),"professional in-world wording: "+locale+"/"+pair.Key);
            }
        }
        Console.WriteLine($"All {passed} checks passed.");
    }
}
