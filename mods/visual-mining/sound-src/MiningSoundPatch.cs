using System;
using System.Collections.Generic;
using System.Linq;
#nullable disable
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Vintagestory.API.Common;

namespace Kuboczoch.VisualMining
{
    /// <summary>Owns only the vanilla positive-resistance mining hit sound call site.</summary>
    public static class MiningSoundPatch
    {
        private const string HarmonyId = "kuboczoch.visualmining.mining-hit-sound";
        private static readonly object Gate = new object();
        private static readonly Harmony Harmony = new Harmony(HarmonyId);
        private static System.Func<IPlayer, BlockSelection, ItemSlot, bool> suppress;
        private static volatile bool owned;
        private static volatile bool compatible;
        public static bool Installed => owned && compatible;
        public static string LastError { get; private set; }

        internal static readonly MethodInfo Target = AccessTools.Method(typeof(Block), nameof(Block.OnGettingBroken),
            new[] { typeof(IPlayer), typeof(BlockSelection), typeof(ItemSlot), typeof(float), typeof(float), typeof(int) });
        private static readonly MethodInfo Hit = AccessTools.Method(typeof(BlockSounds), nameof(BlockSounds.GetHitSound), new[] { typeof(IPlayer) });
        private static readonly MethodInfo Break = AccessTools.Method(typeof(BlockSounds), nameof(BlockSounds.GetBreakSound), new[] { typeof(IPlayer) });
        private static readonly MethodInfo Play = AccessTools.Method(typeof(IWorldAccessor), nameof(IWorldAccessor.PlaySoundAt),
            new[] { typeof(SoundAttributes), typeof(double), typeof(double), typeof(double), typeof(int), typeof(IPlayer), typeof(float) });
        internal static readonly MethodInfo Helper = AccessTools.Method(typeof(MiningSoundPatch), nameof(PlayMiningSound));

        /// <summary>False means no sound replacement is safe; caller must disable its replacement audio.</summary>
        public static bool Install(System.Func<IPlayer, BlockSelection, ItemSlot, bool> shouldSuppress)
        {
            if (shouldSuppress == null) throw new ArgumentNullException(nameof(shouldSuppress));
            lock (Gate)
            {
                Dispose();
                LastError = null;
                suppress = shouldSuppress;
                try
                {
                    if (Target == null || Hit == null || Break == null || Play == null) throw new MissingMethodException("Mining sound API signature changed");
                    Harmony.Patch(Target, transpiler: new HarmonyMethod(typeof(MiningSoundPatch), nameof(Transpiler)));
                    if (!compatible) throw new InvalidOperationException("Vanilla mining sound IL did not match the supported shape");
                    owned = true;
                    return true;
                }
                catch (Exception error)
                {
                    LastError = error.Message;
                    Dispose();
                    return false;
                }
            }
        }

        public static void Dispose()
        {
            lock (Gate)
            {
                owned = false;
                suppress = null;
                compatible = false;
                if (Target != null) Harmony.Unpatch(Target, HarmonyPatchType.Transpiler, HarmonyId);
            }
        }

