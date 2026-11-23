using HarmonyLib;
using Kuboczoch.StopperReturn;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.Common;

sealed class NativeHotbar : InventoryPlayerHotbar
{
    public NativeHotbar(ICoreAPI api) : base("hotbar", "test", api) { }
    public override void updateSlotStatMods(ItemSlot slot) { }
}
sealed class NativeManager : PlayerInventoryManager
{
    public NativeManager(Vintagestory.API.Datastructures.OrderedDictionary<string, InventoryBase> inv, IPlayer player) : base(inv, player) { }
    public override ItemSlot CurrentHoveredSlot { get; set; } = null!;
    public override void BroadcastHotbarSlot() { }
    public override void NotifySlot(IPlayer p, ItemSlot slot) { }
    public override bool DropItem(ItemSlot slot, bool fullStack) => false;
}
static class NativeRegression
{
    public static int Lifecycle()
    {
        var fixture = new FlowFixture();
        var a = new StopperReturnMod(); var b = new StopperReturnMod();
        try {
            a.Start(fixture.Api); b.Start(fixture.Api);
            a.Start(fixture.Api); // repeated Start on one instance is idempotent too.
            Console.WriteLine("LIFECYCLE double Start succeeded");
            var failing = new Harmony("stopper-test-failing-owner");
            bool refused = false;
            try { StopperReturnMod.Install(failing); }
            catch { refused = true; }
            if (!refused || Harmony.GetPatchInfo(StopperReturnMod.Target)?.Owners.Contains(failing.Id) == true
                || Harmony.GetPatchInfo(StopperReturnMod.Target)?.Transpilers.Count(x => x.owner == StopperReturnMod.HarmonyId) != 1)
                throw new Exception("failed install rollback removed peer or leaked failed owner");
            Console.WriteLine("PASS failed install rolls back only attempted owner; live peer retained");
            a.Dispose();
            var info = Harmony.GetPatchInfo(StopperReturnMod.Target);
            if (info?.Transpilers.Count(x => x.owner == StopperReturnMod.HarmonyId) != 1)
                throw new Exception("first Dispose removed live peer");
            b.Dispose();
            if (Harmony.GetPatchInfo(StopperReturnMod.Target)?.Owners.Contains(StopperReturnMod.HarmonyId) == true)
                throw new Exception("last Dispose leaked patch");
            a = new StopperReturnMod(); b = new StopperReturnMod();
            a.Start(fixture.Api); b.Start(fixture.Api); b.Dispose();
            if (Harmony.GetPatchInfo(StopperReturnMod.Target)?.Transpilers.Count(x => x.owner == StopperReturnMod.HarmonyId) != 1)
                throw new Exception("reverse Dispose removed live peer");
            a.Dispose();
            a.Dispose(); b.Dispose(); // double disposal is harmless.
            var peers = Enumerable.Range(0,8).Select(_=>new StopperReturnMod()).ToArray();
            Parallel.ForEach(peers, p=>p.Start(fixture.Api));
            if (Harmony.GetPatchInfo(StopperReturnMod.Target)?.Transpilers.Count(x=>x.owner==StopperReturnMod.HarmonyId) != 1)
                throw new Exception("concurrent Start duplicated patch");
            Parallel.ForEach(peers,p=>p.Dispose());
            if (Harmony.GetPatchInfo(StopperReturnMod.Target)?.Owners.Contains(StopperReturnMod.HarmonyId) == true)
                throw new Exception("concurrent Dispose leaked patch");
            Console.WriteLine("PASS lifecycle both dispose orders, reload, double disposal, eight concurrent peers"); return 0;
        } catch(Exception e) {
            Console.WriteLine("FAIL lifecycle " + e);
            Console.WriteLine("LIFECYCLE after failure live-owner-transpilers=" +
                (Harmony.GetPatchInfo(StopperReturnMod.Target)?.Transpilers.Count(x=>x.owner==StopperReturnMod.HarmonyId) ?? 0));
            return 1;
        }
        finally { a.Dispose(); b.Dispose(); }
    }
    public static int Flow(bool red, bool singleInstall = false)
    {
        var h = new Harmony(StopperReturnMod.HarmonyId);
        var serverMod = new StopperReturnMod(); var clientMod = new StopperReturnMod();
        if (!red) { var boot = new FlowFixture(); if (singleInstall) StopperReturnMod.Install(h); else { serverMod.Start(boot.Api); clientMod.Start(boot.Api); } }
        int errors = 0;
        try {
            var server = new FlowFixture(); var client = new FlowFixture(EnumAppSide.Client);
            foreach (var f in new[]{ server, client }) {
                f.Held.Itemstack = new ItemStack(f.OpenBottle,6);
                f.Offhand.Itemstack = new ItemStack(f.Items[0],6);
            }
            for (int i=1; i<=6; i++) {
                int before = Enumerable.Range(1,10).Sum(k => client.Hotbar![k].Itemstack?.Collectible == client.Bottle ? client.Hotbar[k].StackSize : 0);
                var liveInput = server.Offhand.Itemstack;
                client.OpenOne(true); server.OpenOne(true);
                int ghosts = Enumerable.Range(1,10).Sum(k => client.Hotbar![k].Itemstack?.Collectible == client.Bottle ? client.Hotbar[k].StackSize : 0) - before;
                int nested = Enumerable.Range(0,11).Sum(k => {
                    var s = server.Hotbar![k].Itemstack;
                    return s?.Collectible == server.Bottle ? (server.Bottle.GetContents(server.Api.World,s).ElementAtOrDefault(1)?.StackSize ?? -100) * s.StackSize : 0;
                });
                Console.WriteLine("SERVER " + string.Join(";", server.Hotbar!.Select((s,k)=>$"{k}:{s.Itemstack?.Collectible.Code}:{s.Itemstack?.StackSize}")));
                Console.WriteLine($"INSERT {i} client-created-return-bottles={ghosts} server-nested={nested} offhand={server.Offhand.Itemstack?.StackSize.ToString() ?? "null"}");
                bool aliased = server.Hotbar.Any(s=>s.Itemstack?.Collectible==server.Bottle &&
                    ReferenceEquals(server.Bottle.GetContents(server.Api.World,s.Itemstack).ElementAtOrDefault(1),liveInput));
                Console.WriteLine($"INSERT-OWNERSHIP {i} nested-alias-live-input={aliased}");
                if (nested != i || server.Offhand.Itemstack?.StackSize == 0 || aliased) errors++;
                if (ghosts != 0) errors++;
                Sync(server,client);
            }

            // Select the stopped bottle stack and extract all six through actual provider method.
            server.Hotbar[0].TryPutInto(server.Api.World,server.Hotbar[1],1);
            int index = Enumerable.Range(0,11).OrderByDescending(k=>server.Hotbar![k].StackSize).First(k => server.Hotbar![k].Itemstack?.Collectible == server.Bottle);
            var stack = server.Hotbar[index].Itemstack;
            server.Hotbar[index].Itemstack = server.Hotbar[0].Itemstack;
            server.Hotbar[0].Itemstack = stack;
            Sync(server,client);
            for(int i=1;i<=6;i++) {
                int before = client.Hotbar.Sum(s=>s.Itemstack?.Collectible==client.OpenBottle ? s.StackSize:0);
                client.OpenOne(); server.OpenOne();
                // When the last held bottle changes in place it is prediction,
                // not a separately returned bottle.
                int predicted = i==6 ? 1:0;
                int created = client.Hotbar.Sum(s=>s.Itemstack?.Collectible==client.OpenBottle ? s.StackSize:0) - before - predicted;
                Console.WriteLine($"EXTRACT-AUTHORITY {i} client-created-return-bottles={created}");
                if(created!=0 || client.Drops.Count!=0 || server.Drops.Count!=0) errors++;
                Sync(server,client);
                Console.WriteLine($"EXTRACT {i} offhand={server.Offhand.Itemstack?.StackSize.ToString() ?? "null"}");
                if(i==4) { server.Offhand.TryPutInto(server.Api.World,server.Hotbar[3],64); Sync(server,client); }
            }
            int loose = server.Hotbar.Sum(s => s.Itemstack?.Collectible == server.Items[0] ? s.StackSize : 0);
            int opened = server.Hotbar.Sum(s => s.Itemstack?.Collectible == server.OpenBottle ? s.StackSize : 0);
            if (loose != 6 || opened != 6 || server.Offhand.Itemstack?.StackSize == 0) errors++;
            errors += FullInventoryFallback();
            Console.WriteLine($"RESULT native-paired-flow red={red} failures={errors} loose={loose} open={opened}");
            return errors == 0 ? 0 : 1;
        } catch(Exception e) { Console.WriteLine("HARNESS ERROR " + e); return 2; }
        finally { serverMod.Dispose(); clientMod.Dispose(); h.UnpatchAll(StopperReturnMod.HarmonyId); }
    }
    static int FullInventoryFallback()
    {
        int errors=0;
        foreach(bool inserting in new[]{true,false})
        {
            var server=new FlowFixture(); var client=new FlowFixture(EnumAppSide.Client);
            foreach(var f in new[]{server,client})
            {
                for(int i=1;i<=10;i++) f.Hotbar[i].Itemstack=new ItemStack(f.OpenBottle,64);
                f.Held.Itemstack=new ItemStack(inserting?f.OpenBottle:f.Bottle,6);
                if(inserting) f.Offhand.Itemstack=new ItemStack(f.Items[0],6);
                else f.Bottle.SetStopper(f.Held.Itemstack,new ItemStack(f.Items[0],1));
            }
            client.OpenOne(inserting); server.OpenOne(inserting);
            bool good = client.Drops.Count==0 && server.Drops.Count==1
                && server.Drops[0].StackSize==1
                && server.Drops[0].Collectible==(inserting?server.Bottle:server.OpenBottle)
                && server.Held.StackSize==5 && server.Offhand.StackSize==(inserting?5:1);
            if(!good) errors++;
            Console.WriteLine($"FULL-INVENTORY inserting={inserting} client-drops={client.Drops.Count} server-drops={server.Drops.Count} pass={good}");
            Sync(server,client);
        }
        return errors;
    }
    static void Sync(FlowFixture server, FlowFixture client)
    {
        var tree = new TreeAttribute(); server.Hotbar!.ToTreeAttributes(tree);
        // Actual inventory persistence tree, encoded/decoded to eliminate shared references.
        using var stream = new MemoryStream();
        using(var w = new BinaryWriter(stream,System.Text.Encoding.UTF8,true)) tree.ToBytes(w);
        stream.Position=0;
        var copy = new TreeAttribute(); copy.FromBytes(new BinaryReader(stream));
        client.Hotbar!.FromTreeAttributes(copy);
        var actual = new TreeAttribute(); client.Hotbar.ToTreeAttributes(actual);
        using var actualStream = new MemoryStream();
        actual.ToBytes(new BinaryWriter(actualStream));
        if (!stream.ToArray().SequenceEqual(actualStream.ToArray()))
            throw new Exception("native inventory roundtrip did not reconcile client to server");
        for(int i=0;i<server.Hotbar.Count;i++)
            if (server.Hotbar[i].Itemstack != null && ReferenceEquals(server.Hotbar[i].Itemstack,client.Hotbar[i].Itemstack))
                throw new Exception("client/server stacks share process references after serialization");
    }
}
