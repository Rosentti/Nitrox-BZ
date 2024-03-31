using HarmonyLib;

namespace NitroxPatcher_BelowZero.Patches
{
    public interface INitroxPatch
    {
        void Patch(Harmony instance);
        void Restore(Harmony instance);
    }
}
