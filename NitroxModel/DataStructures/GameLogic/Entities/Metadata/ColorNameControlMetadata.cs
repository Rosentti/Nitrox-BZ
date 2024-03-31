using System;
using System.Runtime.Serialization;
using BinaryPack.Attributes;
using NitroxModel.DataStructures.Unity;

namespace NitroxModel.DataStructures.GameLogic.Entities.Metadata;

[Serializable]
[DataContract]
public class ColorNameControlMetadata : NamedColoredMetadata
{
    [IgnoreConstructor]
    protected ColorNameControlMetadata()
    {
        // Constructor for serialization. Has to be "protected" for json serialization.
    }

    public ColorNameControlMetadata(string name, NitroxVector3[] colors) : base(name, colors)
    {

    }

    public override string ToString()
    {
        return $"[{nameof(ColorNameControlMetadata)} {base.ToString()}]";
    }
}
