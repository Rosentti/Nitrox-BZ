using System.Reflection;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.Helper;
using NitroxModel.Packets;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class PinManager_NotifyAdd_Patch : NitroxPatch, IDynamicPatch
{
    public static readonly MethodInfo TARGET_METHOD = Reflect.Method((PinManager t) => t.NotifyAdd(default));

    public static void Prefix(TechType techType)
    {
        if (!Multiplayer.Main || !Multiplayer.Main.InitialSyncCompleted)
        {
            return;
        }
        Resolve<IPacketSender>().Send(new RecipePinned((int)techType, true));
    }
}
