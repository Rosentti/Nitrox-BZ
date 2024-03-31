using System.Reflection;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel.Helper;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class VehicleDockingBay_OnUndockingComplete_Patch : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((VehicleDockingBay t) => t.OnUndockingComplete(default(Player)));

    public static void Prefix(VehicleDockingBay __instance, Player player)
    {
        Dockable vehicle = __instance.GetDockedObject();
        Resolve<Vehicles>().BroadcastVehicleUndocking(__instance, vehicle, false);
    }
}
