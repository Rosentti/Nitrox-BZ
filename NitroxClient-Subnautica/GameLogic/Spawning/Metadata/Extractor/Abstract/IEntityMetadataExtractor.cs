using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using NitroxModel.DataStructures.Util;

namespace NitroxClient_Subnautica.GameLogic.Spawning.Metadata.Extractor.Abstract;

public interface IEntityMetadataExtractor
{
    public Optional<EntityMetadata> From(object o);
}
