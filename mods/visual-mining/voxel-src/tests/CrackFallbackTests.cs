using System.Collections;
using System.Reflection;
using Kuboczoch.VisualMining;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.Client.NoObf;

// Invoke Run() from the headless test entry point; no live world or gameplay writes.
internal static class CrackFallbackTests
{
    public static void Run()
    {
        const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Static;
        Type renderer = typeof(CosmeticVoxelRenderer);
        MethodInfo ready = renderer.GetMethod("HasMeshReplacement", flags)
            ?? throw new Exception("Missing committed-and-replaced crack fallback gate");
        var entries = (IDictionary)renderer.GetField("Entries", flags)!.GetValue(null)!;
        Type keyType = renderer.GetNestedType("Key", BindingFlags.NonPublic)!;
        Type entryType = renderer.GetNestedType("Entry", BindingFlags.NonPublic)!;
        object key = Activator.CreateInstance(keyType, 19, 82, -40, 2)!;
        object entry = Activator.CreateInstance(entryType, 42)!;
        var damage = new BlockDamage { Position = new BlockPos(2).Set(19, 82, -40), Block = new Block { BlockId = 42 } };
        bool Ready() => (bool)ready.Invoke(null, new object?[] { damage })!;
        void Check(bool value, string name) { if (!value) throw new Exception(name); Console.WriteLine("PASS " + name); }
        try
        {
            Check(!Ready(), "missing chip entry keeps vanilla cracks");
            entries.Add(key, entry);
            Check(!Ready(), "uncommitted entry keeps vanilla cracks");
            entryType.GetField("Committed")!.SetValue(entry, 0.3f);
            Check(!Ready(), "committed chip without mesh replacement keeps vanilla cracks");
            entryType.GetField("MeshReplaced")!.SetValue(entry, true);
            Check(Ready(), "committed chip and successful replacement permits suppression");
            entryType.GetField("Committed")!.SetValue(entry, 0f);
            Check(!Ready(), "replacement flag alone does not suppress cracks");
            entryType.GetField("Committed")!.SetValue(entry, 0.3f);
            damage.Block = new Block { BlockId = 43 };
            Check(!Ready(), "replacement for another block ID keeps vanilla cracks");
            damage.Block = new Block { BlockId = 42 };
            damage.Position.dimension = 1;
            Check(!Ready(), "replacement in another dimension keeps vanilla cracks");
            damage.Position.dimension = 2;
            damage.Position.Y++;
            Check(!Ready(), "replacement at another position keeps vanilla cracks");
            damage.Position = null;
            Check(!Ready(), "missing position keeps vanilla cracks");
        }
        finally { entries.Remove(key); }
    }
}
