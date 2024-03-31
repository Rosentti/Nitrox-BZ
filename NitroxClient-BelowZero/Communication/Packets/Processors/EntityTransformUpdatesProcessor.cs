using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;
using NitroxModel_BelowZero.DataStructures;
using UnityEngine;
using static NitroxModel.Packets.EntityTransformUpdates;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class EntityTransformUpdatesProcessor : ClientPacketProcessor<EntityTransformUpdates>
{
    public override void Process(EntityTransformUpdates packet)
    {
        foreach (EntityTransformUpdate update in packet.Updates)
        {
            Optional<GameObject> opGameObject = NitroxEntity.GetObjectFrom(update.Id);

            if (!opGameObject.HasValue)
            {
                continue;
            }

            RemotelyControlled remotelyControlled = opGameObject.Value.GetComponent<RemotelyControlled>();

            if (!remotelyControlled)
            {
                remotelyControlled = opGameObject.Value.AddComponent<RemotelyControlled>();
            }

            if (update is SplineTransformUpdate splineUpdate)
            {
                remotelyControlled.UpdateKnownSplineUser(splineUpdate.Position.ToUnity(), splineUpdate.Rotation.ToUnity(), splineUpdate.DestinationPosition.ToUnity(), splineUpdate.DestinationDirection.ToUnity(), splineUpdate.Velocity);
            }
            else
            {
                remotelyControlled.UpdateOrientation(update.Position.ToUnity(), update.Rotation.ToUnity());
            }
        }
    }
}
