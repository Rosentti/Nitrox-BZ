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

            TaskResult<GameObject> prefabResult = new();
            yield return DefaultWorldEntitySpawner.RequestPrefabByFilename("WorldEntities/Tools/LifepodDrop.prefab", prefabResult);

            GameObject gameObject = GameObjectHelper.InstantiateWithId(prefabResult.Get(), entity.Id);

            // If we don't add our own RespawnPoint, the player will teleport under the lifepod
            // This hack works well enough. (We could fix the RespawnPoint to the correct positions instead, but that's a lot of effort when this does the job just fine)
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
