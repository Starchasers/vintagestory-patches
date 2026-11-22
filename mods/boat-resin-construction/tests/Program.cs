using System.Reflection;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KuboczochPitchGlueCompat;
using RoughSeas.Shipyard;
using RoughSeas.SeaAndWeather;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

int passed=0;
void Check(bool ok,string name) { if(!ok) throw new Exception(name); passed++; Console.WriteLine("PASS "+name); }
string assets= args[0], own= args[1];
var resin=new Item { ItemId=1,Code=new("game:resin") };
var glue=new Item { ItemId=2,Code=new("game:glueportion-pitch-hot"), Variant=new Vintagestory.API.Util.RelaxedReadOnlyDictionary<string,string>(new Dictionary<string,string>{{"state","hot"}}),Attributes=new JsonObject(JObject.Parse("{waterTightContainerProps:{containable:true,itemsPerLitre:100}}")) };
var modRecord=new TestMod(); typeof(Mod).GetProperty(nameof(Mod.Info))!.SetValue(modRecord,new ModInfo {ModID="roughseas",Version="5.0.2"});
var loader=NativeProxy.Make<IModLoader>((m,a)=>m.Name=="GetMod"?modRecord:null);
var logger=NativeProxy.Make<ILogger>((m,a)=>null);
EnumAppSide side=EnumAppSide.Server;
long milliseconds=0;
var world=NativeProxy.Make<IWorldAccessor>((m,a)=>m.Name switch {
 "get_ElapsedMilliseconds"=> milliseconds+=10000, "get_Side"=>side,"GetItem"=> a?[0] is int id ? (id==1?resin:glue) : resin,
 "SearchItems"=>new[]{resin}.Where(i=>i.Code== (AssetLocation)a![0]!).ToArray(), _=>null });
