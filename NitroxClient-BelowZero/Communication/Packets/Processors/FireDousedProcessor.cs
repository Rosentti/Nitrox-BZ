using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
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
