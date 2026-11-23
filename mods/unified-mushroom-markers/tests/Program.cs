using System.Reflection;
using System.Runtime.Loader;
using Egocarib.AutoMapMarkers.Settings;
using Egocarib.AutoMapMarkers.Utilities;
using HarmonyLib;
using Kuboczoch.UnifiedMushroomMarkers;
using Newtonsoft.Json;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Settings = Egocarib.AutoMapMarkers.Settings.MapMarkerConfig.Settings;

class Program
{
    static int assertions;
    static void Check(bool value, string description) { if (!value) throw new Exception(description); assertions++; Console.WriteLine("PASS " + description); }
    static void Main(string[] args)
    {
        var game = args[0];
        AssemblyLoadContext.Default.Resolving += (_, name) =>
        {
            foreach (var folder in new[] { game, Path.Combine(game, "Lib"), Path.Combine(game, "Mods") })
            {
                var path = Path.Combine(folder, name.Name + ".dll");
                if (File.Exists(path)) return AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(path));
            }
            return null;
        };
        typeof(Vintagestory.API.Config.GamePaths).GetProperty("AssetsPath")!.SetValue(null, Path.Combine(game, "assets"));
        Vintagestory.API.Config.Lang.PreLoad(null, Path.Combine(game, "assets"), "en");
        Run(args[1]);
    }
    static void Run(string fixture)
    {
        var defs = JsonConvert.DeserializeObject<List<MarkerCategoryDef>>(File.ReadAllText(fixture))!;
        Check(defs.SelectMany(c => c.Entries).Count(e => MushroomPolicy.IsMushroom(e.Label)) == 2, "production pack contains both original categories");
        var baseline = MarkerSettingsPersistence.LoadSettings(Path.GetDirectoryName(fixture)!, defs);
        var baselineLayout = MarkerSettingsPersistence.BuildSettingLayout(baseline, defs);
        Check(baselineLayout.Tabs.SelectMany(t => t.Subgroups).SelectMany(g => g.Entries).Count(e => MushroomPolicy.IsMushroom(e.EntryLabel)) == 2, "unpatched baseline exposes two safety-specific UI categories");
        Check(MarkerDetectionRegistry.Build(defs, baseline).FindMatches("mushroom-deathcap-normal").Count == 2, "unpatched baseline produces separate safety matches");
        var harmony = new Harmony("kuboczoch.unifiedmushroommarkers.tests");
        harmony.PatchAll(typeof(MushroomPolicy).Assembly);
        try
        {
            Check(new UnifiedMushroomMarkersMod().ShouldLoad(EnumAppSide.Client) && !new UnifiedMushroomMarkersMod().ShouldLoad(EnumAppSide.Server), "client-only mod lifecycle");
            foreach (var method in new[] { AccessTools.Method(typeof(MarkerDetectionRegistry), "Build"), AccessTools.Method(typeof(MarkerSettingsPersistence), "BuildSettingLayout"), AccessTools.Method(typeof(MarkerSettingsPersistence), "LoadSettings"), AccessTools.Method(typeof(MapMarkerConfig), "GetSettings"), AccessTools.Method(typeof(MapMarkerConfig), "SaveSettings") })
                Check(Harmony.GetPatchInfo(method).Owners.Contains(harmony.Id), "production Harmony target " + method.Name);
            var settings = MarkerSettingsPersistence.LoadSettings(Path.GetDirectoryName(fixture)!, defs);
            Check(ReferenceEquals(settings.AutoMapMarkers.OrganicMatter.SafeMushroom, settings.AutoMapMarkers.OrganicMatter.UnsafeMushroom), "loaded settings share unified object");
            var layout = MarkerSettingsPersistence.BuildSettingLayout(settings, defs);
            var entries = layout.Tabs.SelectMany(t => t.Subgroups).SelectMany(g => g.Entries).ToList();
            var mushrooms = entries.Where(e => MushroomPolicy.IsMushroom(e.EntryLabel)).ToList();
            Check(mushrooms.Count == 1 && mushrooms[0].DisplayName == "Mushrooms", "real UI layout has exactly one neutral mushroom row");
            Check(!entries.Any(e => e.EntryLabel.StartsWith(MushroomPolicy.Unsafe)), "poisonous category absent from real UI layout");
            Check(!mushrooms[0].IsExpandable && mushrooms[0].SubEntries == null, "no safety or psychedelic subcategories in UI");
            Check(entries.Any(e => e.EntryLabel == "game:item-resin"), "unrelated resin UI retained");
            settings.AutoMapMarkers.OrganicMatter.SafeMushroom.Enabled = false;
            MushroomPolicy.Normalize(settings);
            Check(!settings.AutoMapMarkers.OrganicMatter.UnsafeMushroom.Enabled, "unified toggle can be disabled without stale unsafe enable");
            var saveDir = Path.Combine(Path.GetDirectoryName(fixture)!, "roundtrip-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(saveDir);
            try
            {
                MarkerSettingsPersistence.SaveSettings(saveDir, settings, defs);
                var reloaded = MarkerSettingsPersistence.LoadSettings(saveDir, defs);
                Check(!reloaded.AutoMapMarkers.OrganicMatter.SafeMushroom.Enabled, "disabled unified toggle survives native save/reload");
                Check(defs.SelectMany(c => c.Entries).Where(e => MushroomPolicy.IsMushroom(e.Label)).All(e => !reloaded.IsExpanded(e)), "saved source categories stay collapsed");
            }
            finally { Directory.Delete(saveDir, true); }
            settings.AutoMapMarkers.OrganicMatter.SafeMushroom.Enabled = true;
            settings.AutoMapMarkers.OrganicMatter.SafeMushroom.MarkerTitle = "Poisonous!";
            settings.AutoMapMarkers.OrganicMatter.SafeMushroom.MarkerColor = "crimson";
            settings.AutoMapMarkers.OrganicMatter.SafeMushroom.MarkerIcon = "skull";
            var registry = MarkerDetectionRegistry.Build(defs, settings);
            typeof(MapMarkerConfig).GetField("_cachedRegistry", BindingFlags.NonPublic | BindingFlags.Static)!.SetValue(null, registry);
            foreach (var path in new[] { "mushroom-fieldmushroom-normal", "mushroom-deathcap-normal", "mushroom-flyagaric-normal", "mushroom-modadded-normal" })
            {
                var matches = registry.FindMatches(path);
                Check(matches.Count == 1 && !matches[0].DynamicTitle, "single non-dynamic production registry match " + path);
                var marker = matches[0].Setting;
                Check(marker.MarkerTitle == "Mushrooms" && marker.MarkerColor == "#A89880" && marker.MarkerIcon == "mushroom", "no title/color/icon clues " + path);
                var block = new Vintagestory.GameContent.BlockMushroom { Code = new AssetLocation("game", path) };
                var identifier = new ThingIdentifier(block, new BlockPos(1, 2, 3));
                Check(identifier.Identify(settings) && identifier.DynamicTitleComponent == null, "real ThingIdentifier suppresses species title " + path);
                using var wire = new MemoryStream();
                ProtoBuf.Serializer.Serialize(wire, new Egocarib.AutoMapMarkers.Network.ClientWaypointRequest
                {
                    waypointPosition = new Vec3d(1, 2, 3), waypointSettings = identifier.GetMapMarkerSettings(),
                    dynamicTitleComponent = identifier.DynamicTitleComponent
                });
                wire.Position = 0;
                var received = ProtoBuf.Serializer.Deserialize<Egocarib.AutoMapMarkers.Network.ClientWaypointRequest>(wire);
                var formatted = AccessTools.Method(typeof(WaypointUtil), "FormatDynamicTitle").Invoke(null, new object?[] { received.waypointSettings.MarkerTitle, received.dynamicTitleComponent });
                Check((string)formatted! == "Mushrooms" && received.waypointSettings.MarkerColorInteger.HasValue && received.waypointSettings.MarkerIcon == "mushroom", "actual protobuf payload/server title formatting remains neutral " + path);
            }
            Check(registry.FindMatches("log-resin-oak").Count == 1, "unrelated resin registry retained");
            Check(defs.SelectMany(c => c.Entries).Count(e => MushroomPolicy.IsMushroom(e.Label)) == 2, "original definitions not mutated");
            var separate = new Settings();
            separate.AutoMapMarkers.OrganicMatter.SafeMushroom.Enabled = false;
            separate.AutoMapMarkers.OrganicMatter.UnsafeMushroom.Enabled = true;
            MushroomPolicy.Normalize(separate);
            Check(separate.AutoMapMarkers.OrganicMatter.SafeMushroom.Enabled, "unsafe-only parent enable migrates to unified toggle");
            var expandedSettings = new Settings();
            var childSettings = (Dictionary<string, Settings.AutoMapMarkerSetting>)AccessTools.Field(typeof(Settings), "AddonMarkerSettings").GetValue(expandedSettings)!;
            var expandStates = (Dictionary<string, bool>)AccessTools.Field(typeof(Settings), "ExpandStates").GetValue(expandedSettings)!;
            foreach (var parent in defs.SelectMany(c => c.Entries).Where(e => MushroomPolicy.IsMushroom(e.Label)))
            {
                expandStates[parent.Label] = true;
                foreach (var child in parent.ExpandableEntries!) childSettings[child.Label] = new Settings.AutoMapMarkerSetting(false, false, "danger", "red", "skull", 9);
            }
            expandedSettings.AutoMapMarkers.OrganicMatter.SafeMushroom.Enabled = true;
            expandedSettings.AutoMapMarkers.OrganicMatter.UnsafeMushroom.Enabled = true;
            MushroomPolicy.Initialize(expandedSettings, defs);
            Check(!expandedSettings.AutoMapMarkers.OrganicMatter.SafeMushroom.Enabled, "expanded disabled children override inactive enabled parents");
            var enabledChild = new Settings();
            var enabledChildren = (Dictionary<string, Settings.AutoMapMarkerSetting>)AccessTools.Field(typeof(Settings), "AddonMarkerSettings").GetValue(enabledChild)!;
            var enabledExpandStates = (Dictionary<string, bool>)AccessTools.Field(typeof(Settings), "ExpandStates").GetValue(enabledChild)!;
            foreach (var parent in defs.SelectMany(c => c.Entries).Where(e => MushroomPolicy.IsMushroom(e.Label)))
            {
                enabledExpandStates[parent.Label] = true;
                foreach (var child in parent.ExpandableEntries!) enabledChildren[child.Label] = new Settings.AutoMapMarkerSetting(child.Label.StartsWith(MushroomPolicy.Unsafe), false, "danger", "red", "skull", 9);
            }
            MushroomPolicy.Initialize(enabledChild, defs);
            Check(enabledChild.AutoMapMarkers.OrganicMatter.SafeMushroom.Enabled, "expanded enabled unsafe child migrates to unified toggle");
        }
        finally { harmony.UnpatchAll(harmony.Id); }
        var mod = new UnifiedMushroomMarkersMod();
        var build = AccessTools.Method(typeof(MarkerDetectionRegistry), "Build");
        for (int attempt = 0; attempt < 2; attempt++)
        {
            mod.StartPre(null!);
            Check(Harmony.GetPatchInfo(build).Owners.Contains("kuboczoch.unifiedmushroommarkers"), "real StartPre installs patches " + attempt);
            mod.Dispose();
            Check(Harmony.GetPatchInfo(build)?.Owners.Contains("kuboczoch.unifiedmushroommarkers") != true, "real Dispose removes patches " + attempt);
        }
        Console.WriteLine($"ALL {assertions} native assembly assertions passed");
    }
}
