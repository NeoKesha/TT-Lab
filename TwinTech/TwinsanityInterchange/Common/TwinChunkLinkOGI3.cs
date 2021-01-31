using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class TwinChunkLinkOGI3 : ITwinSerializable
    {
        public Int32 Type;
        public BoundingBoxBuilder OGIType3;

        public TwinChunkLinkOGI3()
        {
            OGIType3 = new BoundingBoxBuilder();
        }

        public Int32 GetLength()
        {
            return 4 + OGIType3.GetLength();
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            Type = reader.ReadInt32();
            OGIType3.Read(reader, length);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Type);
            OGIType3.Write(writer);
        }
    }
}
