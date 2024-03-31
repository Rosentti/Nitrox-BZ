using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxModel;
using NitroxModel.Packets;
using NitroxModel_BelowZero.DataStructures;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class PDAScanFinishedProcessor : ClientPacketProcessor<PDAScanFinished>
{
    public override void Process(PDAScanFinished packet)
    {
        if (packet.Id != null)
        {
            StoryManager.ScanCompleted(packet.Id, packet.Destroy);
        }
        if (packet.WasAlreadyResearched)
        {
            return;
        }
        TechType packetTechType = packet.TechType.ToUnity();
        if (packet.FullyResearched)
        {
            PDAScanner.partial.RemoveAllFast(packetTechType, static (item, techType) => item.techType == techType);
            PDAScanner.complete.Add(packetTechType);
            return;
        }
        if (PDAScanner.GetPartialEntryByKey(packetTechType, out PDAScanner.Entry entry))
        {
            entry.unlocked = packet.UnlockedAmount;
        }
        else
        {
            PDAScanner.Add(packetTechType, packet.UnlockedAmount);
        }
    }
}
