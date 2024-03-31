using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor;

public class EatableMetadataExtractor : EntityMetadataExtractor<Eatable, EatableMetadata>
{
    public override EatableMetadata Extract(Eatable eatable)
    {
        if (eatable.decomposes)
        {
            return new(eatable.timeDecayStart);
        }
        return null;
    }
}
