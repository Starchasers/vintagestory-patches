namespace Kuboczoch.FoodHandbook;
internal sealed record HeadingPart(string Key, string? Link);
internal static class ProcessHeadingPolicy
{
 internal static IReadOnlyList<HeadingPart> For(ProcessKind kind) => kind switch {
  ProcessKind.Simmering => new[]{new HeadingPart("process-1","handbook://simmeringguide")},
  ProcessKind.Kneading => new[]{new HeadingPart("process-2","handbook://kneadingmixingguide")},
  ProcessKind.Mixing => new[]{new HeadingPart("process-4","handbook://kneadingmixingguide")},
  // One combined output group: localized parent followed by two independently linked headings.
  ProcessKind.Simmering | ProcessKind.Kneading => new[]{new HeadingPart("process-3",null),new HeadingPart("process-1","handbook://simmeringguide"),new HeadingPart("process-2","handbook://kneadingmixingguide")},
  _ => Array.Empty<HeadingPart>()
 };
}
