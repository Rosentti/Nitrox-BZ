using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel_BelowZero.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
{
    public class CyclopsFireCreatedProcessor : ClientPacketProcessor<CyclopsFireCreated>
    {
        private readonly IPacketSender packetSender;
        private readonly Fires fires;

        public CyclopsFireCreatedProcessor(IPacketSender packetSender, Fires fires)
        {
            this.packetSender = packetSender;
            this.fires = fires;
        }

        public override void Process(CyclopsFireCreated packet)
        {
            fires.Create(packet.FireCreatedData);
        }
    }
}
