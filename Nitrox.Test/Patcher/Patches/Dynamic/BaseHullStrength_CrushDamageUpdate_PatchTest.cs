using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using HarmonyLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NitroxTest.Patcher;

namespace NitroxPatcher_Subnautica.Patches.Dynamic;

[TestClass]
public class BaseHullStrength_CrushDamageUpdate_PatchTest
{
    [TestMethod]
    public void Sanity()
    {
        IEnumerable<CodeInstruction> originalIl = PatchTestHelper.GetInstructionsFromMethod(BaseHullStrength_CrushDamageUpdate_Patch.TARGET_METHOD);
        IEnumerable<CodeInstruction> transformedIl = BaseHullStrength_CrushDamageUpdate_Patch.Transpiler(originalIl);
        transformedIl.Count().Should().Be(originalIl.Count() + 3);
    }
}
