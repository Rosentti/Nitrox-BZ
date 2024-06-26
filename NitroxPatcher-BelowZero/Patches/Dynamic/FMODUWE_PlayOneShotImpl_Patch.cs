using System.Reflection;
using NitroxClient_BelowZero.GameLogic.FMOD;
using NitroxModel_BelowZero.DataStructures;
using NitroxModel.GameLogic.FMOD;
using NitroxModel.Helper;
using UnityEngine;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class FMODUWE_PlayOneShotImpl_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method(() => FMODUWE.PlayOneShotImpl(default(string), default(Vector3), default(float)));

    public static bool Prefix()
    {
        return !FMODSoundSuppressor.SuppressFMODEvents;
    }

    public static void Postfix(string eventPath, Vector3 position, float volume)
    {
        if (Resolve<FMODWhitelist>().IsWhitelisted(eventPath))
        {
            Resolve<FMODSystem>().SendAssetPlay(eventPath, position.ToDto(), volume);
        }
    }
}
