using ACulinaryArtillery;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.Util;
using Vintagestory.Common;
sealed class FlowFixture
{
    public NativeHotbar Hotbar;
    public ItemSlot Held => Hotbar[0];
    public ItemSlot Offhand => Hotbar[11];
    public BlockStoppableContainer Bottle, OpenBottle;
    public Item[] Items;
    public EntityPlayer Entity;
    public ICoreAPI Api;
    public List<ItemStack> Drops = new();
    public FlowFixture(EnumAppSide side = EnumAppSide.Server)
    {
        Items = new[]{ new Item { ItemId=1, Code=new AssetLocation("aculinaryartillery:stopper-ceramic-fired"), MaxStackSize=64,
            StorageFlags=(EnumItemStorageFlags)257, Attributes=JsonObject.FromJson("{\"isLiquidContainerStopper\":true}") }};
        Bottle=MakeBottle(2,"corked"); OpenBottle=MakeBottle(3,"opened");
        Entity = new EntityPlayer { WatchedAttributes=new SyncedTreeAttribute() };
        NativeManager? manager=null;
        var proxyManager=Proxy.Make<IPlayerInventoryManager>((m,a)=>m.Name switch {
            "get_OffhandHotbarSlot"=>Offhand,
            "TryGiveItemstack"=>manager!.TryGiveItemstack((ItemStack)a[0]!, (bool)a[1]!), _=>null });
        var player=PlayerProxy.Create(proxyManager,Entity);
        var world=Proxy.Make<IWorldAccessor>((m,a)=>m.Name switch {
            "PlayerByUid"=>player, "get_Side"=>side, "SpawnItemEntity"=>Drop((ItemStack)a[0]!),
            "GetItem"=>Items[0],
            "GetBlock"=>a[0] is int id ? (id==2?Bottle:OpenBottle) : ((AssetLocation)a[0]!).Path.EndsWith("corked") ? Bottle:OpenBottle,
            "get_Collectibles"=>new List<CollectibleObject>{ Items[0],Bottle,OpenBottle },
            _=>null });
        var events=Proxy.Make<IEventAPI>((m,a)=>null);
        var registry=Proxy.Make<IClassRegistryAPI>((m,a)=>null);
        Api=Proxy.Make<ICoreAPI>((m,a)=>m.Name switch { "get_World"=>world, "get_ClassRegistry"=>registry, "get_Event"=>events, "get_Side"=>side, _=>null });
        Entity.World=world; Entity.Api=Api;
        foreach(var c in new CollectibleObject[]{Items[0],Bottle,OpenBottle}) AccessTools.Field(typeof(CollectibleObject),"api").SetValue(c,Api);
        Hotbar=new NativeHotbar(Api);
        var inventories=new Vintagestory.API.Datastructures.OrderedDictionary<string,InventoryBase>(); inventories.Add("hotbar-test",Hotbar);
        manager=new NativeManager(inventories,player);
    }
    object? Drop(ItemStack stack) { Drops.Add(stack); return null; }
    public void OpenOne(bool shift=false)
    {
        Entity.Controls.ShiftKey=shift;
        var handling=EnumHandHandling.NotHandled;
        ((BlockStoppableContainer)Held.Itemstack!.Collectible).OnHeldInteractStart(Held,Entity,null!,null!,true,ref handling);
    }
    static BlockStoppableContainer MakeBottle(int id,string state)=>new() { BlockId=id, MaxStackSize=64,
        Attributes=JsonObject.FromJson("{\"uncorkedVariant\":\"opened\"}"), Code=new AssetLocation("aculinaryartillery:bottle-"+state),
        Variant=new RelaxedReadOnlyDictionary<string,string>(new Dictionary<string,string>{["type"]=state}) };
}
