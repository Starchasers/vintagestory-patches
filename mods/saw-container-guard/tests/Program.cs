using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

internal static class Program
{
    static Type upstreamType = null!;
    static int passed;
    static BlockEntityChisel? observedEntity;
    static Block? observedSource;
    static int placedCalls, dirtyCalls;
    static readonly BlockPos Position = new(12, 34, 56);
    static readonly Item Saw = new() { Code = new AssetLocation("game:saw-copper"), Tool = EnumTool.Saw };
    static readonly IPlayer? Player = null; // Native 1.22.7 IPlayer has internal methods DispatchProxy cannot implement; null is supported by upstream.

    public static int Main(string[] args)
    {
        try
        {
            if(args.Length == 1 && args[0] == "--absent")
            {
                Check(!AppDomain.CurrentDomain.GetAssemblies().Any(a => a.GetName().Name == "SawChiseling"), "isolated absent-upstream process has no original assembly");
                var before = Harmony.GetAllPatchedMethods().ToHashSet();
                foreach (var side in new[] { EnumAppSide.Client, EnumAppSide.Server })
                {
                    var logs = new List<string>();
                    var logger = Proxy.Make<ILogger>((m, a) => { logs.Add(string.Join(" ", a!.Select(value => value?.ToString()))); return null; });
                    var api = Proxy.Make<ICoreAPI>((m, a) => m.Name switch { "get_Logger" => logger, "get_Side" => side, _ => Proxy.Default(m.ReturnType) });
                    var lifecycle = new SawContainerGuard.SawContainerGuardModSystem();
                    lifecycle.Start(api);
                    lifecycle.Dispose();
                    Check(logs.Any(message => message.Contains("Saw Chiseling absent")), $"{side} absent upstream startup logs no-op");
                    Check(before.SetEquals(Harmony.GetAllPatchedMethods()), $"{side} absent upstream leaves Harmony unchanged");
                }
                Console.WriteLine($"PASS: {passed} absent-upstream startup assertions.");
                return 0;
            }
            if(args.Length is < 1 or > 2 || args.Length==2 && args[1]!="--controls-only") throw new ArgumentException("Pass the exact upstream SawChiseling.dll path and optional --controls-only, or --absent.");
            var upstream = Assembly.LoadFrom(Path.GetFullPath(args[0]));
            upstreamType = upstream.GetType("SawChiseling.SawChiselPatch", true)!;
            Console.WriteLine($"Native API: {typeof(Block).Assembly.Location}\nUpstream: {upstream.Location}");
            foreach(var name in new[]{"TryResolveTargetBlock","PlaceChiseledBlock","HandleInteractStart","TryServerPlaceWithSaw"}) Console.WriteLine("Exact upstream: "+Method(name));
            var observations = new Harmony("SawContainerGuard.native-tests.observation");
            // Narrow observation seam: only the replacement BE owned by the current fixture is intercepted.
            // The real upstream resolver, eligibility, placement and SetBlock execute unchanged.
            observations.Patch(AccessTools.Method(typeof(BlockEntityMicroBlock), "WasPlaced"), prefix: new HarmonyMethod(typeof(Program), nameof(ObserveWasPlaced)));
            observations.Patch(AccessTools.Method(typeof(BlockEntity), "MarkDirty"), prefix: new HarmonyMethod(typeof(Program), nameof(ObserveDirty)));
            try
            {
                foreach(var name in new[]{"reed","wood","labeled","reinforced","modded","interface","groundstorage"})
                foreach(var full in new[]{false,true})
                {
                    var f = Container(name, full);
                    Convert(f, "UNPATCHED destructive " + name + " control " + full);
                }
                if(args.Length==2) { Console.WriteLine($"CONTROLS ONLY PASS: {passed} assertions; production guard NOT tested."); return 0; }
                var guard = Assembly.Load("SawContainerGuard").GetType("SawContainerGuard.GuardPatches", true)!;
                var install = guard.GetMethod("Install", BindingFlags.Public|BindingFlags.Static)!;
                var harmony = new Harmony("SawContainerGuard.native-tests.guard");
                var before = Harmony.GetAllPatchedMethods().ToHashSet();
                Check(!(bool)install.Invoke(null, new object[]{harmony, typeof(Program).Assembly})!, "absent upstream returns false");
                Check(before.SetEquals(Harmony.GetAllPatchedMethods()), "absent install makes no patch changes");
                Check((bool)install.Invoke(null, new object[]{harmony, upstream})!, "install actual upstream");
                foreach(var name in new[]{"TryResolveTargetBlock", "PlaceChiseledBlock", "HandleInteractStart"})
                    Check(Harmony.GetPatchInfo(Method(name))!.Owners.Contains(harmony.Id), "installed exact " + name);
                try
                {
                    foreach(var name in new[]{"reed","wood","labeled","reinforced","modded","interface","groundstorage"})
                    foreach(var full in new[]{false,true}) Rejected(Container(name, full), name + (full?" full":" empty"));
                    Rejected(new Fixture(Wood("custom-stateful"), new CustomEntity()), "custom noncontainer BE");
                    Rejected(new Fixture(Wood("declared-entity", entityClass:"CustomModEntity")), "declared EntityClass without instantiated BE");
                    Rejected(new Fixture(Wood("container-base", new BlockContainer())), "native BlockContainer without BE");
                    var behavioral = Wood("behavior-only-container");
                    behavioral.BlockBehaviors = new BlockBehavior[] { new BlockBehaviorContainer(behavioral) };
                    Rejected(new Fixture(behavioral), "native container behavior without BE");
                    var creature = Wood("creature-container");
                    creature.BlockBehaviors = new BlockBehavior[] { new BlockBehaviorCreatureContainer(creature) };
                    Rejected(new Fixture(creature), "native creature container behavior without BE");
                    Rejected(new Fixture(Wood("inventory-microblock", new BlockMicroBlock()), new InterfaceContainer()), "microblock subclass cannot exempt inventory BE");
                    foreach(var name in new[]{"planks-oak","log-placed-oak"}) Convert(new Fixture(Wood(name)), "patched stateless " + name);
                    var stone = new Fixture(Wood("stone-granite")); stone.Block!.BlockMaterial=EnumBlockMaterial.Stone;
                    Check(!Call("TryServerPlaceWithSaw",stone.World,Player,Position) && stone.SetCalls==0,"nonwood untouched");
                    Rejected(new Fixture(null), "null block");
                    var broken = new Fixture(Wood("broken-accessor")){ThrowLookup=true}; Rejected(broken,"lookup exception");
                    foreach(var args2 in new object?[][] {new object?[]{null,Position,null},new object?[]{new Fixture(Wood("planks")).World,null,null}})
                    {
                        Check(!CallArray("TryResolveTargetBlock",args2) && args2[2] == null,"invalid resolver fails closed");
                        Check(!Call("PlaceChiseledBlock",args2[0],args2[1],Wood("planks")),"invalid placement fails closed");
                    }
                    Check(!Call("PlaceChiseledBlock",new Fixture(Wood("planks")).World,Position,null),"null source placement fails closed");
                    var unavailable = new Fixture(Wood("unavailable-accessor")){NullAccessor=true}; Rejected(unavailable,"null accessor");
                    foreach(var block in new Block[]{new BlockChisel(),new BlockMicroBlock()})
                    {
                        var f = new Fixture(Wood("existing-edit",block),new BlockEntityChisel());
                        var resolve = new object?[]{f.World,Position,null};
                        Check(CallArray("TryResolveTargetBlock",resolve) && ReferenceEquals(resolve[2],block),"existing " + block.GetType().Name + " resolver exemption");
                    }
                }
                finally { harmony.UnpatchAll(harmony.Id); }
                foreach (var side in new[] { EnumAppSide.Client, EnumAppSide.Server })
                {
                    var logs = new List<string>();
                    var logger = Proxy.Make<ILogger>((m, a) => { logs.Add(string.Join(" ", a!.Select(value => value?.ToString()))); return null; });
                    var api = Proxy.Make<ICoreAPI>((m, a) => m.Name switch
                    {
                        "get_Logger" => logger,
                        "get_Side" => side,
                        _ => Proxy.Default(m.ReturnType)
                    });
                    var lifecycle = new SawContainerGuard.SawContainerGuardModSystem();
                    lifecycle.Start(api);
                    Check(Harmony.GetPatchInfo(Method("PlaceChiseledBlock"))?.Prefixes.Any(p => p.owner == "kuboczoch.sawcontainerguard") == true,
                        $"production {side} Start discovers original assembly and patches placement");
                    Rejected(new Fixture(Wood("lifecycle-container", new BlockContainer())), $"production {side} startup protection");
                    Check(logs.Any(message => message.Contains("Container conversion guard installed")), $"production {side} startup logs installation");
                    lifecycle.Dispose();
                    Check(Harmony.GetPatchInfo(Method("PlaceChiseledBlock"))?.Prefixes.Any(p => p.owner == "kuboczoch.sawcontainerguard") != true,
                        $"production {side} Dispose removes own prefixes");
                }
            }
            finally { observations.UnpatchAll(observations.Id); }
            Console.WriteLine($"PASS: {passed} native assertions. WasPlaced/MarkDirty are scoped observation seams, not a full initialized game world.");
            return 0;
        }
        catch(Exception e) { Console.Error.WriteLine(e); return 1; }
    }
    static bool ObserveWasPlaced(BlockEntityMicroBlock __instance, Block block)
    { if(!ReferenceEquals(__instance,observedEntity)) return true; observedSource=block; placedCalls++; return false; }
    static bool ObserveDirty(BlockEntity __instance)
    { if(!ReferenceEquals(__instance,observedEntity)) return true; dirtyCalls++; return false; }
    static MethodInfo Method(string name) => upstreamType.GetMethod(name,BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.Static)!;
    static bool Call(string name, params object?[] args) => CallArray(name,args);
    static bool CallArray(string name, object?[] args) => (bool)Method(name).Invoke(null,args)!;
    static void Check(bool ok,string text) { if(!ok) throw new Exception("FAIL: "+text); passed++; Console.WriteLine("PASS "+text); }
    static Block Wood(string code, Block? block=null,string? entityClass=null)
    { block??=new Block(); block.Code=new AssetLocation(code); block.BlockId=100; block.BlockMaterial=EnumBlockMaterial.Wood; block.Shape=new CompositeShape { Base=new AssetLocation("block/basic/cube") }; block.EntityClass=entityClass; return block; }
    static void Convert(Fixture f,string name)
    {
        observedEntity=f.Replacement; observedSource=null; placedCalls=dirtyCalls=0;
        var resolved=new object?[]{f.World,Position,null};
        Check(CallArray("TryResolveTargetBlock",resolved), name+" resolves source");
        Check(Call("TryServerPlaceWithSaw",f.World,Player,Position), name+" upstream returns true");
        Check(f.SetCalls==1 && ReferenceEquals(f.Block,f.Chiseled) && ReferenceEquals(f.Entity,f.Replacement),name+" actual SetBlock replacement");
        Check(placedCalls==1 && dirtyCalls==1 && ReferenceEquals(observedSource,resolved[2]),name+" native BE initialization calls observed");
        observedEntity=null;
    }
    static void Rejected(Fixture f,string name)
    {
        var original=f.Block; var entity=f.Entity;
        var inventory=(entity as IBlockEntityContainer)?.Inventory;
        var slots=inventory?.Select(s=>s.Itemstack).ToArray();
        var sizes=slots?.Select(s=>s?.StackSize??0).ToArray();
        var bytes=slots?.Select(s=>s?.ToBytes()).ToArray();
        var resolve=new object?[]{f.World,Position,Wood("sentinel")};
        Check(!CallArray("TryResolveTargetBlock",resolve) && resolve[2]==null,name+" resolver false + null out");
        Check(!Call("TryServerPlaceWithSaw",f.World,Player,Position),name+" server denied");
        Check(!Call("PlaceChiseledBlock",f.World,Position,original),name+" direct placement denied");
        foreach(var side in new[]{EnumAppSide.Client,EnumAppSide.Server})
        {
            f.Side=side;
            var actor=new EntityPlayer { World=f.World };
            var slot=new ItemSlot(new InventoryGeneric(1,"held-1",null!)); slot.Itemstack=new ItemStack(Saw);
            var args=new object?[]{Saw,slot,actor,new BlockSelection{Position=Position},null,true,EnumHandHandling.NotHandled};
            Check(CallArray("HandleInteractStart",args) && (EnumHandHandling)args[6]! == EnumHandHandling.NotHandled,name+" "+side+" resumes native held handler");
        }
        Check(f.SetCalls==0 && ReferenceEquals(f.Block,original) && ReferenceEquals(f.Entity,entity),name+" world identity unchanged");
        if(inventory!=null)
        {
            Check(ReferenceEquals(((IBlockEntityContainer)entity!).Inventory,inventory) && inventory.Select(s=>s.Itemstack).Zip(slots!).All(p=>ReferenceEquals(p.First,p.Second)) && inventory.Select(s=>s.Itemstack?.StackSize??0).SequenceEqual(sizes!),name+" inventory + stacks unchanged");
            Check(inventory.Select(s=>s.Itemstack?.ToBytes()).Zip(bytes!).All(p=>p.First==null?p.Second==null:p.Second!=null&&p.First.SequenceEqual(p.Second)),name+" serialized stack bytes unchanged");
        }
    }
    static Fixture Container(string name,bool full)
    {
        Block block=name switch { "labeled"=>new BlockLabeledChest(), "groundstorage"=>new BlockGroundStorage(), "interface"=>new Block(), "modded"=>new ModdedContainer(), _=>new BlockGenericTypedContainer() };
        BlockEntity entity=name switch {"labeled"=>new BlockEntityLabeledChest(),"modded"=>new ModdedChest(),"interface"=>new InterfaceContainer(),"groundstorage"=>new BlockEntityGroundStorage(),_=>new BlockEntityGenericTypedContainer()};
        var inventory=new InventoryGeneric(4,"fixture-1",null!);
        if(full) { inventory[0].Itemstack=new ItemStack(Wood("planks-oak"),17); inventory[2].Itemstack=new ItemStack(Wood("log-oak"),3); }
        if(entity is InterfaceContainer custom) custom.Inv=inventory;
        else
        {
            FieldInfo? field=null;
            for(var t=entity.GetType();t!=null && field==null;t=t.BaseType) field=t.GetFields(BindingFlags.Instance|BindingFlags.NonPublic|BindingFlags.Public|BindingFlags.DeclaredOnly).FirstOrDefault(f=>typeof(InventoryBase).IsAssignableFrom(f.FieldType));
            if(field==null) throw new Exception("No native inventory field for "+entity.GetType());
            field.SetValue(entity,inventory);
        }
        Check(ReferenceEquals(((IBlockEntityContainer)entity).Inventory,inventory),name+" real native inventory wired");
        return new Fixture(Wood(name+"-chest",block),entity);
    }
    sealed class ModdedContainer:BlockGenericTypedContainer {}
    sealed class ModdedChest:BlockEntityGenericTypedContainer {}
    sealed class CustomEntity:BlockEntity {}
    sealed class InterfaceContainer:BlockEntity,IBlockEntityContainer
    { public InventoryGeneric Inv=null!; public IInventory Inventory=>Inv; public string InventoryClassName=>"modded"; public void DropContents(Vec3d at) {} public void CheckInventoryClearedMidTick() {} }
    sealed class Fixture
    {
        public Block? Block; public readonly Block? Original; public BlockEntity? Entity;
        public readonly Block Chiseled=new BlockChisel { BlockId=900, Code=new AssetLocation("chiseledblock") };
        public readonly BlockEntityChisel Replacement=new();
        public int SetCalls; public bool ThrowLookup, NullAccessor; public EnumAppSide Side=EnumAppSide.Server;
        public IWorldAccessor World;
        public Fixture(Block? block,BlockEntity? entity=null)
        {
            Block=Original=block; Entity=entity;
            var accessor=Proxy.Make<IBlockAccessor>((m,a)=>
            {
                if(ThrowLookup && (m.Name=="GetBlock"||m.Name=="GetBlockEntity")) throw new InvalidOperationException("simulated unavailable chunk");
                if(m.Name=="GetBlock") return Block;
                if(m.Name=="GetBlockEntity") return Entity;
                if(m.Name=="SetBlock") { if((int)a![0]! != Chiseled.Id) throw new Exception("Unexpected replacement ID"); SetCalls++; Block=Chiseled; Entity=Replacement; return null; }
                return Proxy.Default(m.ReturnType);
            });
            World=Proxy.Make<IWorldAccessor>((m,a)=>m.Name switch {
                "get_BlockAccessor"=>NullAccessor?null:accessor,"get_Side"=>Side,"GetBlock"=>Chiseled,
                "get_Claims"=>Proxy.Make(m.ReturnType,(n,b)=>n.Name=="TryAccess"?true:Proxy.Default(n.ReturnType)),
                _=>Proxy.Default(m.ReturnType)});
        }
    }
}
public class Proxy:DispatchProxy
{
    public System.Func<MethodInfo,object?[]?,object?> Handler=null!;
    protected override object? Invoke(MethodInfo? targetMethod,object?[]? args)=>Handler(targetMethod!,args);
    public static T Make<T>(System.Func<MethodInfo,object?[]?,object?> handler) where T:class => (T)Make(typeof(T),handler);
    public static object Make(Type type,System.Func<MethodInfo,object?[]?,object?> handler) { var p=Create(type,typeof(Proxy)); ((Proxy)p).Handler=handler; return p; }
    public static object? Default(Type type)=>type==typeof(void)?null:type.IsValueType?Activator.CreateInstance(type):null;
}
