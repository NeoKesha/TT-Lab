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
    public class OGIType2 : ITwinSerializeable
    {
        public UInt32 UnkInt1 { get; set; }
        public UInt32 UnkInt2 { get; set; }
        public Matrix4 Matrix { get; private set; }
        public OGIType2()
        {
            Matrix = new Matrix4();
        }
        public int GetLength()
        {
            return Constants.SIZE_OGI_TYPE2;
        }

        public void Read(BinaryReader reader, int length)
        {
            UnkInt1 = reader.ReadUInt32();
            UnkInt2 = reader.ReadUInt32();

            Matrix.Read(reader, Constants.SIZE_MATRIX4);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(UnkInt1);
            writer.Write(UnkInt2);
            Matrix.Write(writer);
        }
    }
}
