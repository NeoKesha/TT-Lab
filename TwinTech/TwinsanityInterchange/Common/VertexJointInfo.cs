using System;
using System.Diagnostics;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class VertexJointInfo : ITwinSerializable
    {
        public Single Weight1;
        public Single Weight2;
        public Single Weight3;
        public Int32 JointIndex1;
        public Int32 JointIndex2;
        public Int32 JointIndex3;
        public Boolean Connection;

        public Int32 GetLength()
        {
            return Constants.SIZE_VECTOR4;
        }

        public Vector4 GetVector4()
        {
            Debug.Assert(Math.Abs(Weight1 + Weight2 + Weight3 - 1.0f) < 0.000001f, "Weights must sum up to 1");
            Vector4 v = new()
            {
                X = Weight1,
                Y = Weight2,
                Z = Weight3
            };
            var xComp = v.GetBinaryX() | (UInt32)(JointIndex1 * 4);
            var yComp = v.GetBinaryY() | (UInt32)(JointIndex2 * 4);
            var zComp = v.GetBinaryZ() | (UInt32)(JointIndex3 * 4);
            v.SetBinaryX(xComp);
            v.SetBinaryY(yComp);
            v.SetBinaryZ(zComp);
            UInt32 wComp = Connection ? 0 : (0x80U << 8);
            var weightCount = 1;
            var totalWeight = Weight1 + Weight2;
            if (totalWeight < 1)
            {
                weightCount++;
            }
            if (totalWeight + Weight3 <= 1)
            {
                weightCount++;
            }
            wComp |= (UInt32)weightCount;
            v.SetBinaryW(wComp);
            return v;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            throw new NotImplementedException();
        }

        public void Write(BinaryWriter writer)
        {
            var v = GetVector4();
            v.Write(writer);
        }
    }
}
