using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace KuboczochModpackGuide.Client;

/// <summary>Plain registered ingredients and stations, using the same native clickable
/// slideshow contract as attributed Toolsmith families; never changes gameplay data.</summary>
public sealed class ToolsmithItemReferences : ModSystem
{
    private const string TagName = "toolsmithitem";
    private Tag2RichTextDelegate? registered;
    private Tag2RichTextDelegate? previous;
    public override bool ShouldLoad(EnumAppSide side) => side == EnumAppSide.Client;
    public override void StartClientSide(ICoreClientAPI api)
    {
        if (registered != null) return;
        VtmlUtil.TagConverters.TryGetValue(TagName, out previous);
        registered = Convert;
        VtmlUtil.TagConverters[TagName] = registered;
    }
    public static RichTextComponentBase Convert(ICoreClientAPI api, VtmlTagToken tag,
        Stack<CairoFont> fonts, Action<LinkTextComponent> didClickLink)
    {
        bool block = tag.Attributes.GetValueOrDefault("type") == "block";
        var stacks = new List<ItemStack>();
        foreach (string code in tag.Attributes.GetValueOrDefault("code", "").Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            CollectibleObject? collectible = block ? api.World.GetBlock(new AssetLocation(code)) : api.World.GetItem(new AssetLocation(code));
            if (collectible == null) throw new FormatException("Unregistered guide reference: " + code);
            stacks.Add(new ItemStack(collectible));
        }
        if (stacks.Count == 0) throw new FormatException("toolsmithitem requires at least one registered code");
        var extents = fonts.Peek().GetFontExtents();
        var component = new SlideshowItemstackTextComponent(api, stacks.ToArray(), extents.Height / Vintagestory.API.Config.RuntimeEnv.GUIScale, EnumFloat.Inline,
            stack => ToolsmithFamilySlideshows.OpenStackHandbook(api, stack))
        { Background = true, VerticalAlign = EnumVerticalAlign.Middle };
        component.renderSize *= 1.3f;
        component.BoundsPerLine[0].Ascent = extents.Ascent;
        return component;
    }
    public override void Dispose()
    {
        if (registered != null && VtmlUtil.TagConverters.TryGetValue(TagName, out var current) && current == registered)
        {
            if (previous == null) VtmlUtil.TagConverters.Remove(TagName);
            else VtmlUtil.TagConverters[TagName] = previous;
        }
        registered = null;
        base.Dispose();
    }
}
