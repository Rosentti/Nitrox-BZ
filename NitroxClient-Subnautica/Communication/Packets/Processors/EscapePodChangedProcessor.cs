﻿using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient_Subnautica.Communication.Packets.Processors
{
    public class EscapePodChangedProcessor : ClientPacketProcessor<EscapePodChanged>
    {
        private readonly PlayerManager remotePlayerManager;

        public EscapePodChangedProcessor(PlayerManager remotePlayerManager)
        {
            this.remotePlayerManager = remotePlayerManager;
        }

        public override void Process(EscapePodChanged packet)
        {
            Optional<RemotePlayer> remotePlayer = remotePlayerManager.Find(packet.PlayerId);

            if (remotePlayer.HasValue)
            {
                EscapePod escapePod = null;

                if (packet.EscapePodId.HasValue)
                {
                    GameObject sub = NitroxEntity.RequireObjectFrom(packet.EscapePodId.Value);
                    escapePod = sub.GetComponent<EscapePod>();
                }

                remotePlayer.Value.SetEscapePod(escapePod);
            }
        }
    }
}


