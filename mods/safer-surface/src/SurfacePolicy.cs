namespace SaferSurface;
public readonly record struct PositionStamp(int X,int Y,int Z,int Dimension);
public static class SurfacePolicy {
 public static bool Daylight(SurfaceConfig c,double sunElevationDegrees,double hour) => c.SeasonalDaylight ? double.IsFinite(sunElevationDegrees)&&sunElevationDegrees>c.MinimumSunElevation : c.DayStartHour<c.DayEndHour ? hour>=c.DayStartHour&&hour<c.DayEndHour : hour>=c.DayStartHour||hour<c.DayEndHour;
 public static bool Outside(SurfaceConfig c,double dx,double dy,double dz)=>dx*dx+dz*dz+(c.HorizontalDistance?0:dy*dy)>c.MaximumRange*c.MaximumRange;
 public static bool Reject(SurfaceConfig c,bool vanillaResult,bool player,bool pursuingSamePair,bool retaliatingSamePair,double dx,double dy,double dz,bool monsterSafe,bool playerSafe,bool sameDimension) => c.Enabled&&vanillaResult&&player&&!(c.PreserveExistingPursuit&&pursuingSamePair)&&!(c.VanillaDuringRetaliation&&retaliatingSamePair)&&sameDimension&&Outside(c,dx,dy,dz)&&monsterSafe&&playerSafe;
}
