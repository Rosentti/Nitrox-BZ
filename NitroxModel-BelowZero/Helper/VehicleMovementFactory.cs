﻿using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel_BelowZero.DataStructures;
using NitroxModel_BelowZero.DataStructures.GameLogic;
using UnityEngine;

namespace NitroxModel_BelowZero.Helper
{
    public static class VehicleMovementFactory
    {
        public static VehicleMovementData GetVehicleMovementData(TechType techType, NitroxId id, Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity, float steeringWheelYaw, float steeringWheelPitch, bool appliedThrottle, Vector3 leftAimTarget, Vector3 rightAimTarget)
        {
            switch (techType)
            {
                case TechType.Exosuit:
                    return new ExosuitMovementData(techType.ToDto(), id, position.ToDto(), rotation.ToDto(), velocity.ToDto(), angularVelocity.ToDto(), steeringWheelYaw, steeringWheelPitch, appliedThrottle, leftAimTarget.ToDto(), rightAimTarget.ToDto());
                default:
                    return new BasicVehicleMovementData(techType.ToDto(), id, position.ToDto(), rotation.ToDto(), velocity.ToDto(), angularVelocity.ToDto(), steeringWheelYaw, steeringWheelPitch, appliedThrottle);
            }
        }
    }
}
