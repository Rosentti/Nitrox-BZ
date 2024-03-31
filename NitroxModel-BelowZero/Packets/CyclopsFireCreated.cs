using System;
using NitroxModel.DataStructures;
using NitroxModel.Packets;
using NitroxModel_BelowZero.DataStructures.GameLogic;

namespace NitroxModel_BelowZero.Packets
{
    /// <summary>
    /// Triggered when a fire has been created in <see cref="SubFire.CreateFire(SubFire.RoomFire)"/>
    /// </summary>
    [Serializable]
    public class CyclopsFireCreated : Packet
    {
        public CyclopsFireData FireCreatedData { get; }

        public CyclopsFireCreated(NitroxId id, NitroxId cyclopsId, CyclopsRooms room, int nodeIndex)
        {
            FireCreatedData = new CyclopsFireData(id, cyclopsId, room, nodeIndex);
        }

        /// <remarks>Used for deserialization</remarks>
        public CyclopsFireCreated(CyclopsFireData fireCreatedData)
        {
            FireCreatedData = fireCreatedData;
        }

        public override string ToString()
        {
            return $"[CyclopsFireCreated - {FireCreatedData}]";
        }
    }
}
