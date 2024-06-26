﻿using System.Reflection;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel.Helper;
using NitroxModel_Subnautica.Packets;
using UnityEngine;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

public sealed partial class ExosuitTorpedoArm_Shoot_Patch : NitroxPatch, IDynamicPatch
{
    public static readonly MethodInfo TARGET_METHOD = Reflect.Method((ExosuitTorpedoArm t) => t.Shoot(default(TorpedoType), default(Transform), default(bool)));

    public static void Prefix(ExosuitTorpedoArm __instance, bool __result, TorpedoType torpedoType, Transform siloTransform)
    {
        if (torpedoType != null)
        {
            ExosuitArmAction action = ExosuitArmAction.START_USE_TOOL;
            if (siloTransform == __instance.siloSecond)
            {
                action = ExosuitArmAction.ALT_HIT;
            }
            if (siloTransform != __instance.siloFirst && siloTransform != __instance.siloSecond)
            {
                Log.Error($"Exosuit torpedo arm siloTransform is not first or second silo {__instance.GetId()}");
            }
            Resolve<ExosuitModuleEvent>().BroadcastArmAction(TechType.ExosuitTorpedoArmModule,
                 __instance,
                 action,
                 Player.main.camRoot.GetAimingTransform().forward,
                 Player.main.camRoot.GetAimingTransform().rotation
            );
        }
    }
}
