using UnityEngine;

namespace NitroxClient_BelowZero.GameLogic.Simulation
{
    public class PropulsionGrab : LockRequestContext
    {
        public PropulsionCannon Cannon { get; }
        public GameObject GrabbedObject { get; }

        public PropulsionGrab(PropulsionCannon Cannon, GameObject GrabbedObject)
        {
            this.Cannon = Cannon;
            this.GrabbedObject = GrabbedObject;
        }
    }
}
