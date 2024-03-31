using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.MonoBehaviours.CinematicController;
using NitroxModel.DataStructures.Util;
using NitroxModel.Helper;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class PlayerCinematicControllerCallProcessor : ClientPacketProcessor<PlayerCinematicControllerCall>
{
    private readonly PlayerManager playerManager;

    public PlayerCinematicControllerCallProcessor(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }

    public override void Process(PlayerCinematicControllerCall packet)
    {
        if (!NitroxEntity.TryGetObjectFrom(packet.ControllerID, out GameObject entity))
        {
            return; // Entity can be not spawned yet bc async.
        }

        if (!entity.TryGetComponent(out MultiplayerCinematicReference reference))
        {
            Log.Warn($"Couldn't find {nameof(MultiplayerCinematicReference)} on {entity.name}:{packet.ControllerID}");
            return;
        }

        Optional<RemotePlayer> opPlayer = playerManager.Find(packet.PlayerId);
        Validate.IsPresent(opPlayer);

        if (packet.StartPlaying)
        {
            reference.CallStartCinematicMode(packet.Key, packet.ControllerNameHash, opPlayer.Value);
        }
        else
        {
            reference.CallCinematicModeEnd(packet.Key, packet.ControllerNameHash, opPlayer.Value);
        }
    }
}
