using System.Reflection;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.DataStructures;
using NitroxModel.Helper;
using NitroxModel.Packets;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class LiveMixin_Kill_Patch : NitroxPatch, IDynamicPatch
{
    internal static readonly MethodInfo TARGET_METHOD = Reflect.Method((LiveMixin t) => t.Kill(default));

    public static void Postfix(LiveMixin __instance)
    {
        // Whitelisted object types should have their death handled in other places
        // This broadcaster is a more general one
        if (Multiplayer.Active && __instance.TryGetNitroxId(out NitroxId objectId) &&
            !Resolve<LiveMixinManager>().IsWhitelistedUpdateType(__instance) &&
            (__instance.destroyOnDeath || Resolve<LiveMixinManager>().ShouldBroadcastDeath(__instance)))
        {
            Resolve<IPacketSender>().Send(new EntityDestroyed(objectId));
        }
    }
}
