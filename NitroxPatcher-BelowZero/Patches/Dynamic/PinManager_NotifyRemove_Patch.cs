using System.Reflection;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.Helper;
using NitroxModel.Packets;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class PinManager_NotifyRemove_Patch : NitroxPatch, IDynamicPatch
{
    public static readonly MethodInfo TARGET_METHOD = Reflect.Method((PinManager t) => t.NotifyRemove(default));

    public static void Prefix(TechType techType)
    {
        if (!Multiplayer.Main || !Multiplayer.Main.InitialSyncCompleted)
        {
            return;
        }
        Resolve<IPacketSender>().Send(new RecipePinned((int)techType, false));
    }
}
