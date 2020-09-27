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
    public class SubModel : ITwinSerializable
    {
        public UInt32 Vertixes { get; set; }
        public Byte[] VertexData { get; private set; }
        public Byte[] Blob { get; private set; }
        public SubModel()
        {

        }
        public int GetLength()
        {
            return 12 + (VertexData != null ? VertexData.Length : 0) + (Blob != null ? Blob.Length : 0);
        }

        public void Read(BinaryReader reader, int length)
        {
            Vertixes = reader.ReadUInt32();
            int vertexLen = reader.ReadInt32();
            VertexData = reader.ReadBytes(vertexLen);
            int blobLen = reader.ReadInt32();
            Blob = reader.ReadBytes(blobLen);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Vertixes);
            writer.Write(VertexData.Length);
            writer.Write(VertexData);
            writer.Write(Blob.Length);
            writer.Write(Blob);
        }
    }
}
