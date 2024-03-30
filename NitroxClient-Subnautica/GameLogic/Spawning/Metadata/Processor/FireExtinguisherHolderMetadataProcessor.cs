using NitroxClient_Subnautica.GameLogic.Spawning.Metadata.Processor.Abstract;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using UnityEngine;

namespace NitroxClient_Subnautica.GameLogic.Spawning.Metadata.Processor;

public class FireExtinguisherHolderMetadataProcessor : EntityMetadataProcessor<FireExtinguisherHolderMetadata>
{
    public override void ProcessMetadata(GameObject gameObject, FireExtinguisherHolderMetadata metadata)
    {
        FireExtinguisherHolder holder = gameObject.GetComponent<FireExtinguisherHolder>();

        if (holder)
        {
            holder.fuel = metadata.Fuel;
            holder.hasTank = metadata.HasExtinguisher;
            holder.tankObject.SetActive(metadata.HasExtinguisher);
        }
        else
        {
            Log.Error($"Could not find FireExtinguisherHolder on {gameObject.name}");
        }
    }
}
