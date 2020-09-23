using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    class CameraSub1C0C : ITwinSerializable
    {
        public Byte[] UnkData { get; set; }
        public CameraSub1C0C()
        {
            UnkData = new byte[4];
        }
        public int GetLength()
        {
            return 4;
        }

        public void Read(BinaryReader reader, int length)
        {
            UnkData = reader.ReadBytes(4);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(UnkData);
        }
    }
}
