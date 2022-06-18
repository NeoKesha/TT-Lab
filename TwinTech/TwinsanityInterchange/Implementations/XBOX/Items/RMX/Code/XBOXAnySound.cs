using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.XBOX.Items.RMX.Code
{
    public class XBOXAnySound : PS2AnySound
    {
        public UInt32 Freq;
        public UInt32 UnkInt;
        public Byte[] HeadBytes;

        public override Int32 GetLength()
        {
            return 0x50 + Sound.Length;
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            Header = reader.ReadUInt32();
            Freq = reader.ReadUInt32();
            HeadBytes = reader.ReadBytes(0x38);
            Int32 SoundSize = reader.ReadInt32();
            UnkInt = reader.ReadUInt32();
            SoundSize -= 4;
            Sound = reader.ReadBytes(SoundSize);
            reader.ReadBytes(8); // SoundSize again and zero
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Header);
            writer.Write(Freq);
            writer.Write(HeadBytes);
            writer.Write(Sound.Length);
            writer.Write(UnkInt);
            writer.Write(Sound);
            writer.Write(Sound.Length);
            writer.Write((UInt32)0);
        }

        public override String GetName()
        {
            return $"Sound {id:X}";
        }

        public override ushort GetFreq()
        {
            return (ushort)Freq;
        }
    }
}
