﻿using NitroxModel_BelowZero.Packets;
using NitroxServer.Communication.Packets.Processors.Abstract;
using NitroxServer.GameLogic;

namespace NitroxServer_BelowZero.Communication.Packets.Processors
{
    class CyclopsFireCreatedProcessor : AuthenticatedPacketProcessor<CyclopsFireCreated>
    {
        private readonly PlayerManager playerManager;

        public CyclopsFireCreatedProcessor(PlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public override void Process(CyclopsFireCreated packet, NitroxServer.Player simulatingPlayer)
        {
            playerManager.SendPacketToOtherPlayers(packet, simulatingPlayer);
        }
    }
}
