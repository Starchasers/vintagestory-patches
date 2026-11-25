using System.Reflection;
using System.Linq.Expressions;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.GameContent;

namespace SaferSurface;

public sealed class SaferSurfaceMod : ModSystem {
 const string Id="kuboczochssafersurface";
 Harmony? harmony;
 public override bool ShouldLoad(EnumAppSide side)=>side==EnumAppSide.Server;
 public override void StartServerSide(ICoreServerAPI api){
  if(typeof(GameVersion).GetField("ShortGameVersion")?.GetRawConstantValue() as string != "1.22.7"){api.Logger.Warning("[Safer Surface] Unsupported game version; vanilla AI unchanged (tested 1.22.7 only).");return;}
  SurfaceConfig c;
  try {
   string path=Path.Combine(api.GetOrCreateDataPath("ModConfig"),Id+".json");
   c=ConfigFile.LoadOrCreate(path);
   var error=c.Validate();if(error!=null){api.Logger.Error("[Safer Surface] Invalid configuration: {0}. File preserved; vanilla unchanged. Restart after fixing.",error);return;}
  }catch(Exception e){api.Logger.Error("[Safer Surface] Configuration unavailable/invalid; original preserved, vanilla unchanged: {0}",e.Message);return;}
  if(!c.Enabled)return;
  try {
   AcquisitionGate.Initialize(c,new EnvironmentProbe(api,c));
   harmony=new Harmony(Id);
   AcquisitionGate.Install(harmony);
   api.Logger.Notification("[Safer Surface] Enabled 1.22.7 acquisition-only {0}-block cap. Restart for config changes.",c.MaximumRange);
   if(c.DebugLogging)api.Logger.Notification("[Safer Surface] Diagnostics: scoped acquisition filters; no scheduler hooks/searches/rays; cache capacity {0}, TTL {1}ms. Environment-unavailable warning is once per session.",c.CacheCapacity,c.CacheTtlMs);
  }catch(Exception e){harmony?.UnpatchAll(Id);harmony=null;AcquisitionGate.Dispose();api.Logger.Error("[Safer Surface] AI bindings incompatible; vanilla unchanged: {0}",e.Message);}
 }
 public override void Dispose(){harmony?.UnpatchAll(Id);AcquisitionGate.Dispose();base.Dispose();}
}

