using System.Reflection;
using System.Linq.Expressions;
using HarmonyLib;
using SaferSurface;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

// Only native noise is replaced: every environmental read and cache decision is production code.
static class EnvironmentTests
{
    const string Owner = "safer-surface.environment-tests";
    static float stability;
    static int stabilityCalls;
    static (double X,double Y,double Z) stabilityPosition;
    public static bool StabilityPrefix(double __0,double __1,double __2,ref float __result) {
        stabilityCalls++;stabilityPosition=(__0,__1,__2);__result=stability;return false;
    }
    static void Check(bool value,string message) { if(!value)throw new Exception(message); }
    public static void Register(Action<string,Action> test) {
        test("cold native environmental reads: terrain, light, astronomy, stability and storm",ColdReads);
        test("cold native endpoint independence and dimension",Endpoints);
        test("cold native negative coordinate floor and map indexing",NegativeCoordinates);
        test("native environmental cache TTL, movement and bounded eviction",Cache);
    }
    static void With(Action action) {
        stability=1;stabilityCalls=0;
        var h=new Harmony(Owner);
        var method=AccessTools.Method(typeof(SystemTemporalStability),"GetTemporalStability",[typeof(double),typeof(double),typeof(double)]);
        Check(method!=null&&method.ReturnType==typeof(float),"Native stability signature changed");
        h.Patch(method!,prefix:new HarmonyMethod(typeof(EnvironmentTests),nameof(StabilityPrefix)));
        try { action(); } finally {h.UnpatchAll(Owner);}
    }
    sealed class Fixture : IDisposable {
        public readonly SurfaceConfig Config;
        public readonly EnvironmentProbe Probe;
        public readonly SystemTemporalStability Temporal=new();
        public readonly IServerWorldAccessor World;
        public long Now=10000;
        public int Sunlight=32,Blocklight=0,Height=110,Reads,SunCalls;
        public float SunY=0.5f;
        public double Hour=2;
        public double Days=100;
        public bool Loaded=true,Disposed,Valid=true;
        public BlockPos? LastPos;
        public (int X,int Z) MapPosition;
        public Fixture(SurfaceConfig? config=null) {
            Config=config??new SurfaceConfig {CacheTtlMs=0};
            var chunk=Fake.Create<IWorldChunk>((m,a)=>m.Name=="get_Disposed"?Disposed:throw new Exception("Unexpected chunk "+m));
            var map=Fake.Create<IMapChunk>((m,a)=> {
                if(m.Name is "get_WorldGenTerrainHeightMap" or "get_TerrainHeightMap") {
                    var result=Array.CreateInstance(m.ReturnType.GetElementType()!,32*32);
                    for(int i=0;i<result.Length;i++)result.SetValue(Convert.ChangeType(Height,m.ReturnType.GetElementType()!),i);
                    return result;
                }
                throw new Exception("Unexpected map "+m);
            });
            var blocks=Fake.Create<IBlockAccessor>((m,a)=> {
                switch(m.Name) {
                    case "IsValidPos": LastPos=((BlockPos)a![0]!).Copy();return Valid;
                    case "GetChunkAtBlockPos": Reads++;return Loaded?chunk:null;
                    case "GetMapChunk": MapPosition=((int)a![0]!, (int)a[1]!);return map;
                    case "GetLightLevel": return Convert.ChangeType((EnumLightLevelType)a![1]! == EnumLightLevelType.OnlySunLight?Sunlight:Blocklight,m.ReturnType);
                    default:throw new Exception("Unexpected block accessor "+m);
                }
            });
            var calendar=Fake.Create<IGameCalendar>((m,a)=> {
                switch(m.Name) {
                    case "get_HourOfDay":return Convert.ChangeType(Hour,m.ReturnType);
                    case "get_TotalDays":return Convert.ChangeType(Days,m.ReturnType);
                    case "get_OnGetSolarSphericalCoords":case "get_OnGetLatitude":
                        var invoke=m.ReturnType.GetMethod("Invoke")!;
                        return Expression.Lambda(m.ReturnType,Expression.Default(invoke.ReturnType),invoke.GetParameters().Select(p=>Expression.Parameter(p.ParameterType))).Compile();
                    case "GetSunPosition":SunCalls++;return new Vec3f(0,SunY,0);
                    default:throw new Exception("Unexpected calendar "+m);
                }
            });
            World=Fake.Create<IServerWorldAccessor>((m,a)=>m.Name switch {
                "get_ElapsedMilliseconds"=>Now,"get_BlockAccessor"=>blocks,"get_Calendar"=>calendar,
                _=>throw new Exception("Unexpected world "+m)
            });
            var loader=Fake.Create<IModLoader>((m,a)=>m.Name=="GetModSystem"?Temporal:throw new Exception("Unexpected mod loader "+m));
            var api=Fake.Create<ICoreServerAPI>((m,a)=>m.Name switch {
                "get_World"=>World,"get_ModLoader"=>loader,_=>throw new Exception("Unexpected API "+m)
            });
            Probe=new EnvironmentProbe(api,Config);
        }
        public Entity Entity(long id=1,bool player=false,double x=10,double y=110,double z=10) {
            Entity e=player?new EntityPlayer():new EntityAgent();e.EntityId=id;e.World=World;e.Pos.SetPos(x,y,z);return e;
        }
        public void Dispose()=>Probe.Dispose();
    }
    static void ColdReads()=>With(()=> {
        using var f=new Fixture();var e=f.Entity();
        Check(f.Probe.IsSafe(e),"Cold sunlit surface must be safe (also verifies native proxy return types)");
        Check(f.Reads==1&&f.SunCalls==1&&stabilityCalls==1,"Cold path did not reach native lighting/solar/stability");
        Check(stabilityPosition==(10d,110d,10d),"Native stability coordinates");
        f.Blocklight=32;e.Pos.Y=109;Check(!f.Probe.IsSafe(e),"Torchlit underground accepted");e.Pos.Y=110;
        f.Sunlight=11;Check(!f.Probe.IsSafe(e),"Torch light substituted for weak sunlight");
        f.Sunlight=32;f.SunY=-0.5f;f.Hour=12;Check(!f.Probe.IsSafe(e),"Below-horizon solar position accepted during fixed daylight");
        f.SunY=0.5f;f.Hour=2;Check(f.Probe.IsSafe(e),"Seasonal daylight must replace the fixed-hour window");
        f.Config.SeasonalDaylight=false;Check(!f.Probe.IsSafe(e),"Fixed night accepted");f.Hour=12;Check(f.Probe.IsSafe(e),"Fixed daytime rejected");
        f.Config.MinimumBlockLight=10;f.Blocklight=9;Check(!f.Probe.IsSafe(e),"Additional block-light threshold ignored");f.Blocklight=10;
        stability=0.89f;Check(!f.Probe.IsSafe(e),"Unstable native environment accepted");stability=0.95f;Check(f.Probe.IsSafe(e),"Stable native environment rejected");
        stability=float.NaN;Check(!f.Probe.IsSafe(e),"Nonfinite stability accepted");stability=1;
        f.Temporal.StormData.nowStormActive=true;int reads=f.Reads;Check(!f.Probe.IsSafe(e)&&f.Reads==reads,"Storm failed to bypass reads");f.Temporal.StormData.nowStormActive=false;
        f.Config.VanillaDuringTemporalStorms=false;f.Temporal.StormData.nowStormActive=true;Check(f.Probe.IsSafe(e),"Storm bypass opt-out ignored");f.Temporal.StormData.nowStormActive=false;f.Config.VanillaDuringTemporalStorms=true;
        f.Sunlight=0;f.Config.VanillaInDarkLocations=false;Check(f.Probe.IsSafe(e),"Dark bypass opt-out ignored");f.Config.VanillaInDarkLocations=true;f.Sunlight=32;
        stability=0;f.Config.VanillaInUnstableRegions=false;Check(f.Probe.IsSafe(e),"Stability opt-out ignored");f.Config.VanillaInUnstableRegions=true;stability=1;
        e.Pos.Y=109;Check(f.Probe.IsSafe(e,false),"Optional hostile terrain override ignored");Check(!f.Probe.IsSafe(e),"Terrain requirement not independent of override cache");e.Pos.Y=110;
        f.Loaded=false;Check(!f.Probe.IsSafe(e),"Unloaded chunk accepted");f.Loaded=true;f.Disposed=true;Check(!f.Probe.IsSafe(e),"Disposed chunk accepted");
    });
    static void Endpoints()=>With(()=> {
        using var f=new Fixture(new SurfaceConfig {CacheTtlMs=500});
        var monster=f.Entity();var player=f.Entity(2,true,y:109);
        Check(f.Probe.IsSafe(monster)&&!f.Probe.IsSafe(player),"Safe monster leaked evidence to underground player");
        player.Pos.Y=110;monster.Pos.Y=109;
        Check(f.Probe.IsSafe(player)&&!f.Probe.IsSafe(monster),"Player/monster endpoint evidence must be independent");
        int reads=f.Reads;player.Pos.Dimension=1;Check(!f.Probe.IsSafe(player)&&f.Reads==reads,"Other dimension reused or requested overworld evidence");
    });
    static void NegativeCoordinates()=>With(()=> {
        using var f=new Fixture();
        var negative=f.Entity(3,x:-0.2,z:-32.2);
        Check(f.Probe.IsSafe(negative),"Negative coordinates rejected on loaded sunlit surface");
        Check(f.LastPos!.X==-1&&f.LastPos.Z==-33&&f.MapPosition==(-1,-2),"Negative coordinates must floor block and map chunk indices");
        Check(stabilityPosition==(-1d,110d,-33d),"Negative native stability coordinates truncated");
        int reads=f.Reads;negative.Pos.Dimension=1;Check(!f.Probe.IsSafe(negative)&&f.Reads==reads,"Other dimension reused or requested overworld evidence");
    });
    static void Cache()=>With(()=> {
        using var f=new Fixture(new SurfaceConfig {CacheTtlMs=500,CacheCapacity=16});var e=f.Entity();
        Check(f.Probe.IsSafe(e)&&f.Reads==1,"Cold cache read");f.Sunlight=0;f.Now+=499;
        Check(f.Probe.IsSafe(e)&&f.Reads==1,"TTL expired early");f.Now++;
        Check(!f.Probe.IsSafe(e)&&f.Reads==2,"Exact TTL boundary did not refresh");f.Sunlight=32;f.Now+=500;
        Check(f.Probe.IsSafe(e),"Expired negative evidence did not refresh");
        e.Pos.X+=0.1;int reads=f.Reads;Check(f.Probe.IsSafe(e)&&f.Reads==reads,"Same-block movement invalidated cache");
        f.Sunlight=0;e.Pos.X++;Check(!f.Probe.IsSafe(e)&&f.Reads==reads+1,"Block movement reused safe evidence");
        f.Sunlight=32;f.Now+=500;Check(f.Probe.IsSafe(e),"Cache reset");
        f.Temporal.StormData.nowStormActive=true;reads=f.Reads;Check(!f.Probe.IsSafe(e)&&f.Reads==reads,"Storm failed to override cached safe endpoint");f.Temporal.StormData.nowStormActive=false;
        f.Sunlight=0;f.Days+=1;reads=f.Reads;Check(!f.Probe.IsSafe(e)&&f.Reads==reads+1,"Calendar jump reused stale cached environment");f.Sunlight=32;f.Now+=500;Check(f.Probe.IsSafe(e),"Calendar jump reset");
        f.Disposed=true;reads=f.Reads;Check(!f.Probe.IsSafe(e)&&f.Reads==reads+1,"Disposed chunk reused safe cached environment");f.Disposed=false;f.Now+=500;Check(f.Probe.IsSafe(e),"Chunk disposal reset");
        for(int id=2;id<=17;id++)Check(f.Probe.IsSafe(f.Entity(id)),"Capacity fill failed");
        f.Sunlight=0;reads=f.Reads;Check(!f.Probe.IsSafe(e)&&f.Reads==reads+1,"Oldest entry not evicted at capacity");
        var cache=(System.Collections.IDictionary)AccessTools.Field(typeof(EnvironmentProbe),"cache").GetValue(f.Probe)!;Check(cache.Count==16,"Cache exceeded configured capacity");
        f.Probe.Dispose();reads=f.Reads;Check(!f.Probe.IsSafe(e)&&f.Reads==reads&&cache.Count==0,"Disposed cache retained evidence or read world");
    });
}
