using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class SubSkin : ITwinSerializable
    {
        public UInt32 Material;
        private Int32 BlobSize;
        private Int32 VertexAmount;
        private Byte[] VifCode;
        public List<Vector4> Vertexes { get; set; }
        public List<Vector4> UVW { get; set; }
        public List<Vector4> Normals { get; set; }
        public List<Vector4> Colors { get; set; }

        public int GetLength()
        {
            return 12 + VifCode.Length;
        }

        public void Read(BinaryReader reader, int length)
        {
            Material = reader.ReadUInt32();
            BlobSize = reader.ReadInt32();
            VertexAmount = reader.ReadInt32();
            VifCode = reader.ReadBytes(BlobSize);
            
        }
        public void CalculateData()
        {
            var interpreter = VIFInterpreter.InterpretCode(VifCode);
            var data = interpreter.GetMem();
            Vertexes = new List<Vector4>();
            UVW = new List<Vector4>();
            Normals = new List<Vector4>();
            Colors = new List<Vector4>();
            for (var i = 0; i < data.Count;)
            {
                var verts = (data[i][0].GetBinaryX() & 0xFF);
                var fields = 0;
                while (data[i + 3 + fields].Count == verts)
                {
                    ++fields;
                    if (i + fields + 3 >= data.Count)
                    {
                        break;
                    }
                }
                Vertexes.AddRange(data[i + 3]);
                if (fields > 1)
                {
                    UVW.AddRange(data[i + 4]);
                }
                if (fields > 2)
                {
                    Normals.AddRange(data[i + 5]);
                }
                if (fields > 3)
                {
                    Colors.AddRange(data[i + 6]);
                }
                i += fields + 3;
                TrimList(UVW, Vertexes.Count);
                TrimList(Normals, Vertexes.Count);
                TrimList(Colors, Vertexes.Count, new Vector4(0.5f, 0.5f, 0.5f, 0.5f));
            }
        }
        public void Write(BinaryWriter writer)
        {
            writer.Write(Material);
            writer.Write(BlobSize);
            writer.Write(VertexAmount);
            writer.Write(VifCode);
        }
        private void TrimList(List<Vector4> list, Int32 desiredLength, Vector4 defaultValue = null)
        {
            if (list != null)
            {
                if (list.Count > desiredLength)
                {
                    list.RemoveRange(desiredLength, list.Count - desiredLength);
                }
                while (list.Count < desiredLength)
                {
                    if (defaultValue != null)
                    {
                        list.Add(new Vector4(defaultValue));
                    }
                    else
                    {
                        list.Add(new Vector4());
                    }
                }
            }
        }
    }
}