var api=NativeProxy.Make<ICoreAPI>((m,a)=>m.Name switch {"get_World"=>world,"get_Side"=>side,"get_ModLoader"=>loader,"get_Logger"=>logger,_=>null});
var patch=new ResinConstructionMod();
var config=new FramingConfig {Pitch="game:glueportion-pitch-*",Rope="game:rope",Cloth="game:cloth-*",Laths="roughseas:lath-*",Poles="roughseas:pole-*",Planks="game:plank-*",Hides="game:hide-*",Oars="game:oar-*"};
var stage=new FramingStage {Work=FramingWork.Pitch,Seconds=7.5,Group="Pitch",Pieces=3,Litres=1};
var resinSlot=new DummySlot(new ItemStack(resin,5));
var bucket=new BlockBucket {BlockId=3,Code=new("game:woodbucket"),Attributes=new JsonObject(new JObject())};
typeof(CollectibleObject).GetField("api",BindingFlags.NonPublic|BindingFlags.Instance)!.SetValue(bucket,api);
var bucketSlot=new DummySlot(new ItemStack(bucket)); bucket.SetContent(bucketSlot.Itemstack!,new ItemStack(glue,400));
Check(Laying.For(stage,config) is PitchLaying,"unpatched Pitch uses native PitchLaying");
Check(!Laying.For(stage,config).Takes(resinSlot),"unpatched control rejects raw resin");
Check(Laying.For(stage,config).Takes(bucketSlot),"unpatched control accepts actual hot-pitch bucket");
patch.Start(api); patch.AssetsFinalize(api);
Check(!resin.HasBehavior<CollectibleBehaviorFraming>(),"unknown startup version does not attach resin behavior");
Check(Laying.For(stage,config) is PitchLaying,"unknown startup version does not patch factory");
modRecord.Info.Version="5.0.1";
var files=Directory.GetFiles(Path.Combine(assets,"entities"),"*.json",SearchOption.AllDirectories).Order().ToArray();
var configs=new List<(string File,string Key,FramingConfig Config)>();
foreach(var f in files) {
 var json=JObject.Parse(File.ReadAllText(f));
 foreach(var p in json.Descendants().OfType<JProperty>().Where(p=>p.Name=="stages" && p.Value is JArray)) {
  var obj=(JObject)p.Parent!;var parsed=obj.ToObject<FramingConfig>()!;
  var shapeName=json["client"]?["shape"]?["base"]?.Value<string>();
  if(shapeName!=null){var path=shapeName.Split(':').Last();parsed.Measure(JObject.Parse(File.ReadAllText(Path.Combine(assets,"shapes",path+".json"))));}
  configs.Add((Path.GetFileName(f),obj.Path,parsed));
 }
}
var baseline=configs.SelectMany(c=>c.Config.Stages.Select(s=>(s,Type:Laying.For(s,c.Config).GetType(),Json:JsonConvert.SerializeObject(s)))).ToArray();
Console.WriteLine($"DISCOVERY entities={files.Length}; configs={configs.Count}; vessels={string.Join(',',configs.Select(c=>c.File).Distinct())}");
Check(files.Length==7 && configs.Count>0,"enumerated every retained ship entity file and discovered native construction configs");
config=configs.Single(c=>c.File=="barkcanoe.json").Config;
stage=config.Stages.Single(s=>s.Work==FramingWork.Pitch);
Check(stage.Pieces==4&&stage.Seconds==7.5&&stage.Seals&&stage.Litres==0.25,"native shape measurement: four barkcanoe seams, 7.5s, sealing flag and original litres untouched");
patch.Start(api); patch.Start(api);
patch.AssetsFinalize(api);patch.AssetsFinalize(api);
Check(resin.CollectibleBehaviors.Count(b=>b is CollectibleBehaviorFraming)==1,"server resin registration idempotent");
side=EnumAppSide.Client; patch.AssetsFinalize(api);patch.AssetsFinalize(api);
Check(resin.CollectibleBehaviors.Count(b=>b is CollectibleBehaviorFraming)==1,"client registration preserves server-synchronized behavior");
resin.CollectibleBehaviors=[];patch.AssetsFinalize(api);
Check(resin.CollectibleBehaviors.Single() is CollectibleBehaviorFraming,"client local registry independently gains native framing behavior");
Check(patch.ShouldLoad(EnumAppSide.Server)&&patch.ShouldLoad(EnumAppSide.Client),"universal mod lifecycle");
Check(patch.ExecuteOrder()>new ShipyardModSystem().ExecuteOrder(),"finalization runs after retained RoughSeas system");
Check(Harmony.GetPatchInfo(typeof(Laying).GetMethod(nameof(Laying.For))!)!.Postfixes.Count(p=>p.owner==ResinConstructionMod.HarmonyId)==1,"repeated startup installs exactly one narrow postfix");
Check(Harmony.GetAllPatchedMethods().Count(m=>Harmony.GetPatchInfo(m)!.Owners.Contains(ResinConstructionMod.HarmonyId))==1,"no held-use/bucket/repair/ManualDishes patches");
var laying=Laying.For(stage,config);
Check(laying is PieceLaying && laying.Takes(resinSlot),"patched factory uses real native PieceLaying and accepts resin");
Check(!laying.Takes(bucketSlot) && !laying.Takes(new DummySlot(new ItemStack(glue))),"bucket glue and loose glue excluded");
Check(!laying.Takes(new DummySlot(new ItemStack(new Item {ItemId=4,Code=new("other:resin")}))) && !laying.Takes(new DummySlot()),"foreign resin and empty slot excluded");
Check(laying.Shown(world).Single().Collectible==resin && laying.Shown(world).Single().StackSize==1,"native Shown icon is one game:resin");
Check(laying.Action==ResinConstructionMod.ActionKey,"native action uses own localization key");
foreach(string lang in new[]{"en","pl"}) {
 var json=JObject.Parse(File.ReadAllText(Path.Combine(own,"assets/kuboczochpitchgluecompat/lang",lang+".json")));
 Check(json["shipyard-resin"]?.Value<string>() is {Length:>0},lang+" localized resin action exists");
}
int at=0,pitchStages=0;
foreach(var c in configs) foreach(var s in c.Config.Stages) {
 var b=baseline[at++]; var l=Laying.For(s,c.Config);string name=$"{c.File}:{c.Key}/{s.Group}/{s.Work}";
 Check(l.GetType()==(s.Work==FramingWork.Pitch?typeof(PieceLaying):b.Type),"factory scope "+name);
 Check(JsonConvert.SerializeObject(s)==b.Json,"stage duration/progression/flags unchanged "+name);
 if(s.Work==FramingWork.Pitch) {pitchStages++;Check(l.Takes(resinSlot)&&!l.Takes(bucketSlot),"resin-only requirement "+name);}
}
Check(pitchStages>0,"actual retained pitch stages exercised");
Console.WriteLine($"SCOPE entities={files.Length}; vessel configs={configs.Count}; stages={at}; Pitch={pitchStages}");
foreach(var file in files) Console.WriteLine($"ENTITY {Path.GetFileName(file)} construction-configs={configs.Count(c=>c.File==Path.GetFileName(file))}");
foreach(var c in configs) Console.WriteLine($"CONFIG {c.File}:{c.Key} stages={c.Config.Stages.Length} pitch={c.Config.Stages.Count(s=>s.Work==FramingWork.Pitch)}");
// Real native framing state and collectible hold lifecycle; world/actor/hull are headless fixtures.
side=EnumAppSide.Server;
var actor=new FixturePlayer {EntityId=91,Api=api,World=world}; actor.Controls.HandUse=EnumHandInteract.HeldItemInteract;
var target=new FixtureEntity(world,api);
typeof(Mod).GetProperty(nameof(Mod.Logger))!.SetValue(modRecord,logger);
var shipyard=new ShipyardModSystem();typeof(ModSystem).GetProperty(nameof(ModSystem.Mod))!.SetValue(shipyard,modRecord);
var hull=new FixtureHull(target,config,stage,shipyard);
target.Properties.Server.Behaviors=[hull];target.Properties.Client=new EntityClientProperties([],new()){Behaviors=[hull]};var selection=new EntitySelection {Entity=target};
void Reset() {resinSlot.Itemstack=new ItemStack(resin,5);hull.Reset();}
void Begin() {var h=EnumHandHandling.NotHandled;resin.OnHeldUseStart(resinSlot,actor,null!,selection,EnumHandInteract.HeldItemInteract,true,ref h);}
void Stop(float t=7.5f)=>resin.OnHeldUseStop(t,resinSlot,actor,null!,selection,EnumHandInteract.HeldItemInteract);
Reset();Begin();
Check(resin.OnHeldUseStep(7.3f,resinSlot,actor,null!,selection)==EnumHandInteract.HeldItemInteract,"native stage stays active before original duration");
Check(resin.OnHeldUseStep(7.5f,resinSlot,actor,null!,selection)==EnumHandInteract.None,"native stage completes at original duration");Stop();
Check(resinSlot.StackSize==4&&hull.Frame.Laid(stage)==1,"complete seam spends exactly one resin and increments native framing once");Stop();
Check(resinSlot.StackSize==4&&hull.Frame.Laid(stage)==1,"duplicate stop cannot consume or advance twice");
Begin();Stop();Begin();Stop();Begin();Stop();
Check(resinSlot.StackSize==1&&hull.Frame.Built,"four native measured seams cost four resin and complete original stage");
Check(hull.Frame.Sealed,"native seals flag retained: completed resin seams seal the hull");
Check(hull.WorkWith(bucketSlot)==null,"native EntityBehaviorFraming rejects glue bucket after patch");
Reset();Begin();Stop(7.3f);Check(resinSlot.StackSize==5&&hull.Frame.Laid(stage)==0,"early stop spends nothing and preserves progress");
Reset();Begin();resin.OnHeldUseCancel(2,resinSlot,actor,null!,selection,default);Stop();Check(resinSlot.StackSize==5&&hull.Frame.Laid(stage)==0,"cancel and later stop spend nothing");
side=EnumAppSide.Client;Reset();Begin();
Check(resin.OnHeldUseStep(7.3f,resinSlot,actor,null!,selection)==EnumHandInteract.HeldItemInteract,"client native lifecycle accepts resin and displays timed work before completion");
resin.OnHeldUseStep(7.5f,resinSlot,actor,null!,selection);Stop();
Check(resinSlot.StackSize==5&&hull.Frame.Laid(stage)==0,"client native lifecycle does not consume resin or mutate hull");
side=EnumAppSide.Server;
var saved=new TreeAttribute();saved.SetInt("Pitch",3);saved.SetString("wood","birch");saved.SetDouble("setsAt",123);saved.SetString("unrelated","keep");var before=saved.ToJsonToken().ToString();
var frame=new Framing(()=>saved,()=>{},new[]{stage});_ = Laying.For(stage,config);
Check(saved.ToJsonToken().ToString()==before&&frame.Laid(stage)==3&&!frame.Built,"existing partial save progress and unrelated state unchanged by substitution");
frame.Lay(null,0);Check(frame.Built&&saved.GetString("wood")=="birch"&&saved.GetDouble("setsAt")==123,"remaining seam completes old save without resetting wood or curing time");
patch.Dispose();Check(Laying.For(stage,config) is PitchLaying&&!Laying.For(stage,config).Takes(resinSlot),"unpatched removal control restores resin refusal");
Console.WriteLine($"RESULT {passed} checks passed; actual RoughSeas={typeof(Laying).Assembly.Location}; game={typeof(Item).Assembly.Location}");

