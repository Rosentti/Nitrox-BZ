using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic.HUD;
using NitroxClient_BelowZero.MonoBehaviours.Gui.HUD;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class PlayerStatsProcessor : ClientPacketProcessor<PlayerStats>
{
    private readonly PlayerVitalsManager vitalsManager;

    public PlayerStatsProcessor(PlayerVitalsManager vitalsManager)
    {
        this.vitalsManager = vitalsManager;
    }

    public override void Process(PlayerStats playerStats)
    {
        if (vitalsManager.TryFindForPlayer(playerStats.PlayerId, out RemotePlayerVitals vitals))
        {
            vitals.SetOxygen(playerStats.Oxygen, playerStats.MaxOxygen);
            vitals.SetHealth(playerStats.Health);
            vitals.SetFood(playerStats.Food);
            vitals.SetWater(playerStats.Water);
            vitals.SetTemperature(playerStats.Temperature);
        }
    }
}
