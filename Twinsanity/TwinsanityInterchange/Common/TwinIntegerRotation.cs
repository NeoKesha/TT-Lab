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
    }
}
