using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Processor.Abstract;

public interface IEntityMetadataProcessor
{
    public abstract void ProcessMetadata(GameObject gameObject, EntityMetadata metadata);
}
