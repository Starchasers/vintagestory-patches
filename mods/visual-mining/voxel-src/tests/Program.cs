using System.Reflection;
using Kuboczoch.VisualMining;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

int cases = 0;
foreach (EnumTool tool in new[] { EnumTool.Pickaxe, EnumTool.Axe, EnumTool.Shovel })
foreach (BlockFacing face in BlockFacing.ALLFACES)
{
    var state = new VoxelDamageState(1);
    uint original = state.BuildCuboids().Single();
    var solid = (bool[])typeof(VoxelDamageState).GetField("solid", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(state)!;
    if (solid.Count(v => v) != 4096) throw new Exception("Not initially solid");
    int changes = 0;
    int previous = solid.Count(v => v);
    for (int i = 0; i < 12; i++)
    {
        if (state.ApplyHit(new Vec3d(0.5, 0.5, 0.5), face, tool, 1f, true)) changes++;
        int count = solid.Count(v => v);
        if (count > previous || count == 0) throw new Exception("Nonmonotonic or completely erased");
        previous = count;
        if (state.BuildCuboids().Count == 0) throw new Exception("No surviving mesh");
    }
    if (previous >= 4096 || !state.HasOpenBoundary(face.Normali.X, face.Normali.Y, face.Normali.Z)) throw new Exception("No boundary carving");
    for (int i = 0; i < changes; i++) state.RestoreLastHit();
    if (solid.Count(v => v) != 4096 || state.BuildCuboids().Single() != original) throw new Exception("Rollback not exact");
    cases++;
}
var harmony = new HarmonyLib.Harmony("kuboczoch.visualmining.voxels.tests");
try
{
    foreach (Type type in typeof(CosmeticVoxelRenderer).GetNestedTypes(BindingFlags.NonPublic).Where(t => t.Name.EndsWith("MeshPatch") || t.Name == "CrackDecalPatch"))
        harmony.CreateClassProcessor(type).Patch();
    int installed=HarmonyLib.Harmony.GetAllPatchedMethods().Count(method=>HarmonyLib.Harmony.GetPatchInfo(method)?.Owners.Contains(harmony.Id)==true);
    if(installed!=7)throw new Exception("Expected six tessellation/neighbor patches and one decal filter, got "+installed);
    Console.WriteLine("PASS: all six Harmony tessellation patches (five dispatch paths incl TopSoil plus neighbor visibility) and local crack-decal filter installed against actual game assemblies.");
}
finally { harmony.UnpatchAll(harmony.Id); }
CrackFallbackTests.Run();
Console.WriteLine($"PASS: {cases} tool/face cases; 12 sequential hits each; monotonic removal, surviving cuboids, open boundaries and exact rollback.");
