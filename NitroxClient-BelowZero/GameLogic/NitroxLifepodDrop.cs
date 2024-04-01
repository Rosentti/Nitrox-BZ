using System.Diagnostics;
using NitroxClient_BelowZero.GameLogic.Spawning;
using NitroxClient_BelowZero.Unity.Helper;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UWE;

namespace NitroxClient_BelowZero.GameLogic {
    public class NitroxLifepodDrop {
        public static GameObject main;
        public static LifepodDrop drop => main.RequireComponent<LifepodDrop>();
        public static RespawnPoint respawnPoint => main.RequireComponentInChildren<RespawnPoint>(true);
    }
}