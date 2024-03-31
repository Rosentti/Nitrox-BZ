using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.Abstract
{
    public interface IColorSwapStrategy
    {
        Color SwapColor(Color originalColor);
    }
}
