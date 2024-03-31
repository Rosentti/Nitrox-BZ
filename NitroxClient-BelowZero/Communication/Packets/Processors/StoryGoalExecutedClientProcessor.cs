using NitroxClient_BelowZero.Communication.Abstract;
using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxModel;
using NitroxModel.Packets;
using NitroxModel_BelowZero.DataStructures;
using Story;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class StoryGoalExecutedClientProcessor : ClientPacketProcessor<StoryGoalExecuted>
{
    private readonly IPacketSender packetSender;

    public StoryGoalExecutedClientProcessor(IPacketSender packetSender)
    {
        this.packetSender = packetSender;
    }

    public override void Process(StoryGoalExecuted packet)
    {
        StoryGoalScheduler.main.schedule.RemoveAllFast(packet.Key, static (goal, packetGoalKey) => goal.goalKey == packetGoalKey);

        using (PacketSuppressor<StoryGoalExecuted>.Suppress())
        using (PacketSuppressor<PDALogEntryAdd>.Suppress())
        using (PacketSuppressor<KnownTechEntryAdd>.Suppress()) // StoryGoalManager => OnGoalUnlockTracker => UnlockBlueprintData => KnownTech.Add
        using (PacketSuppressor<PDAEncyclopediaEntryAdd>.Suppress())
        {
            StoryGoal.Execute(packet.Key, packet.Type.ToUnity());
        }
    }
}
