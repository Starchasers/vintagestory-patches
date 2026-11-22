using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.API.Common;

namespace Kuboczoch.FirepitTinderReturn;

/// <summary>Keep dry-grass construction in the vanilla domain; leave straw in AoG's domain.</summary>
public sealed class FirepitTinderReturnSystem : ModSystem
{
    public const string PatchId = "kuboczoch.firepittinderreturn";
    private static readonly object Gate = new();
    private static Harmony? harmony;
    private static int owners;
    private bool ownsPatch;
    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        ApplyPatch();
        api.Logger.Notification("[Kuboczoch Firepit Tinder Return] Dry grass uses game firepits; straw keeps Art of Growing firepits.");
    }
    public void ApplyPatch()
    {
        lock (Gate)
        {
        if (ownsPatch) return;
        if (harmony != null) { owners++; ownsPatch = true; return; }
        var type = AccessTools.TypeByName("ArtOfGrowing.Items.AOGItemDryGrass")
            ?? throw new InvalidOperationException("Art of Growing AOGItemDryGrass was not loaded.");
        var method = AccessTools.DeclaredMethod(type, "OnHeldInteractStartThatch", new[] {
            typeof(ItemSlot), typeof(EntityAgent), typeof(BlockSelection), typeof(EntitySelection),
            typeof(bool), typeof(EnumHandHandling).MakeByRefType() })
            ?? throw new MissingMethodException(type.FullName, "OnHeldInteractStartThatch");
        if (method.IsStatic) throw new InvalidOperationException("Unsupported AoG placement: expected instance method.");
        harmony = new Harmony(PatchId);
        try
        {
            harmony.Patch(method, transpiler: new HarmonyMethod(typeof(FirepitTinderReturnSystem), nameof(ChooseTinderDomain)));
            owners = 1;
            ownsPatch = true;
        }
        catch { harmony.UnpatchAll(PatchId); harmony = null; throw; }
        }
    }
    public static IEnumerable<CodeInstruction> ChooseTinderDomain(IEnumerable<CodeInstruction> input)
    {
        var instructions = input.ToList();
        var matches = instructions.Count(i => i.opcode == OpCodes.Ldstr && Equals(i.operand, "artofgrowing:firepit-construct1"));
        if (matches != 1) throw new InvalidOperationException($"Unsupported AoG placement IL: expected one firepit constructor literal, found {matches}.");
        foreach (var instruction in instructions)
        {
            if (instruction.opcode == OpCodes.Ldstr && Equals(instruction.operand, "artofgrowing:firepit-construct1"))
            {
                // Change only the selected construction identity, not AoG's access, consumption,
                // handling, placement validation, ground storage, sound or animation code.
                var loadSlot = new CodeInstruction(OpCodes.Ldarg_1);
                loadSlot.labels.AddRange(instruction.labels);
                loadSlot.blocks.AddRange(instruction.blocks);
                yield return loadSlot;
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(FirepitTinderReturnSystem), nameof(FirepitCode)));
            }
            else yield return instruction;
        }
    }
    public static string FirepitCode(ItemSlot slot) => slot?.Itemstack?.Collectible?.Code?.ToString() == "game:drygrass"
        ? "game:firepit-construct1" : "artofgrowing:firepit-construct1";
    public override void Dispose()
    {
        lock (Gate)
        {
            if (ownsPatch)
            {
                ownsPatch = false;
                if (--owners == 0) { harmony?.UnpatchAll(PatchId); harmony = null; }
            }
        }
        base.Dispose();
    }
}
