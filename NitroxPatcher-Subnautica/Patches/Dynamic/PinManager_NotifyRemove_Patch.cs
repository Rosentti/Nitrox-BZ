using System.Reflection;
using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxModel.Helper;
using NitroxModel.Packets;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

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
