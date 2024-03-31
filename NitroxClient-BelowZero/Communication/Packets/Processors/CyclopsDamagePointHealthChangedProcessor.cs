using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel_BelowZero.Packets;
using UnityEngine;

namespace NitroxClient_BelowZero.Communication.Packets.Processors
{
    public class CyclopsDamagePointHealthChangedProcessor : ClientPacketProcessor<CyclopsDamagePointRepaired>
    {
        private readonly IPacketSender packetSender;

        public CyclopsDamagePointHealthChangedProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public override void Process(CyclopsDamagePointRepaired packet)
        {
            GameObject gameObject = NitroxEntity.RequireObjectFrom(packet.Id);
            SubRoot cyclops = gameObject.RequireComponent<SubRoot>();

            using (PacketSuppressor<CyclopsDamage>.Suppress())
            using (PacketSuppressor<CyclopsDamagePointRepaired>.Suppress())
            {
                cyclops.damageManager.damagePoints[packet.DamagePointIndex].liveMixin.AddHealth(packet.RepairAmount);
            }
        }
    }
}
