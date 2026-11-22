using System.Reflection;
using System.Text.Json;
using KuboczochModpackGuide.Client;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

public class Fake : DispatchProxy
{
    public System.Func<MethodInfo, object?[]?, object?>? Handler;
    protected override object? Invoke(MethodInfo? m, object?[]? a) => Handler?.Invoke(m!, a) ?? (m!.ReturnType.IsValueType ? Activator.CreateInstance(m.ReturnType) : null);
    public static T Create<T>(System.Func<MethodInfo, object?[]?, object?> handler) where T : class
    {
        var result = DispatchProxy.Create<T, Fake>();
        ((Fake)(object)result).Handler = handler;
        return result;
    }
}

public static class FamilyTests
{
    static void Check(bool ok, string name) { if (!ok) throw new Exception(name); Console.WriteLine("PASS " + name); }
    static void Field(object o, string name, object value)
    {
        for (Type? t = o.GetType(); t != null; t = t.BaseType)
        {
            var f = t.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (f != null) { f.SetValue(o, value); return; }
        }
        throw new Exception("Missing field " + name);
    }
    public static void Main(string[] args) => Run(args[0], args[1]);
    public static void Run(string inventoryPath, string snapshotPath)
    {
        System.Runtime.InteropServices.NativeLibrary.SetDllImportResolver(typeof(Cairo.Context).Assembly, (name, assembly, path) => name == "libcairo-2" ? System.Runtime.InteropServices.NativeLibrary.Load("libcairo.so.2") : IntPtr.Zero);
        Check(GameVersion.ShortGameVersion == "1.22.7", "exact native API 1.22.7");
        var families = new ToolsmithFamilySlideshows(File.ReadAllText(snapshotPath));
        using var inv = JsonDocument.Parse(File.ReadAllText(inventoryPath));
        var entries = inv.RootElement.GetProperty("codes").EnumerateArray().ToArray();
        var items = new Dictionary<string, Item>(); var blocks = new Dictionary<string, Block>();
        int id = 1;
        foreach (var e in entries)
        {
            string c = e.GetProperty("code").GetString()!;
            if (e.GetProperty("kind").GetString() == "block") blocks.Add(c, new Block { Code = new AssetLocation(c), BlockId = id++ });
            else items.Add(c, new Item { Code = new AssetLocation(c), ItemId = id++ });
        }
        var logger = Fake.Create<ILogger>((m,a) => null);
        var world = Fake.Create<IClientWorldAccessor>((m,a) => m.Name switch
        {
            "GetItem" when a![0] is AssetLocation loc => items.GetValueOrDefault(loc.ToString()),
            "GetBlock" when a![0] is AssetLocation loc => blocks.GetValueOrDefault(loc.ToString()),
            "get_Logger" => logger,
            _ => null
        });
        string? routed = null;
        var protocols = new Dictionary<string, Action<LinkTextComponent>> { ["handbook"] = c => routed = c.Href };
        var registry = Fake.Create<IClassRegistryAPI>((m,a) => m.ReturnType == typeof(Dictionary<string, Type>) ? new Dictionary<string, Type>() : null);
        var api = Fake.Create<ICoreClientAPI>((m,a) => m.Name switch { "get_ClassRegistry" => registry, "get_World" => world, "get_LinkProtocols" => protocols, "get_Logger" => logger, _ => null });
        var allCodes = string.Join("|", entries.Select(e => e.GetProperty("code").GetString()));
        var stacks = families.BuildStacks(api, allCodes);
        var expected = entries.SelectMany(e => e.GetProperty("stacks").EnumerateArray()).ToArray();
        Check(stacks.Length == 85 && expected.Length == stacks.Length, "all 85 exact inventory snapshots materialized");
        for (int i = 0; i < stacks.Length; i++)
            if (GuiHandbookItemStackPage.PageCodeForStack(stacks[i]) != expected[i].GetProperty("pageCode").GetString()) throw new Exception("snapshot page mismatch " + i);
        Check(true, "all 85 native attributed page codes equal source inventory");
        var handle = families.BuildStacks(api, "toolsmith:handle");
        Check(families.BuildStacks(api, "toolsmith:handle|toolsmith:handle").Length == handle.Length, "duplicate codes do not duplicate slideshow variants");
        handle[0].Attributes.SetString("testMutation", "x");
        Check(!families.BuildStacks(api, "toolsmith:handle")[0].Attributes.HasAttribute("testMutation"), "fresh attribute trees per build");
        var missing = items["toolsmith:handle"]; items.Remove("toolsmith:handle");
        Check(families.BuildStacks(api, "toolsmith:handle").Length == 0, "uninstalled collectible skipped without air fallback");
        items.Add("toolsmith:handle", missing);
        Check(families.CreateComponent(api, "toolsmith:unknown|game:stick") == null, "empty/unknown selection creates no native component");
        var component = families.CreateComponent(api, allCodes)!;
        Check(component.GetType() == typeof(SlideshowItemstackTextComponent) && component.Itemstacks.Length == 85, "actual native slideshow class contains combined variants");
        typeof(Lang).GetProperty("CurrentLocale")!.SetValue(null, "en");
        Lang.AvailableLanguages["en"] = Fake.Create<ITranslationService>((m,a) => null);
        component.BoundsPerLine[0] = new LineRectangled(0, 0, 24, 24);
        foreach (var stack in stacks)
        {
            Field(component, "slot", new DummySlot(stack));
            routed = null;
            component.OnMouseDown(new MouseEvent(10, 10, EnumMouseButton.Left, 0));
            if (routed != "handbook://" + GuiHandbookItemStackPage.PageCodeForStack(stack)) throw new Exception("callback mismatch");
        }
        Check(true, "native OnMouseDown routes all 85 displayed stacks through handbook protocol");
        routed = null; component.OnMouseDown(new MouseEvent(50,50,EnumMouseButton.Left,0));
        Check(routed == null, "outside-bounds click does not navigate");
        protocols.Clear(); ToolsmithFamilySlideshows.OpenStackHandbook(api, stacks[0]);
        Check(routed == null, "missing handbook protocol safely does nothing");
        Tag2RichTextDelegate converter = families.Convert;
        var signature = typeof(Tag2RichTextDelegate).GetMethod("Invoke")!;
        Console.WriteLine("API delegate: " + signature);
        Check(signature.GetParameters().Length == 4, "converter binds exact installed API delegate signature");
        VtmlUtil.TagConverters[ToolsmithFamilySlideshows.TagName] = converter;
        Check(VtmlUtil.TagConverters[ToolsmithFamilySlideshows.TagName] == converter, "native converter registry accepts own converter");
        VtmlUtil.TagConverters.Remove(ToolsmithFamilySlideshows.TagName);
        var token = (VtmlTagToken)VtmlParser.Tokenize(logger, "<toolsmithfamily code=\"toolsmith:handle|toolsmith:handle\"/>")[0];
        Check(token.Name == ToolsmithFamilySlideshows.TagName && token.Attributes["code"] == "toolsmith:handle|toolsmith:handle", "native VTML parser accepts family pipe syntax");
        var fontStack = new Stack<CairoFont>(); fontStack.Push(CairoFont.WhiteSmallText());
        var converted = (SlideshowItemstackTextComponent)converter(api, token, fontStack, _ => throw new Exception("must use handbook protocol instead"));
        Check(converted.Itemstacks.Length == handle.Length, "actual Tag2RichTextDelegate invocation produces native slideshow");
        try { new ToolsmithFamilySlideshows("{\"codes\":[{\"code\":\"toolsmith:handle\",\"kind\":\"item\",\"stacks\":[{\"attributes\":{\"x\":1}}]}]}"); throw new Exception("expected invalid snapshot rejection"); }
        catch (FormatException) { Check(true, "non-plain snapshot values rejected without polymorphic deserialization"); }
        Console.WriteLine("RESULT headless native class/callback/snapshot checks passed; no GUI or downloaded mod DLL execution");
    }
}
