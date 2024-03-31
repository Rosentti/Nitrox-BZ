using System.Reflection;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxModel.Helper;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class uGUI_OnApplicationQuit_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((uGUI t) => t.OnApplicationQuit());

    public static void Prefix()
    {
        Resolve<IMultiplayerSession>().Disconnect();
    }
}
