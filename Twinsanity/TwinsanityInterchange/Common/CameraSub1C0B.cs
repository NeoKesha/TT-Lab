using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace Twinsanity.TwinsanityInterchange.Common
{
    class CameraSub1C0B : ITwinSerializable
    {
        public UInt32 UnkInt { get; set; }
        public Single UnkFloat1 { get; set; }
        public Single UnkFloat2 { get; set; }
        public Vector4 UnkVec { get; private set; }
        public Single UnkFloat3 { get; set; }
        public Byte UnkByte { get; set; }
        public CameraSub1C0B()
        {
            UnkVec = new Vector4();
        }
        public int GetLength()
        {
            return 17 + Constants.SIZE_VECTOR4;
        }

        public void Read(BinaryReader reader, int length)
        {
            UnkInt = reader.ReadUInt32();
            UnkFloat1 = reader.ReadSingle();
            UnkFloat2 = reader.ReadSingle();
            UnkVec.Read(reader, Constants.SIZE_VECTOR4);
            UnkFloat3 = reader.ReadSingle();
            UnkByte = reader.ReadByte();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(UnkInt);
            writer.Write(UnkFloat1);
            writer.Write(UnkFloat2);
            UnkVec.Write(writer);
            writer.Write(UnkFloat3);
            writer.Write(UnkByte);
        }
    }
}
