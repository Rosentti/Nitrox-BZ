using System;
using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxModel.Packets;
using NitroxModel_Subnautica.DataStructures;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

public class KnownTechEntryProcessorAdd : ClientPacketProcessor<KnownTechEntryAdd>
{
    private readonly IPacketSender packetSender;

    public KnownTechEntryProcessorAdd(IPacketSender packetSender)
    {
        this.packetSender = packetSender;
    }

    public override void Process(KnownTechEntryAdd packet)
    {
        using (PacketSuppressor<KnownTechEntryAdd>.Suppress())
        {
            switch (packet.Category)
            {
                case KnownTechEntryAdd.EntryCategory.KNOWN:
                    KnownTech.Add(packet.TechType.ToUnity(), packet.Verbose);
                    break;
                case KnownTechEntryAdd.EntryCategory.ANALYZED:
                    KnownTech.Analyze(packet.TechType.ToUnity(), packet.Verbose);
                    break;
                default:
                    string categoryName = Enum.GetName(typeof(KnownTechEntryAdd.EntryCategory), packet.Category);
                    Log.Error($"Received an unknown category type for {nameof(KnownTechEntryAdd)} packet: {categoryName}");
                    break;
            }
        }
    }
}
