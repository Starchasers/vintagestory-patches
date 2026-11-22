from pathlib import Path
import json
p = Path(__file__).parent
src=p/'src'; src.mkdir(exist_ok=True)
(src/'KuboczochWoodworkingTutorial.csproj').write_text('''<Project Sdk="Microsoft.NET.Sdk">
<PropertyGroup><TargetFramework>net10.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings><Nullable>enable</Nullable><AssemblyName>KuboczochWoodworkingTutorial</AssemblyName><EnableDefaultCompileItems>false</EnableDefaultCompileItems><GameDir>../vs</GameDir></PropertyGroup>
<ItemGroup><Compile Include="Mod.cs" />
<Reference Include="VintagestoryAPI"><HintPath>$(GameDir)/VintagestoryAPI.dll</HintPath><Private>false</Private></Reference>
<Reference Include="VSSurvivalMod"><HintPath>$(GameDir)/Mods/VSSurvivalMod.dll</HintPath><Private>false</Private></Reference>
<Reference Include="0Harmony"><HintPath>$(GameDir)/Lib/0Harmony.dll</HintPath><Private>false</Private></Reference>
</ItemGroup></Project>''')
(src/'Mod.cs').write_text('''using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
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
        api.Logger.Notification("[Kuboczoch Woodworking Tutorial] Firewood tutorial compatibility enabled (tested API: VS 1.22.7 / IW 1.3.13).");
    }
    public override void Dispose()
    {
        harmony?.UnpatchAll(HarmonyId);
        harmony = null;
    }
}

public static class TutorialPatch
{
    public const string TextKey = "kuboczochwoodworkingtutorial:firewood";
    private static readonly FieldInfo StepsField = AccessTools.Field(typeof(TutorialBase), "steps");
    private static readonly FieldInfo ApiField = AccessTools.Field(typeof(TutorialBase), "capi");
    private static readonly FieldInfo CodeField = AccessTools.Field(typeof(TutorialStepBase), "code");

    public static void Postfix(FirstStepsTutorial __instance)
    {
        var api = (ICoreClientAPI)ApiField.GetValue(__instance)!;
        if (!Replace((List<TutorialStepBase>)StepsField.GetValue(__instance)!, api))
            api.Logger.Warning("[Kuboczoch Woodworking Tutorial] Expected craftfirewood step not found exactly once; tutorial left unchanged.");
    }

    // Keep the original step ID and position: vanilla tutorial save files remain compatible.
    public static bool Replace(List<TutorialStepBase> steps, ICoreClientAPI api)
    {
        var matches = steps.Select((step, position) => (step, position))
            .Where(pair => (string?)CodeField.GetValue(pair.step) == "craftfirewood").ToArray();
        if (matches.Length != 1) return false;
        var (old, position) = matches[0];
        steps[position] = TutorialStepBase.Collect(api, "craftfirewood", TextKey,
            stack => stack?.Collectible is ItemFirewood, 4);
        steps[position].index = old.index;
        return true;
    }
}
''')
pkg=p/'package'; pkg.mkdir(exist_ok=True)
info={'type':'code','modid':'kuboczochwoodworkingtutorial','name':'Kuboczoch Woodworking Tutorial','authors':['Kuboczoch'],'description':'Adapts the vanilla First Steps firewood tutorial to Immersive Woodworking: chopping block instructions and collection-based completion. Built for VS 1.22.7 / IW 1.3.13.','version':'1.0.0','side':'Client','requiredOnClient':False,'requiredOnServer':False,'dependencies':{'game':'1.22.7','survival':'1.22.7','immersivewoodworking':'1.3.13'}}
(pkg/'modinfo.json').write_text(json.dumps(info,indent=2)+'\n')
lang=pkg/'assets/kuboczochwoodworkingtutorial/lang'; lang.mkdir(parents=True,exist_ok=True)
translations={
'en': 'Make firewood with Immersive Woodworking, then collect {0} more firewood (4 total).<br/><br/>1. Craft a chopping block: put an axe immediately LEFT of a log in the crafting grid. Place the chopping block on the ground.<br/>2. With a log in hand, right-click the chopping block to load it.<br/>3. With an axe in hand, HOLD right-click on the block until the log splits.<br/>4. With this modpack\'s settings, the log becomes half-logs. Pick them up, put a half-log back on the block, and HOLD right-click with the axe again to split it into firewood.<br/>5. Pick up the dropped firewood. This step checks collected firewood, not the disabled vanilla crafting recipe.<br/><br/>If half-log splitting is disabled in the server settings, logs turn directly into firewood. Firewood already in your inventory also counts.',
'pl': 'Przygotuj drewno opałowe za pomocą Immersive Woodworking i zbierz jeszcze {0} szt. (łącznie 4).<br/><br/>1. Wytwórz pieniek do rąbania: umieść siekierę bezpośrednio PO LEWEJ stronie kłody w siatce wytwarzania. Postaw pieniek na ziemi.<br/>2. Trzymając kłodę, kliknij pieniek prawym przyciskiem myszy, aby ją położyć.<br/>3. Trzymając siekierę, PRZYTRZYMAJ prawy przycisk myszy na pieńku, aż kłoda się rozłupie.<br/>4. W ustawieniach tej paczki kłoda zamienia się w połówki. Podnieś je, połóż połówkę na pieńku i ponownie PRZYTRZYMAJ prawy przycisk myszy z siekierą, aby uzyskać drewno opałowe.<br/>5. Podnieś drewno opałowe z ziemi. Ten etap sprawdza zebrane drewno, a nie wyłączoną recepturę podstawowej gry.<br/><br/>Jeśli serwer wyłączył etap połówek, kłody od razu dają drewno opałowe. Drewno opałowe już obecne w ekwipunku również jest zaliczane.'}
for code,text in translations.items(): (lang/(code+'.json')).write_text(json.dumps({'firewood':text},indent=2,ensure_ascii=False)+'\n')
print('Created mod source, manifest, EN/PL tutorial assets.')
