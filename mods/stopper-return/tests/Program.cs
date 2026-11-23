using System.Reflection;
using System.Runtime.CompilerServices;
using ACulinaryArtillery;
using HarmonyLib;
using Kuboczoch.StopperReturn;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

bool red = args.Contains("--red");
bool cloneOnly = args.Contains("--clone-only");
string manifest = args.FirstOrDefault(a => a.EndsWith(".json"))
    ?? throw new ArgumentException("Pass the JSON manifest from discover_stoppers.py");
var specs = Newtonsoft.Json.JsonConvert.DeserializeObject<StopperSpec[]>(File.ReadAllText(manifest))!;
Console.WriteLine($"ACA SHA256 {Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(File.ReadAllBytes(typeof(BlockStoppableContainer).Assembly.Location)))}; mode={(red ? "unpatched" : cloneOnly ? "previous-clone-only" : "patched")}; variants={specs.Length}");
var harmony = new Harmony(StopperReturnMod.HarmonyId);
if (cloneOnly) harmony.Patch(StopperReturnMod.Target, transpiler: new HarmonyMethod(typeof(Legacy), nameof(Legacy.CloneOnly)));
else if (!red)
{
    string? external = args.SingleOrDefault(a => a.StartsWith("--mod-assembly="));
    if (external == null) StopperReturnMod.Install(harmony);
    else
    {
        string path = Path.GetFullPath(external["--mod-assembly=".Length..]);
        Console.WriteLine($"Testing release assembly SHA256 {Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(File.ReadAllBytes(path)))}");
        Assembly.LoadFrom(path).GetType(typeof(StopperReturnMod).FullName!)!
            .GetMethod(nameof(StopperReturnMod.Install))!.Invoke(null, new object[] { harmony });
    }
}
int cases = 0, failures = 0;
try
{
    foreach (var spec in specs)
    foreach (int bottles in new[] { 1, 3 })
    {
        foreach (int count in new[] { 0, 2, spec.MaxStackSize - 1, spec.MaxStackSize })
            Run(spec, spec, bottles, count, false);
        foreach (var other in specs.Where(other => other.Code != spec.Code))
            Run(spec, other, bottles, 2, false);
        // An unrelated item (including an unfired/non-stopper item) must still block.
        Run(spec, new StopperSpec { Code = "game:unrelated", MaxStackSize = 64,
            StorageFlags = 257, Attributes = Newtonsoft.Json.Linq.JObject.Parse("{\"isLiquidContainerStopper\":false}") }, bottles, 2, true);
    }
    bool refused = false;
    try { StopperReturnMod.DetachRemovedStopper(Array.Empty<CodeInstruction>()).ToArray(); }
    catch (InvalidOperationException) { refused = true; }
    Check(refused, "changed extraction IL rejected"); cases++;
    var changed = PatchProcessor.GetOriginalInstructions(StopperReturnMod.Target).ToList();
    int firstRead = changed.FindIndex(c => c.operand is MethodInfo m && m.Name == "GetStopper");
    changed[firstRead + 7].opcode = System.Reflection.Emit.OpCodes.Pop;
    refused = false;
    try { StopperReturnMod.DetachRemovedStopper(changed).ToArray(); }
    catch (InvalidOperationException) { refused = true; }
    Check(refused, "changed compatibility IL rejected"); cases++;
    Console.WriteLine($"RESULT cases={cases} failures={failures}; materials={string.Join(',', specs.Select(s => s.Code.Split('-')[1]).Distinct().Order())}");
    if (failures > 0) Environment.ExitCode = 1;
}
finally { harmony.UnpatchAll(StopperReturnMod.HarmonyId); }

