using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Processor.Abstract;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Processor;

public class PrecursorTeleporterMetadataProcessor : EntityMetadataProcessor<PrecursorTeleporterMetadata>
{
    public override void ProcessMetadata(GameObject gameObject, PrecursorTeleporterMetadata metadata)
    {
        Log.Debug($"Received precursor teleporter metadata change for {gameObject.name} with data of {metadata}");

        PrecursorTeleporter precursorTeleporter = gameObject.GetComponent<PrecursorTeleporter>();
        if (precursorTeleporter)
        {
            precursorTeleporter.ToggleDoor(metadata.IsOpen);
        }
    }
}
