using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor;

public class FlashlightMetadataExtractor : EntityMetadataExtractor<FlashLight, FlashlightMetadata>
{
    public override FlashlightMetadata Extract(FlashLight entity)
    {
        ToggleLights lights = entity.RequireComponent<ToggleLights>();

        return new(lights.lightsActive);
    }
}
