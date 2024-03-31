using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.MonoBehaviours.Gui.Modals;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
{
    public class UserKickedProcessor : ClientPacketProcessor<PlayerKicked>
    {
        private readonly IMultiplayerSession session;

        public UserKickedProcessor(IMultiplayerSession session)
        {
            this.session = session;
        }

        public override void Process(PlayerKicked packet)
        {
            string message = Language.main.Get("Nitrox_PlayerKicked");

            if (!string.IsNullOrEmpty(packet.Reason))
            {
                message += $"\n {packet.Reason}";
            }

            session.Disconnect();
            Modal.Get<KickedModal>()?.Show(message);
        }
    }
}
