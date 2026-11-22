using Newtonsoft.Json.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace Kuboczoch.VisualMining;

/// <summary>Local presentation assets must not depend on server-supplied entity metadata.</summary>
public sealed class LocalAnimationAssets
{
    public AnimationMetaData[] Metadata { get; }
    private readonly Animation[] animations;
    public LocalAnimationAssets(IEnumerable<string> patchJson) {
        var meta = new List<AnimationMetaData>();
        var shapes = new List<Animation>();
        foreach (var json in patchJson) foreach (JObject op in JArray.Parse(json)) {
            string? path = (string?)op["path"];
            if (path == "/client/animations/-") meta.Add(op["value"]!.ToObject<AnimationMetaData>()!);
            else if (path == "/animations/-") shapes.Add(op["value"]!.ToObject<Animation>()!);
        }
        Metadata = meta.ToArray(); animations = shapes.ToArray();
        if (Metadata.Length != 8 || animations.Length != 8 || Metadata.Any(m => !m.Code.StartsWith("kvm-") || !animations.Any(a => a.Code == m.Animation)))
            throw new InvalidDataException("Expected eight standalone animation metadata/shape pairs");
        foreach (var m in Metadata) m.Init();
    }
    public void RegisterMetadata(EntityClientProperties client) {
        client.AnimationsByMetaCode ??= new(StringComparer.OrdinalIgnoreCase);
        client.AnimationsByCrc32 ??= new();
        var missing = Metadata.Where(m => !client.AnimationsByMetaCode.ContainsKey(m.Code)).Select(m => m.Clone()).ToArray();
        foreach (var m in missing) {
            client.AnimationsByMetaCode[m.Code] = m;
            client.AnimationsByCrc32[m.CodeCrc32] = m;
        }
        client.Animations = (client.Animations ?? Array.Empty<AnimationMetaData>()).Concat(missing).ToArray();
    }
    public Shape PrepareShape(Shape original) {
        if (animations.All(a => original.Animations?.Any(existing => existing.Code == a.Code) == true)) return original;
        // Never modify the shared player shape, nor remove other mods' animations/elements.
        var shape = original.Clone();
        var existingAnimations = shape.Animations ?? Array.Empty<Animation>();
        shape.Animations = existingAnimations.Concat(animations.Where(a => !existingAnimations.Any(existing => existing.Code == a.Code)).Select(a => a.Clone())).ToArray();
        return shape;
    }
    public void PrepareTesselation(ref Shape shape, ref bool shapeIsCloned) {
        var prepared = PrepareShape(shape);
        // The normal engine skips AnimationCache for private cloned shapes. This also
        // keeps the caller's mesh, TP animator and self-FP animator on one shape.
        shape = ReferenceEquals(prepared, shape) && !shapeIsCloned ? shape.Clone() : prepared;
        shapeIsCloned = true;
    }
    public bool Registered(EntityClientProperties client) => Metadata.All(m => client.AnimationsByMetaCode.ContainsKey(m.Code));
}
