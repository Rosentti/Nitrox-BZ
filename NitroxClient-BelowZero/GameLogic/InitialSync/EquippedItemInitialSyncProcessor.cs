using System.Collections;
using System.Collections.Generic;
using NitroxClient_BelowZero.Communication;
using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.GameLogic.Helper;
using NitroxClient_BelowZero.GameLogic.InitialSync.Abstract;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Util;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.InitialSync;

public class EquippedItemInitialSyncProcessor : InitialSyncProcessor
{
    private readonly IPacketSender packetSender;

    public EquippedItemInitialSyncProcessor(IPacketSender packetSender)
    {
        this.packetSender = packetSender;

        AddDependency<PlayerInitialSyncProcessor>();
        AddDependency<RemotePlayerInitialSyncProcessor>();
        AddDependency<GlobalRootInitialSyncProcessor>();
    }

    public override IEnumerator Process(InitialPlayerSync packet, WaitScreen.ManualWaitItem waitScreenItem)
    {
        int totalEquippedItemsDone = 0;

        using (PacketSuppressor<EntitySpawnedByClient>.Suppress())
        {
            foreach (EquippedItemData equippedItem in packet.EquippedItems)
            {
                waitScreenItem.SetProgress(totalEquippedItemsDone, packet.EquippedItems.Count);

                GameObject gameObject = SerializationHelper.GetGameObject(equippedItem.SerializedData);
                NitroxEntity.SetNewId(gameObject, equippedItem.ItemId);

                Pickupable pickupable = gameObject.RequireComponent<Pickupable>();
                Optional<GameObject> opGameObject = NitroxEntity.GetObjectFrom(equippedItem.ContainerId);

                if (opGameObject.HasValue)
                {
                    GameObject owner = opGameObject.Value;

                    Optional<Equipment> opEquipment = EquipmentHelper.FindEquipmentComponent(owner);

                    if (opEquipment.HasValue)
                    {
                        Equipment equipment = opEquipment.Value;
                        InventoryItem inventoryItem = new(pickupable);
                        inventoryItem.container = equipment;
                        inventoryItem.item.Reparent(equipment.tr);

                        Dictionary<string, InventoryItem> itemsBySlot = equipment.equipment;
                        itemsBySlot[equippedItem.Slot] = inventoryItem;

                        equipment.UpdateCount(pickupable.GetTechType(), true);
                        Equipment.SendEquipmentEvent(pickupable, 0, owner, equippedItem.Slot);
                        equipment.NotifyEquip(equippedItem.Slot, inventoryItem);
                    }
                    else
                    {
                        Log.Info($"Could not find equipment type for {gameObject.name}");
                    }
                }
                else
                {
                    Log.Info($"Could not find Container for {gameObject.name}");
                }

                totalEquippedItemsDone++;
                yield return null;
            }
        }

        Log.Info($"Recieved initial sync with {totalEquippedItemsDone} pieces of equipped items");
    }
}
