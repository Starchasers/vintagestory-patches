using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace KuboczochWoodworkingTutorial;

public sealed class WoodworkingTutorialMod : ModSystem
{
    private const string HarmonyId = "kuboczoch.woodworkingtutorial";
    private Harmony? harmony;
    public override bool ShouldLoad(EnumAppSide side) => side == EnumAppSide.Client;
    public override void StartClientSide(ICoreClientAPI api)
    {
        harmony = new Harmony(HarmonyId);
        harmony.Patch(AccessTools.DeclaredMethod(typeof(FirstStepsTutorial), "initTutorialSteps"),
            postfix: new HarmonyMethod(typeof(TutorialPatch), nameof(TutorialPatch.Postfix)));
        api.Logger.Notification("[Kuboczoch Woodworking Tutorial] Two-stage firewood tutorial enabled.");
    }
    public override void Dispose() { harmony?.UnpatchAll(HarmonyId); harmony = null; }
}

public static class TutorialPatch
{
    public const string TextKey = "kuboczochwoodworkingtutorial:firewood";
    public const string Prefix = "kuboczochwoodworkingtutorial:";
    private static readonly FieldInfo StepsField = AccessTools.Field(typeof(TutorialBase), "steps");
    private static readonly FieldInfo ApiField = AccessTools.Field(typeof(TutorialBase), "capi");
    private static readonly FieldInfo CodeField = AccessTools.Field(typeof(TutorialStepBase), "code");

    public static bool IsWood(CollectibleObject? collectible, string path) =>
        collectible?.Code?.Domain == "immersivewoodworking" && collectible.Code.Path == path;

    public static void Postfix(FirstStepsTutorial __instance)
    {
        var api = (ICoreClientAPI)ApiField.GetValue(__instance)!;
        if (!Replace((List<TutorialStepBase>)StepsField.GetValue(__instance)!, api))
            api.Logger.Warning("[Kuboczoch Woodworking Tutorial] Unexpected tutorial structure; left unchanged.");
    }

    public static bool Replace(List<TutorialStepBase> steps, ICoreClientAPI api)
    {
        var matches = steps.Select((step,position)=>(step,position))
            .Where(pair => (string?)CodeField.GetValue(pair.step) == "craftfirewood").ToArray();
        if (matches.Length != 1 || steps.Any(s=>((string?)CodeField.GetValue(s))?.StartsWith("kuboczoch_iw_")==true)) return false;
        var position = matches[0].position;
        var stages = new TutorialStepBase[] {
            new PreparationStep(api, "kuboczoch_iw_placeblock", "setup",
                TutorialStepBase.Place(api,"kuboczoch_iw_placeblock",Prefix+"setup",(p,b,s)=>IsWood(b,"choppingblock"),1),
                acceptsExistingBlock:true),
            TutorialStepBase.Collect(api,"craftfirewood",TextKey,s=>s?.Collectible is ItemFirewood,4)
        };
        steps.RemoveAt(position); steps.InsertRange(position,stages);
        for (int i=0;i<steps.Count;i++) steps[i].index=i;
        return true;
    }
}

// Delegate to native watchers, adding compatibility with old completed saves and an existing workstation.
public sealed class PreparationStep : TutorialStepBase
{
    private readonly ICoreClientAPI api;
    private readonly TutorialStepBase inner;
    private readonly bool acceptsExistingBlock;
    private readonly TutorialStepBase? inventoryProbe;
    public PreparationStep(ICoreClientAPI api,string code,string key,TutorialStepBase inner,
        bool acceptsExistingBlock=false,bool acceptsInventoryBlock=false)
    {
        this.api=api; this.code=code; text=TutorialPatch.Prefix+key; this.inner=inner;
        this.acceptsExistingBlock=acceptsExistingBlock;
        if(acceptsInventoryBlock) inventoryProbe=TutorialStepBase.Collect(api,code,text,s=>TutorialPatch.IsWood(s?.Collectible,"choppingblock"),1);
    }
    public override bool Complete => inner.Complete;
    public override RichTextComponentBase[] GetText(CairoFont font) => inner.GetText(font);
    public override void Skip() => inner.Skip();
    public override void Restart() {inner.Restart(); inventoryProbe?.Restart();}
    public override void FromJson(JsonObject job)
    {
        inner.FromJson(job);
        // v1.0.0 and vanilla use this same achieved/goal pair; never reopen completed firewood work.
        if(job["craftfirewood"]["achieved"].AsInt()>=4) inner.Skip();
    }
    public override void ToJson(JsonObject job) => inner.ToJson(job);
    public override bool OnItemStackReceived(ItemStack stack,string eventName) => inner.OnItemStackReceived(stack,eventName);
    public override bool OnBlockPlaced(BlockPos pos,Block block,ItemStack held)
    {
        if(acceptsExistingBlock && TutorialPatch.IsWood(block,"choppingblock")) {inner.Skip(); return true;}
        return inner.OnBlockPlaced(pos,block,held);
    }
    public override bool OnBlockLookedAt(BlockSelection selection)
    {
        if(acceptsExistingBlock && selection?.Position!=null &&
            TutorialPatch.IsWood(api.World.BlockAccessor.GetBlock(selection.Position),"choppingblock"))
        {inner.Skip(); return true;}
        return inner.OnBlockLookedAt(selection);
    }
    public override bool DoCheckPlayerInventory(IPlayerInventoryManager manager)
    {
        bool changed=inner.DoCheckPlayerInventory(manager);
        if(inventoryProbe!=null && inventoryProbe.DoCheckPlayerInventory(manager) && inventoryProbe.Complete)
        {inner.Skip(); return true;}
        return changed;
    }
}
