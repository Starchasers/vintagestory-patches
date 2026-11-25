using System;
using System.Collections.Generic;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace SaferSurface;

/// <summary>
/// On-demand server-thread-only environmental predicate. False always leaves vanilla
/// acquisition alone. The caller must test BOTH the hostile and player endpoints.
/// No listeners, timers, world scans, rays, or visibility/pathfinding queries.
/// </summary>
public sealed class EnvironmentProbe : IDisposable
{
    private readonly ICoreServerAPI api;
    private readonly SurfaceConfig config;
    private readonly Dictionary<long, Entry> cache;
    private readonly long[] evictionOrder;
    private int nextEviction;
    private bool disposed;
    private bool warnedUnavailable;
    private SystemTemporalStability? temporal;

    private readonly struct Entry
    {
        public readonly int X, Y, Z, Dimension;
        public readonly long CheckedAt;
        public readonly bool Safe;
        public readonly bool SurfaceRequired;
        public readonly double CalendarDay;
        public readonly IWorldChunk? Chunk;

        public Entry(int x, int y, int z, int dimension, long checkedAt, bool safe) : this(x,y,z,dimension,checkedAt,safe,true) { }
        public Entry(int x, int y, int z, int dimension, long checkedAt, bool safe, bool surfaceRequired, double calendarDay = double.NaN, IWorldChunk? chunk = null)
        {
            X = x;
            Y = y;
            Z = z;
            Dimension = dimension;
            CheckedAt = checkedAt;
            Safe = safe;
            SurfaceRequired = surfaceRequired;
            CalendarDay = calendarDay;
            Chunk = chunk;
        }

        public bool Matches(int x, int y, int z, int dimension, long now, int ttl)
        {
            // Spatial identity is mandatory even while the TTL is still valid.
            return X == x && Y == y && Z == z && Dimension == dimension
                && now >= CheckedAt && now - CheckedAt < ttl;
        }
    }

    public EnvironmentProbe(ICoreServerAPI api, SurfaceConfig config)
    {
        this.api = api ?? throw new ArgumentNullException(nameof(api));
        this.config = config ?? throw new ArgumentNullException(nameof(config));
        // Bound memory even if an unvalidated configuration reaches this adapter.
        int capacity = Math.Clamp(config.CacheCapacity, 1, 65536);
        cache = new Dictionary<long, Entry>(capacity);
        evictionOrder = new long[capacity];
    }

    public bool IsSafe(Entity entity, bool requireSurface = true)
    {
        if (disposed || !config.Enabled || entity == null) return false;

        try
        {
            var position = entity.Pos;
            if (position == null || !double.IsFinite(position.X)
                || !double.IsFinite(position.Y) || !double.IsFinite(position.Z)
                || position.X < int.MinValue || position.X >= int.MaxValue
                || position.Y < int.MinValue || position.Y >= int.MaxValue
                || position.Z < int.MinValue || position.Z >= int.MaxValue) return false;

            int x = (int)Math.Floor(position.X);
            int y = (int)Math.Floor(position.Y);
            int z = (int)Math.Floor(position.Z);
            int dimension = position.Dimension;

            // Map-column terrain and native stability do not expose a dimension
            // parameter. Never mistake another dimension for the overworld surface.
            if (dimension != 0) return false;

            temporal ??= api.ModLoader.GetModSystem<SystemTemporalStability>();
            // A global storm must override even a previously safe cached endpoint.
            if (temporal == null || temporal.StormData == null) return Unavailable();
            if (config.VanillaDuringTemporalStorms && temporal.StormData.nowStormActive) return false;

            long now = api.World.ElapsedMilliseconds;
            long id = entity.EntityId;
            bool existing = cache.TryGetValue(id, out Entry entry);
            if (existing && entry.SurfaceRequired == requireSurface && entry.Matches(x, y, z, dimension, now, Math.Max(0, config.CacheTtlMs))
                && (entry.Chunk == null || !entry.Chunk.Disposed)
                && (double.IsNaN(entry.CalendarDay) || Math.Abs(api.World.Calendar.TotalDays-entry.CalendarDay)<=1.0/1440))
                return entry.Safe;

            bool safe = Evaluate(x, y, z, requireSurface, out var sampledChunk);
            if (!existing)
            {
                if (cache.Count == evictionOrder.Length)
                    cache.Remove(evictionOrder[nextEviction]);
                evictionOrder[nextEviction] = id;
                nextEviction = (nextEviction + 1) % evictionOrder.Length;
            }
            cache[id] = new Entry(x, y, z, dimension, now, safe, requireSurface, api.World.Calendar?.TotalDays ?? double.NaN, sampledChunk);
            return safe;
        }
        catch (Exception)
        {
            // Missing systems, unloaded data and unavailable API services must not
            // suppress vanilla behavior. Do not log per acquisition attempt.
            return Unavailable();
        }
    }

