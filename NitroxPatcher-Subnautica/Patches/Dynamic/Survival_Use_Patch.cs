using System.Reflection;
using NitroxClient_Subnautica.Communication.Abstract;
using NitroxModel.DataStructures;
using NitroxModel.Helper;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

/// <summary>
/// Let the server know when the player successfully uses a consumable item (such as a first aid kit).
/// </summary>
public sealed partial class Survival_Use_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((Survival t) => t.Use(default(GameObject)));

    public static void Postfix(bool __result, GameObject useObj)
    {
        if (__result && useObj.TryGetIdOrWarn(out NitroxId id))
        {
            Resolve<IPacketSender>().Send(new EntityDestroyed(id));
        }
    }
}
