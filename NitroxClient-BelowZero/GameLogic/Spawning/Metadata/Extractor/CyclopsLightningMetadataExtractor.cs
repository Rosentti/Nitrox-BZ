using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor;

public class CyclopsLightningMetadataExtractor : EntityMetadataExtractor<CyclopsLightingPanel, CyclopsLightingMetadata>
{
    public override CyclopsLightingMetadata Extract(CyclopsLightingPanel lighting)
    {
        return new(lighting.floodlightsOn, lighting.lightingOn);
    }
}
