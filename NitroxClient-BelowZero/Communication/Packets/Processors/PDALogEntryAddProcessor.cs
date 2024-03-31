using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
{
    public class PDALogEntryAddProcessor : ClientPacketProcessor<PDALogEntryAdd>
    {
        private readonly IPacketSender packetSender;

        public PDALogEntryAddProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public override void Process(PDALogEntryAdd packet)
        {
            using (PacketSuppressor<PDALogEntryAdd>.Suppress())
            {
                PDALog.Add(packet.Key, packet.PlaySound);
            }
        }
    }
}
