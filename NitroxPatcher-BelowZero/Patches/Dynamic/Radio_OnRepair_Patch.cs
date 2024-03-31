using System.Reflection;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using NitroxModel.Helper;
using NitroxModel_BelowZero.DataStructures;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class Radio_OnRepair_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((Radio t) => t.OnRepair());

    public static void Prefix(Radio __instance)
    {
        if (__instance.TryGetIdOrWarn(out NitroxId id))
        {
            Resolve<Entities>().BroadcastMetadataUpdate(id, new RepairedComponentMetadata(TechType.Radio.ToDto()));
        }
    }
}
