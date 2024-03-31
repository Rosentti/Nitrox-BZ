using System.Collections.ObjectModel;

namespace NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.Equipment.Abstract
{
    public interface IEquipmentVisibilityHandler
    {
        void UpdateEquipmentVisibility(ReadOnlyCollection<TechType> currentEquipment);
    }
}
