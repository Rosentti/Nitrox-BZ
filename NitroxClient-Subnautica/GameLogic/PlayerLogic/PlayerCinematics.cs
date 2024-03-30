using NitroxClient_Subnautica.Communication.Abstract;
using NitroxModel.DataStructures;
using NitroxModel.Packets;

namespace NitroxClient_Subnautica.GameLogic.PlayerLogic;

public class PlayerCinematics
{
    private readonly IPacketSender packetSender;

    public PlayerCinematics(IPacketSender packetSender)
    {
        this.packetSender = packetSender;
    }

    public void StartCinematicMode(ushort playerId, NitroxId controllerID, int controllerNameHash, string key)
    {
        packetSender.Send(new PlayerCinematicControllerCall(playerId, controllerID, controllerNameHash, key, true));
    }

    public void EndCinematicMode(ushort playerId, NitroxId controllerID, int controllerNameHash, string key)
    {
        packetSender.Send(new PlayerCinematicControllerCall(playerId, controllerID, controllerNameHash, key, false));
    }
}
