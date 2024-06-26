﻿using System.Reflection;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel_BelowZero.Packets;
using NitroxModel.Helper;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class ExosuitTorpedoArm_OnUseUp_Patch : NitroxPatch, IDynamicPatch
{
    public static readonly MethodInfo TARGET_METHOD = Reflect.Method((ExosuitTorpedoArm t) => ((IExosuitArm)t).OnUseUp(out Reflect.Ref<float>.Field));

    public static void Prefix(ExosuitTorpedoArm __instance)
    {
        Resolve<ExosuitModuleEvent>().BroadcastArmAction(TechType.ExosuitTorpedoArmModule, __instance, ExosuitArmAction.END_USE_TOOL);
    }
}
