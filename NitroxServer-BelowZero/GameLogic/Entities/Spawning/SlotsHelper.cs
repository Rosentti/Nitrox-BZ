using System.Collections.Generic;

namespace NitroxServer_BelowZero.GameLogic.Entities.Spawning
{
    public static class SlotsHelper
    {
        private static Dictionary<EntitySlotData.EntitySlotType, EntitySlot.Type> typeMapping = new Dictionary<EntitySlotData.EntitySlotType, EntitySlot.Type>
            {
                { EntitySlotData.EntitySlotType.Small, EntitySlot.Type.Small },
                { EntitySlotData.EntitySlotType.Medium, EntitySlot.Type.Medium },
                { EntitySlotData.EntitySlotType.Large, EntitySlot.Type.Large },
                { EntitySlotData.EntitySlotType.Tall, EntitySlot.Type.Tall },
                { EntitySlotData.EntitySlotType.Creature, EntitySlot.Type.Creature }
            };

        public static List<EntitySlot.Type> ConvertSlotTypes(EntitySlotData.EntitySlotType entitySlotType)
        {
            List<EntitySlot.Type> slotsTypes = new List<EntitySlot.Type>();

            foreach (KeyValuePair<EntitySlotData.EntitySlotType, EntitySlot.Type> mapping in typeMapping)
            {
                EntitySlotData.EntitySlotType slotType = mapping.Key;
                EntitySlot.Type type = mapping.Value;

                if ((entitySlotType & slotType) == slotType)
                {
                    slotsTypes.Add(type);
                }
            }

            return slotsTypes;
        }
    }
}