public class FixtureHull:EntityBehaviorFraming {
 readonly FramingStage stage;
 public Framing Frame=> (Framing)AccessTools.Field(typeof(EntityBehaviorFraming),"framing").GetValue(this)!;
 public FixtureHull(Entity entity,FramingConfig config,FramingStage stage,ShipyardModSystem shipyard):base(entity){
  this.stage=stage;
  AccessTools.Field(typeof(EntityBehaviorFraming),"config").SetValue(this,config);
  AccessTools.Field(typeof(EntityBehaviorFraming),"layings").SetValue(this,new Dictionary<FramingStage,Laying>{{stage,Laying.For(stage,config)}});
  AccessTools.Property(typeof(EntityBehaviorBuild),"Shipyard").SetValue(this,shipyard);
  var clock=(SeaClock)Activator.CreateInstance(typeof(SeaClock),BindingFlags.NonPublic|BindingFlags.Instance,null,new object[]{(Func<double>)(()=>100),(Func<double>)(()=>0.5),(Func<double>)(()=>60)},null)!;
  sea=NativeProxy.Make<ISeaWeather>((m,a)=>m.Name=="get_Clock"?clock:null);
  Reset();
 }
 public void Reset(){var tree=new TreeAttribute();entity.WatchedAttributes.SetAttribute(EntityBehaviorBuild.TreeKey,tree);framing=new Framing(()=>tree,()=>{},new[]{stage});}
}
public class FixtureEntity:EntityAgent {public FixtureEntity(IWorldAccessor world,ICoreAPI api){World=world;Api=api;Properties=new EntityProperties{Server=new EntityServerProperties([],new()){Behaviors=new()}};}}
public class FixturePlayer:EntityPlayer {public override string GetName()=>"headless player"; public override void WalkInventory(OnInventorySlot handler){}}
public class TestMod:Mod {}
