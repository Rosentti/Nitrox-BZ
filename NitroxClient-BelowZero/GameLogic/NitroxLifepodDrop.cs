using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace NitroxClient_BelowZero.GameLogic {
    public class NitroxLifepodDrop {
        private static GameObject _main;
        public static GameObject main {
            get {
                if (_main == null) {
                    _main = Addressables.LoadAssetAsync<GameObject>("Assets/AddressableResources/WorldEntities/Tools/LifepodDrop.prefab").WaitForCompletion();
                }

                return _main;
            }
        }
    }
}