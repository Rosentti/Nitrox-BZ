using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor;

public class ConstructorMetadataExtractor : EntityMetadataExtractor<Constructor, ConstructorMetadata>
{
    public override ConstructorMetadata Extract(Constructor entity)
    {
        return new(entity.deployed);
    }
}
