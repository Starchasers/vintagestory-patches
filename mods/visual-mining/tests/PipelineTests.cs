#nullable enable
using System.Collections;
using System.Reflection;
using Kuboczoch.VisualMining;
using Newtonsoft.Json.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.Client.NoObf;
using Vintagestory.GameContent;

// Real renderer/contact/voxel code with only world queries and render invalidation mocked.
internal static class PipelineTests {
 public static void Run(string game, AnimationManager manager, ContactTracker contact) {
  var fixture=JObject.Parse(File.ReadAllText(Path.Combine(game,"assets/survival/blocktypes/wood/woodtyped/log.json")));
  if ((string?)fixture["class"]!="BlockLog" || (string?)fixture["drawtype"]!="cube" || (string?)fixture["blockmaterial"]!="Wood") throw new Exception("Grown log fixture changed");
  var block=new BlockLog { BlockId=42, Code=new AssetLocation("game:log-grown-oak-ud"), Resistance=(float?)fixture["resistance"] ?? 3, CollisionBoxes=new[]{new Cuboidf(0,0,0,1,1,1)}, SideOpaque=new Vintagestory.API.Datastructures.SmallBoolArray(63), SideSolid=new Vintagestory.API.Datastructures.SmallBoolArray(63) };
  var pos=new BlockPos(0).Set(47,82,-40);
  int redraws=0; int redrawBatch=1+BlockFacing.ALLFACES.Length;
  var validRedrawPositions=BlockFacing.ALLFACES.Select(face => pos.AddCopy(face)).Append(pos).Select(p=>(p.X,p.Y,p.Z,p.dimension)).ToHashSet();
  var accessor=StrictProxy.Create<IBlockAccessor>((m,a)=>m.Name switch {"GetBlock"=>block,"MarkBlockDirty"=>Dirty(a),_=>throw new Exception("Unexpected world operation: "+m.Name)});
  object? Dirty(object?[]? args){ var p=(BlockPos)args![0]!; if(!validRedrawPositions.Contains((p.X,p.Y,p.Z,p.dimension)))throw new Exception("Redraw changed world coordinate domain");redraws++;return null; }
  var world=StrictProxy.Create<IClientWorldAccessor>((m,a)=>m.Name=="get_BlockAccessor"?accessor:throw new Exception(m.Name));
  var logger=StrictProxy.Create<ILogger>((m,a)=>null);
  var api=StrictProxy.Create<ICoreClientAPI>((m,a)=>m.Name switch {"get_World"=>world,"get_Logger"=>logger,_=>throw new Exception(m.Name)});
  var type=typeof(CosmeticVoxelRenderer);const BindingFlags flags=BindingFlags.NonPublic|BindingFlags.Static;
  var entries=(IDictionary)type.GetField("Entries",flags)!.GetValue(null)!;
  type.GetField("api",flags)!.SetValue(null,api);
  var damage=new BlockDamage{Position=pos,Block=block,RemainingResistance=block.Resistance*.75f};
  var selection=new BlockSelection{Position=pos,Face=BlockFacing.NORTH,HitPosition=new Vec3d(.5,.5,0)};
  try {
   CosmeticVoxelRenderer.Update(damage,selection,EnumTool.Axe);
   if(entries.Count!=1||redraws!=0)throw new Exception("Damage observation entry failed or prematurely redrew");
   var entry=entries.Values.Cast<object>().Single();var committed=entry.GetType().GetField("Committed")!;
   if((float)committed.GetValue(entry)!!=0)throw new Exception("Damage observation prematurely committed");
   var inactive=manager.GetAnimationState("kvm-axe");
   if(inactive==null)throw new Exception("Runtime registered axe missing");
   contact.Reset(); contact.Sample("grownlog",0,0,6);
   if(contact.Sample("grownlog",0,7,6))CosmeticVoxelRenderer.CommitImpact(damage,selection,EnumTool.Axe);
   if((float)committed.GetValue(entry)!<=0||redraws!=redrawBatch)throw new Exception("Activation/contact/entry/commit/redraw pipeline failed");
   CosmeticVoxelRenderer.CommitImpact(damage,selection,EnumTool.Axe);
   if(redraws!=redrawBatch)throw new Exception("Unchanged damage redrew twice");
   CosmeticVoxelRenderer.Reset(pos);
   if(entries.Count!=0||redraws!=2*redrawBatch)throw new Exception("Reset failed to restore vanilla redraw");
   Console.WriteLine("PASS real grown-log fixture: registered axe → contact → observed damage → committed voxel entry → world-coordinate redraw → vanilla restoration; strict accessor rejects gameplay writes");
  } finally {entries.Clear();type.GetField("api",flags)!.SetValue(null,null);}
 }
}
public class StrictProxy:DispatchProxy {
 public System.Func<MethodInfo,object?[]?,object?> Handler=null!;
 protected override object? Invoke(MethodInfo? targetMethod,object?[]? args)=>Handler(targetMethod!,args);
 public static T Create<T>(System.Func<MethodInfo,object?[]?,object?> handler)where T:class {var value=Create<T,StrictProxy>();((StrictProxy)(object)value).Handler=handler;return value;}
}
