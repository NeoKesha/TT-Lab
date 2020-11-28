using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout
{
    public class PS2AnyTemplate : ITwinTemplate
    {
        UInt32 id;
        public String Name { get; set; }
        public UInt16 ObjectId { get; set; }
        public UInt16 Bitfield { get; set; }
        public UInt32 Header1 { get; set; }
        public UInt32 Header2 { get; set; }
        public UInt32 Header3 { get; set; }
        public UInt16 UnkShort { get; set; }
        public Byte[] UnkFlags { get; private set; }
        public UInt32 Properties { get; set; }
        public List<UInt32> Flags { get; private set; }
        public List<Single> Floats { get; private set; }
        public List<UInt32> Ints { get; private set; }
        public PS2AnyTemplate()
        {
            UnkFlags = new byte[6];
            Floats = new List<float>();
            Ints = new List<uint>();
            Flags = new List<uint>();
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return 4 + Name.Length + 16 + ((Header1 == 1)?2:0) + UnkFlags.Length + 4 + 12 + Flags.Count*4 + Floats.Count*4 + Ints.Count*4;
        }

        public void Read(BinaryReader reader, int length)
        {
            Int32 nameLen = reader.ReadInt32();
            Name = new string(reader.ReadChars(nameLen));
            ObjectId = reader.ReadUInt16();
            Bitfield = reader.ReadUInt16();
            Header1 = reader.ReadUInt32();
            Header2 = reader.ReadUInt32();
            Header3 = reader.ReadUInt32();
            if (Header1 == 1)
            {
                UnkShort = reader.ReadUInt16();
            }
            for (int i = 0; i < 6; ++i)
            {
                UnkFlags[i] = reader.ReadByte();
            }
            Properties = reader.ReadUInt32();
            Int32 flags = reader.ReadInt32();
            Flags.Clear();
            for (int i = 0; i < flags; ++i)
            {
                Flags.Add(reader.ReadUInt32());
            }
            Int32 floats = reader.ReadInt32();
            Floats.Clear();
            for (int i = 0; i < floats; ++i)
            {
                Floats.Add(reader.ReadSingle());
            }
            Int32 ints = reader.ReadInt32();
            Ints.Clear();
            for (int i = 0; i < ints; ++i)
            {
                Ints.Add(reader.ReadUInt32());
            }
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray());
            writer.Write(ObjectId);
            writer.Write(Bitfield);
            writer.Write(Header1);
            writer.Write(Header2);
            writer.Write(Header3);
            if (Header1 == 1)
            {
                writer.Write(UnkShort);
            }
            for (int i = 0; i < 6; ++i)
            {
                writer.Write(UnkFlags[i]);
            }
            writer.Write(Properties);
            writer.Write(Flags.Count);
            foreach (UInt32 e in Flags)
            {
                writer.Write(e);
            }
            writer.Write(Floats.Count);
            foreach (Single e in Floats)
            {
                writer.Write(e);
            }
            writer.Write(Ints.Count);
            foreach (UInt32 e in Ints)
            {
                writer.Write(e);
            }
        }
    }
}
