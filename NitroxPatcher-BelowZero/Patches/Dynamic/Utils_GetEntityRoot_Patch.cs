using System.Reflection;
using NitroxClient_BelowZero.GameLogic.PlayerLogic;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.Helper;
using UnityEngine;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

/// <summary>
/// When looking for an EntityRoot, we want to make sure that remote players can be recognized as one.
/// </summary>
public sealed partial class Utils_GetEntityRoot_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method(() => UWE.Utils.GetEntityRoot(default));

    public static bool Prefix(GameObject go, ref GameObject __result)
    {
        if (go.TryGetComponent(out RemotePlayerIdentifier remotePlayerIdentifier) ||
            go.TryGetComponentInParent(out remotePlayerIdentifier, true))
        {
            __result = remotePlayerIdentifier.gameObject;
            return false;
        }
        return true;
    }
}