void Run(StopperSpec spec, StopperSpec other, int bottles, int offhandCount, bool blocked)
{
    cases++;
    try
    {
        var f = new Fixture(bottles, offhandCount, spec, other);
        var originalOffhand = f.Offhand.Itemstack;
        var originalHeld = f.Held.Itemstack;
        int expectedTotal = bottles + (spec.Code == other.Code ? offhandCount : 0);
        for (int i = 0; i < (blocked ? 1 : bottles); i++)
        {
            var handling = EnumHandHandling.NotHandled;
            f.Corked.OnHeldInteractStart(f.Held, f.Entity, null!, null!, true, ref handling);
            if (blocked)
            {
                Check(handling != EnumHandHandling.PreventDefault && ReferenceEquals(originalHeld, f.Held.Itemstack)
                    && f.Held.StackSize == bottles && f.OpenedCount == 0 && f.DroppedCount == 0, "non-stopper routing unchanged");
                Check(ReferenceEquals(originalOffhand, f.Offhand.Itemstack) && f.Offhand.StackSize == offhandCount, "blocked offhand unchanged");
                break;
            }
            Check(handling == EnumHandHandling.PreventDefault, "provider handled uncorking");
            Check(f.StopperTotal == expectedTotal, $"stopper conservation expected={expectedTotal}, actual={f.StopperTotal}");
            Check(f.OpenedCount == i + 1, "opened exactly one bottle per interaction");
            bool mismatch = spec.Code != other.Code;
            int merged = mismatch ? 0 : Math.Min(i + 1, spec.MaxStackSize - offhandCount);
            Check(f.Offhand.StackSize == offhandCount + merged, "offhand count/capacity");
            Check(f.DroppedCount == i + 1 - merged, "exact ground-drop fallback");
            Check(f.Offhand.Itemstack!.Collectible.Code.ToString() == (mismatch ? other.Code : spec.Code), "offhand collectible unchanged or correct return");
            if (mismatch) Check(ReferenceEquals(originalOffhand, f.Offhand.Itemstack), "different stopper offhand instance unchanged");
            if (f.Held.Itemstack!.Collectible == f.Corked)
            {
                Check(f.Corked.GetStopper(f.Held.Itemstack!)!.StackSize == 1, "remaining nested stopper preserved");
                Check(f.AllDropsDetached, "drops detached from nested stopper");
            }
        }
        if (!blocked) Check(f.Held.Itemstack!.Collectible == f.Open && f.OpenedCount == bottles, "container conservation");
    }
    catch (Exception e)
    {
        failures++;
        if (failures <= 8) Console.WriteLine($"FAIL {spec.Code} offhand={other.Code}:{offhandCount} bottles={bottles}: {e.Message}");
    }
}

static void Check(bool condition, string message) { if (!condition) throw new Exception(message); }

sealed class StopperSpec
{
    public string Code { get; set; } = "";
    public int MaxStackSize { get; set; }
    public int StorageFlags { get; set; }
    public Newtonsoft.Json.Linq.JObject Attributes { get; set; } = new();
}

static class Legacy
{
    public static IEnumerable<CodeInstruction> CloneOnly(IEnumerable<CodeInstruction> instructions)
    {
        var code = StopperReturnMod.DetachRemovedStopper(instructions).ToList();
        var comparison = code.Single(c => c.operand is MethodInfo m && m.Name == nameof(StopperReturnMod.CanRemoveWith));
        comparison.opcode = System.Reflection.Emit.OpCodes.Ceq;
        comparison.operand = null;
        return code;
    }
}

