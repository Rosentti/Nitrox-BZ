using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

public class MutePlayerProcessor : ClientPacketProcessor<MutePlayer>
{
    private readonly PlayerManager playerManager;

    public delegate void PlayerMuted(ushort playerId, bool muted);
    public PlayerMuted OnPlayerMuted;

    public MutePlayerProcessor(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }

    public override void Process(MutePlayer packet)
    {
        // We only need to notice if that's another player than local player
        Optional<RemotePlayer> player = playerManager.Find(packet.PlayerId);
        if (player.HasValue)
        {
            player.Value.PlayerContext.IsMuted = packet.Muted;
        }
        OnPlayerMuted(packet.PlayerId, packet.Muted);
    }
}
