using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class SubModel : ITwinSerializable
    {
        private UInt32 VertexesCount { get; set; }
        private Byte[] VertexData { get; set; }
        public Byte[] UnusedBlob { get; set; }
        public List<Vector4> Vertexes { get; set; }
        public List<Vector4> UVW { get; set; }
        public List<Vector4> Normals { get; set; }
        public List<Vector4> Colors { get; set; }
        public SubModel()
        {

        }
        public int GetLength()
        {
            return 12 + (VertexData != null ? VertexData.Length : 0) + (UnusedBlob != null ? UnusedBlob.Length : 0);
        }

        public void Read(BinaryReader reader, int length)
        {
            VertexesCount = reader.ReadUInt32();
            int vertexLen = reader.ReadInt32();
            VertexData = reader.ReadBytes(vertexLen);
            int blobLen = reader.ReadInt32();
            UnusedBlob = reader.ReadBytes(blobLen);
        }

        public void CalculateData()
        {
            var interpreter = VIFInterpreter.InterpretCode(VertexData);
            var data = interpreter.GetMem();
            Vertexes = new List<Vector4>(data[2]);
            if (data.Count > 3)
            {
                UVW = new List<Vector4>(data[3]);
            }
            else
            {
                UVW = new List<Vector4>();
                TrimList(UVW, (Int32)VertexesCount);
            }
            if (data.Count > 4)
            {
                Normals = new List<Vector4>(data[4]);
            }
            else
            {
                Normals = new List<Vector4>();
                TrimList(Normals, (Int32)VertexesCount);
            }
            if (data.Count > 5)
            {
                Colors = new List<Vector4>(data[5]);
            }
            else
            {
                Colors = new List<Vector4>();
                TrimList(Colors, (Int32)VertexesCount);
            }
        }
        public void Write(BinaryWriter writer)
        {
            //TODO: uncomment VertixesCount = (UInt32)Vertixes.Count();
            var unkVec1 = new Vector4();
            var unkVec2 = new Vector4();
            TrimList(UVW, (Int32)VertexesCount);
            TrimList(Normals, (Int32)VertexesCount);
            TrimList(Colors, (Int32)VertexesCount);
            var data = new List<List<Vector4>>();
            data.Add(new List<Vector4>() { unkVec1 });
            data.Add(new List<Vector4>() { unkVec2 });
            data.Add(Vertexes);
            data.Add(UVW);
            data.Add(Normals);
            data.Add(Colors);
            //TODO: Pack data to VIF
            writer.Write(VertexesCount);
            writer.Write(VertexData.Length);
            writer.Write(VertexData);
            writer.Write(UnusedBlob.Length);
            writer.Write(UnusedBlob);
        }

        private void TrimList(List<Vector4> list, Int32 desiredLength)
        {
            if (list != null)
            {
                if (list.Count > desiredLength)
                {
                    list.RemoveRange(desiredLength, list.Count - desiredLength);
                }
                while (UVW.Count < desiredLength)
                {
                    list.Add(new Vector4());
                }
            }
        }
    }
}
