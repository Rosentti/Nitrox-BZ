using System.Linq;
using NitroxClient_Subnautica.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxClient_Subnautica.Unity.Helper;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using NitroxModel.DataStructures.Unity;
using NitroxModel_Subnautica.DataStructures;

namespace NitroxClient_Subnautica.GameLogic.Spawning.Metadata.Extractor;

public class SubNameInputMetadataExtractor : EntityMetadataExtractor<SubNameInput, SubNameInputMetadata>
{
    public override SubNameInputMetadata Extract(SubNameInput subNameInput)
    {
        SubName subName = subNameInput.target;
        return new(subNameInput.selectedColorIndex, GetName(subName), GetColors(subName));
    }

    public static string GetName(SubName subName)
    {
        return subName.AliveOrNull()?.hullName.AliveOrNull()?.text;
    }

    public static NitroxVector3[] GetColors(SubName subName)
    {
        return subName.AliveOrNull()?.GetColors().Select(color => color.ToDto()).ToArray();
    }
}
