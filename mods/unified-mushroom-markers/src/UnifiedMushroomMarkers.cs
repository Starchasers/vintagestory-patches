using Egocarib.AutoMapMarkers.Settings;
using Egocarib.AutoMapMarkers.Utilities;
using HarmonyLib;
using Vintagestory.API.Common;
using Settings = Egocarib.AutoMapMarkers.Settings.MapMarkerConfig.Settings;

namespace Kuboczoch.UnifiedMushroomMarkers;

public sealed class UnifiedMushroomMarkersMod : ModSystem
{
    private Harmony? harmony;
    public override bool ShouldLoad(EnumAppSide side) => side == EnumAppSide.Client;
    // Install before Auto Map Markers initializes its settings and registry.
    public override double ExecuteOrder() => 0;
    public override void StartPre(ICoreAPI api)
    {
        base.StartPre(api);
        harmony = new Harmony("kuboczoch.unifiedmushroommarkers");
        try { harmony.PatchAll(typeof(UnifiedMushroomMarkersMod).Assembly); }
        catch { harmony.UnpatchAll(harmony.Id); harmony = null; throw; }
    }
    public override void Dispose() { harmony?.UnpatchAll(harmony.Id); harmony = null; base.Dispose(); }
}

public static class MushroomPolicy
{
    public const string Safe = "egocarib-mapmarkers:safe-mushrooms";
    public const string Unsafe = "egocarib-mapmarkers:unsafe-mushrooms";
    public static bool IsMushroom(string? label) => label == Safe || label == Unsafe ||
        label?.StartsWith(Safe + "-", StringComparison.Ordinal) == true ||
        label?.StartsWith(Unsafe + "-", StringComparison.Ordinal) == true;

    public static void Initialize(Settings settings, List<MarkerCategoryDef> definitions)
    {
        // Read effective pre-patch enablement, including selected expanded subcategories.
        // Only do this at load time; reapplying it on every registry rebuild would resurrect disabled rows.
        var children = (Dictionary<string, Settings.AutoMapMarkerSetting>)AccessTools.Field(typeof(Settings), "AddonMarkerSettings").GetValue(settings)!;
        var parents = definitions.SelectMany(c => c.Entries).Where(e => e.Label == Safe || e.Label == Unsafe).ToList();
        if (parents.Count > 0)
        {
            bool enabled = parents.Any(e => settings.IsExpanded(e)
                ? e.ExpandableEntries!.Any(child => children.TryGetValue(child.Label, out var value) && value.Enabled)
                : (e.Label == Safe ? settings.AutoMapMarkers.OrganicMatter.SafeMushroom.Enabled : settings.AutoMapMarkers.OrganicMatter.UnsafeMushroom.Enabled));
            settings.AutoMapMarkers.OrganicMatter.SafeMushroom.Enabled = enabled;
            settings.AutoMapMarkers.OrganicMatter.UnsafeMushroom.Enabled = enabled;
        }
        Normalize(settings);
    }

    public static void Normalize(Settings settings)
    {
        var organic = settings.AutoMapMarkers.OrganicMatter;
        var unified = organic.SafeMushroom;
        if (!ReferenceEquals(unified, organic.UnsafeMushroom))
            unified.Enabled |= organic.UnsafeMushroom.Enabled;
        // These are deliberately fixed: neither saved overrides nor dynamic names may leak safety.
        unified.MarkerTitle = "Mushrooms";
        unified.MarkerColor = "#A89880";
        unified.MarkerIcon = "mushroom";
        organic.UnsafeMushroom = unified;
        var expanded = (Dictionary<string, bool>)AccessTools.Field(typeof(Settings), "ExpandStates").GetValue(settings)!;
        expanded[Safe] = false;
        expanded[Unsafe] = false;
        var children = (Dictionary<string, Settings.AutoMapMarkerSetting>)AccessTools.Field(typeof(Settings), "AddonMarkerSettings").GetValue(settings)!;
        foreach (var label in children.Keys.Where(IsMushroom).ToArray()) children[label] = unified;
    }

    // Only the registry receives a projected list. Original definitions/settings files stay intact.
    public static List<MarkerCategoryDef> ForRegistry(List<MarkerCategoryDef> definitions)
    {
        var mushrooms = definitions.SelectMany(c => c.Entries).Where(e => IsMushroom(e.Label)).ToList();
        if (mushrooms.Count == 0) return definitions;
        var unified = new MarkerEntryDef
        {
            Label = Safe,
            AssetPaths = mushrooms.SelectMany(e => e.AssetPaths).Distinct(StringComparer.Ordinal).ToList(),
            // Ignore per-safety exclusions: the combined category must match both source sets.
            DynamicTitle = false,
            Defaults = new MarkerDefaultsDef { Title = "Mushrooms", Icon = "mushroom", Color = "#A89880", CoverageRadius = 9 }
        };
        bool added = false;
        return definitions.Select(category => new MarkerCategoryDef
        {
            Category = category.Category,
            Entries = category.Entries.SelectMany(entry =>
            {
                if (!IsMushroom(entry.Label)) return new[] { entry };
                if (added) return Array.Empty<MarkerEntryDef>();
                added = true;
                return new[] { unified };
            }).ToList()
        }).ToList();
    }

    public static void FilterLayout(MarkerSettingLayout layout, Settings settings)
    {
        Normalize(settings);
        bool added = false;
        foreach (var tab in layout.Tabs)
        foreach (var group in tab.Subgroups)
        {
            group.Entries = group.Entries.Where(entry =>
            {
                if (!IsMushroom(entry.EntryLabel)) return true;
                if (entry.EntryLabel != Safe || added) return false;
                added = true;
                entry.DisplayName = "Mushrooms";
                entry.Setting = settings.AutoMapMarkers.OrganicMatter.SafeMushroom;
                entry.IsExpandable = false;
                entry.IsExpanded = false;
                entry.SubEntries = null;
                return true;
            }).ToList();
        }
    }
}

[HarmonyPatch(typeof(MarkerSettingsPersistence), nameof(MarkerSettingsPersistence.LoadSettings))]
internal static class LoadPatch
{
    private static void Postfix(Settings __result, List<MarkerCategoryDef> definitions) => MushroomPolicy.Initialize(__result, definitions);
}
[HarmonyPatch(typeof(MapMarkerConfig), nameof(MapMarkerConfig.GetSettings))]
internal static class GetPatch
{
    private static void Postfix(Settings? __result) { if (__result != null) MushroomPolicy.Normalize(__result); }
}
[HarmonyPatch(typeof(MapMarkerConfig), nameof(MapMarkerConfig.SaveSettings))]
internal static class SavePatch
{
    private static void Prefix(Settings settings) => MushroomPolicy.Normalize(settings);
}
[HarmonyPatch(typeof(MarkerSettingsPersistence), nameof(MarkerSettingsPersistence.BuildSettingLayout))]
internal static class LayoutPatch
{
    private static void Postfix(Settings settings, MarkerSettingLayout __result) => MushroomPolicy.FilterLayout(__result, settings);
}
[HarmonyPatch(typeof(MarkerDetectionRegistry), nameof(MarkerDetectionRegistry.Build))]
internal static class RegistryPatch
{
    private static void Prefix(ref List<MarkerCategoryDef> definitions, Settings settings)
    {
        MushroomPolicy.Normalize(settings);
        definitions = MushroomPolicy.ForRegistry(definitions);
    }
}
