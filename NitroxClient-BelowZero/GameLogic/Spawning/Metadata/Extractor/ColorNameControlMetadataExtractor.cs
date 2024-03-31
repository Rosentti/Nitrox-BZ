using System.Linq;
using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor.Abstract;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.DataStructures.GameLogic.Entities.Metadata;
using NitroxModel.DataStructures.Unity;
using NitroxModel_BelowZero.DataStructures;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Extractor;

public class ColorNameControlMetadataExtractor : EntityMetadataExtractor<ColorNameControl, ColorNameControlMetadata>
{
    public override ColorNameControlMetadata Extract(ColorNameControl nameControl)
    {
        return new(GetName(nameControl), GetColors(nameControl));
    }

    public static string GetName(ColorNameControl nameControl)
    {
        return nameControl.AliveOrNull()?.namePlate.AliveOrNull()?.namePlateText;
    }

    public static NitroxVector3[] GetColors(ColorNameControl nameControl)
    {
        return (nameControl.AliveOrNull() as ICustomizeable).GetColors().Select(color => color.ToDto()).ToArray();
    }
}