sealed class Fixture
{
    public BlockStoppableContainer Corked { get; }
    public BlockStoppableContainer Open { get; }
    public DummySlot Held { get; }
    public EntityPlayer Entity { get; }
    private readonly Item stopper;
    private readonly ItemSlotOffhand offhand;
    public ItemSlotOffhand Offhand => offhand;
    public int DroppedCount => dropped.Where(s => s.Collectible == stopper).Sum(s => s.StackSize);
    private readonly List<ItemStack> given = new();
    private readonly List<ItemStack> dropped = new();
    public bool AllDropsDetached => dropped.All(s => !ReferenceEquals(s, Corked.GetStopper(Held.Itemstack!)));
    public int StopperTotal => (offhand.Itemstack?.Collectible == stopper ? offhand.StackSize : 0) + given.Concat(dropped).Where(s => s.Collectible == stopper).Sum(s => s.StackSize)
        + (Held.Itemstack!.Collectible == Corked ? Held.StackSize * Corked.GetStopper(Held.Itemstack!)!.StackSize : 0);
    public int OpenedCount => (Held.Itemstack!.Collectible == Open ? Held.StackSize : 0)
        + given.Concat(dropped).Where(s => s.Collectible == Open).Sum(s => s.StackSize);

    public Fixture(int count, int offhandCount, StopperSpec spec, StopperSpec other)
    {
        stopper = MakeItem(1, spec);
        var otherItem = other.Code == spec.Code ? stopper : MakeItem(4, other);
        Corked = Bottle(2, "corked"); Open = Bottle(3, "opened");
        offhand = new ItemSlotOffhand(null!);
        if (offhandCount > 0) offhand.Itemstack = new ItemStack(otherItem, offhandCount);
        var manager = Proxy.Make<IPlayerInventoryManager>((method, values) => method.Name switch {
            "get_OffhandHotbarSlot" => offhand,
            "TryGiveItemstack" => Give((ItemStack)values[0]!),
            _ => null });
        var player = PlayerProxy.Create(manager);
        var world = Proxy.Make<IWorldAccessor>((method, values) => method.Name switch {
            "PlayerByUid" => player,
            "GetItem" => stopper,
            "GetBlock" => Open,
            "get_Side" => EnumAppSide.Server,
            "SpawnItemEntity" => Drop((ItemStack)values[0]!),
            _ => null });
        var api = Proxy.Make<ICoreAPI>((method, _) => method.Name switch { "get_World" => world, "get_Side" => EnumAppSide.Server, _ => null });
        foreach (var collectible in new CollectibleObject[] { stopper, otherItem, Corked, Open })
            AccessTools.Field(typeof(CollectibleObject), "api").SetValue(collectible, api);
        Entity = new EntityPlayer();
        Entity.World = world;
        Entity.WatchedAttributes = new SyncedTreeAttribute();
        Held = new DummySlot(new ItemStack(Corked, count));
        Corked.SetStopper(Held.Itemstack!, new ItemStack(stopper, 1));
    }
    private static Item MakeItem(int id, StopperSpec spec) => new() {
        ItemId = id, Code = new AssetLocation(spec.Code), MaxStackSize = spec.MaxStackSize,
        StorageFlags = (EnumItemStorageFlags)spec.StorageFlags,
        Attributes = JsonObject.FromJson(spec.Attributes.ToString()) };
    private bool Give(ItemStack stack) { given.Add(stack); return true; }
    private object? Drop(ItemStack stack) { dropped.Add(stack); return null; }
    private static BlockStoppableContainer Bottle(int id, string state) => new() {
        BlockId = id, Attributes = JsonObject.FromJson("{\"uncorkedVariant\":\"opened\"}"), Code = new AssetLocation("aculinaryartillery:bottle-" + state),
        Variant = new Vintagestory.API.Util.RelaxedReadOnlyDictionary<string, string>(new Dictionary<string, string> { ["type"] = state }) };
}

public class Proxy : DispatchProxy
{
    public System.Func<MethodInfo, object?[], object?>? Handler;
    public static T Make<T>(System.Func<MethodInfo, object?[], object?> handler) where T : class
    { var proxy = Create<T, Proxy>(); ((Proxy)(object)proxy).Handler = handler; return proxy; }
    protected override object? Invoke(MethodInfo? method, object?[]? values) => Handler!(method!, values!)
        ?? (method!.ReturnType != typeof(void) && method.ReturnType.IsValueType ? Activator.CreateInstance(method.ReturnType) : null);
}
