using System.Reflection;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.GameLogic.PlayerLogic;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.MonoBehaviours.CinematicController;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.Helper;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class PlayerCinematicController_OnPlayerCinematicModeEnd_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo targetMethod = Reflect.Method((PlayerCinematicController t) => t.OnPlayerCinematicModeEnd());

    public static void Prefix(PlayerCinematicController __instance)
    {
        if (!__instance.cinematicModeActive)
        {
            return;
        }

        if (!__instance.TryGetComponentInParent(out NitroxEntity entity, true))
        {
            Log.Warn($"[{nameof(PlayerCinematicController_OnPlayerCinematicModeEnd_Patch)}] - No NitroxEntity for \"{__instance.gameObject.GetFullHierarchyPath()}\" found!");
            return;
        }

        int identifier = MultiplayerCinematicReference.GetCinematicControllerIdentifier(__instance.gameObject, entity.gameObject);
        Resolve<PlayerCinematics>().EndCinematicMode(Resolve<IMultiplayerSession>().Reservation.PlayerId, entity.Id, identifier, __instance.playerViewAnimationName);
    }
}
