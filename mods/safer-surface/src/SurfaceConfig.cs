namespace SaferSurface;
public sealed class SurfaceConfig {
 public bool Enabled {get;set;}=true;
 public double MaximumRange {get;set;}=8;
 public bool HorizontalDistance {get;set;}
 public bool SeasonalDaylight {get;set;}=true;
 public bool VanillaDuringTemporalStorms {get;set;}=true;
 public bool VanillaInDarkLocations {get;set;}=true;
 public bool VanillaInUnstableRegions {get;set;}=true;
 public bool VanillaDuringRetaliation {get;set;}=true;
 public bool PreserveExistingPursuit {get;set;}=true;
 public bool RequireBothEndpointsOnSurface {get;set;}=true;
 public bool DebugLogging {get;set;}
 public double DayStartHour {get;set;}=9;
 public double DayEndHour {get;set;}=19;
 public double MinimumSunElevation {get;set;}=0;
 public int MinimumSunlight {get;set;}=12;
 public int MinimumBlockLight {get;set;}=0;
 public double MinimumTemporalStability {get;set;}=0.9;
 public int SurfaceDepthTolerance {get;set;}=0;
 public int CacheTtlMs {get;set;}=500;
 public int CacheCapacity {get;set;}=2048;
 public string[] IncludeEntities {get;set;}=["game:drifter-*","game:shiver-*","game:bowtorn-*"];
 public string[] ExcludeEntities {get;set;}=["*-boss*","game:drifter-double*","game:drifter-stilt","game:drifter-bellhead","game:drifter-deepsplit","game:drifter-gearfoot","game:shiver-double*","game:bowtorn-double*"];
 public string? Validate(){
  if(!double.IsFinite(MaximumRange)||MaximumRange<=0||MaximumRange>128)return "MaximumRange must be finite and in (0,128]";
  if(!double.IsFinite(DayStartHour)||!double.IsFinite(DayEndHour)||DayStartHour<0||DayStartHour>=24||DayEndHour<0||DayEndHour>24||DayStartHour==DayEndHour)return "Fixed daylight hours must be distinct, start in [0,24), end in [0,24] (wrapping supported)";
  if(!double.IsFinite(MinimumSunElevation)||MinimumSunElevation< -90||MinimumSunElevation>90)return "MinimumSunElevation must be degrees in [-90,90]";
  if(MinimumSunlight<0||MinimumSunlight>32||MinimumBlockLight<0||MinimumBlockLight>32)return "Light thresholds must be in [0,32]";
  if(!double.IsFinite(MinimumTemporalStability)||MinimumTemporalStability<0||MinimumTemporalStability>1)return "MinimumTemporalStability must be [0,1]";
  if(SurfaceDepthTolerance<0||SurfaceDepthTolerance>8)return "SurfaceDepthTolerance must be [0,8]";
  if(CacheTtlMs<0||CacheTtlMs>10000||CacheCapacity<16||CacheCapacity>65536)return "Cache requires TTL [0,10000] ms and capacity [16,65536]";
  if(IncludeEntities==null||ExcludeEntities==null||IncludeEntities.Length>128||ExcludeEntities.Length>128)return "Entity lists required (max 128 each)";
  foreach(var pattern in IncludeEntities.Concat(ExcludeEntities))if(string.IsNullOrWhiteSpace(pattern)||pattern.Length>128||pattern.Count(ch=>ch=='*')>2)return "Entity patterns require nonempty <=128-character strings and max two wildcards";
  return null;
 }
 public bool Includes(string code){foreach(var p in ExcludeEntities)if(Matches(code,p))return false;foreach(var p in IncludeEntities)if(Matches(code,p))return true;return false;}
 static bool Matches(string value,string pattern){int v=0,p=0,star=-1,mark=0;while(v<value.Length){if(p<pattern.Length&&pattern[p]==value[v]){p++;v++;}else if(p<pattern.Length&&pattern[p]=='*'){star=p++;mark=v;}else if(star>=0){p=star+1;v=++mark;}else return false;}while(p<pattern.Length&&pattern[p]=='*')p++;return p==pattern.Length;}
}
