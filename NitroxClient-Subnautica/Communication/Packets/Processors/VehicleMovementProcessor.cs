using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors
{
    public class VehicleMovementProcessor : ClientPacketProcessor<VehicleMovement>
    {
        private readonly PlayerManager remotePlayerManager;
        private readonly Vehicles vehicles;

        public VehicleMovementProcessor(PlayerManager remotePlayerManager, Vehicles vehicles)
        {
            this.remotePlayerManager = remotePlayerManager;
            this.vehicles = vehicles;
        }

        public override void Process(VehicleMovement vehicleMovement)
        {
            VehicleMovementData vehicleModel = vehicleMovement.VehicleMovementData;
            Optional<RemotePlayer> player = remotePlayerManager.Find(vehicleMovement.PlayerId);
            vehicles.UpdateVehiclePosition(vehicleModel, player);
        }
    }
}
