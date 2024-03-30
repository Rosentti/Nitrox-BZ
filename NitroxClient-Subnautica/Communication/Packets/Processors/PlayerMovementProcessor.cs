using System.Collections;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxClient_Subnautica.Unity.Helper;
using NitroxModel_Subnautica.DataStructures;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

public class PlayerMovementProcessor : ClientPacketProcessor<PlayerMovement>
{
    private readonly PlayerManager remotePlayerManager;

    public PlayerMovementProcessor(PlayerManager remotePlayerManager)
    {
        this.remotePlayerManager = remotePlayerManager;
    }

    public override void Process(PlayerMovement movement)
    {
        Optional<RemotePlayer> remotePlayer = remotePlayerManager.Find(movement.PlayerId);
        if (!remotePlayer.HasValue)
        {
            return;
        }

        Multiplayer.Main.StartCoroutine(QueueForFixedUpdate(remotePlayer.Value, movement));
    }

    private IEnumerator QueueForFixedUpdate(RemotePlayer player, PlayerMovement movement)
    {
        yield return Yielders.WaitForFixedUpdate;
        player.UpdatePosition(movement.Position.ToUnity(),
                              movement.Velocity.ToUnity(),
                              movement.BodyRotation.ToUnity(),
                              movement.AimingRotation.ToUnity());
    }
}
