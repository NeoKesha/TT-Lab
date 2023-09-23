using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Common.DynamicScenery;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class DynamicSceneryModel : ITwinSerializable
    {
        public Int32 UnkInt1;
        public List<BoundingBoxBuilder> BoundingBoxBuilders;
        public Int32 AnimatedFrames;
        public TwinDynamicSceneryAnimation Animation;
        public Byte LodFlag;
        public UInt32 ID;
        public Vector4[] BoundingBox;

        public DynamicSceneryModel()
        {
            BoundingBoxBuilders = new List<BoundingBoxBuilder>();
            BoundingBox = new Vector4[2];
            Animation = new();
        }

        public Int32 GetLength()
        {
            return 4 + 4 + BoundingBoxBuilders.Sum(o => o.GetLength()) + 4 + Animation.GetLength() + 1 + 4 + 2 * Constants.SIZE_VECTOR4;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            UnkInt1 = reader.ReadInt32();
            var bbBuildersAmount = reader.ReadInt32();
            if (bbBuildersAmount != 0)
            {
                for (var i = 0; i < bbBuildersAmount; ++i)
                {
                    var bbBuilder = new BoundingBoxBuilder();
                    BoundingBoxBuilders.Add(bbBuilder);
                    bbBuilder.Read(reader, length);
                }
            }
            AnimatedFrames = reader.ReadInt32();
            Animation.Read(reader, length);
            LodFlag = reader.ReadByte();
            ID = reader.ReadUInt32();
            for (var i = 0; i < 2; ++i)
            {
                BoundingBox[i] = new Vector4();
                BoundingBox[i].Read(reader, Constants.SIZE_VECTOR4);
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(UnkInt1);
            writer.Write(BoundingBoxBuilders.Count);
            foreach (var bbBuilder in BoundingBoxBuilders)
            {
                bbBuilder.Write(writer);
            }
            writer.Write(AnimatedFrames);
            Animation.Write(writer);
            writer.Write(LodFlag);
            writer.Write(ID);
            foreach (var v in BoundingBox)
            {
                v.Write(writer);
            }
        }
    }
}
