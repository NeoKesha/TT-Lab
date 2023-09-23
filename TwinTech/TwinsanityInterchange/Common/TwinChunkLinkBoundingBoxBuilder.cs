using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class TwinChunkLinkBoundingBoxBuilder : ITwinSerializable
    {
        public Int32 Type;
        public BoundingBoxBuilder BondingBoxBuilder;

        public TwinChunkLinkBoundingBoxBuilder()
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
