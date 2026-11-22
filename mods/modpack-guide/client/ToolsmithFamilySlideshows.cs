using System.Text.Json;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace KuboczochModpackGuide.Client;

/// <summary>Only materializes plain object/string snapshots, never mod types or creative hooks.</summary>
public sealed class ToolsmithFamilySlideshows
{
    public const string TagName = "toolsmithfamily";
    public const string AssetPath = "kuboczochmodpackguide:config/toolsmith-families.json";
    private readonly Dictionary<string, Family> families = new(StringComparer.Ordinal);
    private sealed record Family(bool IsBlock, JsonElement[] Attributes);

    /// <param name="snapshotJson">Bundled config, generated verbatim from attribute_inventory.json codes/stacks.</param>
    public ToolsmithFamilySlideshows(string snapshotJson)
    {
        using var doc = JsonDocument.Parse(snapshotJson);
        foreach (var entry in doc.RootElement.GetProperty("codes").EnumerateArray())
        {
            string code = entry.GetProperty("code").GetString()!;
            if (!code.StartsWith("toolsmith:", StringComparison.Ordinal)) throw new FormatException("Non-Toolsmith snapshot code");
            string kind = entry.GetProperty("kind").GetString()!;
            if (kind != "item" && kind != "block") throw new FormatException("Invalid snapshot kind");
            var attrs = entry.GetProperty("stacks").EnumerateArray().Select(s => s.GetProperty("attributes").Clone()).ToArray();
            foreach (var attr in attrs) BuildAttributes(attr); // validate even snapshots not selected by a tag
            families.Add(code, new Family(kind == "block", attrs));
        }
    }

    private static TreeAttribute BuildAttributes(JsonElement snapshot)
    {
        if (snapshot.ValueKind != JsonValueKind.Object) throw new FormatException("Snapshot must be an object");
        var tree = new TreeAttribute();
        foreach (var p in snapshot.EnumerateObject())
        {
            if (p.Value.ValueKind == JsonValueKind.Object) tree.SetAttribute(p.Name, BuildAttributes(p.Value));
            else if (p.Value.ValueKind == JsonValueKind.String) tree.SetString(p.Name, p.Value.GetString()!);
            else throw new FormatException("Only plain nested objects and strings are allowed in family snapshots");
        }
        return tree;
    }

    /// <summary>Exact pipe-separated codes only. Unknown/uninstalled codes are omitted, never air substitutes.
    /// Each returned stack owns a fresh attribute tree; no collectible behaviors or mod DLLs are invoked here.</summary>
    public ItemStack[] BuildStacks(ICoreClientAPI api, string codes)
    {
        ArgumentNullException.ThrowIfNull(api);
        var result = new List<ItemStack>();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (string code in codes.Split('|'))
        {
            if (!seen.Add(code) || !families.TryGetValue(code, out var family)) continue;
            var location = new AssetLocation(code);
            CollectibleObject? collectible = family.IsBlock ? api.World.GetBlock(location) : api.World.GetItem(location);
            if (collectible == null) continue;
            foreach (var attributes in family.Attributes)
                result.Add(new ItemStack(collectible) { Attributes = BuildAttributes(attributes) });
        }
        return result.ToArray();
    }

    /// <summary>Explicitly routes the displayed clicked stack, including its native attribute suffix.</summary>
    public static void OpenStackHandbook(ICoreClientAPI api, ItemStack stack)
    {
        if (api.LinkProtocols.TryGetValue("handbook", out var open))
            open(new LinkTextComponent("handbook://" + GuiHandbookItemStackPage.PageCodeForStack(stack)));
    }

    /// <summary>Also callable by a headless harness: construction does not render or open a GUI.</summary>
    public SlideshowItemstackTextComponent? CreateComponent(ICoreClientAPI api, string codes, double unscaledSize = 24)
    {
        var stacks = BuildStacks(api, codes);
        if (stacks.Length == 0) return null; // native renderer indexes Itemstacks; never pass an empty array
        return new SlideshowItemstackTextComponent(api, stacks, unscaledSize, EnumFloat.Inline,
            clicked => OpenStackHandbook(api, clicked))
        {
            Background = true,
            VerticalAlign = EnumVerticalAlign.Middle
        };
    }

    // Exact Tag2RichTextDelegate signature from VtmlUtil and verified against the installed API by compilation/tests.
    public RichTextComponentBase? Convert(ICoreClientAPI api, VtmlTagToken tag,
        Stack<CairoFont> fonts, Action<LinkTextComponent> didClickLink)
    {
        if (!tag.Attributes.TryGetValue("code", out var codes)) return null;
        var extents = fonts.Peek().GetFontExtents();
        var component = CreateComponent(api, codes, extents.Height / RuntimeEnv.GUIScale);
        if (component != null)
        {
            component.renderSize *= 1.3f;
            component.BoundsPerLine[0].Ascent = extents.Ascent;
        }
        // Deliberately do not use didClickLink: family icons always use the native handbook protocol.
        return component;
    }
}

public sealed class ToolsmithFamilyClientSystem : ModSystem
{
    private Tag2RichTextDelegate? registered;
    private Tag2RichTextDelegate? previous;
    public override bool ShouldLoad(EnumAppSide side) => side == EnumAppSide.Client;
    public override void StartClientSide(ICoreClientAPI api)
    {
        var slideshows = new ToolsmithFamilySlideshows(api.Assets.Get(new AssetLocation(ToolsmithFamilySlideshows.AssetPath)).ToText());
        VtmlUtil.TagConverters.TryGetValue(ToolsmithFamilySlideshows.TagName, out previous);
        registered = slideshows.Convert;
        VtmlUtil.TagConverters[ToolsmithFamilySlideshows.TagName] = registered;
    }
    public override void Dispose()
    {
        if (registered != null && VtmlUtil.TagConverters.TryGetValue(ToolsmithFamilySlideshows.TagName, out var current) && current == registered)
        {
            if (previous == null) VtmlUtil.TagConverters.Remove(ToolsmithFamilySlideshows.TagName);
            else VtmlUtil.TagConverters[ToolsmithFamilySlideshows.TagName] = previous;
        }
        registered = null;
    }
}
