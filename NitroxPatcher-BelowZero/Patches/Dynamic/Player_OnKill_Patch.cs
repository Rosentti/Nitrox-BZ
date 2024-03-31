using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using NitroxModel.Helper;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class Player_OnKill_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((Player t) => t.OnKill(default(DamageType)));

    private static readonly MethodInfo SKIP_METHOD = Reflect.Method(() => GameModeManager.GetOption<bool>(GameOption.PermanentDeath));
    public static IEnumerable<CodeInstruction> Transpiler(MethodBase original, IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> instructionList = instructions.ToList();
        /**
        * Skips
        * if (GameModeUtils.IsPermadeath())
        * {
        *      SaveLoadManager.main.ClearSlotAsync(SaveLoadManager.main.GetCurrentSlot());
        *      this.EndGame();
        *      return;
        * }
        */
        for (int i = 0; i < instructionList.Count; i++)
        {
            CodeInstruction instr = instructionList[i];

            if (instr.opcode == OpCodes.Call && instr.operand.Equals(SKIP_METHOD))
            {
                CodeInstruction newInstr = CodeInstruction.Call(typeof(Player_OnKill_Patch), nameof(StubFunc));
                newInstr.labels = instr.labels;
                yield return newInstr;
            }
            else
            {
                yield return instr;
            }
        }
    }

    public static bool StubFunc(GameOption opt) {
        return false;
    }
}
