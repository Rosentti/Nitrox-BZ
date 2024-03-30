using System.Collections;
using System.Reflection;
using NitroxClient_Subnautica.GameLogic;
using NitroxClient_Subnautica.GameLogic.ChatUI;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxModel.DataStructures;
using NitroxModel.Helper;
using UnityEngine;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

public sealed partial class Bench_ExitSittingMode_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((Bench t) => t.ExitSittingMode(default, default));

    public static void Prefix(ref bool __runOriginal)
    {
        __runOriginal = !Resolve<PlayerChatManager>().IsChatSelected && !DevConsole.instance.selected;
    }

    public static void Postfix(Bench __instance, bool __runOriginal)
    {
        if (!__runOriginal)
        {
            return;
        }

        if (__instance.TryGetIdOrWarn(out NitroxId id))
        {
            // Request to be downgraded to a transient lock so we can still simulate the positioning.
            Resolve<SimulationOwnership>().RequestSimulationLock(id, SimulationLockType.TRANSIENT);

            Resolve<LocalPlayer>().AnimationChange(AnimChangeType.BENCH, AnimChangeState.OFF);
            __instance.StartCoroutine(ResetAnimationDelayed(__instance.standUpCinematicController.interpolationTimeOut));
        }
    }

    private static IEnumerator ResetAnimationDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        Resolve<LocalPlayer>().AnimationChange(AnimChangeType.BENCH, AnimChangeState.UNSET);
    }
}
