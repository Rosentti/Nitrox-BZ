using System.Linq;
using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using NitroxModel.DataStructures.Unity;
using NitroxModel_BelowZero.DataStructures;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor;

public class SubNameInputMetadataExtractor : EntityMetadataExtractor<SubNameInput, SubNameInputMetadata>
{
    public override SubNameInputMetadata Extract(SubNameInput subNameInput)
    {
        SubName subName = (SubName)subNameInput.target;
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
