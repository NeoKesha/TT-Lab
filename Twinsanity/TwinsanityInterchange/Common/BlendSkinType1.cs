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
    public class BlendSkinType1 : ITwinSerializable
    {
        public Int32 ListLength { set; get; }
        public Int32 BlobSize;
        public Int32 UnkInt;
        public Byte[] Blob;
        public Byte[] UnkData;
        public List<Byte[]> UnkBlobs;
        public List<Int32> UnkInts;

        public BlendSkinType1()
        {
            UnkBlobs = new List<Byte[]>();
            UnkInts = new List<Int32>();
        }

        public int GetLength()
        {
            return 20 + Blob.Length + UnkInts.Count * Constants.SIZE_UINT32 + UnkBlobs.Sum((blob) => blob.Length) + UnkBlobs.Count * Constants.SIZE_UINT32;
        }

        public void Read(BinaryReader reader, int length)
        {
            BlobSize = reader.ReadInt32();
            UnkInt = reader.ReadInt32();
            Blob = reader.ReadBytes(BlobSize);
            UnkData = reader.ReadBytes(0xC);
            for (int i = 0; i < ListLength; ++i)
            {
                var blobSize = reader.ReadInt32();
                UnkInts.Add(reader.ReadInt32());
                UnkBlobs.Add(reader.ReadBytes(blobSize << 4));
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(BlobSize);
            writer.Write(UnkInt);
            writer.Write(Blob);
            writer.Write(UnkData);
            for (int i = 0; i < ListLength; ++i)
            {
                writer.Write(UnkBlobs[i].Length);
                writer.Write(UnkInts[i]);
                writer.Write(UnkBlobs[i]);
            }
        }
    }
}