        // Retains the original receiver and all original PlaySoundAt arguments on the stack.
        // Resistance is the computed local used by vanilla, not the input resistance.
        internal static int PlayMiningSound(IWorldAccessor world, SoundAttributes sound, double x, double y, double z,
            int dimension, IPlayer player, float range, float resistance, BlockSelection selection, ItemSlot slot)
        {
            var predicate = suppress;
            if (Installed && resistance > 0 && predicate != null)
            {
                try { if (predicate(player, selection, slot)) return 0; }
                catch { /* Fail closed to vanilla sound, never silence on a predicate failure. */ }
            }
            return world.PlaySoundAt(sound, x, y, z, dimension, player, range);
        }

        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> source)
        {
            var code = source.ToList();
            compatible = false;
            var locals = Target?.GetMethodBody()?.LocalVariables;
            if (locals == null || locals.Count < 2 || locals[1].LocalType != typeof(float)) return code;
            // Refuse ambiguity, reordered sound paths, altered stack shapes, or exception regions.
            var hits = Enumerable.Range(0, code.Count).Where(i => code[i].Calls(Hit)).ToArray();
            var breaks = Enumerable.Range(0, code.Count).Where(i => code[i].Calls(Break)).ToArray();
            var plays = Enumerable.Range(0, code.Count).Where(i => code[i].Calls(Play)).ToArray();
            if (hits.Length != 1 || breaks.Length != 1 || plays.Length != 1 || code.Any(i => i.blocks.Count != 0)) return code;
            int h = hits[0], b = breaks[0], p = plays[0];
            if (b < 5 || h != b + 4 || p != h + 9 || p + 1 >= code.Count) return code;
            // ldloc.1; ldc.r4 0; bgt hit; ldloc sounds; ldarg.1; GetBreakSound; br merge;
            // ldloc sounds; ldarg.1; GetHitSound; merge: ldloc x/y/z; selection.Position.dimension; player; 1; PlaySoundAt
            int c = b - 5;
            if (c < 0 || code[c].opcode != OpCodes.Ldloc_1 || code[c + 1].opcode != OpCodes.Ldc_R4 || !Equals(code[c + 1].operand, 0f)
                || !IsBranch(code[c + 2], OpCodes.Bgt, OpCodes.Bgt_S) || !LoadsLocal(code[b - 2])
                || code[b - 1].opcode != OpCodes.Ldarg_1 || !IsBranch(code[b + 1], OpCodes.Br, OpCodes.Br_S)
                || code[h - 2].opcode != code[b - 2].opcode || !Equals(code[h - 2].operand, code[b - 2].operand)
                || code[h - 1].opcode != OpCodes.Ldarg_1
                || !(code[c + 2].operand is Label hitLabel) || !code[h - 2].labels.Contains(hitLabel)
                || !(code[b + 1].operand is Label mergeLabel) || !code[h + 1].labels.Contains(mergeLabel)
                || !LoadsLocal(code[h + 1]) || !LoadsLocal(code[h + 2]) || !LoadsLocal(code[h + 3])
                || code[h + 4].opcode != OpCodes.Ldarg_2
                || !code[h + 5].LoadsField(AccessTools.Field(typeof(BlockSelection), nameof(BlockSelection.Position)))
                || !code[h + 6].LoadsField(AccessTools.Field(typeof(Vintagestory.API.MathTools.BlockPos), "dimension"))
                || code[h + 7].opcode != OpCodes.Ldarg_1 || code[h + 8].opcode != OpCodes.Ldc_R4 || !Equals(code[h + 8].operand, 1f)
                || code[p].labels.Count != 0 || code[p + 1].opcode != OpCodes.Pop) return code;
            // Existing instructions (and their labels) remain intact. Only the shared call changes;
            // the helper's strict >0 gate guarantees final-break and NaN paths always forward.
            code.InsertRange(p, new[] { new CodeInstruction(OpCodes.Ldloc_1), new CodeInstruction(OpCodes.Ldarg_2), new CodeInstruction(OpCodes.Ldarg_3) });
            code[p + 3] = new CodeInstruction(OpCodes.Call, Helper);
            compatible = true;
            return code;
        }

        private static bool IsBranch(CodeInstruction i, OpCode wide, OpCode shortForm) => i.opcode == wide || i.opcode == shortForm;
        private static bool LoadsLocal(CodeInstruction i) => i.opcode == OpCodes.Ldloc || i.opcode == OpCodes.Ldloc_S
            || i.opcode == OpCodes.Ldloc_0 || i.opcode == OpCodes.Ldloc_1 || i.opcode == OpCodes.Ldloc_2 || i.opcode == OpCodes.Ldloc_3;
    }
}
