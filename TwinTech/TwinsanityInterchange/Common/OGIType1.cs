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
    public class OGIType1 : ITwinSerializable
    {
        public UInt32[] UnkInt { get; private set; }
        public Vector4[] Vectors { get; private set; }
        public OGIType1()
        {
            UnkInt = new UInt32[5];
            Vectors = new Vector4[5];
            for (int i = 0; i < Vectors.Length; ++i)
            {
                Vectors[i] = new Vector4();
            }
        }
        public int GetLength()
        {
            return Constants.SIZE_OGI_TYPE1;
        }

        public void Read(BinaryReader reader, int length)
        {
            for (int i = 0; i < UnkInt.Length; ++i)
            {
                UnkInt[i] = reader.ReadUInt32();
            }
            for (int i = 0; i < Vectors.Length; ++i)
            {
                Vectors[i].Read(reader, Constants.SIZE_VECTOR4);
            }
        }

        public void Write(BinaryWriter writer)
        {
            for (int i = 0; i < UnkInt.Length; ++i)
            {
                writer.Write(UnkInt[i]);
            }
            for (int i = 0; i < Vectors.Length; ++i)
            {
                Vectors[i].Write(writer);
            }
        }
    }
}
