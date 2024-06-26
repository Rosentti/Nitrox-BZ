using System.Collections;
using NitroxClient_BelowZero.GameLogic.Spawning.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.DataStructures.GameLogic.Entities.Bases;
using NitroxModel.DataStructures.Util;
using NitroxModel_BelowZero.DataStructures;
using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.Spawning.Bases;

public class BaseLeakEntitySpawner : SyncEntitySpawner<BaseLeakEntity>
{
    private readonly LiveMixinManager liveMixinManager;

    public BaseLeakEntitySpawner(LiveMixinManager liveMixinManager)
    {
        this.liveMixinManager = liveMixinManager;
    }

    protected override IEnumerator SpawnAsync(BaseLeakEntity entity, TaskResult<Optional<GameObject>> result)
    {
        SpawnSync(entity, result);
        yield break;
    }

    protected override bool SpawnsOwnChildren(BaseLeakEntity entity) => false;

    protected override bool SpawnSync(BaseLeakEntity entity, TaskResult<Optional<GameObject>> result)
    {
        if (!NitroxEntity.TryGetComponentFrom(entity.ParentId, out BaseHullStrength baseHullStrength))
        {
            Log.Error($"[{nameof(BaseLeakEntitySpawner)}] Couldn't find a {nameof(BaseHullStrength)} from id {entity.ParentId}");
            return true;
        }

        BaseLeakManager baseLeakManager = baseHullStrength.gameObject.EnsureComponent<BaseLeakManager>();
        baseLeakManager.EnsureLeak(entity.RelativeCell.ToUnity(), entity.Id, entity.Health);
        return true;
    }
}
