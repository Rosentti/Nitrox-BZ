using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxClient_Subnautica.Unity.Helper;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient_Subnautica.Communication.Packets.Processors
{
    public class OpenableStateChangedProcessor : ClientPacketProcessor<OpenableStateChanged>
    {
        private readonly IPacketSender packetSender;

        public OpenableStateChangedProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public override void Process(OpenableStateChanged packet)
        {
            GameObject gameObject = NitroxEntity.RequireObjectFrom(packet.Id);
            Openable openable = gameObject.RequireComponent<Openable>();

            using (PacketSuppressor<OpenableStateChanged>.Suppress())
            {
                openable.PlayOpenAnimation(packet.IsOpen, packet.Duration);
            }
        }
    }
}
