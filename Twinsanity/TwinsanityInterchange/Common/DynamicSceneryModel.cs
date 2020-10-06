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
        public List<OGIType3> OGIType3s;
        public Int32 UnkInt2;
        public UInt32 UnkBlobSizePacked { get; private set; }
        public UInt16 UnkBlobSizeHelper { get; private set; }
        public Byte[] UnkBlob { get; private set; }
        public Byte UnkByte;
        public UInt32 ID;
        public Vector4 UnkVec1;
        public Vector4 UnkVec2;

        public DynamicSceneryModel()
        {
            OGIType3s = new List<OGIType3>();
            UnkVec1 = new Vector4();
            UnkVec2 = new Vector4();
            UnkBlob = new Byte[0];
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
                    var type3 = new OGIType3();
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
            UnkByte = reader.ReadByte();
            ID = reader.ReadUInt32();
            UnkVec1.Read(reader, Constants.SIZE_VECTOR4);
            UnkVec2.Read(reader, Constants.SIZE_VECTOR4);
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
            writer.Write(UnkByte);
            writer.Write(ID);
            UnkVec1.Write(writer);
            UnkVec2.Write(writer);
        }
    }
}
