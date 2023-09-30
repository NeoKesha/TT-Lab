using System;
using System.Collections.Generic;
using Twinsanity.TwinsanityInterchange.Common;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code
{
    public interface ITwinOGI : ITwinItem
    {
        enum HeaderInfo
        {
            JOINT_AMOUNT = 0,
            EXIT_POINT_AMOUNT = 1,
            REACT_JOINT_AMOUNT = 2,
            RIGID_MODELS_AMOUNT = 5,
            HAS_SKIN = 6,
            HAS_BLEND_SKIN = 7,
            COLLISIONS_AMOUNT = 8,
        }

        List<TwinJoint> Joints { get; set; }
        List<TwinExitPoint> ExitPoints { get; set; }
        Vector4[] BoundingBox { get; set; }
        List<Byte> JointIndices { get; set; }
        List<UInt32> RigidModelIds { get; set; }
        List<Matrix4> SkinInverseBindMatrices { get; set; }
        UInt32 SkinID { get; set; }
        UInt32 BlendSkinID { get; set; }
        List<TwinBoundingBoxBuilder> Collisions { get; set; }
        List<Byte> CollisionJointIndices { get; set; }
    }
}
