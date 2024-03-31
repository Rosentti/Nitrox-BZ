using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class GameModeChangedProcessor : ClientPacketProcessor<GameModeChanged>
{
    private readonly LocalPlayer localPlayer;
    private readonly PlayerManager playerManager;

    public GameModeChangedProcessor(LocalPlayer localPlayer, PlayerManager playerManager)
    {
        this.localPlayer = localPlayer;
        this.playerManager = playerManager;
    }

    public override void Process(GameModeChanged packet)
    {
        if (packet.AllPlayers || packet.PlayerId == localPlayer.PlayerId)
        {
            //TODO: support advanced options
            GameModeManager.SetGameOptions((GameModePresetId)(int)packet.GameMode, null);
        }
        if (packet.AllPlayers)
        {
            foreach (RemotePlayer remotePlayer in playerManager.GetAll())
            {
                remotePlayer.SetGameMode(packet.GameMode);
            }
        }
        else if (playerManager.TryFind(packet.PlayerId, out RemotePlayer remotePlayer))
        {
            remotePlayer.SetGameMode(packet.GameMode);
        }
    }
}
