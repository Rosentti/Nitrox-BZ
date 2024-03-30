using System.Collections;
using NitroxModel.DataStructures.GameLogic.Entities;
using NitroxModel.DataStructures.Util;
using UnityEngine;

namespace NitroxClient_Subnautica.GameLogic.Spawning.WorldEntities
{
    /**
     * Allows us to create custom entity spawners for different world entity types.
     */
    public interface IWorldEntitySpawner
    {
        IEnumerator SpawnAsync(WorldEntity entity, Optional<GameObject> parent, EntityCell cellRoot, TaskResult<Optional<GameObject>> result);

        bool SpawnsOwnChildren();
    }
}
