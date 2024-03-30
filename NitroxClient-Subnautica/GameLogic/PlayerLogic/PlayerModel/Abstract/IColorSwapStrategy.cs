using UnityEngine;

namespace NitroxClient_Subnautica.GameLogic.PlayerLogic.PlayerModel.Abstract
{
    public interface IColorSwapStrategy
    {
        Color SwapColor(Color originalColor);
    }
}
