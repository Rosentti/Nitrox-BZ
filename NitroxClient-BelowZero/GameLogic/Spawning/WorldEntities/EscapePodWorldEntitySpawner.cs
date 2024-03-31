using System;
using System.Collections;
using NitroxClient_BelowZero.GameLogic.Spawning.Metadata;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.MonoBehaviours.CinematicController;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.DataStructures.GameLogic.Entities;
using NitroxModel.DataStructures.Util;
using NitroxModel_BelowZero.DataStructures;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UWE;

namespace NitroxClient_BelowZero.GameLogic.Spawning.WorldEntities
{
    public class EscapePodWorldEntitySpawner : IWorldEntitySpawner
    {
        private EntityMetadataManager entityMetadataManager;

        public EscapePodWorldEntitySpawner(EntityMetadataManager entityMetadataManager)
        {
            this.entityMetadataManager = entityMetadataManager;
        }

        public IEnumerator SpawnAsync(WorldEntity entity, Optional<GameObject> parent, EntityCell cellRoot, TaskResult<Optional<GameObject>> result)
        {
            if (entity is not EscapePodWorldEntity escapePodEntity)
            {
                result.Set(Optional.Empty);
                Log.Error($"Received incorrect entity type: {entity.GetType()}");
                yield break;
            }

            //TODO: get with filename 'WorldEntities/Tools/LifepodDrop.prefab' instead
            string classid = "";
            foreach (var file in PrefabDatabase.prefabFiles)
            {
                if (file.Value == "WorldEntities/Tools/LifepodDrop.prefab") {
                    classid = file.Key;
                }
            }

            if (string.IsNullOrEmpty(classid)) {
                Log.Error("Did not find LifepodDrop.prefab");
                yield break;
            }

            if (!DefaultWorldEntitySpawner.TryGetCachedPrefab(out GameObject prefab, classId: classid))
            {
                TaskResult<GameObject> prefabResult = new();
                yield return DefaultWorldEntitySpawner.RequestPrefab(classid, prefabResult);
                if (!prefabResult.Get())
                {
                    Log.Error($"Couldn't find a prefab for {nameof(WorldEntity)} of ClassId '{classid}'");
                    yield break;
                }
                prefab = prefabResult.Get();
            }

            GameObject gameObject = GameObjectHelper.InstantiateWithId(prefab, entity.Id);

            // The RespawnPoint is missing, although it should exist. Just add it here since it's logic is very simple.
            gameObject.AddComponent<RespawnPoint>();

            entityMetadataManager.ApplyMetadata(gameObject, escapePodEntity.Metadata);

            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                Log.Error("Escape pod did not have a rigid body!");
            }

            gameObject.transform.position = escapePodEntity.Transform.Position.ToUnity();

            FixStartMethods(gameObject);

            // Start() isn't executed for the EscapePod, why? Idk, maybe because it's a scene...
            MultiplayerCinematicReference reference = gameObject.AddComponent<MultiplayerCinematicReference>();
            foreach (PlayerCinematicController controller in gameObject.GetComponentsInChildren<PlayerCinematicController>())
            {
                reference.AddController(controller);
            }
            
            result.Set(Optional.Of(gameObject));
        }

        /// <summary>
        /// Start() isn't executed for the EscapePod and children (Why? Idk, maybe because it's a scene...) so we call the components here where we have patches in Start.
        /// </summary>
        private static void FixStartMethods(GameObject escapePod)
        {
            foreach (FMOD_CustomEmitter customEmitter in escapePod.GetComponentsInChildren<FMOD_CustomEmitter>())
            {
                customEmitter.Start();
            }

            foreach (FMOD_StudioEventEmitter studioEventEmitter in escapePod.GetComponentsInChildren<FMOD_StudioEventEmitter>())
            {
                studioEventEmitter.Start();
            }
        }

        public bool SpawnsOwnChildren()
        {
            return false;
        }
    }

}