    private bool Unavailable()
    {
        if (!warnedUnavailable)
        {
            warnedUnavailable=true;
            try { api.Logger.Warning("[Safer Surface] Native environment unavailable; affected acquisition remains vanilla. This warning is emitted once per session."); } catch { }
        }
        return false;
    }

    private bool Evaluate(int x, int y, int z, bool requireSurface, out IWorldChunk? sampledChunk)
    {
        sampledChunk=null;
        var world = api.World;
        var accessor = world.BlockAccessor;
        var calendar = world.Calendar;
        if (accessor == null || calendar == null) return Unavailable();

        var pos = new BlockPos(x, y, z, 0);
        if (!accessor.IsValidPos(pos)) return false;
        var chunk = accessor.GetChunkAtBlockPos(pos);
        if (chunk == null || chunk.Disposed) return false;
        sampledChunk=chunk;

        const int chunkSize = GlobalConstants.ChunkSize;
        int chunkX = (int)Math.Floor(x / (double)chunkSize);
        int chunkZ = (int)Math.Floor(z / (double)chunkSize);
        var mapChunk = accessor.GetMapChunk(chunkX, chunkZ);
        var heights = mapChunk?.WorldGenTerrainHeightMap;
        int index = (z - chunkZ * chunkSize) * chunkSize + (x - chunkX * chunkSize);
        if (heights == null || index < 0 || index >= heights.Length) return false;
        // Generated terrain excludes tree/roof height; sunlight independently
        // excludes dark cover. This deliberately isn't an exact cave/roof test.
        if (requireSurface && (long)y < (long)heights[index] - Math.Max(0, config.SurfaceDepthTolerance)) return false;

        int sunlight = accessor.GetLightLevel(pos, EnumLightLevelType.OnlySunLight);
        if (sunlight < 0 || sunlight > 32 || (config.VanillaInDarkLocations && sunlight < Math.Max(1, Math.Clamp(config.MinimumSunlight, 0, 32)))) return false;
        // Torch/block light can be an ADDITIONAL requirement, never a substitute
        // for sunlight (including when MinimumSunlight is configured as zero).
        int blocklight = accessor.GetLightLevel(pos, EnumLightLevelType.OnlyBlockLight);
        if (blocklight < 0 || blocklight > 32 || (config.VanillaInDarkLocations && blocklight < Math.Clamp(config.MinimumBlockLight, 0, 32))) return false;

        if (config.SeasonalDaylight)
        {
            if (calendar.OnGetSolarSphericalCoords == null || calendar.OnGetLatitude == null) return Unavailable();
            // Native astronomy evaluates the local latitude and current season.
            // Y is the normalized solar elevation sine; the setting is in degrees.
            // This branch replaces, rather than intersects, the fixed-hour window.
            var sun = calendar.GetSunPosition(new Vec3d(x, y, z), calendar.TotalDays);
            if (sun == null || !float.IsFinite(sun.Y) || sun.Y < -1 || sun.Y > 1
                || !double.IsFinite(config.MinimumSunElevation)) return false;
            double elevation = Math.Asin(sun.Y) * (180.0 / Math.PI);
            if (elevation <= config.MinimumSunElevation) return false;
        }
        else
        {
            double hour = calendar.HourOfDay;
            double start = config.DayStartHour;
            double end = config.DayEndHour;
            if (!double.IsFinite(hour) || !double.IsFinite(start) || !double.IsFinite(end)
                || start < 0 || start >= 24 || end < 0 || end > 24 || start == end) return false;
            bool daytime = start < end ? hour >= start && hour < end : hour >= start || hour < end;
            if (!daytime) return false;
        }

        // The native environmental value includes altitude/noise, storm glitch
        // strength and registered modifiers; it is NOT a player's stored gear meter.
        if (!config.VanillaInUnstableRegions) return true;
        float stability = temporal?.GetTemporalStability(x, y, z) ?? float.NaN;
        return float.IsFinite(stability) && double.IsFinite(config.MinimumTemporalStability)
            && stability >= config.MinimumTemporalStability;
    }

    public void Dispose()
    {
        disposed = true;
        cache.Clear();
        temporal = null;
    }
}
