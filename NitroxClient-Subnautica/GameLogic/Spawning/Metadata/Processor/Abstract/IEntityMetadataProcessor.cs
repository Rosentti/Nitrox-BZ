using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using UnityEngine;

namespace NitroxClient_Subnautica.GameLogic.Spawning.Metadata.Processor.Abstract;

public interface IEntityMetadataProcessor
{
    public abstract void ProcessMetadata(GameObject gameObject, EntityMetadata metadata);
}
