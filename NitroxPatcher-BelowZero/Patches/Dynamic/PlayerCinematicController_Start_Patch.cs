using System.Reflection;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.MonoBehaviours.CinematicController;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel_BelowZero.Helper;
using NitroxModel.Helper;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class PlayerCinematicController_Start_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo targetMethod = Reflect.Method((PlayerCinematicController t) => t.Start());

    public static void Postfix(PlayerCinematicController __instance)
    {
        if (!__instance.TryGetComponentInParent(out NitroxEntity entity, true))
        {
            if (__instance.GetRootParent().gameObject.name != SubnauticaConstants.LIGHTMAPPED_PREFAB_NAME) // ignore calls from "blueprint prefabs"
            {
                Log.Warn($"[PlayerCinematicController_Start_Patch] - No NitroxEntity for \"{__instance.gameObject.GetFullHierarchyPath()}\" found!");
            }

            return;
        }

        entity.gameObject.EnsureComponent<MultiplayerCinematicReference>().AddController(__instance);
    }
}
