﻿using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;
using NitroxModel_Subnautica.DataStructures;
using UnityEngine;

namespace NitroxClient_Subnautica.Communication.Packets.Processors
{
    class ItemPositionProcessor : ClientPacketProcessor<ItemPosition>
    {
        private const float ITEM_TRANSFORM_SMOOTH_PERIOD = 0.25f;

        public override void Process(ItemPosition drop)
        {
            Optional<GameObject> opItem = NitroxEntity.GetObjectFrom(drop.Id);

            if (opItem.HasValue)
            {
                MovementHelper.MoveRotateGameObject(opItem.Value, drop.Position.ToUnity(), drop.Rotation.ToUnity(), ITEM_TRANSFORM_SMOOTH_PERIOD);
            }
        }
    }
}
