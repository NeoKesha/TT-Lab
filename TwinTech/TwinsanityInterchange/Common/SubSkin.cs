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
        public List<Vector4> EmitColor { get; set; }
        public List<Vector4> Colors { get; set; }
        public List<bool> Connection { get; set; }

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
            EmitColor = new List<Vector4>();
            Colors = new List<Vector4>();
            Connection = new List<bool>();
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
                var vertexes = data[i + 3];
                var divNum = 65535f;
                foreach (var v in vertexes)
                {
                    Vertexes.Add(new Vector4((v.GetBinaryX() & 0xFFFF) / divNum,
                        (v.GetBinaryY() & 0xFFFF) / divNum,
                        (v.GetBinaryZ() & 0xFFFF) / divNum,
                        (v.GetBinaryW() & 0xFFFF) / divNum));
                }
                if (fields > 1)
                {
                    // What is this?
                    UVW.AddRange(data[i + 4].Select(v => new Vector4((v.GetBinaryX() & 0xFFFF) / divNum,
                        (v.GetBinaryY() & 0xFFFF) / divNum,
                        (v.GetBinaryZ() & 0xFFFF) / divNum,
                        (v.GetBinaryW() & 0xFFFF) / divNum)).ToList());
                }
                if (fields > 2)
                {
                    foreach (var e in data[i + 5])
                    {
                        Vector4 emit = new Vector4(e);
                        emit.X = (emit.X + 126.0f) / 256.0f;
                        emit.Y = (emit.Y + 126.0f) / 256.0f;
                        emit.Z = (emit.Z + 126.0f) / 256.0f;
                        emit.W = (emit.W + 126.0f) / 256.0f;
                        EmitColor.Add(emit);
                    }
                }
                if (fields > 3)
                {
                    var colors_conn = data[i + 6];
                    foreach (var e in colors_conn)
                    {
                        var conn = (e.GetBinaryW() & 0xFF00) >> 8;
                        Connection.Add(conn == 128 ? false : true);
                        var r = e.X;
                        var g = e.X;
                        var b = e.X;
                        Colors.Add(new Vector4(r, g, b, 1.0f));
                    }
                }
                i += fields + 3;
                TrimList(UVW, Vertexes.Count);
                TrimList(EmitColor, Vertexes.Count);
                TrimList(Colors, Vertexes.Count, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
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
