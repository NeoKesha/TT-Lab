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
    public class DynamicSceneryModel : ITwinSerializable
    {
        public Int32 UnkInt1;
        public List<BoundingBoxBuilder> OGIType3s;
        public Int32 UnkInt2;
        public UInt32 UnkBlobSizePacked { get; set; }
        public UInt16 UnkBlobSizeHelper { get; set; }
        public Byte[] UnkBlob { get; set; }
        public Byte LodFlag;
        public UInt32 ID;
        public Vector4[] BoundingBox;

        public DynamicSceneryModel()
        {
            OGIType3s = new List<BoundingBoxBuilder>();
            BoundingBox = new Vector4[2];
            UnkBlob = Array.Empty<Byte>();
        }

        public Int32 GetLength()
        {
            return 4 + 4 + OGIType3s.Sum(o => o.GetLength()) + 10 + UnkBlob.Length + 1 + 4 + 2 * Constants.SIZE_VECTOR4;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            UnkInt1 = reader.ReadInt32();
            var type3s = reader.ReadInt32();
            if (type3s != 0)
            {
                for (var i = 0; i < type3s; ++i)
                {
                    var type3 = new BoundingBoxBuilder();
                    OGIType3s.Add(type3);
                    type3.Read(reader, length);
                }
            }
            UnkInt2 = reader.ReadInt32();
            UnkBlobSizePacked = reader.ReadUInt32();
            UnkBlobSizeHelper = reader.ReadUInt16();
            var blobSize = (UnkBlobSizePacked & 0x7F) * 0x8 +
                (UnkBlobSizePacked >> 0x9 & 0x1FFC) +
                (UnkBlobSizePacked >> 0x16) * UnkBlobSizeHelper * 0x4;
            UnkBlob = reader.ReadBytes((Int32)blobSize);
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
            writer.Write(OGIType3s.Count);
            foreach (var type3 in OGIType3s)
            {
                type3.Write(writer);
            }
            writer.Write(UnkInt2);
            writer.Write(UnkBlobSizePacked);
            writer.Write(UnkBlobSizeHelper);
            writer.Write(UnkBlob);
            writer.Write(LodFlag);
            writer.Write(ID);
            foreach (var v in BoundingBox)
            {
                v.Write(writer);
            }
        }
    }
}
