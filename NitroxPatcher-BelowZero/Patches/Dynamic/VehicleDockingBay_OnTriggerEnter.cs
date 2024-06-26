﻿using System.Reflection;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.Util;
using NitroxModel.Helper;
using UnityEngine;

namespace NitroxPatcher_BelowZero.Patches.Dynamic;

public sealed partial class VehicleDockingBay_OnTriggerEnter : NitroxPatch, IDynamicPatch
{
    private static readonly MethodInfo TARGET_METHOD = Reflect.Method((VehicleDockingBay t) => t.OnTriggerEnter(default(Collider)));
    private static Dockable prevInterpolatingDockable;

    public static bool Prefix(VehicleDockingBay __instance, Collider other)
    {
        Vehicle vehicle = other.GetComponentInParent<Vehicle>();
        prevInterpolatingDockable = __instance.interpolatingDockable;
        Optional<NitroxId> opVehicleId = vehicle.GetId();
        return !vehicle || (opVehicleId.HasValue && Resolve<SimulationOwnership>().HasAnyLockType(opVehicleId.Value));
    }

    public static void Postfix(VehicleDockingBay __instance)
    {
        Dockable interpolatingVehicle = __instance.interpolatingDockable;
        // Only send data, when interpolatingVehicle changes to avoid multiple packages send
        if (!interpolatingVehicle || interpolatingVehicle == prevInterpolatingDockable)
        {
            return;
        }

        if (interpolatingVehicle.TryGetIdOrWarn(out NitroxId id) && Resolve<SimulationOwnership>().HasAnyLockType(id))
        {
            Log.Debug($"Will send vehicle docking for {id}");
            Resolve<Vehicles>().BroadcastVehicleDocking(__instance, interpolatingVehicle);
        }
    }
}
