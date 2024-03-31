using NitroxModel.DataStructures.GameLogic;
using NitroxModel.DataStructures.Unity;
using NitroxModel_BelowZero.DataStructures.GameLogic;
using NitroxModel_BelowZero.DataStructures.Surrogates;
using NitroxServer.Serialization;
using ProtoBufNet.Meta;
using UnityEngine;

namespace NitroxServer_BelowZero.Serialization
{
    class BelowZeroServerProtoBufSerializer : ServerProtoBufSerializer
    {
        public BelowZeroServerProtoBufSerializer(params string[] assemblies) : base(assemblies)
        {
            RegisterHardCodedTypes();
        }

        // Register here all hard coded types, that come from NitroxModel-BelowZero or NitroxServer-BelowZero
        private void RegisterHardCodedTypes()
        {
            Model.Add(typeof(Light), true);
            Model.Add(typeof(BoxCollider), true);
            Model.Add(typeof(SphereCollider), true);
            Model.Add(typeof(MeshCollider), true);
            Model.Add(typeof(Vector3), false).SetSurrogate(typeof(Vector3Surrogate));
            Model.Add(typeof(NitroxVector3), false).SetSurrogate(typeof(Vector3Surrogate));
            Model.Add(typeof(Quaternion), false).SetSurrogate(typeof(QuaternionSurrogate));
            Model.Add(typeof(NitroxQuaternion), false).SetSurrogate(typeof(QuaternionSurrogate));
            Model.Add(typeof(Transform), false).SetSurrogate(typeof(NitroxTransform));
            Model.Add(typeof(GameObject), false).SetSurrogate(typeof(NitroxServer.UnityStubs.GameObject));

            MetaType movementData = Model.Add(typeof(VehicleMovementData), false);
            movementData.AddSubType(100, typeof(BasicVehicleMovementData));
            movementData.AddSubType(200, typeof(ExosuitMovementData));
        }
    }
}
