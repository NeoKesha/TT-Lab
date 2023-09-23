using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class TwinIntegerRotation : ITwinSerializable
    {
        public UInt16 Angle { get; set; }
        public UInt16 Fract { get; set; }
        public int GetLength()
        {
            return Constants.SIZE_UINT32;
        }

        public void Read(BinaryReader reader, int length)
        {
            Angle = reader.ReadUInt16();
            Fract = reader.ReadUInt16();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Angle);
            writer.Write(Fract);
        }

        public Single GetRotation()
        {
            var result = (Single)Math.Floor(Angle / (Single)UInt16.MaxValue * 360);
            result += (Fract / (Single)UInt16.MaxValue);
            return result;
        }

        public void SetRotation(Single angle)
        {
            Angle = (UInt16)(Math.Floor(angle) / 360 * UInt16.MaxValue);
            Fract = (UInt16)((angle - Math.Truncate(angle)) * UInt16.MaxValue);
        }
    }
}
