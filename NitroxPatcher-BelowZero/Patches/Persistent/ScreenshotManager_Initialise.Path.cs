﻿using System.IO;
using System.Reflection;
using NitroxModel.Helper;

namespace NitroxPatcher_BelowZero.Patches.Persistent
{
    public partial class ScreenshotManager_Initialise : NitroxPatch, IPersistentPatch
    {
        private static readonly MethodInfo TARGET_METHOD = Reflect.Method(() => ScreenshotManager.Initialize(default(string)));

        public static void Prefix(ScreenshotManager __instance, ref string _savePath)
        {
            _savePath = Path.GetFullPath(NitroxUser.LauncherPath ?? ".");
        }
    }
}
