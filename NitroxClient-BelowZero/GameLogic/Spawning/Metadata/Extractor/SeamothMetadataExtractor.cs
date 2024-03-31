using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor;

public class SeamothMetadataExtractor : EntityMetadataExtractor<SeaMoth, SeamothMetadata>
{
    public override SeamothMetadata Extract(SeaMoth seamoth)
    {
        bool lightsOn = (seamoth.toggleLights) ? seamoth.toggleLights.GetLightsActive() : true;
        LiveMixin liveMixin = seamoth.liveMixin;

        return new(lightsOn, liveMixin.health, ColorNameControlMetadataExtractor.GetName(seamoth.colorNameControl), ColorNameControlMetadataExtractor.GetColors(seamoth.colorNameControl));
    }
}
