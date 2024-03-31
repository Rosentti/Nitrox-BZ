using System.Collections.ObjectModel;
using NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.Equipment.Abstract;
using UnityEngine;

//TODO: How to do this in BZ?
// namespace NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.Equipment
// {
//     public class ReinforcedSuitVisibilityHandler : IEquipmentVisibilityHandler
//     {
//         private readonly GameObject gloves;
//         private readonly GameObject suit;

//         public ReinforcedSuitVisibilityHandler(GameObject playerModel)
//         {
//             gloves = playerModel.transform.Find(PlayerEquipmentConstants.REINFORCED_GLOVES_GAME_OBJECT_NAME).gameObject;
//             suit = playerModel.transform.Find(PlayerEquipmentConstants.REINFORCED_SUIT_GAME_OBJECT_NAME).gameObject;
//         }
//         public void UpdateEquipmentVisibility(ReadOnlyCollection<TechType> currentEquipment)
//         {
//             bool glovesVisible = currentEquipment.Contains(TechType.ReinforcedGloves);
//             bool bodyVisible = currentEquipment.Contains(TechType.ReinforcedDiveSuit);

//             gloves.SetActive(glovesVisible);
//             suit.SetActive(bodyVisible);
//         }
//     }
// }
