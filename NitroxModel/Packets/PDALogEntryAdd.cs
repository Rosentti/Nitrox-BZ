using System;

namespace NitroxModel.Packets;

[Serializable]
public class PDALogEntryAdd : Packet
{
    public string Key { get; }
    public float Timestamp { get; }

    /// <summary>
    ///     (BZ ONLY) If this log entry being added should play a sound
    /// </summary>
    public bool PlaySound { get; }

    public PDALogEntryAdd(string key, float timestamp, bool playSound)
    {
        Key = key;
        Timestamp = timestamp;
        PlaySound = playSound;
    }
}
