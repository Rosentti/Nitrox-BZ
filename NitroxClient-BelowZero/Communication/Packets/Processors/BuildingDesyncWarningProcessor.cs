using System.Collections.Generic;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic.Bases;
using NitroxClient_BelowZero.GameLogic.Settings;
using NitroxModel.DataStructures;
using NitroxModel.Packets;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class BuildingDesyncWarningProcessor : ClientPacketProcessor<BuildingDesyncWarning>
{
    public override void Process(BuildingDesyncWarning packet)
    {        
        if (!BuildingHandler.Main)
        {
            return;
        }

        foreach (KeyValuePair<NitroxId, int> operation in packet.Operations)
        {
            OperationTracker tracker = BuildingHandler.Main.EnsureTracker(operation.Key);
            tracker.LastOperationId = operation.Value;
            tracker.FailedOperations++;
        }

        if (NitroxPrefs.SafeBuildingLog.Value)
        {
            Log.InGame(Language.main.Get("Nitrox_BuildingDesyncDetected"));
        }
    }
}
