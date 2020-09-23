using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.CameraSubtypes
{
    public class CameraSub1C0D : ITwinSerializable
    {
        public UInt32 UnkInt { get; set; }
        public Single UnkFloat1 { get; set; }
        public Single UnkFloat2 { get; set; }
        public Vector4[] BoundingBox { get; private set; }
        public Single UnkFloat3 { get; set; }
        public Single UnkFloat4 { get; set; }
        public CameraSub1C0D()
        {
            BoundingBox = new Vector4[2];
            for (int i = 0; i < BoundingBox.Length; ++i)
            {
                BoundingBox[i] = new Vector4();
            }
        }
        public int GetLength()
        {
            return 20 + BoundingBox.Length * Constants.SIZE_VECTOR4;
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
            UnkFloat3 = reader.ReadSingle();
            UnkFloat4 = reader.ReadSingle();
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
            writer.Write(UnkFloat3);
            writer.Write(UnkFloat4);
        }
    }
}
