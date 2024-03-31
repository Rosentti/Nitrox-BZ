using System;
using System.Collections;
using System.Collections.Generic;
using NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.Abstract;
using NitroxClient_BelowZero.GameLogic.Spawning.Abstract;
using NitroxClient_BelowZero.GameLogic.Spawning.Metadata;
using NitroxClient_BelowZero.GameLogic.Spawning.WorldEntities;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.DataStructures.GameLogic.Entities;
using NitroxModel.DataStructures.Util;
using NitroxModel.Helper;
using NitroxModel_BelowZero.DataStructures;
using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.Spawning;

public class WorldEntitySpawner : SyncEntitySpawner<WorldEntity>
{
    private readonly WorldEntitySpawnerResolver worldEntitySpawnResolver;
    private readonly Dictionary<Int3, BatchCells> batchCellsById;

    public WorldEntitySpawner(EntityMetadataManager entityMetadataManager, PlayerManager playerManager, ILocalNitroxPlayer localPlayer, Entities entities, SimulationOwnership simulationOwnership)
    {
        worldEntitySpawnResolver = new WorldEntitySpawnerResolver(entityMetadataManager, playerManager, localPlayer, entities, simulationOwnership);

        if (NitroxEnvironment.IsNormal)
        {
            batchCellsById = (Dictionary<Int3, BatchCells>)LargeWorldStreamer.main.cellManager.batch2cells;
        }
    }

    protected override IEnumerator SpawnAsync(WorldEntity entity, TaskResult<Optional<GameObject>> result)
    {
        EntityCell cellRoot = EnsureCell(entity);
        if (cellRoot == null)
        {
            // Error logging is done in EnsureCell
            return null;
        }

        Optional<GameObject> parent = (entity.ParentId != null) ? NitroxEntity.GetObjectFrom(entity.ParentId) : Optional.Empty;

        IWorldEntitySpawner entitySpawner = worldEntitySpawnResolver.ResolveEntitySpawner(entity);

        if (entitySpawner is IWorldEntitySyncSpawner syncSpawner &&
            syncSpawner.SpawnSync(entity, parent, cellRoot, result))
        {
            return null;
        }

        return entitySpawner.SpawnAsync(entity, parent, cellRoot, result);
    }

    protected override bool SpawnsOwnChildren(WorldEntity entity)
    {
        IWorldEntitySpawner entitySpawner = worldEntitySpawnResolver.ResolveEntitySpawner(entity);
        return entitySpawner.SpawnsOwnChildren();
    }

    protected override bool SpawnSync(WorldEntity entity, TaskResult<Optional<GameObject>> result)
    {
        EntityCell cellRoot = EnsureCell(entity);
        if (cellRoot == null)
        {
            // Error logging is done in EnsureCell
            return true;
        }

        Optional<GameObject> parent = (entity.ParentId != null) ? NitroxEntity.GetObjectFrom(entity.ParentId) : Optional.Empty;
        IWorldEntitySpawner entitySpawner = worldEntitySpawnResolver.ResolveEntitySpawner(entity);

        return entitySpawner is IWorldEntitySyncSpawner syncSpawner && syncSpawner.SpawnSync(entity, parent, cellRoot, result);
    }

    public EntityCell EnsureCell(WorldEntity entity)
    {
        EntityCell entityCell;

        Int3 batchId = entity.AbsoluteEntityCell.BatchId.ToUnity();
        Int3 cellId = entity.AbsoluteEntityCell.CellId.ToUnity();

        if (!batchCellsById.TryGetValue(batchId, out BatchCells batchCells))
        {
            batchCells = LargeWorldStreamer.main.cellManager.InitializeBatchCells(batchId);
        }

        try
        {
            entityCell = batchCells.EnsureCell(cellId, entity.Level);
        }
        catch (Exception)
        {
            // Error logging is done in BatchCells.EnsureCell
            return null;
        }

        entityCell.EnsureRoot();

        return entityCell;
    }
}
