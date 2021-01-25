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
    public class OGIType3 : ITwinSerializable
    {
        public UInt16[] Data { get; set; }
        public Byte[] Blob { get; set; }
        public OGIType3()
        {
            Data = new UInt16[11];
            Blob = Array.Empty<byte>();
        }
        public int GetLength()
        {
            return 4 + Data.Length * 2 + Blob.Length;
        }

        public void Read(BinaryReader reader, int length)
        {
            Data = new UInt16[11];
            for (var i = 0; i < 11; ++i)
            {
                Data[i] = reader.ReadUInt16();
            }
            Int32 blobSize = reader.ReadInt32();
            Blob = reader.ReadBytes(blobSize);
        }

        public void Write(BinaryWriter writer)
        {
            for (var i = 0; i < 11; ++i)
            {
                writer.Write(Data[i]);
            }
            writer.Write(Blob.Length);
            writer.Write(Blob);
        }
    }
}
