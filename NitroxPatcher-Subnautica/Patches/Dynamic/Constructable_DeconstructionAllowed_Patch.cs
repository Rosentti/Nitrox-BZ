using System.Reflection;
using NitroxClient_Subnautica.GameLogic.Bases;
using NitroxClient_Subnautica.GameLogic.Settings;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxClient_Subnautica.Unity.Helper;
using NitroxModel.DataStructures;
using NitroxModel.Helper;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

/// <summary>
/// Prevents deconstruction if the target base is desynced.
/// </summary>
public sealed partial class Constructable_DeconstructionAllowed_Patch : NitroxPatch, IDynamicPatch
{
    public static readonly MethodInfo TARGET_METHOD = Reflect.Method((Constructable t) => t.DeconstructionAllowed(out Reflect.Ref<string>.Field));

    public static void Postfix(Constructable __instance, ref bool __result, ref string reason)
    {
        if (!__result || !BuildingHandler.Main || !__instance.TryGetComponentInParent(out NitroxEntity parentEntity, true))
        {
            return;
        }
        DeconstructionAllowed(parentEntity.Id, ref __result, ref reason);
    }

    public static void DeconstructionAllowed(NitroxId baseId, ref bool __result, ref string reason)
    {
        if (BuildingHandler.Main.BasesCooldown.ContainsKey(baseId))
        {
            __result = false;
            reason = Language.main.Get("Nitrox_ErrorRecentBuildUpdate");
        }
        else if (BuildingHandler.Main.EnsureTracker(baseId).IsDesynced() && NitroxPrefs.SafeBuilding.Value)
        {
            __result = false;
            reason = Language.main.Get("Nitrox_ErrorDesyncDetected");
        }
    }
}
