using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel_Subnautica.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors
{
    class CyclopsDecoyLaunchProcessor : ClientPacketProcessor<CyclopsDecoyLaunch>
    {
        private readonly IPacketSender packetSender;
        private readonly Cyclops cyclops;

        public CyclopsDecoyLaunchProcessor(IPacketSender packetSender, Cyclops cyclops)
        {
            this.packetSender = packetSender;
            this.cyclops = cyclops;
        }

        public override void Process(CyclopsDecoyLaunch decoyLaunchPacket)
        {
            cyclops.LaunchDecoy(decoyLaunchPacket.Id);
        }
    }
}
