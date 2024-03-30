using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxModel.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors
{
    public class MultiplayerSessionPolicyProcessor : ClientPacketProcessor<MultiplayerSessionPolicy>
    {
        private readonly IMultiplayerSession multiplayerSession;

        public MultiplayerSessionPolicyProcessor(IMultiplayerSession multiplayerSession)
        {
            this.multiplayerSession = multiplayerSession;
        }

        public override void Process(MultiplayerSessionPolicy packet)
        {
            Log.Info("Processing session policy information.");
            multiplayerSession.ProcessSessionPolicy(packet);
        }
    }
}
