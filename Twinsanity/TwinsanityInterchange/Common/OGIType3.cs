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
    public class OGIType3 : ITwinSerializeable
    {
        public Byte[] Data { get; private set; }
        public Byte[] Blob { get; set; }
        public OGIType3()
        {
            Data = new byte[0x16];
            Blob = Array.Empty<byte>();
        }
        public int GetLength()
        {
            return 4 + Data.Length + Blob.Length;
        }

        public void Read(BinaryReader reader, int length)
        {
            reader.Read(Data, 0, Data.Length);
            Int32 blobSize = reader.ReadInt32();
            Blob = reader.ReadBytes(blobSize);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Data);
            writer.Write(Blob.Length);
            writer.Write(Blob);
        }
    }
}
