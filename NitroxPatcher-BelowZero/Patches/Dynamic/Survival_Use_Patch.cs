using System.Reflection;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxModel.DataStructures;
using NitroxModel.Helper;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

/// <summary>
/// Let the server know when the player successfully uses a consumable item (such as a first aid kit).
/// </summary>
public sealed partial class Survival_Use_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((Survival t) => t.Use(default(GameObject), default(Inventory)));

    public static void Postfix(bool __result, GameObject useObj, Inventory inventory)
    {
        if (__result && useObj.TryGetIdOrWarn(out NitroxId id))
        {
            Resolve<IPacketSender>().Send(new EntityDestroyed(id));
        }
    }
}
