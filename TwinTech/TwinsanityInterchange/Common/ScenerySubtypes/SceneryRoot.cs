using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes
{
    public class SceneryRoot : SceneryType_0x1600
    {
        public UInt32 UnkUInt;

        public override Int32 GetLength()
        {
            return base.GetLength() + 4;
        }

        public override void Read(BinaryReader reader, Int32 length)
        {
            UnkUInt = reader.ReadUInt32();
        }

        public override void Read(BinaryReader reader, Int32 length, IList<SceneryBaseType> sceneries)
        {
            Read(reader, length);
            base.Read(reader, length, sceneries);
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(UnkUInt);
            base.Write(writer);
        }
    }
}
