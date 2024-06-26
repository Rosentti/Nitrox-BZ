﻿using NitroxClient_Subnautica.Communication.Abstract;
using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxClient_Subnautica.Unity.Helper;
using NitroxModel_Subnautica.Packets;
using UnityEngine;

namespace NitroxClient_Subnautica.Communication.Packets.Processors
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
