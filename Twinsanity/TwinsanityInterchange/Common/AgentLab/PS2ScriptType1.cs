using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class PS2ScriptType1 : ITwinSerializable
    {
        public Byte UnkByte1;
        public Byte UnkByte2;
        public List<Byte> Bytes;
        public List<Single> Floats;
        public UInt16 UnkUShort;
        public Int32 UnkInt;

        public PS2ScriptType1()
        {
            Bytes = new List<Byte>();
            Floats = new List<Single>();
        }

        public int GetLength()
        {
            return 8 + Bytes.Count + Floats.Count * 4;
        }

        public void Read(BinaryReader reader, int length)
        {
            UnkByte1 = reader.ReadByte();
            UnkByte2 = reader.ReadByte();
            UnkUShort = reader.ReadUInt16();
            UnkInt = reader.ReadInt32();
            Floats.Clear();
            for (var i = 0; i < UnkByte2; ++i)
            {
                Floats.Add(reader.ReadSingle());
            }
            Bytes.Clear();
            for (var i = 0; i < UnkByte1; ++i)
            {
                Bytes.Add(reader.ReadByte());
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(UnkByte1);
            writer.Write(UnkByte2);
            writer.Write(UnkUShort);
            writer.Write(UnkInt);
            foreach (var f in Floats)
            {
                writer.Write(f);
            }
            foreach (var b in Bytes)
            {
                writer.Write(b);
            }
        }
    }
}
