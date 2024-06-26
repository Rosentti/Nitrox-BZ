using System.Reflection;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using NitroxModel.DataStructures;
using NitroxModel.Helper;
using NitroxModel_Subnautica.DataStructures;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

public sealed partial class EscapePod_OnRepair_Patch : NitroxPatch, IDynamicPatch
{
    public static readonly MethodInfo TARGET_METHOD = Reflect.Method((EscapePod t) => t.OnRepair());

    public static void Prefix(EscapePod __instance)
    {
        if (__instance.TryGetIdOrWarn(out NitroxId id))
        {
            Resolve<Entities>().BroadcastMetadataUpdate(id, new RepairedComponentMetadata(TechType.EscapePod.ToDto()));
        }
    }
}
