using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.Helper;
using NitroxModel.Packets;
using NitroxModel_BelowZero.DataStructures;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class KnownTech_NotifyAdd_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method(() => KnownTech.NotifyAdd(default, default));

    public static void Prefix(TechType techType, bool verbose)
    {
        if (!Multiplayer.Main || !Multiplayer.Main.InitialSyncCompleted)
        {
            return;
        }
        List<PDAScanner.Entry> partialEntries = new();
        PDAScanner.GetPartialEntriesWhichUnlocks(techType, partialEntries, true);

        Resolve<IPacketSender>().Send(new KnownTechEntryAdd(
            KnownTechEntryAdd.EntryCategory.KNOWN,
            techType.ToDto(),
            verbose,
            partialEntries.Select(entry => entry.techType.ToDto()).ToList()
        ));
    }
}
