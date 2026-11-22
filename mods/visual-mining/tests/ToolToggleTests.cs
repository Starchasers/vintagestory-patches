#nullable enable
using System.Collections;
using System.Reflection;
using HarmonyLib;
using Kuboczoch.VisualMining;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;

// Headless production-path regression: only the engine's external world is mocked.
internal static class ToolToggleTests {
 const BindingFlags All = BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static;
 static int checks;
 static readonly List<string> Failures=new();
 static void Check(bool value,string why) { checks++; if(!value)Failures.Add(why); }
 static FieldInfo Field(Type type,string name) { for(Type? t=type;t!=null;t=t.BaseType){var f=t.GetField(name,All|BindingFlags.DeclaredOnly);if(f!=null)return f;}throw new MissingFieldException(type.FullName,name); }
 static void Set(object target,string name,object? value)=>Field(target.GetType(),name).SetValue(target,value);
 static object? Get(object target,string name)=>Field(target.GetType(),name).GetValue(target);
 static object? Call(object target,string name,params object?[] args)=>target.GetType().GetMethod(name,All)!.Invoke(target,args);
 static object Proxy(Type type,System.Func<MethodInfo,object?[]?,object?> handler)=>typeof(StrictProxy).GetMethod(nameof(StrictProxy.Create))!.MakeGenericMethod(type).Invoke(null,new object[]{handler})!;
 static readonly Dictionary<MethodBase,object?> PlayerValues=new();
 static bool PlayerProperty<T>(ref T __result, MethodBase __originalMethod) {__result=(T)PlayerValues[__originalMethod]!;return false;}
 static void InstallPlayerSeam(Harmony harmony) {
  var type=typeof(Vintagestory.Client.NoObf.ClientPlayer);
  foreach(var method in type.GetMethods(All).Where(m=>m.Name.EndsWith("get_Entity")||m.Name.EndsWith("get_InventoryManager")||m.Name.EndsWith("get_WorldData")||m.Name.EndsWith("get_CurrentBlockSelection")||m.Name.EndsWith("get_CurrentEntitySelection")||m.Name.EndsWith("get_CameraMode")))
   harmony.Patch(method,prefix:new HarmonyMethod(typeof(ToolToggleTests).GetMethod(nameof(PlayerProperty),All)!.MakeGenericMethod(method.ReturnType)));
 }
 public static void Run(string game, AnimationManager readyManager) {
  checks=0;Failures.Clear();ConfigSerialization();ConfigStartup();
  var prior=Field(typeof(VisualMiningMod),"Instance").GetValue(null);
  var renderer=typeof(CosmeticVoxelRenderer);var oldApi=Field(renderer,"api").GetValue(null);var oldGuard=Field(renderer,"allowPresentation").GetValue(null);
  var harmony=new Harmony("kuboczoch.tests.tool-toggles");
  try {
   VisualMiningMod.InstallCorePatches(harmony);InstallPlayerSeam(harmony);
   for(int mask=0;mask<8;mask++) foreach(var tool in new[]{EnumTool.Pickaxe,EnumTool.Axe,EnumTool.Shovel}) foreach(bool offhand in new[]{false,true}) foreach(var camera in new[]{EnumCameraMode.FirstPerson,EnumCameraMode.ThirdPerson}) {
    using var f=new Fixture(readyManager,mask,tool,offhand,camera);
    bool expected=f.Config.For(tool)!.Enabled;
    // Real Harmony-patched engine getter, plus direct postfix with a nonstandard vanilla result.
    string vanilla="another-mod-original"; object?[] args={f.Slot,f.Entity,vanilla};
    typeof(VisualMiningMod).GetMethod("AnimationPostfix",All)!.Invoke(null,args);
    Check((string)args[2]! == (expected?f.Code:vanilla),f.Label+" direct animation postfix preserves vanilla exactly when disabled");
    var got=f.Slot.Itemstack!.Collectible.GetHeldTpHitAnimation(f.Slot,f.Entity);
    Check(got==(expected?f.Code:"vanilla-test-hit"),f.Label+" real engine held animation getter");
    Check((bool)Call(f.Mod,"ShouldSuppressSound",f.Player,f.Selection,f.Slot)! ==expected,f.Label+" sound predicate");
    if(expected) EnabledObservationLifecycle(f,tool);
    Call(f.Mod,"Observe",f.Damage);
    Check((Get(f.Mod,"damage")!=null)==expected,f.Label+" observe eligibility");
    Check(f.Entries.Count==(expected?1:0),f.Label+" disabled tool never creates voxel damage");
    if(expected) {
     CosmeticVoxelRenderer.CommitImpact(f.Damage,f.Selection,tool);
     var entry=f.Entries.Values.Cast<object>().Single(); Set(entry,"MeshReplaced",true);
     Check(!f.CrackPrefix(),f.Label+" live renderer gate suppresses vanilla decal for enabled tool");
    } else Check(f.CrackPrefix(),f.Label+" live renderer gate preserves vanilla decal for disabled tool");
    // A tool switch must clear stale damage, selection, contact and carve state, and stop
    // only this mod's animations in BOTH camera managers (including FP variants).
    if(expected) {
     f.SeedOwnedAnimations();
     ((ContactTracker)Get(f.Mod,"contact")!).Sample("stale-target",0,8,6);
     f.Config.For(tool)!.Enabled=false;
     Check(f.CrackPrefix(),f.Label+" stale mesh replacement immediately restores vanilla decal when tool disabled");
     Call(f.Mod,"OnTick",.01f);
     Check(Get(f.Mod,"damage")==null && Get(f.Mod,"selection")==null && Get(f.Mod,"stack")==null,f.Label+" tick clears observation after disabling");
     Check(f.Entries.Count==0,f.Label+" tick restores vanilla voxel state");
     var contact=(ContactTracker)Get(f.Mod,"contact")!;
     Check(Get(contact,"identity")==null && !(bool)Get(contact,"fired")! && (float)Get(contact,"previous")! ==0,f.Label+" tick actually clears primed contact state");
     Check(!contact.Sample("new-target",0,0,6)&&contact.Sample("new-target",0,7,6),f.Label+" reset contact has no stale crossing");
     foreach(var manager in new IAnimationManager[]{f.Entity.Tp,f.Entity.Fp}) {
      Check(!manager.ActiveAnimationsByAnimCode.Keys.Any(k=>k.StartsWith("kvm-",StringComparison.Ordinal)),f.Label+" reset removes owned FP/TP animations");
      Check(manager.IsAnimationActive("walk"),f.Label+" reset preserves other animations");
     }
     f.Config.For(tool)!.Enabled=true;Call(f.Mod,"Observe",f.Damage);f.SeedOwnedAnimations();
     var switchedTool=tool==EnumTool.Pickaxe?EnumTool.Axe:EnumTool.Pickaxe;
     f.Config.For(switchedTool)!.Enabled=false;
     ((ContactTracker)Get(f.Mod,"contact")!).Sample("before-real-tool-switch",0,8,6);
     f.Slot.Itemstack=new ItemStack(new Item{Tool=switchedTool,HeldTpHitAnimation="vanilla-switched-held-hit"});
     Check(f.Slot.Itemstack.Collectible.GetHeldTpHitAnimation(f.Slot,f.Entity)=="vanilla-switched-held-hit",f.Label+" switching to disabled supported tool immediately preserves vanilla held-hit");
     Call(f.Mod,"OnTick",.01f);
     Check(Get(f.Mod,"damage")==null && Get(f.Mod,"selection")==null && Get(f.Mod,"stack")==null && f.Entries.Count==0,f.Label+" actual enabled-to-disabled tool switch clears observation and carve");
     Check(Get(Get(f.Mod,"contact")!,"identity")==null,f.Label+" actual tool switch clears contact");
     foreach(var manager in new IAnimationManager[]{f.Entity.Tp,f.Entity.Fp}) {
      Check(!manager.ActiveAnimationsByAnimCode.Keys.Any(k=>k.StartsWith("kvm-",StringComparison.Ordinal)),f.Label+" actual tool switch stops owned animations in both managers");
      Check(manager.IsAnimationActive("walk") && manager.IsAnimationActive("vanilla-test-hit"),f.Label+" tool switch leaves vanilla walk and held-hit animations alone");
     }
    }
    // Fresh disabled observation also resets previously tracked state, not just early-returning.
    f.Config.For(tool)!.Enabled=false; Call(f.Mod,"Observe",f.Damage);Call(f.Mod,"OnTick",.01f);
    Check(Get(f.Mod,"damage")==null && f.Entries.Count==0,f.Label+" disabled observe/tick remains vanilla");
   }
  } finally {
   harmony.UnpatchAll(harmony.Id);Field(typeof(VisualMiningMod),"Instance").SetValue(null,prior);
   ((IDictionary)Field(renderer,"Entries").GetValue(null)!).Clear();Field(renderer,"api").SetValue(null,oldApi);Field(renderer,"allowPresentation").SetValue(null,oldGuard);
  }
  if(Failures.Count>0)throw new Exception($"Tool toggle regressions: {Failures.Count}/{checks} failed\n"+string.Join("\n",Failures));
  Console.WriteLine($"PASS tool toggles: {checks} checks; all 8 enable combinations × 3 tools × occupied/empty offhand × FP/TP; real inherited client manager routing/FP alias, enabled observation lifecycle, actual enabled-to-disabled tool switch, patched getter, sound and decal gate; config default/preservation");
 }
 static void EnabledObservationLifecycle(Fixture f,EnumTool tool) {
  f.SeedOwnedAnimations();
  var snapshots=new[]{f.Entity.Tp,f.Entity.Fp}.Select(m=>m.ActiveAnimationsByAnimCode.ToDictionary(p=>p.Key,p=>p.Value)).ToArray();
  void Preserved(string phase) {
   int i=0;foreach(var manager in new[]{f.Entity.Tp,f.Entity.Fp}) {
    var before=snapshots[i++];
    Check(manager.ActiveAnimationsByAnimCode.Count==before.Count && before.All(p=>manager.ActiveAnimationsByAnimCode.TryGetValue(p.Key,out var value)&&ReferenceEquals(value,p.Value)),f.Label+" "+phase+" preserves every owned and vanilla animation in both managers");
   }
  }
  var contact=(ContactTracker)Get(f.Mod,"contact")!;
  void PrimeContact()=>contact.Sample("old-target",0,8,6);
  void ContactCleared(string phase)=>Check(Get(contact,"identity")==null && !(bool)Get(contact,"fired")! && (float)Get(contact,"previous")! ==0,f.Label+" "+phase+" clears contact");
  Call(f.Mod,"OnTick",.01f);Preserved("enabled no-damage tick");
  Check(Get(f.Mod,"damage")==null && f.Entries.Count==0,f.Label+" no-damage tick remains observation-free");
  Call(f.Mod,"Observe",f.Damage);Preserved("first enabled observe");
  Check(ReferenceEquals(Get(f.Mod,"damage"),f.Damage) && f.Entries.Count==1,f.Label+" first observe tracks damage/carve");
  CosmeticVoxelRenderer.CommitImpact(f.Damage,f.Selection,tool);PrimeContact();
  f.ElapsedMilliseconds+=201;Call(f.Mod,"OnTick",.01f);Preserved("stale 201ms tick");
  Check(Get(f.Mod,"damage")==null && Get(f.Mod,"selection")==null && Get(f.Mod,"stack")==null && f.Entries.Count==0,f.Label+" stale 201ms clears observation/carve");ContactCleared("stale 201ms");
  Call(f.Mod,"Observe",f.Damage);CosmeticVoxelRenderer.CommitImpact(f.Damage,f.Selection,tool);PrimeContact();
  object oldEntry=f.Entries.Values.Cast<object>().Single();var original=f.Selection.Position.Copy();
  f.Selection.Position=f.Selection.Position.Copy().Add(1,0,0);
  var retarget=new BlockDamage{Position=f.Selection.Position.Copy(),Block=f.Damage.Block,RemainingResistance=3};
  Call(f.Mod,"Observe",retarget);Preserved("enabled retarget");ContactCleared("enabled retarget");
  Check(ReferenceEquals(Get(f.Mod,"damage"),retarget) && f.Entries.Count==1 && !f.Entries.Values.Cast<object>().Any(e=>ReferenceEquals(e,oldEntry)),f.Label+" retarget replaces old damage/carve without stopping animation");
  f.ElapsedMilliseconds+=201;Call(f.Mod,"OnTick",.01f);Preserved("retarget stale tick");
  Check(f.Entries.Count==0 && Get(f.Mod,"damage")==null,f.Label+" retarget carve clears on expiry");
  f.Selection.Position=original;
 }
 sealed class StartupBoundary:Exception {}
 static void ConfigStartup() {
  var prior=Field(typeof(VisualMiningMod),"Instance").GetValue(null);
  try {
   foreach(var (missing,invalid) in new[]{(false,false),(true,false),(false,true)}) {
    var config=JsonConvert.DeserializeObject<VisualMiningConfig>("{\"Pickaxe\":{\"Enabled\":false,\"AnimationSpeed\":1.4,\"ContactFrame\":9,\"CameraShake\":0.2},\"VoxelChips\":false,\"ImpactSounds\":false,\"ImpactParticles\":false}")!;
    var before=JObject.FromObject(config);int writes=0;bool loaded=false,boundary=false;object? written=null;
    var loader=Proxy(typeof(ICoreAPI).GetProperty("ModLoader")!.PropertyType,(m,a)=>m.Name=="IsModEnabled"?false:throw new Exception("modloader "+m.Name));
    var logger=StrictProxy.Create<ILogger>((m,a)=>null);
    var api=StrictProxy.Create<ICoreClientAPI>((m,a)=>m.Name switch {
     "get_ModLoader"=>loader,"get_Logger"=>logger,"get_World"=>null,"LoadModConfig"=>Load(),"StoreModConfig"=>Store(a),
     "get_Assets" or "get_Event"=>throw new StartupBoundary(),_=>throw new Exception("Unexpected startup operation "+m.Name)
    });
    object? Load(){loaded=true;if(invalid)throw new FormatException("invalid user JSON");return missing?null:config;}
    object? Store(object?[]? a){writes++;written=a![0];Check((string)a[1]! =="kuboczochvisualmining.json","startup config filename stays stable");return null;}
    var mod=new VisualMiningMod();try{mod.StartClientSide(api);}catch(StartupBoundary){boundary=true;}
    Check(loaded&&boundary,"startup exercised real config load before asset/event boundary");
    Check(writes==(missing?1:0),missing?"missing config writes defaults exactly once":"existing config is NEVER overwritten on startup");
    if(missing) Check(written is VisualMiningConfig c&&c.Pickaxe.Enabled&&c.Axe.Enabled&&c.Shovel.Enabled,"new file defaults all tools to enabled");
    else if(invalid) Check(writes==0 && ((VisualMiningConfig)Get(mod,"Config")!).Pickaxe.Enabled,"invalid config uses defaults without overwriting user file");
    else Check(JToken.DeepEquals(before,JObject.FromObject(Get(mod,"Config")!)),"startup preserves every existing config field");
   }
  } finally {Field(typeof(VisualMiningMod),"Instance").SetValue(null,prior);}
 }
 static void ConfigSerialization() {
  foreach(string json in new[]{"{}","{\"Pickaxe\":{},\"Axe\":{},\"Shovel\":{}}"}) {
   var c=JsonConvert.DeserializeObject<VisualMiningConfig>(json)!;c.Normalize();
   Check(c.Pickaxe.Enabled&&c.Axe.Enabled&&c.Shovel.Enabled,"missing Enabled fields default true");
  }
  var raw=JObject.Parse("{\"Pickaxe\":{\"Enabled\":false,\"AnimationSpeed\":1.25,\"ContactFrame\":7.5,\"CameraShake\":0.23},\"Axe\":{\"Enabled\":false,\"AnimationSpeed\":1.5,\"ContactFrame\":22.5,\"CameraShake\":0.34},\"Shovel\":{\"Enabled\":false,\"AnimationSpeed\":0.85,\"ContactFrame\":16.5,\"CameraShake\":0.45},\"VoxelChips\":false,\"ImpactSounds\":false,\"ImpactParticles\":false}");
  var config=raw.ToObject<VisualMiningConfig>()!;var before=JObject.FromObject(config);config.Normalize();
  Check(JToken.DeepEquals(before,JObject.FromObject(config)) && !config.Pickaxe.Enabled && !config.Axe.Enabled && !config.Shovel.Enabled && !config.VoxelChips && !config.ImpactSounds && !config.ImpactParticles,"deserialize/normalize preserves explicit false and every cosmetic field exactly");
  var defaults=new VisualMiningConfig();defaults.Normalize();
  Check(defaults.Pickaxe.Enabled&&defaults.Axe.Enabled&&defaults.Shovel.Enabled&&defaults.VoxelChips&&defaults.ImpactSounds&&defaults.ImpactParticles,"all default presentation remains enabled");
 }
 sealed class TestEntity:EntityPlayer {
  public readonly PlayerAnimationManager Tp=new();public readonly PlayerAnimationManager Fp=new();
  public ItemSlot Offhand=new ItemSlot(null);
  public TestEntity(){Set(this,"selfFpAnimManager",Fp);Set(this,"animManager",Tp);}
  // Intentionally inherit EntityPlayer.AnimManager: the engine aliases FP for the local
  // first-person player, and OtherAnimManager must retain its real camera routing.
  public override ItemSlot LeftHandItemSlot=>Offhand;
 }
 sealed class Fixture:IDisposable {
  public readonly VisualMiningMod Mod=new();public readonly VisualMiningConfig Config=new();public readonly TestEntity Entity=new();
  public readonly ItemSlot Slot=new(null); public readonly BlockSelection Selection;public readonly BlockDamage Damage;public readonly IClientPlayer Player;
  public readonly IDictionary Entries=(IDictionary)Field(typeof(CosmeticVoxelRenderer),"Entries").GetValue(null)!;
  public readonly string Code,Label;
  public long ElapsedMilliseconds=1000;
  readonly ICoreClientAPI api;
  public Fixture(AnimationManager ready,int mask,EnumTool tool,bool offhand,EnumCameraMode camera) {
   Config.Pickaxe.Enabled=(mask&1)!=0;Config.Axe.Enabled=(mask&2)!=0;Config.Shovel.Enabled=(mask&4)!=0;
   Code="kvm-"+tool.ToString().ToLowerInvariant()+(offhand?"-onehand":"");Label=$"mask={mask} {tool} offhand={offhand} {camera}";
   Slot.Itemstack=new ItemStack(new Item{Tool=tool,HeldTpHitAnimation="vanilla-test-hit"});
   if(offhand)Entity.Offhand.Itemstack=new ItemStack(new Item());
   typeof(Entity).GetProperty("Properties")!.SetValue(Entity,new EntityProperties{Client=new EntityClientProperties(Array.Empty<Vintagestory.API.Datastructures.JsonObject>(),new()){AnimationsByMetaCode=new Dictionary<string,AnimationMetaData>()}});
   Entity.EntityId=918;Entity.WatchedAttributes.SetString("playerUID","toggle-local-player");Entity.Controls.LeftMouseDown=true;Entity.Tp.Animator=ready.Animator;Entity.Fp.Animator=ready.Animator;
   var block=new Block{BlockId=992,Code=new AssetLocation(tool==EnumTool.Axe?"game:log-grown-oak-ud":"game:stone-granite"),BlockMaterial=tool==EnumTool.Axe?EnumBlockMaterial.Wood:tool==EnumTool.Shovel?EnumBlockMaterial.Soil:EnumBlockMaterial.Stone,Resistance=4,CollisionBoxes=new[]{new Cuboidf(0,0,0,1,1,1)},SideOpaque=new Vintagestory.API.Datastructures.SmallBoolArray(63),SideSolid=new Vintagestory.API.Datastructures.SmallBoolArray(63)};
   Selection=new BlockSelection{Position=new BlockPos(0).Set(30,50,70),Face=BlockFacing.NORTH,HitPosition=new Vec3d(.5,.5,0)};
   Damage=new BlockDamage{Position=Selection.Position.Copy(),Block=block,RemainingResistance=3};
   var inventory=Proxy(typeof(IPlayer).GetProperty("InventoryManager")!.PropertyType,(m,a)=>m.Name=="get_ActiveHotbarSlot"?Slot:throw new Exception("inventory "+m.Name));
   var worldData=Proxy(typeof(IPlayer).GetProperty("WorldData")!.PropertyType,(m,a)=>m.Name=="get_CurrentGameMode"?EnumGameMode.Survival:throw new Exception("worlddata "+m.Name));
   // IClientPlayer has a nonpublic default interface member that DispatchProxy cannot emit.
   // Use the real engine type, replacing ONLY environment-backed property getters.
   Player=(IClientPlayer)System.Runtime.CompilerServices.RuntimeHelpers.GetUninitializedObject(typeof(Vintagestory.Client.NoObf.ClientPlayer));
   foreach(var method in typeof(Vintagestory.Client.NoObf.ClientPlayer).GetMethods(All)) {
    if(method.Name.EndsWith("get_Entity"))PlayerValues[method]=Entity;
    if(method.Name.EndsWith("get_CurrentBlockSelection"))PlayerValues[method]=Selection;
    if(method.Name.EndsWith("get_CurrentEntitySelection"))PlayerValues[method]=null;
    if(method.Name.EndsWith("get_InventoryManager"))PlayerValues[method]=inventory;
    if(method.Name.EndsWith("get_WorldData"))PlayerValues[method]=worldData;
    if(method.Name.EndsWith("get_CameraMode"))PlayerValues[method]=camera;
   }
   var accessor=StrictProxy.Create<IBlockAccessor>((m,a)=>m.Name switch {"GetBlock"=>block,"MarkBlockDirty"=>null,_=>throw new Exception("Unexpected gameplay/world write "+m.Name)});
   var world=StrictProxy.Create<IClientWorldAccessor>((m,a)=>m.Name switch {"get_Player"=>Player,"get_BlockAccessor"=>accessor,"get_ElapsedMilliseconds"=>ElapsedMilliseconds,"get_Side"=>EnumAppSide.Client,"get_EntityDebugMode"=>false,_=>throw new Exception("Unexpected world effect "+m.Name)});
   Entity.World=world;
   var logger=StrictProxy.Create<ILogger>((m,a)=>null);
   var strings=StrictProxy.Create<ISettingsClass<string>>((m,a)=>m.Name=="get_Item" && (string)a![0]! =="playeruid"?Entity.PlayerUID:throw new Exception("settings string "+m.Name));
   var bools=StrictProxy.Create<ISettingsClass<bool>>((m,a)=>m.Name=="get_Item" && (string)a![0]! =="immersiveFpMode"?false:throw new Exception("settings bool "+m.Name));
   var settings=StrictProxy.Create<ISettings>((m,a)=>m.Name switch {"get_String"=>strings,"get_Bool"=>bools,_=>throw new Exception("settings "+m.Name)});
   var render=StrictProxy.Create<IRenderAPI>((m,a)=>m.Name=="get_CameraType"?camera:throw new Exception("render "+m.Name));
   api=StrictProxy.Create<ICoreClientAPI>((m,a)=>m.Name switch {"get_World"=>world,"get_Logger"=>logger,"get_Side"=>EnumAppSide.Client,"get_Settings"=>settings,"get_Render"=>render,_=>throw new Exception("api "+m.Name)});
   Entity.Api=api;
   foreach(var manager in new[]{Entity.Tp,Entity.Fp}) {Set(manager,"entity",Entity);Set(manager,"plrEntity",Entity);Set(manager,"api",api);Set(manager,"capi",api);}
   Entity.Tp.UseFpAnmations=false; // Match the real EntityPlayer constructor; FP retains its default true.
   Check(Entity.World.Side==EnumAppSide.Client && Entity.Api.Side==EnumAppSide.Client,Label+" real client-side entity and animation environment");
   Check(ReferenceEquals(Entity.AnimManager,camera==EnumCameraMode.FirstPerson?Entity.Fp:Entity.Tp),Label+" inherited engine AnimManager routes to camera manager");
   Check(ReferenceEquals(Entity.OtherAnimManager,camera==EnumCameraMode.FirstPerson?Entity.Tp:Entity.Fp),Label+" inherited engine OtherAnimManager routes to opposite manager");
   Set(Mod,"api",api);Set(Mod,"Config",Config);Set(Mod,"enabled",true);Set(Mod,"assetsAvailable",true);Field(typeof(VisualMiningMod),"Instance").SetValue(null,Mod);
   Field(typeof(CosmeticVoxelRenderer),"api").SetValue(null,api);
   Field(typeof(CosmeticVoxelRenderer),"allowPresentation").SetValue(null,new System.Func<BlockDamage,bool>(d=>(bool)Call(Mod,"IsMining",Player,Selection,Slot)!));
  }
  public bool CrackPrefix()=>(bool)typeof(CosmeticVoxelRenderer).GetNestedType("CrackDecalPatch",BindingFlags.NonPublic)!.GetMethod("Prefix",All)!.Invoke(null,new object[]{Damage})!;
  public void SeedOwnedAnimations() {
   foreach(var manager in new IAnimationManager[]{Entity.Tp,Entity.Fp}) foreach(var code in new[]{"walk","vanilla-test-hit","kvm-pickaxe","kvm-axe-horizontal-onehand-fp","kvm-shovel-onehand-ifp"})manager.ActiveAnimationsByAnimCode[code]=new AnimationMetaData{Code=code,Animation=code};
  }
  public void Dispose(){Call(Mod,"Reset");Entries.Clear();}
 }
}
