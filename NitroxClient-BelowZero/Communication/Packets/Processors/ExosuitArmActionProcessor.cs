﻿using NitroxClient_BelowZero.Communication.Packets.Processors.Abstract;
using NitroxClient_BelowZero.GameLogic;
using NitroxClient_BelowZero.MonoBehaviours;
using NitroxModel_BelowZero.DataStructures;
using NitroxModel_BelowZero.Packets;
using UnityEngine;

namespace NitroxClient_BelowZero.Communication.Packets.Processors;

public class ExosuitArmActionProcessor : ClientPacketProcessor<ExosuitArmActionPacket>
{
    public override void Process(ExosuitArmActionPacket packet)
    {
        if (!NitroxEntity.TryGetObjectFrom(packet.ArmId, out GameObject gameObject))
        {
            Log.Error("Could not find exosuit arm");
            return;
        }

        switch (packet.TechType)
        {
            case TechType.ExosuitClawArmModule:
                ExosuitModuleEvent.UseClaw(gameObject.GetComponent<ExosuitClawArm>(), packet.ArmAction);
                break;
            case TechType.ExosuitDrillArmModule:
                ExosuitModuleEvent.UseDrill(gameObject.GetComponent<ExosuitDrillArm>(), packet.ArmAction);
                break;
            case TechType.ExosuitGrapplingArmModule:
                ExosuitModuleEvent.UseGrappling(gameObject.GetComponent<ExosuitGrapplingArm>(), packet.ArmAction, packet.OpVector?.ToUnity());
                break;
            case TechType.ExosuitTorpedoArmModule:
                ExosuitModuleEvent.UseTorpedo(gameObject.GetComponent<ExosuitTorpedoArm>(), packet.ArmAction, packet.OpVector?.ToUnity(), packet.OpRotation?.ToUnity());
                break;
            default:
                Log.Error($"Got an arm tech that is not handled: {packet.TechType} with action: {packet.ArmAction} for id {packet.ArmId}");
                break;
        }
    }
}
