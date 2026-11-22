using System.Reflection;
using System.Runtime.CompilerServices;
using ArtOfGrowing.Items;
using Kuboczoch.FirepitTinderReturn;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

class Program
{
 static int assertions;
 static void Check(bool value,string message) { if(!value)throw new Exception(message); assertions++; Console.WriteLine("PASS "+message); }
 static void Main(string[] args)
 {
  Console.WriteLine("API assembly: "+typeof(Block).Assembly.FullName);
  Console.WriteLine("AoG assembly: "+typeof(AOGItemDryGrass).Assembly.FullName);
  Check(FirepitTinderReturnSystem.FirepitCode(new DummySlot(new ItemStack(new Item{Code=new AssetLocation("other:drygrass")})))=="artofgrowing:firepit-construct1","unrelated mod item identity is not redirected");
  foreach(var literalCount in new[]{0,2})
  {
   bool rejected=false;
   try { FirepitTinderReturnSystem.ChooseTinderDomain(Enumerable.Range(0,literalCount).Select(_=>new HarmonyLib.CodeInstruction(System.Reflection.Emit.OpCodes.Ldstr,"artofgrowing:firepit-construct1"))).ToList(); }
   catch(InvalidOperationException){rejected=true;}
   Check(rejected,$"incompatible IL with {literalCount} literals rejected");
  }
  var ctx=new Context();
  // Real AoG method, no Harmony: reproduce bug before installing addon.
  ctx.Place("game:drygrass"); Check(ctx.Selected=="artofgrowing:firepit-construct1","baseline dry grass selects AoG/straw identity");
  var patch=new FirepitTinderReturnSystem();patch.ApplyPatch();patch.ApplyPatch();
  var second=new FirepitTinderReturnSystem();second.ApplyPatch();patch.Dispose();
  foreach(var name in new[]{"game:drygrass","game:thatch"})
  {
   ctx.Place(name);var domain=name=="game:drygrass"?"game":"artofgrowing";
   Check(ctx.Selected==domain+":firepit-construct1",name+" patched production placement identity");
   Check(ctx.StackLeft==1 && ctx.PlacedId==7,name+" original AoG consumes exactly one and places block");
   Check(ctx.Hand==EnumHandHandling.PreventDefaultAction,name+" original AoG hand handling");
   ctx.Place(name,allow:false);Check(ctx.StackLeft==2 && ctx.PlacedId==0,name+" original claims rejection preserved");
   ctx.Place(name,canPlace:false);Check(ctx.StackLeft==2 && ctx.PlacedId==0,name+" original placement rejection preserved");
   ctx.Place(name); // constructor identity is preserved through actual native construction calls
   for(int stage=1;stage<=4;stage++)
   {
    var b=ctx.MakeFirepit(domain,stage,args[0],args[1]);
    var drops=b.GetDrops(ctx.World,new BlockPos(0,1,0),null,1);
    var expected=name;
    Check(drops.Length==(stage==1?1:2),$"{domain} construct{stage} preserves complete drop list size");
    Check(drops.Count(d=>d.Collectible.Code.ToString()==expected && d.StackSize==1)==1,$"{domain} construct{stage} native GetDrops returns one {expected}");
    Check(drops.Where(d=>d.Collectible.Code.ToString()=="game:firewood").Sum(d=>d.StackSize)==stage-1,$"{domain} construct{stage} preserves firewood quantity");
    var nextPart=stage==4?"cold":"construct"+(stage+1);
    Check(b.NextStageCodePart==nextPart,$"{domain} construct{stage} next stage is {nextPart}");
    ctx.Next=new PlaceableFirepit{Code=new AssetLocation(domain+":firepit-"+nextPart),BlockId=20+stage};
    ctx.Exchanged=0;
    var wood=new Item{Code=new AssetLocation("game:firewood"),Attributes=JsonObject.FromJson("{firepitConstructable:true}")};
    Check(b.TryConstruct(ctx.World,new BlockPos(0,1,0),wood,null),$"{domain} construct{stage} native TryConstruct succeeds");
    Check(ctx.Selected==ctx.Next.Code.ToString() && ctx.Exchanged==ctx.Next.BlockId,$"{domain} construct{stage} native progression preserves domain");
   }
   foreach(var state in new[]{"cold","lit","extinct"})
   {
    var b=ctx.MakeFirepit(domain,0,args[0],args[1],state);
    Check(b.GetDrops(ctx.World,new BlockPos(0,1,0),null,1).Length==0,domain+" "+state+" has no extra tinder refund");
   }
  }
  second.Dispose();ctx.Place("game:drygrass");Check(ctx.Selected=="artofgrowing:firepit-construct1","last owner dispose restores original AoG method");
  Console.WriteLine($"SUCCESS {assertions} assertions");
 }
 public class Proxy:DispatchProxy
 {
  public System.Func<MethodInfo,object[],object> Handler;
  protected override object Invoke(MethodInfo m,object[] a)=>Handler(m,a);
  public static T New<T>(System.Func<MethodInfo,object[],object> h) where T:class {var o=Create<T,Proxy>();((Proxy)(object)o).Handler=h;return o;}
 }
 public class PlaceableFirepit:BlockFirepit
 {
  public bool Allowed=true;
  public override bool CanPlaceBlock(IWorldAccessor w,IPlayer p,BlockSelection s,ref string code)=>Allowed;
 }
 class Context
 {
  public IWorldAccessor World; public string Selected;public int PlacedId,StackLeft,Exchanged;public EnumHandHandling Hand;public Block Next;bool allow;
  PlaceableFirepit placement=new(){Code=new AssetLocation("game:firepit-construct1"),BlockId=7};
  Block support=new(){Code=new AssetLocation("game:stone"),SideSolid=new SmallBoolArray(63)};
  Dictionary<string,Item> items=new();
  public Context()
  {
   foreach(var c in new[]{"game:drygrass","game:thatch","game:firewood"})items[c]=new Item{Code=new AssetLocation(c),ItemId=items.Count+1,MaxStackSize=64};
   var ba=Proxy.New<IBlockAccessor>((m,a)=>m.Name switch {
    "GetBlock"=>support,"GetBlockEntity"=>null,"SetBlock"=>Set(a),"ExchangeBlock"=>Exchange(a),"MarkBlockDirty"=>null,_=>Default(m.ReturnType)});
   var claims=Proxy.New<ILandClaimAPI>((m,a)=>m.Name=="TryAccess"?allow:Default(m.ReturnType));
   World=Proxy.New<IWorldAccessor>((m,a)=>m.Name switch{
    "get_BlockAccessor"=>ba,"get_Claims"=>claims,"GetBlock"=>Lookup(a),"GetItem"=>items.GetValueOrDefault(a[0].ToString()),"get_Rand"=>new Random(123),"PlayerByUid"=>null,_=>Default(m.ReturnType)});
  }
  static object Default(Type t)=>t==typeof(void)?null:t.IsValueType?Activator.CreateInstance(t):null;
  object Set(object[] a){PlacedId=(int)a[0];return null;}
  object Exchange(object[] a){Exchanged=(int)a[0];return null;}
  object Lookup(object[] a){Selected=a[0].ToString();return Next??placement;}
  public void Place(string name,bool allow=true,bool canPlace=true)
  {
   this.allow=allow;Selected=null;PlacedId=0;Next=null;placement.Allowed=canPlace;
   var item=new AOGItemDryGrass{Code=new AssetLocation(name),ItemId=9,MaxStackSize=64};
   var slot=new DummySlot(new ItemStack(item,2));
   var entity=new EntityAgent{World=World}; entity.Controls.ShiftKey=true;
   var sel=new BlockSelection{Position=new BlockPos(0,0,0),Face=BlockFacing.UP};
   Hand=EnumHandHandling.NotHandled;
   item.OnHeldInteractStart(slot,entity,sel,null,true,ref Hand);
   StackLeft=slot.StackSize;
  }
  public BlockFirepit MakeFirepit(string domain,int stage,string nativePath,string aogPath,string state=null)
  {
   var doc=JObject.Parse(File.ReadAllText(domain=="game"?nativePath:aogPath));
   var key="firepit-"+(state??("construct"+stage));
   var b=new BlockFirepit{Code=new AssetLocation(domain+":"+key),BlockId=100+stage};
   b.Drops=(doc["dropsByType"][key]??doc["dropsByType"]["*"]).ToObject<BlockDropItemStack[]>();
   foreach(var d in b.Drops) d.Resolve(World,"regression asset",new AssetLocation(domain+":blocktypes/wood/firepit.json"));
   return b;
  }
 }
}
