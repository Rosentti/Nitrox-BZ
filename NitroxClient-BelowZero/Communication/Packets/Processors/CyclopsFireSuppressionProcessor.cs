using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel_BelowZero.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
{
    public class CyclopsFireSuppressionProcessor : ClientPacketProcessor<CyclopsFireSuppression>
    {
        private readonly IPacketSender packetSender;
        private readonly Cyclops cyclops;

        public CyclopsFireSuppressionProcessor(IPacketSender packetSender, Cyclops cyclops)
        {
            this.packetSender = packetSender;
            this.cyclops = cyclops;
        }

        public override void Process(CyclopsFireSuppression fireSuppressionPacket)
        {
            cyclops.StartFireSuppression(fireSuppressionPacket.Id);
        }
    }
}
