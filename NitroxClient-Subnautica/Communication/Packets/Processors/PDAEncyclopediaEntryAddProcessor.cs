using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxModel.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

public class PDAEncyclopediaEntryAddProcessor : ClientPacketProcessor<PDAEncyclopediaEntryAdd>
{
    private readonly IPacketSender packetSender;

    public PDAEncyclopediaEntryAddProcessor(IPacketSender packetSender)
    {
        this.packetSender = packetSender;
    }

    public override void Process(PDAEncyclopediaEntryAdd packet)
    {
        using (PacketSuppressor<PDAEncyclopediaEntryAdd>.Suppress())
        {
            PDAEncyclopedia.Add(packet.Key, packet.Verbose);
        }
    }
}
