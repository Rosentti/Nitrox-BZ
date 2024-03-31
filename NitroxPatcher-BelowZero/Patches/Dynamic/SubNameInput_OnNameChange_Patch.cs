using System;
using System.Reflection;
using NitroxClient_BelowZero.GameLogic;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.DataStructures;
using NitroxModel.Helper;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class SubNameInput_OnNameChange_Patch : NitroxPatch, IDynamicPatch
{
    public static readonly MethodInfo TARGET_METHOD = Reflect.Method((SubNameInput t) => t.OnNameChange(default(string)));

    public static void Postfix(SubNameInput __instance)
    {
        if (TryGetTargetId(__instance, out object target, out NitroxId targetId))
        {
            Resolve<Entities>().EntityMetadataChanged(target, targetId);
        }
    }

    public static bool TryGetTargetId(SubNameInput subNameInput, out object target, out NitroxId targetId)
    {
        ICustomizeable subName = subNameInput.target;
        Log.Info($"TryGetTargetId for type {subNameInput.target.GetType()}");
        
        if (subName == null)
        {
            target = null;
            targetId = null;
            return false;
        }

        if (subName is ColorNameControl colorNameControl) {
            if (colorNameControl.gameObject.TryGetComponent(out Vehicle vehicle))
            {
                target = vehicle;
                return vehicle.TryGetNitroxId(out targetId);
            }
            else if (colorNameControl.gameObject.TryGetComponent(out Rocket rocket))
            {
                // For some reason only the rocket has a full functioning ghost with a different NitroxId when spawning/constructing, so we are ignoring it.
                if (rocket.TryGetComponentInChildren(out VFXConstructing constructing, true) && !constructing.isDone)
                {
                    target = null;
                    targetId = null;
                    return false;
                }
            } else if (colorNameControl.gameObject.TryGetComponent(out SeaTruckSegment seaTruckSegment)) {
                target = seaTruckSegment;
                return seaTruckSegment.TryGetNitroxId(out targetId);
            } else if (colorNameControl.gameObject.TryGetComponent(out Hoverbike hoverbike)) {
                target = hoverbike;
                return hoverbike.TryGetNitroxId(out targetId);
            }
        }

       
        // Cyclops and Rocket has their SubNameInput and SubName in the same GameObject, marked with a NitroxEntity
        target = subNameInput;
        return subNameInput.TryGetNitroxId(out targetId);
    }
}
