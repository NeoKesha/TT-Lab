using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code
{
    public class PS2AnyAnimation : ITwinAnimation
    {
        UInt32 id;
        public UInt32 Bitfield;
        public TwinBlob UnkBlob1;
        public TwinBlob UnkBlob2;

        public PS2AnyAnimation()
        {
            UnkBlob1 = new TwinBlob();
            UnkBlob2 = new TwinBlob();
        }

        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return 4 + UnkBlob1.GetLength() + UnkBlob2.GetLength();
        }

        public void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadUInt32();
            UnkBlob1.Read(reader, length);
            UnkBlob2.Read(reader, length);
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Bitfield);
            UnkBlob1.Write(writer);
            UnkBlob2.Write(writer);
        }

        public String GetName()
        {
            return $"Animation {id:X}";
        }
    }
}
