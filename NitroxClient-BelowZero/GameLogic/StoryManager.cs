using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel.DataStructures;
using NitroxModel.DataStructures.GameLogic;
using Story;
using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic;

public static class StoryManager
{
    public static void ScanCompleted(NitroxId entityId, bool destroy)
    {
        PDAScanner.cachedProgress.Remove(entityId.ToString());
        if (NitroxEntity.TryGetObjectFrom(entityId, out GameObject scanObject))
        {
            // Copy the SendMessage from PDAScanner.Scan() but we don't care about the EntryData
            scanObject.SendMessage("OnScanned", null, SendMessageOptions.DontRequireReceiver);
            if (!destroy)
            {
                PDAScanner.fragments.Add(entityId.ToString(), 1f);
            }
            else
            {
                PDAScanner.fragments.Remove(entityId.ToString());
                GameObject.Destroy(scanObject);
            }
        }
    }
}
