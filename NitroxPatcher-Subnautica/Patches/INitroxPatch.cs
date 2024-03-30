using HarmonyLib;

namespace NitroxPatcher_Subnautica.Patches
{
    public interface INitroxPatch
    {
        void Patch(Harmony instance);
        void Restore(Harmony instance);
    }
}
