using System.Collections.Generic;
using System.Linq;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Unity;
using NitroxModel_BelowZero.DataStructures;
using NitroxServer.GameLogic.Entities.Spawning;
using NitroxServer.UnityStubs;

namespace NitroxServer_BelowZero.GameLogic.Entities.Spawning
{
    public class BelowZeroEntitySpawnPointFactory : EntitySpawnPointFactory
    {
        private readonly Dictionary<string, EntitySpawnPoint> spawnPointsByUid = new Dictionary<string, EntitySpawnPoint>();

        public override List<EntitySpawnPoint> From(AbsoluteEntityCell absoluteEntityCell, NitroxTransform transform, GameObject gameObject)
        {
            List<EntitySpawnPoint> spawnPoints = new List<EntitySpawnPoint>();
            EntitySlotsPlaceholder entitySlotsPlaceholder = gameObject.GetComponent<EntitySlotsPlaceholder>();

            if (gameObject.CreateEmptyObject)
            {
                SerializedEntitySpawnPoint entitySpawnPoint = new(gameObject.SerializedComponents, gameObject.Layer, absoluteEntityCell, transform);

                HandleParenting(spawnPoints, entitySpawnPoint, gameObject);
                spawnPoints.Add(entitySpawnPoint);
            }
            else if (!ReferenceEquals(entitySlotsPlaceholder, null))
            {
                foreach (EntitySlotData entitySlotData in entitySlotsPlaceholder.slotsData)
                {
                    List<EntitySlot.Type> slotTypes = SlotsHelper.ConvertSlotTypes(entitySlotData.allowedTypes);
                    List<string> stringSlotTypes = slotTypes.Select(s => s.ToString()).ToList();
                    EntitySpawnPoint entitySpawnPoint = new(absoluteEntityCell,
                                             entitySlotData.localPosition.ToDto(),
                                             entitySlotData.localRotation.ToDto(),
                                             stringSlotTypes,
                                             entitySlotData.density,
                                             entitySlotData.biomeType.ToString());


                    HandleParenting(spawnPoints, entitySpawnPoint, gameObject);
                }
            }
            else
            {
                EntitySpawnPoint entitySpawnPoint = new(absoluteEntityCell, transform.LocalPosition, transform.LocalRotation, transform.LocalScale, gameObject.ClassId);

                HandleParenting(spawnPoints, entitySpawnPoint, gameObject);
            }

            return spawnPoints;
        }

        private void HandleParenting(List<EntitySpawnPoint> spawnPoints, EntitySpawnPoint entitySpawnPoint, GameObject gameObject)
        {
            if (gameObject.Parent != null && spawnPointsByUid.TryGetValue(gameObject.Parent, out EntitySpawnPoint parent))
            {
                entitySpawnPoint.Parent = parent;
                parent.Children.Add(entitySpawnPoint);
            }

            spawnPointsByUid[gameObject.Id] = entitySpawnPoint;

            if (gameObject.Parent == null)
            {
                spawnPoints.Add(entitySpawnPoint);
            }
        }
    }
}
