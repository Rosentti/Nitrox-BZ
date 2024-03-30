using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.GameLogic;
using NitroxModel.DataStructures;
using NitroxModel.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

public class SimulationOwnershipChangeProcessor : ClientPacketProcessor<SimulationOwnershipChange>
{
    private readonly SimulationOwnership simulationOwnershipManager;

    public SimulationOwnershipChangeProcessor(SimulationOwnership simulationOwnershipManager)
    {
        this.simulationOwnershipManager = simulationOwnershipManager;
    }

    public override void Process(SimulationOwnershipChange simulationOwnershipChange)
    {
        foreach (SimulatedEntity simulatedEntity in simulationOwnershipChange.Entities)
        {
            simulationOwnershipManager.TreatSimulatedEntity(simulatedEntity);
        }
    }
}

