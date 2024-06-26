﻿using System;
using NitroxModel.DataStructures;
using NitroxModel.Packets;

namespace NitroxModel_BelowZero.Packets;

[Serializable]
public class RocketLaunch : Packet
{
    public NitroxId RocketId { get; }

    public RocketLaunch(NitroxId rocketId)
    {
        RocketId = rocketId;
    }
}
