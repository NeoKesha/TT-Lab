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
    public class CameraSub0A19 : ITwinSerializeable
    {
        public UInt32 UnkInt { get; set; }
        public Single UnkFloat1 { get; set; }
        public Single UnkFloat2 { get; set; }
        public Matrix4 UnkMatrix1 { get; private set; }
        public Matrix4 UnkMatrix2 { get; private set; }
        public Vector4 UnkVector { get; private set; }
        public Byte UnkByte1 { get; set; }
        public Single UnkFloat3 { get; set; }
        public Single UnkFloat4 { get; set; }
        public Single UnkFloat5 { get; set; }
        public Single UnkFloat6 { get; set; }
        public Byte UnkByte2 { get; set; }
        public CameraSub0A19()
        {
            UnkMatrix1 = new Matrix4();
            UnkMatrix2 = new Matrix4();
            UnkVector = new Vector4();
        }
        public int GetLength()
        {
            return 30 + Constants.SIZE_MATRIX4 * 2 + Constants.SIZE_VECTOR4;
        }

        public void Read(BinaryReader reader, int length)
        {
            int pos1 = (int)reader.BaseStream.Position;
            UnkInt = reader.ReadUInt32();
            UnkFloat1 = reader.ReadSingle();
            UnkFloat2 = reader.ReadSingle();
            UnkMatrix1.Read(reader, Constants.SIZE_MATRIX4);
            UnkMatrix2.Read(reader, Constants.SIZE_MATRIX4);
            UnkVector.Read(reader, Constants.SIZE_VECTOR4);
            UnkByte1 = reader.ReadByte();
            UnkFloat3 = reader.ReadSingle();
            UnkFloat4 = reader.ReadSingle();
            UnkFloat5 = reader.ReadSingle();
            UnkFloat6 = reader.ReadSingle();
            UnkByte2 = reader.ReadByte();
            int pos2 = (int)reader.BaseStream.Position;
            int len = pos2 - pos1;
            int g = GetLength();
            int a = 0;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(UnkInt);
            writer.Write(UnkFloat1);
            writer.Write(UnkFloat2);
            UnkMatrix1.Write(writer);
            UnkMatrix1.Write(writer);
            UnkVector.Write(writer);
            writer.Write(UnkByte1);
            writer.Write(UnkFloat3);
            writer.Write(UnkFloat4);
            writer.Write(UnkFloat5);
            writer.Write(UnkFloat6);
            writer.Write(UnkByte2);
        }
    }
}
