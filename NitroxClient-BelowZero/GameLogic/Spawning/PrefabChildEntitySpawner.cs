using System.Collections;
using System.Linq;
using NitroxClient_BelowZero.GameLogic.Spawning.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.DataStructures.GameLogic.Entities;
using NitroxModel.DataStructures.Util;
using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.Spawning;

public class PrefabChildEntitySpawner : SyncEntitySpawner<PrefabChildEntity>
{
    // When we encounter a PrefabChildEntity, we need to assign the id to a prefab with the same class id and index.
    protected override IEnumerator SpawnAsync(PrefabChildEntity entity, TaskResult<Optional<GameObject>> result)
    {
        SpawnSync(entity, result);
        yield break;
    }

    protected override bool SpawnSync(PrefabChildEntity entity, TaskResult<Optional<GameObject>> result)
    {
        GameObject parent = NitroxEntity.RequireObjectFrom(entity.ParentId);
        PrefabIdentifier prefab = parent.GetAllComponentsInChildren<PrefabIdentifier>()
                                        .Where(prefab => prefab.classId == entity.ClassId)
                                        .ElementAtOrDefault(entity.ComponentIndex);

        if (prefab)
        {
            NitroxEntity.SetNewId(prefab.gameObject, entity.Id);
            result.Set(Optional.OfNullable(prefab.gameObject));
        }
        else
        {
            Log.Error($"Unable to find prefab for: {entity}");
            result.Set(Optional.Empty);
        }
        return true;
    }

    protected override bool SpawnsOwnChildren(PrefabChildEntity entity) => false;
}
