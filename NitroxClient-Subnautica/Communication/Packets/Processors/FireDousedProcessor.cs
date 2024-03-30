using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxClient_Subnautica.Unity.Helper;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient_Subnautica.Communication.Packets.Processors
{
    public class FireDousedProcessor : ClientPacketProcessor<FireDoused>
    {
        private readonly IPacketSender packetSender;

        public FireDousedProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        /// <summary>
        /// Finds and executes <see cref="Fire.Douse(float)"/>. If the fire is extinguished, it will pass a large float to trigger the private
        /// <see cref="Fire.Extinguish()"/> method.
        /// </summary>
        public override void Process(FireDoused packet)
        {
            GameObject fireGameObject = NitroxEntity.RequireObjectFrom(packet.Id);

            using (PacketSuppressor<FireDoused>.Suppress())
            {
                fireGameObject.RequireComponent<Fire>().Douse(packet.DouseAmount);
            }
        }
    }
}
