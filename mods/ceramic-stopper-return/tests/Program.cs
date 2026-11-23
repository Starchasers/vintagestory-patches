using System.Reflection;
using System.Runtime.CompilerServices;
using ACulinaryArtillery;
using HarmonyLib;
using Kuboczoch.CeramicStopperReturn;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

bool red = args.Contains("--red");
Console.WriteLine($"ACA {typeof(BlockStoppableContainer).Assembly.GetName().Version}; mode={(red ? "unpatched" : "patched")}");
var harmony = new Harmony(StopperReturnMod.HarmonyId);
if (!red) StopperReturnMod.Install(harmony);
int cases = 0;
try
{
    // Each run invokes the real provider method, not a rewritten interaction.
    foreach (int bottles in new[] { 1, 3 })
    foreach (int offhandCount in new[] { 2, 0, 64 })
    {
        var fixture = new Fixture(bottles, offhandCount);
        int expectedStopperTotal = bottles + offhandCount;
        for (int i = 0; i < bottles; i++)
        {
            var handling = EnumHandHandling.NotHandled;
            fixture.Corked.OnHeldInteractStart(fixture.Held, fixture.Entity, null!, null!, true, ref handling);
            Check(handling == EnumHandHandling.PreventDefault, "provider handled uncorking");
            Check(fixture.StopperTotal == expectedStopperTotal, $"stopper conservation bottles={bottles} offhand={offhandCount} step={i + 1}: expected {expectedStopperTotal}, got {fixture.StopperTotal}");
            if (fixture.Held.Itemstack!.Collectible == fixture.Corked)
                Check(fixture.Corked.GetStopper(fixture.Held.Itemstack!)!.StackSize == 1,
                    $"remaining bottles retain one nested stopper (bottles={bottles}, offhand={offhandCount})");
        }
        Check(fixture.Held.Itemstack!.Collectible == fixture.Open, "last bottle opened");
        Check(fixture.OpenedCount == bottles, "container conservation");
        cases++;
        if (bottles > 1 && offhandCount == 64)
        {
            var droppedFixture = new Fixture(bottles, offhandCount);
            var handling = EnumHandHandling.NotHandled;
            droppedFixture.Corked.OnHeldInteractStart(droppedFixture.Held, droppedFixture.Entity, null!, null!, true, ref handling);
            Check(droppedFixture.DroppedStopperIsDetached, "dropped stopper does not alias remaining bottle contents");
            cases++;
        }
        Console.WriteLine($"PASS real interaction: bottles={bottles}, offhand={offhandCount}, stoppers={fixture.StopperTotal}, opened={fixture.OpenedCount}");
    }
    // Unknown/changed IL must not silently apply a partial or broad patch.
    bool refused = false;
    try { StopperReturnMod.DetachRemovedStopper(Array.Empty<CodeInstruction>()).ToArray(); }
    catch (InvalidOperationException) { refused = true; }
    Check(refused, "changed IL rejected"); cases++;
    Console.WriteLine($"PASS {cases} cases");
}
finally { harmony.UnpatchAll(StopperReturnMod.HarmonyId); }

static void Check(bool condition, string message) { if (!condition) throw new Exception("FAIL " + message); }

sealed class Fixture
{
    public BlockStoppableContainer Corked { get; }
    public BlockStoppableContainer Open { get; }
    public DummySlot Held { get; }
    public EntityPlayer Entity { get; }
    private readonly Item stopper;
    private readonly ItemSlotOffhand offhand;
    private readonly List<ItemStack> given = new();
    private readonly List<ItemStack> dropped = new();
    public bool DroppedStopperIsDetached => dropped.Count == 1 && !ReferenceEquals(dropped[0], Corked.GetStopper(Held.Itemstack!));
    public int StopperTotal => offhand.StackSize + given.Concat(dropped).Where(s => s.Collectible == stopper).Sum(s => s.StackSize)
        + (Held.Itemstack!.Collectible == Corked ? Held.StackSize * Corked.GetStopper(Held.Itemstack!)!.StackSize : 0);
    public int OpenedCount => (Held.Itemstack!.Collectible == Open ? Held.StackSize : 0)
        + given.Concat(dropped).Where(s => s.Collectible == Open).Sum(s => s.StackSize);

    public Fixture(int count, int offhandCount)
    {
        stopper = new Item { ItemId = 1, Code = new AssetLocation("aculinaryartillery:stopper-ceramic"),
            MaxStackSize = 64, StorageFlags = EnumItemStorageFlags.General | EnumItemStorageFlags.Offhand };
        Corked = Bottle(2, "corked"); Open = Bottle(3, "opened");
        offhand = new ItemSlotOffhand(null!);
        if (offhandCount > 0) offhand.Itemstack = new ItemStack(stopper, offhandCount);
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
        foreach (var collectible in new CollectibleObject[] { stopper, Corked, Open })
            AccessTools.Field(typeof(CollectibleObject), "api").SetValue(collectible, api);
        Entity = new EntityPlayer();
        Entity.World = world;
        Entity.WatchedAttributes = new SyncedTreeAttribute();
        Held = new DummySlot(new ItemStack(Corked, count));
        Corked.SetStopper(Held.Itemstack!, new ItemStack(stopper, 1));
    }
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
