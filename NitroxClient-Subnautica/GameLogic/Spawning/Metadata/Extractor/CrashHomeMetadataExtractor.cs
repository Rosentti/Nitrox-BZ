using NitroxClient_Subnautica.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;

namespace NitroxClient_Subnautica.GameLogic.Spawning.Metadata.Extractor;

public class CrashHomeMetadataExtractor : EntityMetadataExtractor<CrashHome, CrashHomeMetadata>
{
    public override CrashHomeMetadata Extract(CrashHome crashHome)
    {
        return new(crashHome.spawnTime);
    }
}
