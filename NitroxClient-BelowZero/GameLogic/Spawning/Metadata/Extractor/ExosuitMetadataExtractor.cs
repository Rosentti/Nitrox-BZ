using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor;

public class ExosuitMetadataExtractor : EntityMetadataExtractor<Exosuit, ExosuitMetadata>
{
    public override ExosuitMetadata Extract(Exosuit exosuit)
    {
        LiveMixin liveMixin = exosuit.liveMixin;
        var colorNameControl = exosuit.colorNameControl;

        return new(liveMixin.health, ColorNameControlMetadataExtractor.GetName(colorNameControl), ColorNameControlMetadataExtractor.GetColors(colorNameControl));
    }
}
