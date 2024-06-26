﻿using System;
using System.Collections.Generic;
using NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.Abstract;
using NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.ColorSwap.Strategy;
using NitroxClient_BelowZero.Unity.Helper;
using NitroxModel_BelowZero.DataStructures;
using UnityEngine;
using static NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.PlayerEquipmentConstants;

//TODO: How to do this in BZ?
// namespace NitroxClient_BelowZero.GameLogic.PlayerLogic.PlayerModel.ColorSwap
// {
//     public class ScubaTankColorSwapManager : IColorSwapManager
//     {
//         public Action<ColorSwapAsyncOperation> CreateColorSwapTask(INitroxPlayer nitroxPlayer)
//         {
//             GameObject playerModel = nitroxPlayer.PlayerModel;
//             Color playerColor = nitroxPlayer.PlayerSettings.PlayerColor.ToUnity();
//             IColorSwapStrategy colorSwapStrategy = new HueSwapper(playerColor);

//             SkinnedMeshRenderer scubaTankRenderer = playerModel.GetRenderer(SCUBA_TANK_GAME_OBJECT_NAME);
//             scubaTankRenderer.material.ApplyClonedTexture();

//             Color[] texturePixels = scubaTankRenderer.material.GetMainTexturePixels();

//             return operation =>
//             {
//                 HsvSwapper scubaTankFilter = new HsvSwapper(colorSwapStrategy);
//                 scubaTankFilter.SetHueRange(0f, 30f);

//                 scubaTankFilter.SwapColors(texturePixels);

//                 operation.UpdateIndex(SCUBA_TANK_INDEX_KEY, texturePixels);
//             };
//         }

//         public void ApplyPlayerColor(Dictionary<string, Color[]> pixelIndex, INitroxPlayer nitroxPlayer)
//         {
//             Color[] scubaTankPixelIndexes = pixelIndex[SCUBA_TANK_INDEX_KEY];

//             GameObject playerModel = nitroxPlayer.PlayerModel;
//             SkinnedMeshRenderer scubaTankRenderer = playerModel.GetRenderer(SCUBA_TANK_GAME_OBJECT_NAME);

//             scubaTankRenderer.material.UpdateMainTextureColors(scubaTankPixelIndexes);
//             scubaTankRenderer.material.SetTexture("_MainTex", scubaTankRenderer.material.mainTexture);
//             scubaTankRenderer.material.SetTexture("_SpecTex", scubaTankRenderer.material.mainTexture);
//         }
//     }
// }
