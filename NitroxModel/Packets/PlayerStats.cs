using System;
using NitroxModel.Networking;

namespace NitroxModel.Packets;

[Serializable]
public class PlayerStats : Packet
{
    public ushort PlayerId { get; set; }
    public float Oxygen { get; }
    public float MaxOxygen { get; }
    public float Health { get; }
    public float Food { get; }
    public float Water { get; }
    public float InfectionAmount { get; }
    public float Temperature { get; }

    public PlayerStats(ushort playerId, float oxygen, float maxOxygen, float health, float food, float water, float infectionAmount, float temperature)
    {
        PlayerId = playerId;
        Oxygen = oxygen;
        MaxOxygen = maxOxygen;
        Health = health;
        Food = food;
        Water = water;
        InfectionAmount = infectionAmount;
        Temperature = temperature;
        DeliveryMethod = NitroxDeliveryMethod.DeliveryMethod.UNRELIABLE_SEQUENCED;
        UdpChannel = UdpChannelId.PLAYER_STATS;
    }
}
