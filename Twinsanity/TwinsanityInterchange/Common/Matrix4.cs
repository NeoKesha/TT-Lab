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
    public class Matrix4 : ITwinSerializeable
    {
        public Vector4 V1 { get; set; }
        public Vector4 V2 { get; set; }
        public Vector4 V3 { get; set; }
        public Vector4 V4 { get; set; }
        public Matrix4()
        {
            V1 = new Vector4();
            V2 = new Vector4();
            V3 = new Vector4();
            V4 = new Vector4();
        }
        public int GetLength()
        {
            return V1.GetLength() + V2.GetLength() + V3.GetLength() + V4.GetLength();
        }

        public void Read(BinaryReader reader, int length)
        {
            V1.Read(reader, Constants.SIZE_VECTOR4);
            V2.Read(reader, Constants.SIZE_VECTOR4);
            V3.Read(reader, Constants.SIZE_VECTOR4);
            V4.Read(reader, Constants.SIZE_VECTOR4);
        }

        public void Write(BinaryWriter writer)
        {
            V1.Write(writer);
            V2.Write(writer);
            V3.Write(writer);
            V4.Write(writer);
        }
    }
}
