using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class BoundingBoxBuilder : ITwinSerializable
    {
        public UInt16[] Header { get; set; }
        public Byte[] Blob { get; set; }
        public List<Vector4> BoundingBoxPoints { get; set; }

        public BoundingBoxBuilder()
        {
            Header = new UInt16[11];
            Blob = Array.Empty<byte>();
            BoundingBoxPoints = new List<Vector4>();
        }
        public int GetLength()
        {
            return 4 + Header.Length * 2 + Blob.Length + BoundingBoxPoints.Count * Constants.SIZE_VECTOR4;
        }

        public void Read(BinaryReader reader, int length)
        {
            Header = new UInt16[11];
            for (var i = 0; i < 11; ++i)
            {
                Header[i] = reader.ReadUInt16();
            }
            Int32 blobSize = reader.ReadInt32();
            var vecs = Header[0];
            for (var i = 0; i < vecs; ++i)
            {
                var vec = new Vector4();
                vec.Read(reader, Constants.SIZE_VECTOR4);
                BoundingBoxPoints.Add(vec);
            }
            // The rest is potentially unused?
            Blob = reader.ReadBytes(blobSize - vecs * Constants.SIZE_VECTOR4);
        }

        public void Write(BinaryWriter writer)
        {
            Header[0] = (UInt16)BoundingBoxPoints.Count;
            for (var i = 0; i < 11; ++i)
            {
                writer.Write(Header[i]);
            }
            writer.Write(Blob.Length + BoundingBoxPoints.Count * Constants.SIZE_VECTOR4);
            foreach (var v in BoundingBoxPoints)
            {
                v.Write(writer);
            }
            writer.Write(Blob);
        }
    }
}
