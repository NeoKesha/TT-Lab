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
    public class CameraSub1C03 : ITwinSerializable
    {
        public UInt32 UnkInt { get; set; }
        public Single UnkFloat1 { get; set; }
        public Single UnkFloat2 { get; set; }
        public Vector4[] BoundingBox { get; private set; }
        public CameraSub1C03()
        {
            BoundingBox = new Vector4[2];
            for (int i = 0; i < BoundingBox.Length; ++i)
            {
                BoundingBox[i] = new Vector4();
            }
        }
        public int GetLength()
        {
            return 12 + BoundingBox.Length * Constants.SIZE_VECTOR4;
        }

        public void Read(BinaryReader reader, int length)
        {
            UnkInt = reader.ReadUInt32();
            UnkFloat1 = reader.ReadSingle();
            UnkFloat2 = reader.ReadSingle();
            for (int i = 0; i < BoundingBox.Length; ++i)
            {
                BoundingBox[i].Read(reader, Constants.SIZE_VECTOR4);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(UnkInt);
            writer.Write(UnkFloat1);
            writer.Write(UnkFloat2);
            for (int i = 0; i < BoundingBox.Length; ++i)
            {
                BoundingBox[i].Write(writer);
            }
        }
    }
}
