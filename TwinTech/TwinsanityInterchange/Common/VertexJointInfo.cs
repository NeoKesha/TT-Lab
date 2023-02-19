using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class VertexJointInfo : ITwinSerializable
    {
        public float Weight1;
        public float Weight2;
        public float Weight3;
        public int JointIndex1;
        public int JointIndex2;
        public int JointIndex3;
        public bool Connection;

        public Int32 GetLength()
        {
            return Constants.SIZE_VECTOR4;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            throw new NotImplementedException();
        }

        public void Write(BinaryWriter writer)
        {
            Vector4 v = new()
            {
                X = Weight1,
                Y = Weight2,
                Z = Weight3
            };
            var xComp = v.GetBinaryX() | (uint)(JointIndex1 * 4);
            var yComp = v.GetBinaryY() | (uint)(JointIndex2 * 4);
            var zComp = v.GetBinaryZ() | (uint)(JointIndex3 * 4);
            v.SetBinaryX(xComp);
            v.SetBinaryY(yComp);
            v.SetBinaryZ(zComp);
            uint wComp = Connection ? 0 : (0x80U << 8);
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
            wComp |= (uint)weightCount;
            v.SetBinaryW(wComp);
            v.Write(writer);
        }
    }
}
