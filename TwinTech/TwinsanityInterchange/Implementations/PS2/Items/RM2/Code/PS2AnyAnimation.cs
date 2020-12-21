using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code
{
    public class PS2AnyAnimation : BaseTwinItem, ITwinAnimation
    {
        public UInt32 Bitfield;
        public TwinBlob UnkBlob1;
        public TwinBlob UnkBlob2;

        public PS2AnyAnimation()
        {
            UnkBlob1 = new TwinBlob();
            UnkBlob2 = new TwinBlob();
        }

        public override int GetLength()
        {
            return 4 + UnkBlob1.GetLength() + UnkBlob2.GetLength();
        }

        public override void Read(BinaryReader reader, int length)
        {
            Bitfield = reader.ReadUInt32();
            UnkBlob1.Read(reader, length);
            UnkBlob2.Read(reader, length);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Bitfield);
            UnkBlob1.Write(writer);
            UnkBlob2.Write(writer);
        }

        public override String GetName()
        {
            return $"Animation {id:X}";
        }
    }
}