/// <summary>Observes native acquisition callbacks; never runs original, searches, LOS or navigation.</summary>
public static class AcquisitionGate {
 [ThreadStatic] static object? currentAcquisition;
 static SurfaceConfig config=null!;
 static EnvironmentProbe? environment;
 static readonly Dictionary<AssetLocation,bool> eligibility=new(512);
 static bool Included(AssetLocation code){if(eligibility.TryGetValue(code,out var included))return included;if(eligibility.Count>=512)return false;included=config.Includes(code.ToString());eligibility.Add(code,included);return included;}
 static System.Func<AiTaskBaseR,EntityAgent> ownerR=null!;
 static System.Func<AiTaskBaseTargetable,Entity?> attackerLegacy=null!;
 static System.Func<AiTaskBaseR,Entity?> attackerR=null!;
 static System.Func<AiTaskBaseTargetable,bool> recentLegacy=null!;
 static System.Func<AiTaskBaseR,bool> recentR=null!;
 public static void Initialize(SurfaceConfig c,EnvironmentProbe probe){
  config=c;environment=probe;
  // Resolve and compile once. No reflection, dynamic invocation or delegate construction per callback.
  ownerR=Field<AiTaskBaseR,EntityAgent>("entity");
  attackerLegacy=Field<AiTaskBaseTargetable,Entity?>("attackedByEntity");
  attackerR=Field<AiTaskBaseR,Entity?>("attackedByEntity");
  recentLegacy=Property<AiTaskBaseTargetable,bool>("RecentlyAttacked");
  recentR=Property<AiTaskBaseR,bool>("RecentlyAttacked");
 }
 static System.Func<T,V> Field<T,V>(string name){var p=Expression.Parameter(typeof(T));var f=AccessTools.Field(typeof(T),name)??throw new MissingFieldException(typeof(T).FullName,name);return Expression.Lambda<System.Func<T,V>>(Expression.Field(p,f),p).Compile();}
 static System.Func<T,V> Property<T,V>(string name){var p=Expression.Parameter(typeof(T));var m=AccessTools.PropertyGetter(typeof(T),name)??throw new MissingMemberException(typeof(T).FullName,name);return Expression.Lambda<System.Func<T,V>>(Expression.Call(p,m),p).Compile();}
 public static void Install(Harmony h){
  // Resolve the complete required patch set before modifying any method.
  Type[] tasks=[typeof(AiTaskSeekEntity),typeof(AiTaskMeleeAttack),typeof(AiTaskThrowAtEntity),typeof(AiTaskStayInRange),typeof(AiTaskTurretMode),typeof(AiTaskSeekEntityR),typeof(AiTaskMeleeAttackR),typeof(AiTaskShootAtEntityR),typeof(AiTaskStayInRangeR),typeof(AiTaskTurretModeR)];
  var should=tasks.Select(t=>AccessTools.DeclaredMethod(t,"ShouldExecute",Type.EmptyTypes)??throw new MissingMethodException(t.FullName,"ShouldExecute")).ToArray();
  MethodInfo[] predicates=[Required(typeof(AiTaskBaseTargetable),"IsTargetableEntity",typeof(Entity),typeof(float)),Required(typeof(AiTaskBaseTargetable),"IsTargetableEntityNoTagsAll",typeof(Entity),typeof(float)),Required(typeof(AiTaskBaseTargetable),"IsTargetableEntityNoTagsNoAll",typeof(Entity),typeof(float)),Required(typeof(AiTaskBaseTargetableR),"IsTargetableEntity",typeof(Entity),typeof(float))];
  var notify= new[]{Required(typeof(AiTaskSeekEntity),"Notify",typeof(string),typeof(object)),Required(typeof(AiTaskSeekEntityR),"Notify",typeof(string),typeof(object))};
  foreach(var m in should)h.Patch(m,prefix:new HarmonyMethod(typeof(AcquisitionGate),nameof(Enter)),finalizer:new HarmonyMethod(typeof(AcquisitionGate),nameof(Leave)));
  foreach(var m in predicates)h.Patch(m,postfix:new HarmonyMethod(typeof(AcquisitionGate),nameof(Filter)));
  foreach(var m in notify)h.Patch(m,prefix:new HarmonyMethod(typeof(AcquisitionGate),nameof(FilterNotify)));
 }
 static MethodInfo Required(Type t,string name,params Type[] args)=>AccessTools.DeclaredMethod(t,name,args)??throw new MissingMethodException(t.FullName,name);
 public static void Enter(object __instance,out object? __state){__state=currentAcquisition;currentAcquisition=__instance;}
 public static void Leave(object? __state){currentAcquisition=__state;}
 public static void Filter(object __instance,Entity __0,ref bool __result){
  if(__result&&ReferenceEquals(currentAcquisition,__instance)&&Reject(__instance,__0))__result=false;
 }
 public static bool FilterNotify(object __instance,string __0,object __1,ref bool __result){
  if(__0=="seekEntity"&&__1 is Entity e&&Reject(__instance,e)){__result=false;return false;}return true;
 }
 static bool Reject(object task,Entity target){
  if(environment==null||target is not EntityPlayer)return false;
  try {
   EntityAgent owner;
   bool retaliating;
   if(task is AiTaskBaseTargetable legacy){owner=legacy.entity;retaliating=ReferenceEquals(attackerLegacy(legacy),target)&&recentLegacy(legacy);}
   else if(task is AiTaskBaseR modern){owner=ownerR(modern);retaliating=ReferenceEquals(attackerR(modern),target)&&recentR(modern);}
   else return false;
   if((config.VanillaDuringRetaliation&&retaliating)||!Included(owner.Code))return false;
   var a=owner.Pos;var b=target.Pos;
   if(a.Dimension!=b.Dimension||!SurfacePolicy.Outside(config,a.X-b.X,a.Y-b.Y,a.Z-b.Z))return false;
   var manager=owner.GetBehavior<EntityBehaviorTaskAI>()?.TaskManager;
   // Unknown AI ownership => fail open. Vanilla has exactly eight slots, no entity scan.
   if(manager==null)return false;
   var active=manager.ActiveTasksBySlot;
   if(active.Length!=8)return false;
   for(int i=0;config.PreserveExistingPursuit&&i<active.Length;i++){
    var t=active[i];
    if(t is AiTaskSeekEntity or AiTaskMeleeAttack or AiTaskThrowAtEntity or AiTaskStayInRange or AiTaskTurretMode){if(ReferenceEquals(((AiTaskBaseTargetable)t).TargetEntity,target))return false;}
    if(t is AiTaskSeekEntityR or AiTaskMeleeAttackR or AiTaskShootAtEntityR or AiTaskStayInRangeR or AiTaskTurretModeR){if(ReferenceEquals(((AiTaskBaseTargetableR)t).TargetEntity,target))return false;}
   }
   return environment.IsSafe(owner,config.RequireBothEndpointsOnSurface)&&environment.IsSafe(target);
  }catch(Exception){return false;}
 }
 public static void Dispose(){environment?.Dispose();environment=null;eligibility.Clear();currentAcquisition=null;}
}
