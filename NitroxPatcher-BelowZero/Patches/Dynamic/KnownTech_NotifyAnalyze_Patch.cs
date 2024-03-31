using System.Reflection;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.Helper;
using NitroxModel.Packets;
using NitroxModel_BelowZero.DataStructures;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class KnownTech_NotifyAnalyze_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method(() => KnownTech.NotifyAnalyze(default, default));

    public static void Prefix(KnownTech.AnalysisTech analysis, bool verbose)
    {
        if (!Multiplayer.Main || !Multiplayer.Main.InitialSyncCompleted)
        {
            return;
        }

        Resolve<IPacketSender>().Send(new KnownTechEntryAdd(KnownTechEntryAdd.EntryCategory.ANALYZED, analysis.techType.ToDto(), verbose));
    }
}
