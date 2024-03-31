using System.Reflection;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.Helper;
using NitroxModel.Packets;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class PDAEncyclopedia_Add_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method(() => PDAEncyclopedia.Add(default, default, default, default));

    public static void Postfix(string key, PDAEncyclopedia.Entry entry, bool verbose, bool postNotification)
    {
        if (!Multiplayer.Main || !Multiplayer.Main.InitialSyncCompleted)
        {
            return;
        }

        Resolve<IPacketSender>().Send(new PDAEncyclopediaEntryAdd(key, verbose, postNotification));
    }
}
