#if DEBUG
using System.Reflection;
using NitroxClient_BelowZero.GameLogic;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.Helper;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class FPSCounter_UpdateDisplay_Patch : NitroxPatch, IDynamicPatch
{
    public static readonly MethodInfo TARGET_METHOD = Reflect.Method((FPSCounter t) => t.UpdateDisplay());

    public static void Postfix(FPSCounter __instance)
    {
        if (!Multiplayer.Active)
        {
            return;
        }
        __instance.strBuffer.Append("Loading entities: ").AppendLine(Resolve<Entities>().EntitiesToSpawn.Count.ToString());
        __instance.strBuffer.Append("Real time elapsed: ").AppendLine(Resolve<TimeManager>().RealTimeElapsed.ToString());
        __instance.text.SetText(__instance.strBuffer);
    }
}
#endif
