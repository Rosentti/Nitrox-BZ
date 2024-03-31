using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel_BelowZero.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
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
