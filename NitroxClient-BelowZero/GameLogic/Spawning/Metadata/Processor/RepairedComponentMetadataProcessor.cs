using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Processor.Abstract;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Processor;

public class RepairedComponentMetadataProcessor : EntityMetadataProcessor<RepairedComponentMetadata>
{
    public override void ProcessMetadata(GameObject gameObject, RepairedComponentMetadata metadata)
    {
        Radio radio = gameObject.GetComponent<Radio>();

        if (radio)
        {
            radio.liveMixin.health = radio.liveMixin.maxHealth;
        }
    }
}
