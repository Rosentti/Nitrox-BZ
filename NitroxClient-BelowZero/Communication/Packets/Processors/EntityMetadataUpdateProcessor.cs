using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic.Spawning.Metadata;
using NitroxClient_BelowZero.GameLogic.Spawning.Metadata.Processor.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.DataStructures.Util;
using NitroxModel.Helper;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class EntityMetadataUpdateProcessor : ClientPacketProcessor<EntityMetadataUpdate>
{
    private readonly EntityMetadataManager entityMetadataManager;

    public EntityMetadataUpdateProcessor(EntityMetadataManager entityMetadataManager)
    {
        this.entityMetadataManager = entityMetadataManager;
    }

    public override void Process(EntityMetadataUpdate update)
    {
        if (!NitroxEntity.TryGetObjectFrom(update.Id, out GameObject gameObject))
        {
            entityMetadataManager.RegisterNewerMetadata(update.Id, update.NewValue);
            return;
        }

        Optional<IEntityMetadataProcessor> metadataProcessor = entityMetadataManager.FromMetaData(update.NewValue);
        Validate.IsTrue(metadataProcessor.HasValue, $"No processor found for EntityMetadata of type {update.NewValue.GetType()}");

        metadataProcessor.Value.ProcessMetadata(gameObject, update.NewValue);
    }
}
