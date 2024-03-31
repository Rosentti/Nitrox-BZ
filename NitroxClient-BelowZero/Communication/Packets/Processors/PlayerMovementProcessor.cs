using System.Collections;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel_BelowZero.DataStructures;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

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
