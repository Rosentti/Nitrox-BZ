using System;
using NitroxModel.DataStructures.GameLogic;
using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.Containers
{
    class NoOpContainerAddItemPostProcessor : ContainerAddItemPostProcessor
    {
        public override Type[] ApplicableComponents { get; } = Type.EmptyTypes;

        public override void process(GameObject item, ItemData itemData)
        {
            // No-Op!
        }

    }
}
