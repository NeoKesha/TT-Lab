using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.CameraSubtypes
{
    public class CameraSub1C0F : ITwinSerializable
    {
        public Byte[] UnkData1 { get; set; }
        public Byte[] UnkData2 { get; set; }
        public CameraSub1C0F()
        {
            UnkData1 = new byte[80];
            UnkData2 = new byte[80];
        }
        public int GetLength()
        {
            return 160;
        }

        public void Read(BinaryReader reader, int length)
        {
            UnkData1 = reader.ReadBytes(80);
            UnkData2 = reader.ReadBytes(80);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(UnkData1);
            writer.Write(UnkData2);
        }
    }
}
