using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel_Subnautica.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors
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
