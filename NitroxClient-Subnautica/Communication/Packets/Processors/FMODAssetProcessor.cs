using NitroxClient_Subnautica.Communication.Packets.Processors.Abstract;
using NitroxClient_Subnautica.MonoBehaviours;
using NitroxModel_Subnautica.DataStructures;
using NitroxModel.GameLogic.FMOD;
using NitroxModel.Packets;

namespace NitroxClient_Subnautica.Communication.Packets.Processors;

public class FMODAssetProcessor : ClientPacketProcessor<FMODAssetPacket>
{
    private readonly FMODWhitelist fmodWhitelist;

    public FMODAssetProcessor(FMODWhitelist fmodWhitelist)
    {
        this.fmodWhitelist = fmodWhitelist;
    }

    public override void Process(FMODAssetPacket packet)
    {
        if (!fmodWhitelist.TryGetSoundData(packet.AssetPath, out SoundData soundData))
        {
            Log.ErrorOnce($"[{nameof(FMODAssetProcessor)}] Whitelist has no item for {packet.AssetPath}.");
            return;
        }

        FMODEmitterController.PlayEventOneShot(packet.AssetPath, soundData.Radius, packet.Position.ToUnity(), packet.Volume);
    }
}
