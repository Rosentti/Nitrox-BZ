using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
{
    public class MultiplayerSessionReservationProcessor : ClientPacketProcessor<MultiplayerSessionReservation>
    {
        private readonly IMultiplayerSession multiplayerSession;

        public MultiplayerSessionReservationProcessor(IMultiplayerSession multiplayerSession)
        {
            this.multiplayerSession = multiplayerSession;
        }

        public override void Process(MultiplayerSessionReservation packet)
        {
            multiplayerSession.ProcessReservationResponsePacket(packet);
        }
    }
}
