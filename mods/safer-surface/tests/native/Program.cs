using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;
using SaferSurface;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

static class Program
{
    const string Owner = "safer-surface.native-tests";
    const string ModOwner = "kuboczochssafersurface";
    static int failed, passed;
    static readonly Dictionary<string,int> Counts = new();
    static readonly FieldInfo Scope = AccessTools.Field(typeof(AcquisitionGate), "currentAcquisition");
    static readonly Type[] Tasks = [typeof(AiTaskSeekEntity),typeof(AiTaskMeleeAttack),typeof(AiTaskThrowAtEntity),typeof(AiTaskStayInRange),typeof(AiTaskTurretMode),typeof(AiTaskSeekEntityR),typeof(AiTaskMeleeAttackR),typeof(AiTaskShootAtEntityR),typeof(AiTaskStayInRangeR),typeof(AiTaskTurretModeR)];
    static readonly MethodInfo[] Predicates = [Method(typeof(AiTaskBaseTargetable),"IsTargetableEntity",typeof(Entity),typeof(float)),Method(typeof(AiTaskBaseTargetable),"IsTargetableEntityNoTagsAll",typeof(Entity),typeof(float)),Method(typeof(AiTaskBaseTargetable),"IsTargetableEntityNoTagsNoAll",typeof(Entity),typeof(float)),Method(typeof(AiTaskBaseTargetableR),"IsTargetableEntity",typeof(Entity),typeof(float))];
    static MethodInfo Method(Type t,string n,params Type[] a) => AccessTools.DeclaredMethod(t,n,a) ?? throw new Exception($"Missing {t.FullName}.{n}");
    static void Check(bool condition,string message) { if(!condition) throw new Exception(message); }
    static void Test(string name,Action body) { try { body();passed++;Console.WriteLine("PASS "+name); } catch(Exception e) {failed++;Console.WriteLine("FAIL "+name+": "+e); } finally { new Harmony(Owner).UnpatchAll(Owner);new Harmony(ModOwner).UnpatchAll(ModOwner);AcquisitionGate.Dispose(); } }
    static object Raw(Type t) => RuntimeHelpers.GetUninitializedObject(t);
    static void Set(object target,string name,object? value) => (AccessTools.Field(target.GetType(),name) ?? throw new Exception("Missing field "+name)).SetValue(target,value);
    static ICoreServerAPI Api() => Fake.Create<ICoreServerAPI>((m,a) => m.Name switch {
        "GetOrCreateDataPath" => DataPath(), "get_Logger" => Fake.Create<ILogger>((_,_)=>null), _ => throw new NotSupportedException(m.Name)
    });
    static string DataPath() { var p=Path.Combine(AppContext.BaseDirectory,"rollback-data");Directory.CreateDirectory(p);return p; }
    public static int Main() {
        var version=(string)typeof(Vintagestory.API.Config.GameVersion).GetField("ShortGameVersion")!.GetRawConstantValue()!;
        Check(version=="1.22.7","Tests require exact SDK 1.22.7; got "+version);
        Console.WriteLine("SDK="+typeof(Entity).Assembly.Location+"; version="+version);
        Test("required SDK signatures and real Harmony installation/removal",Bindings);
        Test("compiled runtime field/property bindings",()=>AcquisitionGate.Initialize(new(),new EnvironmentProbe(Api(),new())));
        Test("real partial install startup rollback",Rollback);
        Test("Harmony harness scope/nesting/exception + original false",HarnessTests);
        Test("native predicate original bodies exactly once; zero sensing for dead targets",NativePredicates);
        Test("native ShouldExecute original bodies exactly once on native cooldown early-outs",NativeShouldExecute);
        Test("SDK-backed runtime filtering: range, active same-pair, ranged retaliation, notify",RuntimeFiltering);
        Test("cached endpoint evidence: storm overrides, movement/dimension invalidation, disposal",EndpointEvidence);
        Test("compiled mod IL forbids sensing/search/pathfinding/scheduler calls",IlGuard);
        EnvironmentTests.Register(Test);
        Console.WriteLine($"RESULT {passed} passed, {failed} failed");return failed==0?0:1;
    }
    static void Bindings() {
        var h=new Harmony(Owner);AcquisitionGate.Install(h);
        var expected=Tasks.Select(t=>Method(t,"ShouldExecute")).Concat(Predicates).Concat(new[]{Method(typeof(AiTaskSeekEntity),"Notify",typeof(string),typeof(object)),Method(typeof(AiTaskSeekEntityR),"Notify",typeof(string),typeof(object))}).ToHashSet<MethodBase>();
        var actual=Harmony.GetAllPatchedMethods().Where(m=>Harmony.GetPatchInfo(m)?.Owners.Contains(Owner)==true).ToHashSet();
        Check(actual.SetEquals(expected),"Patch target set differs; scheduler/pathfinding must not be patched");
        foreach(var m in expected) {
            var info=Harmony.GetPatchInfo(m)!;
            if(m.Name=="ShouldExecute") Check(info.Prefixes.Count(p=>p.owner==Owner&&p.PatchMethod.Name=="Enter")==1&&info.Finalizers.Count(p=>p.owner==Owner&&p.PatchMethod.Name=="Leave")==1,"scope binding "+m);
            else if(m.Name=="Notify") Check(info.Prefixes.Count(p=>p.owner==Owner&&p.PatchMethod.Name=="FilterNotify")==1,"notify binding");
            else Check(info.Postfixes.Count(p=>p.owner==Owner&&p.PatchMethod.Name=="Filter")==1,"predicate binding");
            Console.WriteLine("BOUND "+m.DeclaringType!.FullName+"."+m+" ["+m.DeclaringType.Assembly.GetName().Name+"]");
        }
        Check(actual.Count==16,"Expected exact 16 target methods");h.UnpatchAll(Owner);
        Check(!Harmony.GetAllPatchedMethods().Any(m=>Harmony.GetPatchInfo(m)?.Owners.Contains(Owner)==true),"Unpatch left a target behind");
    }
    static int installs;
    public static void InstallationFault(Harmony __instance) { if(__instance.Id==ModOwner&&++installs==3) throw new InvalidOperationException("test injected partial-install fault"); }
    static void Rollback() {
        installs=0;
        var patch=typeof(Harmony).GetMethods().Single(m=>m.Name=="Patch"&&m.GetParameters().Length==5);
        new Harmony(Owner).Patch(patch,prefix:new HarmonyMethod(typeof(Program),nameof(InstallationFault)));
        var mod=new SaferSurfaceMod();mod.StartServerSide(Api());
        Check(installs==3,"Did not reach fault after two actual native method patches (binding/startup failure)");
        Check(!Harmony.GetAllPatchedMethods().Any(m=>Harmony.GetPatchInfo(m)?.Owners.Contains(ModOwner)==true),"Failed startup left partial patches");
        Check(AccessTools.Field(typeof(AcquisitionGate),"environment").GetValue(null)==null,"Failed startup retained probe");
        mod.Dispose();
    }
    static void HarnessTests() {
        var h=new Harmony(Owner);
        h.Patch(Method(typeof(Harness),nameof(Harness.ShouldExecute),typeof(bool),typeof(bool)),prefix:new HarmonyMethod(typeof(AcquisitionGate),nameof(AcquisitionGate.Enter)),finalizer:new HarmonyMethod(typeof(AcquisitionGate),nameof(AcquisitionGate.Leave)));
        h.Patch(Method(typeof(Harness),nameof(Harness.Predicate),typeof(Entity),typeof(bool)),postfix:new HarmonyMethod(typeof(AcquisitionGate),nameof(AcquisitionGate.Filter)));
        var outer=new object();Scope.SetValue(null,outer);var task=new Harness();
        Check(!task.ShouldExecute(false,false),"Original false was overwritten");Check(task.ShouldCalls==1&&task.PredicateCalls==1,"Original callbacks must execute once");Check(ReferenceEquals(Scope.GetValue(null),outer),"normal scope leaked");
        try {task.ShouldExecute(true,true);throw new Exception("Expected exception");} catch(HarnessException) {}
        Check(task.ShouldCalls==2&&task.PredicateCalls==2,"exception callback replayed");Check(ReferenceEquals(Scope.GetValue(null),outer),"exception scope leaked");
        var child=new Harness();task.Child=child;Check(task.ShouldExecute(true,false),"nested native true lost");Check(child.ShouldCalls==1&&child.PredicateCalls==1,"nested invocation count");Check(ReferenceEquals(Scope.GetValue(null),outer),"nested scope leaked");Scope.SetValue(null,null);
        Check(!task.Predicate(null!,false),"out-of-scope false changed");Check(Scope.GetValue(null)==null,"out-of-scope predicate created scope");
    }
    public static void Hit(string key) {Counts[key]=Counts.GetValueOrDefault(key)+1;}
    public static IEnumerable<CodeInstruction> CountBody(IEnumerable<CodeInstruction> code,MethodBase __originalMethod) {
        yield return new CodeInstruction(OpCodes.Ldstr,Key(__originalMethod));yield return new CodeInstruction(OpCodes.Call,Method(typeof(Program),nameof(Hit),typeof(string)));
        foreach(var instruction in code) yield return instruction;
    }
    static string Key(MethodBase m)=>m.DeclaringType!.FullName+"."+m.Name;
    static void Instrument(IEnumerable<MethodInfo> methods) {foreach(var m in methods)new Harmony(Owner).Patch(m,transpiler:new HarmonyMethod(typeof(Program),nameof(CountBody)));}
    static void NativePredicates() {
        AcquisitionGate.Install(new Harmony(Owner));Counts.Clear();Instrument(Predicates);
        var sense=new[]{Method(typeof(AiTaskBaseTargetable),"CanSense",typeof(Entity),typeof(double)),Method(typeof(AiTaskBaseTargetableR),"CanSense",typeof(Entity),typeof(double))};Instrument(sense);
        var target=new EntityAgent {Alive=false,Code=new AssetLocation("game:player")};
        foreach(var m in Predicates) {
            var task=Raw(m.DeclaringType==typeof(AiTaskBaseTargetable)?typeof(AiTaskSeekEntity):typeof(AiTaskSeekEntityR));
            if(m.DeclaringType==typeof(AiTaskBaseTargetable)) {Set(task,"targetEntityFirstLetters", "p");Set(task,"targetEntityCodesExact",new[]{"player"});Set(task,"targetEntityCodesBeginsWith",Array.Empty<string>());}
            if(task is AiTaskBaseR) ConfigureModern(task);
            AcquisitionGate.Enter(task,out var state);
            try {Check(!(bool)m.Invoke(task,[target,100f])!,"dead target native false changed");} finally {AcquisitionGate.Leave(state);}
            Check(Counts.GetValueOrDefault(Key(m))==1,"Native predicate body not exactly once: "+m);
        }
        Check(sense.All(m=>Counts.GetValueOrDefault(Key(m))==0),"Additional native sensing occurred");
    }
    static void ConfigureModern(object task) {
        var property=AccessTools.Property(task.GetType(),"Config")!;
        var config=Activator.CreateInstance(property.PropertyType)!;
        SetMember(config,"IgnoreDeadEntities",true);SetMember(config,"ExecutionChance",-1);SetMember(config,"RetaliateUnconditionally",false);
        Set(task,"baseConfig",config);
    }
    static void SetMember(object obj,string name,object value) {
        var p=AccessTools.Property(obj.GetType(),name);var f=AccessTools.Field(obj.GetType(),name);var type=p?.PropertyType??f?.FieldType;
        if(type==null)throw new Exception("Missing config member "+name);
        value=Convert.ChangeType(value,type);if(p!=null)p.SetValue(obj,value);else f!.SetValue(obj,value);
    }
    static void NativeShouldExecute() {
        AcquisitionGate.Install(new Harmony(Owner));
        var methods=Tasks.Select(t=>Method(t,"ShouldExecute")).ToArray();Instrument(methods);Instrument(Predicates);
        var sense=new[]{Method(typeof(AiTaskBaseTargetable),"CanSense",typeof(Entity),typeof(double)),Method(typeof(AiTaskBaseTargetableR),"CanSense",typeof(Entity),typeof(double))};Instrument(sense);
        foreach(var t in Tasks) {
            Counts.Clear();var task=Raw(t);int worldCalls=0;
            var world=Fake.Create<IWorldAccessor>((m,a)=> {worldCalls++;if(m.Name=="get_ElapsedMilliseconds")return 1000000L;throw new Exception("Unexpected native world call "+m.Name);});
            var entity=new EntityAgent();Set(entity,"World",world);Set(task,"entity",entity);Set(task,"cooldownUntilMs",long.MaxValue);
            if(task is AiTaskBase) {Set(task,"targetEntityCodesExact",Array.Empty<string>());Set(task,"targetEntityCodesBeginsWith",Array.Empty<string>());Set(task,"ExecutionChance",-1d);}
            else ConfigureModern(task);
            var marker=new object();Scope.SetValue(null,marker);
            Check(Method(t,"ShouldExecute").Invoke(task,null) is false,"Native early-out did not return false: "+t.Name);
            Check(Counts.GetValueOrDefault(Key(Method(t,"ShouldExecute")))==1,"Native ShouldExecute replayed: "+t.Name);
            // TurretModeR legitimately delegates once to its native ShootAtEntityR base body.
            var expected=t==typeof(AiTaskTurretModeR)?2:1;
            Check(Counts.Where(e=>e.Key.EndsWith(".ShouldExecute")).Sum(e=>e.Value)==expected,"Unexpected native body count: "+t.Name);
            Check(Predicates.Concat(sense).All(m=>Counts.GetValueOrDefault(Key(m))==0),"ShouldExecute wrapper added native sensing/predicate work");
            Check(worldCalls==(t==typeof(AiTaskMeleeAttack)?1:0),"Unexpected world search/navigation call on native early-out: "+t.Name);
            Check(ReferenceEquals(Scope.GetValue(null),marker),"Native return leaked scope: "+t.Name);
        }
        Scope.SetValue(null,null);
    }
    static (EnvironmentProbe Probe,SystemTemporalStability Temporal,IWorldAccessor World) Evidence(SurfaceConfig config) {
        var world=Fake.Create<IServerWorldAccessor>((m,a)=>m.Name switch {"get_ElapsedMilliseconds"=>1000000L,"get_Side"=>EnumAppSide.Server,_=>throw new Exception("Forbidden/unexpected test world access: "+m.Name)});
        var api=Fake.Create<ICoreServerAPI>((m,a)=>m.Name=="get_World"?world:throw new Exception("Unexpected test API access: "+m.Name));
        var probe=new EnvironmentProbe(api,config);var temporal=new SystemTemporalStability();Set(probe,"temporal",temporal);
        return(probe,temporal,world);
    }
    static void Prime(EnvironmentProbe probe,Entity entity,bool safe=true) {
        var type=typeof(EnvironmentProbe).GetNestedType("Entry",BindingFlags.NonPublic)!;
        var entry=Activator.CreateInstance(type,[(int)Math.Floor(entity.Pos.X),(int)Math.Floor(entity.Pos.Y),(int)Math.Floor(entity.Pos.Z),entity.Pos.Dimension,1000000L,safe])!;
        var cache=(System.Collections.IDictionary)AccessTools.Field(typeof(EnvironmentProbe),"cache").GetValue(probe)!;cache[entity.EntityId]=entry;
    }
    static TestAgent Agent(IWorldAccessor world) {var e=new TestAgent {Code=new AssetLocation("game:bowtorn-normal"),World=world,EntityId=100};e.Pos.SetPos(0,110,0);var ai=(EntityBehaviorTaskAI)Raw(typeof(EntityBehaviorTaskAI));var manager=new AiTaskManager(e);
        var p=AccessTools.Property(typeof(EntityBehaviorTaskAI),"TaskManager");if(p?.SetMethod!=null)p.SetValue(ai,manager);else {var f=AccessTools.Field(typeof(EntityBehaviorTaskAI),"TaskManager")??AccessTools.Field(typeof(EntityBehaviorTaskAI),"taskManager")??AccessTools.Field(typeof(EntityBehaviorTaskAI),"<TaskManager>k__BackingField");f!.SetValue(ai,manager);}e.AI=ai;return e;}
    static void EndpointEvidence() {
        var c=new SurfaceConfig();var (probe,temporal,world)=Evidence(c);var e=Agent(world);Prime(probe,e);
        Check(probe.IsSafe(e),"Primed safe endpoint unavailable");temporal.StormData.nowStormActive=true;Check(!probe.IsSafe(e),"Storm did not override cached safe result");temporal.StormData.nowStormActive=false;
        e.Pos.X++;Check(!probe.IsSafe(e),"Movement reused stale spatial evidence (fixture refuses fresh environmental requests)");e.Pos.X--;Prime(probe,e);Check(probe.IsSafe(e),"restored evidence unavailable");
        e.Pos.Dimension=1;Check(!probe.IsSafe(e),"Cross-dimension safe evidence leaked");e.Pos.Dimension=0;probe.Dispose();Check(!probe.IsSafe(e),"Disposed evidence used");
    }
    static void RuntimeFiltering() {
        var c=new SurfaceConfig();var (probe,temporal,world)=Evidence(c);AcquisitionGate.Initialize(c,probe);
        var owner=Agent(world);var target=new EntityPlayer {EntityId=101,World=world};target.Pos.SetPos(20,110,0);Prime(probe,owner);Prime(probe,target);
        foreach(var t in Tasks) {
            var task=Raw(t);Set(task,"entity",owner);if(task is AiTaskBaseR)ConfigureModern(task);
            Set(task,"attackedByEntityMs",1000000L);
            bool Filter(bool original=true,bool scoped=true) {bool result=original;if(scoped)AcquisitionGate.Enter(task,out _);try{AcquisitionGate.Filter(task,target,ref result);}finally {Scope.SetValue(null,null);}return result;}
            Check(!Filter(),"Far safe target not rejected: "+t.Name);
            Check(!Filter(false),"Original false changed: "+t.Name);Check(Filter(scoped:false),"Out of scope target filtered: "+t.Name);
            target.Pos.X=8;Check(Filter(),"Exact range boundary rejected: "+t.Name);target.Pos.X=20;
            owner.AI!.TaskManager.ActiveTasksBySlot[3]=(IAiTask)task;Set(task,"targetEntity",target);
            Check(Filter(),"Existing same-pair pursuit rejected: "+t.Name);
            c.PreserveExistingPursuit=false;Check(!Filter(),"Pursuit opt-out not honored: "+t.Name);c.PreserveExistingPursuit=true;
            Set(task,"targetEntity",new EntityPlayer());Check(!Filter(),"Unrelated pursuit bypassed range cap: "+t.Name);owner.AI.TaskManager.ActiveTasksBySlot[3]=null;
            var flee=Raw(typeof(AiTaskFleeEntity));Set(flee,"targetEntity",target);owner.AI.TaskManager.ActiveTasksBySlot[3]=(IAiTask)flee;Check(!Filter(),"Flee task incorrectly treated as pursuit: "+t.Name);owner.AI.TaskManager.ActiveTasksBySlot[3]=null;
            Set(task,"attackedByEntity",target);Check(Filter(),"Recent same-attacker retaliation rejected: "+t.Name);
            c.VanillaDuringRetaliation=false;Check(!Filter(),"Retaliation opt-out not honored: "+t.Name);c.VanillaDuringRetaliation=true;
            Set(task,"attackedByEntity",new EntityPlayer());Check(!Filter(),"Unrelated attacker bypassed range cap: "+t.Name);
            Set(task,"attackedByEntity",target);Set(task,"attackedByEntityMs",0L);Check(!Filter(),"Stale same attacker bypassed range cap: "+t.Name);Set(task,"attackedByEntity",null);
            temporal.StormData.nowStormActive=true;Check(Filter(),"Storm-safe cap remained active: "+t.Name);temporal.StormData.nowStormActive=false;
            if(t==typeof(AiTaskSeekEntity)||t==typeof(AiTaskSeekEntityR)) {bool result=true;Check(!AcquisitionGate.FilterNotify(task,"seekEntity",target,ref result)&&!result,"seekEntity notification bypassed cap");result=true;Check(AcquisitionGate.FilterNotify(task,"unrelated",target,ref result)&&result,"unrelated notification changed");}
        }
    }
    static readonly Dictionary<ushort,OpCode> Opcodes=typeof(OpCodes).GetFields(BindingFlags.Public|BindingFlags.Static).Where(f=>f.FieldType==typeof(OpCode)).Select(f=>(OpCode)f.GetValue(null)!).ToDictionary(o=>unchecked((ushort)o.Value));
    static void IlGuard() {
        var assembly=typeof(AcquisitionGate).Assembly;int bodies=0,edges=0;
        string[] forbidden=["CanSense","IsTargetableEntity","ShouldExecute","GetNearestEntity","GetEntitiesAround","WalkEntities","WalkEntityPartitions","RayTrace","Pathfind","FindPath","NavigateTo","RegisterGameTickListener","RegisterCallback"];
        var external=new SortedSet<string>();
        foreach(var t in assembly.GetTypes()) foreach(var m in t.GetMethods(BindingFlags.DeclaredOnly|BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static).Cast<MethodBase>().Concat(t.GetConstructors(BindingFlags.DeclaredOnly|BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.Static))) {
            var bytes=m.GetMethodBody()?.GetILAsByteArray();if(bytes==null)continue;bodies++;
            for(int i=0;i<bytes.Length;) {
                ushort op=bytes[i++];if(op==0xfe)op=(ushort)(0xfe00|bytes[i++]);var code=Opcodes[op];
                if(code.OperandType==OperandType.InlineMethod) {
                    var target=m.Module.ResolveMethod(BitConverter.ToInt32(bytes,i),t.GetGenericArguments(),m.IsGenericMethod?m.GetGenericArguments():null)!;edges++;
                    var name=target.DeclaringType!.FullName+"."+target.Name;
                    Check(!forbidden.Any(x=>name.Contains(x,StringComparison.OrdinalIgnoreCase)),"Forbidden mod call edge: "+m+" -> "+name);
                    Check(!name.Contains("WaypointsTraverser")&&(!name.Contains("AiTaskManager")||name.EndsWith(".get_ActiveTasksBySlot")),"Unexpected navigation/scheduler edge "+name);
                    if(target.Module.Assembly!=assembly)external.Add(name);
                }
                i+=code.OperandType switch {OperandType.InlineNone=>0,OperandType.ShortInlineBrTarget or OperandType.ShortInlineI or OperandType.ShortInlineVar=>1,OperandType.InlineVar=>2,OperandType.InlineI8 or OperandType.InlineR=>8,OperandType.InlineSwitch=>4+4*BitConverter.ToInt32(bytes,i),_=>4};
            }
        }
        Console.WriteLine($"IL AUDIT {bodies} method bodies; {edges} method operands; {external.Count} distinct external targets (including delegates/constructors)");
        foreach(var e in external.Where(e=>e.StartsWith("Vintagestory")))Console.WriteLine("SDK EDGE "+e);
    }
}
sealed class TestAgent:EntityAgent {
    public EntityBehaviorTaskAI? AI;
    public override TEntityBehavior? GetBehavior<TEntityBehavior>() where TEntityBehavior:class => AI as TEntityBehavior;
}
sealed class HarnessException:Exception {}
sealed class Harness {
    public int ShouldCalls,PredicateCalls;public Harness? Child;
    [MethodImpl(MethodImplOptions.NoInlining)] public bool ShouldExecute(bool value,bool fail) {ShouldCalls++;if(!ReferenceEquals(AccessTools.Field(typeof(AcquisitionGate),"currentAcquisition").GetValue(null),this))throw new Exception("missing harness scope");if(Child!=null) {Child.ShouldExecute(true,false);if(!ReferenceEquals(AccessTools.Field(typeof(AcquisitionGate),"currentAcquisition").GetValue(null),this))throw new Exception("nested scope not restored");} var result=Predicate(null!,value);if(fail)throw new HarnessException();return result;}
    [MethodImpl(MethodImplOptions.NoInlining)] public bool Predicate(Entity target,bool value) {PredicateCalls++;return value;}
}
public class Fake:DispatchProxy {
    public System.Func<MethodInfo,object?[]?,object?> Handler=null!;
    protected override object? Invoke(MethodInfo? method,object?[]? args)=>Handler(method!,args);
    public static T Create<T>(System.Func<MethodInfo,object?[]?,object?> handler) where T:class {var p=Create<T,Fake>();((Fake)(object)p).Handler=handler;return p;}
}
