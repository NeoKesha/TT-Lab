using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class TwinChunkLinkCollisionData : ITwinSerializable
    {
        public Int32 Type;
        public BoundingBoxBuilder BondingBoxBuilder;

        public TwinChunkLinkCollisionData()
        {
            BondingBoxBuilder = new BoundingBoxBuilder();
        }

        public Int32 GetLength()
        {
            return 4 + BondingBoxBuilder.GetLength();
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            Type = reader.ReadInt32();
            BondingBoxBuilder.Read(reader, length);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Type);
            BondingBoxBuilder.Write(writer);
        }
    }
}
