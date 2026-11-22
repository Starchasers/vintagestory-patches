using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Kuboczoch.VisualMining;
using Vintagestory.API.Common;

int checks = 0;
void Check(bool value, string message) { if (!value) throw new Exception(message); checks++; Console.WriteLine("PASS " + message); }
var original = PatchProcessor.GetOriginalInstructions(MiningSoundPatch.Target);
Check(original.Count(i => i.Calls(AccessTools.Method(typeof(BlockSounds), "GetBreakSound", new[] { typeof(IPlayer) }))) == 1, "real IL has one final break call");
Check(MiningSoundPatch.Install((p, s, i) => true), "actual Harmony install: " + MiningSoundPatch.LastError);
Check(MiningSoundPatch.Installed, "Installed true");
Check(Harmony.GetPatchInfo(MiningSoundPatch.Target).Transpilers.Any(p => p.owner == "kuboczoch.visualmining.mining-hit-sound"), "actual target owns transpiler");
var patched = PatchProcessor.GetCurrentInstructions(MiningSoundPatch.Target);
Check(patched.Count(i => i.Calls(MiningSoundPatch.Helper)) == 1, "transformed IL has one helper");
Check(patched.Count(i => i.Calls(AccessTools.Method(typeof(BlockSounds), "GetBreakSound", new[] { typeof(IPlayer) }))) == 1, "final break sound getter retained");
Check(patched.Count(i => i.Calls(AccessTools.Method(typeof(BlockSounds), "GetHitSound", new[] { typeof(IPlayer) }))) == 1, "hit sound getter retained");
Check(patched.Count == original.Count + 3, "only three context loads added");
var directlyTransformed = MiningSoundPatch.Transpiler(original).ToList();
int helperIndex = directlyTransformed.FindIndex(i => i.Calls(MiningSoundPatch.Helper));
var normalized = directlyTransformed.Select(i => new CodeInstruction(i)).ToList();
normalized.RemoveRange(helperIndex - 3, 3);
int vanillaCallIndex = helperIndex - 3;
normalized[vanillaCallIndex] = new CodeInstruction(original[vanillaCallIndex]);
Check(original.Zip(normalized).All(pair => pair.First.opcode == pair.Second.opcode && Equals(pair.First.operand, pair.Second.operand) && pair.First.labels.SequenceEqual(pair.Second.labels)), "every other original instruction and branch label is unchanged");
var proxy = DispatchProxy.Create<IWorldAccessor, WorldProxy>();
var state = (WorldProxy)(object)proxy;
// IPlayer contains a nonpublic default interface member, so DispatchProxy cannot implement it.
IPlayer player = null;
var selection = new BlockSelection();
var slot = new ItemSlot(null);
var sound = default(SoundAttributes);
int Call(float resistance) => MiningSoundPatch.PlayMiningSound(proxy, sound, 1.25, 2.5, 3.75, 7, player, .75f, resistance, selection, slot);
Check(Call(1) == 0 && state.Calls == 0, "positive damage hit suppressed");
int predicateCalls = 0;
MiningSoundPatch.Install((p, s, i) => { predicateCalls++; Check(ReferenceEquals(p, player) && ReferenceEquals(s, selection) && ReferenceEquals(i, slot), "predicate exact context"); return false; });
Check(Call(1) == 123 && state.Calls == 1, "false predicate forwards original return");
Check(state.Args[0].Equals(sound) && (double)state.Args[1] == 1.25 && (double)state.Args[2] == 2.5 && (double)state.Args[3] == 3.75 && (int)state.Args[4] == 7 && ReferenceEquals(state.Args[5], player) && (float)state.Args[6] == .75f, "all original sound arguments forwarded unchanged");
int before = predicateCalls;
foreach (float value in new[] { 0f, -1f, float.NaN }) Check(Call(value) == 123, "final break/nonpositive/NaN forwarded: " + value);
Check(predicateCalls == before, "final break never consults suppression");
MiningSoundPatch.Install((p, s, i) => throw new Exception("predicate failure"));
Check(Call(1) == 123, "predicate exceptions fail closed to vanilla");
var malformed = PatchProcessor.GetOriginalInstructions(MiningSoundPatch.Target);
malformed.RemoveAt(malformed.FindIndex(i => i.Calls(AccessTools.Method(typeof(BlockSounds), "GetHitSound", new[] { typeof(IPlayer) }))));
var rejected = MiningSoundPatch.Transpiler(malformed).ToList();
Check(rejected.SequenceEqual(malformed) && !MiningSoundPatch.Installed, "unknown IL returned unchanged and disables replacement");
Check(Call(1) == 123, "invalidated compatibility forwards vanilla");
Check(MiningSoundPatch.Install((p, s, i) => true), "reinstall after rejection");
MiningSoundPatch.Dispose();
Check(!MiningSoundPatch.Installed, "dispose clears installed state");
Check(Harmony.GetPatchInfo(MiningSoundPatch.Target)?.Transpilers.All(p => p.owner != "kuboczoch.visualmining.mining-hit-sound") != false, "dispose removes own actual Harmony patch");
Check(Call(1) == 123, "disposed helper forwards vanilla");
MiningSoundPatch.Dispose();
Console.WriteLine($"SUCCESS {checks} assertions; real Harmony + game API; no graphical runtime.");

public class WorldProxy : DispatchProxy
{
    public int Calls;
    public object[] Args;
    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        if (targetMethod.Name == "PlaySoundAt") { Calls++; Args = args; return 123; }
        if (targetMethod.ReturnType == typeof(void)) return null;
        return targetMethod.ReturnType.IsValueType ? Activator.CreateInstance(targetMethod.ReturnType) : null;
    }
}
