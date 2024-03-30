using System.Collections;
using NitroxClient_Subnautica.GameLogic.Helper;
using NitroxClient_Subnautica.GameLogic.Spawning.Abstract;
using NitroxClient_Subnautica.GameLogic.Spawning.WorldEntities;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxClient_Subnautica.Unity.Helper;
using NitroxModel.DataStructures.GameLogic.Entities;
using NitroxModel.DataStructures.Util;
using NitroxModel_Subnautica.DataStructures;
using UnityEngine;

namespace NitroxClient_Subnautica.GameLogic.Spawning;

public class InstalledModuleEntitySpawner : SyncEntitySpawner<InstalledModuleEntity>
{
    protected override IEnumerator SpawnAsync(InstalledModuleEntity entity, TaskResult<Optional<GameObject>> result)
    {
        if (!CanSpawn(entity, out GameObject parentObject, out Equipment equipment, out string errorLog))
        {
            Log.Info(errorLog);
            result.Set(Optional.Empty);
            yield break;
        }

        TaskResult<GameObject> gameObjectResult = new();
        yield return DefaultWorldEntitySpawner.CreateGameObject(entity.TechType.ToUnity(), entity.ClassId, entity.Id, gameObjectResult);
        GameObject gameObject = gameObjectResult.Get();

        SetupObject(entity, gameObject, parentObject, equipment);

        result.Set(Optional.Of(gameObject));
    }

    protected override bool SpawnSync(InstalledModuleEntity entity, TaskResult<Optional<GameObject>> result)
    {
        if (!DefaultWorldEntitySpawner.TryGetCachedPrefab(out GameObject prefab, entity.TechType.ToUnity(), entity.ClassId))
        {
            return false;
        }
        if (!CanSpawn(entity, out GameObject parentObject, out Equipment equipment, out string errorLog))
        {
            Log.Error(errorLog);
            return true;
        }

        GameObject gameObject = GameObjectHelper.SpawnFromPrefab(prefab, entity.Id);

        SetupObject(entity, gameObject, parentObject, equipment);

        result.Set(gameObject);
        return true;
    }

    protected override bool SpawnsOwnChildren(InstalledModuleEntity entity) => true;

    private bool CanSpawn(InstalledModuleEntity entity, out GameObject parentObject, out Equipment equipment, out string errorLog)
    {
        if (!NitroxEntity.TryGetObjectFrom(entity.ParentId, out parentObject))
        {
            equipment = null;
            errorLog = $"Unable to find inventory container with id {entity.Id} for {entity}";
            return false;
        }
        // The game considers modules as vehicle equipment.  Get the container and install it into the required slot.
        Optional<Equipment> opEquipment = EquipmentHelper.FindEquipmentComponent(parentObject);
        if (!opEquipment.HasValue)
        {
            equipment = null;
            errorLog = $"Unable to find equipment container inside {parentObject}";
            return false;
        }

        equipment = opEquipment.Value;
        errorLog = null;
        return true;
    }

    private void SetupObject(InstalledModuleEntity entity, GameObject gameObject, GameObject parentObject, Equipment equipment)
    {
        Pickupable pickupable = gameObject.RequireComponent<Pickupable>();
        pickupable.Initialize();

        InventoryItem inventoryItem = new(pickupable)
        {
            container = equipment
        };
        inventoryItem.item.Reparent(equipment.tr);

        equipment.equipment[entity.Slot] = inventoryItem;

        equipment.UpdateCount(pickupable.GetTechType(), true);
        Equipment.SendEquipmentEvent(pickupable, 0, parentObject, entity.Slot);
        equipment.NotifyEquip(entity.Slot, inventoryItem);
    }
}
