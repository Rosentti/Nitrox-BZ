using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.MonoBehaviours.Discord;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class DiscordRequestIPProcessor : ClientPacketProcessor<DiscordRequestIP>
{
    public override void Process(DiscordRequestIP packet)
    {
        DiscordClient.UpdateIpPort(packet.IpPort);
    }
}
