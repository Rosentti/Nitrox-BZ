using System.Reflection;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel.DataStructures;
using NitroxModel.Helper;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

/// <summary>
/// Prevents <see cref="CreatureDeath.OnAttackByCreature"/> from happening on non-simulated entities
/// </summary>
public sealed partial class CreatureDeath_OnAttackByCreature_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((CreatureDeath t) => t.OnAttackByCreature());

    public static bool Prefix(CreatureDeath __instance)
    {
        if (__instance.TryGetNitroxId(out NitroxId creatureId) &&
            Resolve<SimulationOwnership>().HasAnyLockType(creatureId))
        {
            return true;
        }
        return false;
    }
}