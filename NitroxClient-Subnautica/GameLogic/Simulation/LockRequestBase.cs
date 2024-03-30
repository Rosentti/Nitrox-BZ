using NitroxModel.DataStructures;

namespace NitroxClient_Subnautica.GameLogic.Simulation
{
    public abstract class LockRequestBase
    {
        public NitroxId Id { get; }
        public SimulationLockType LockType { get; }

        public abstract void LockRequestComplete(NitroxId id, bool lockAquired);

        public LockRequestBase(NitroxId Id, SimulationLockType LockType) : base()
        {
            this.Id = Id;
            this.LockType = LockType;
        }
    }
}
