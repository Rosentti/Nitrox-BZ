using System.Reflection;
using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel.DataStructures;
using NitroxModel.Helper;
using NitroxModel.Packets;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

public sealed partial class CrashHome_OnDestroy_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((CrashHome t) => t.OnDestroy());

    public static void Prefix(CrashHome __instance)
    {
        if (!__instance.TryGetNitroxId(out NitroxId crashHomeId) ||
            !Resolve<SimulationOwnership>().HasAnyLockType(crashHomeId) ||
            !__instance.crash ||
            !__instance.crash.TryGetNitroxId(out NitroxId crashId))
        {
            return;
        }
        Resolve<IPacketSender>().Send(new EntityDestroyed(crashId));
    }
}
