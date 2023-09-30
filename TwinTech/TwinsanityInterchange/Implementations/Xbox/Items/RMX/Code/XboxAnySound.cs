using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace Twinsanity.TwinsanityInterchange.Implementations.Xbox.Items.RMX.Code
{
    public class XboxAnySound : BaseTwinItem, ITwinSound
    {
        UInt32 frequency;
        Byte[] headBytes;
        UInt32 unkInt;

        public UInt32 Header { get; set; }
        public Byte UnkFlag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Byte FreqFac { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UInt16 Param1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UInt16 Param2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UInt16 Param3 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public UInt16 Param4 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Byte[] Sound { get; set; }

        public override Int32 GetLength()
        {
            return 0x50 + Sound.Length;
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            Header = reader.ReadUInt32();
            frequency = reader.ReadUInt32();
            headBytes = reader.ReadBytes(0x38);
            var soundSize = reader.ReadInt32();
            unkInt = reader.ReadUInt32();
            soundSize -= 4;
            Sound = reader.ReadBytes(soundSize);
            reader.ReadBytes(8); // SoundSize and zero
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Header);
            writer.Write(frequency);
            writer.Write(headBytes);
            writer.Write(Sound.Length + 4);
            writer.Write(unkInt);
            writer.Write(Sound);
            writer.Write(Sound.Length + 4);
            writer.Write(0U);
        }

        public UInt16 GetFreq()
        {
            return (UInt16)frequency;
        }

        public override String GetName()
        {
            return $"Sound {id:X}";
        }
    }
}
