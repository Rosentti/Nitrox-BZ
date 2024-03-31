using System;
using System.Collections.Generic;
using NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.ColorSwap;
using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.Abstract
{
    public interface IColorSwapManager
    {
        Action<ColorSwapAsyncOperation> CreateColorSwapTask(INitroxPlayer nitroxPlayer);
        void ApplyPlayerColor(Dictionary<string, Color[]> pixelIndex, INitroxPlayer nitroxPlayer);
    }
}
