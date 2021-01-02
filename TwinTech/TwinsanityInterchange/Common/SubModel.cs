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
        public List<Vector4> EmitColor { get; set; }
        public List<Vector4> Colors { get; set; }
        public List<bool> Connection { get; set; }
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
            Vertexes = new List<Vector4>();
            UVW = new List<Vector4>();
            EmitColor = new List<Vector4>();
            Colors = new List<Vector4>();
            Connection = new List<bool>();
            for (var i = 0; i < data.Count; )
            {
                var verts = (data[i][0].GetBinaryX() & 0xFF);
                var fields = 0;
                while (data[i+2+fields].Count == verts)
                {
                    ++fields;
                    if (i + fields + 2 >= data.Count)
                    {
                        break;
                    }
                }
                Vertexes.AddRange(data[i + 2]);
                if (fields > 1)
                {
                    var colors_conn = data[i + 3];
                    foreach (var e in colors_conn)
                    {
                        var conn = (e.GetBinaryW() & 0xFF00) >> 8;
                        Connection.Add(conn == 128 ? false : true);
                        Vector4 uv = new Vector4(e);
                        uv.X /= uv.Z;
                        uv.Y /= uv.Z;
                        uv.X -= (float)Math.Truncate(uv.X);
                        uv.Y -= (float)Math.Truncate(uv.Y);
                        uv.X = Math.Abs(uv.X);
                        uv.Y = Math.Abs(uv.Y);
                        UVW.Add(uv);
                    }
                }
                if (fields > 2)
                {
                    foreach (var e in data[i + 4])
                    {
                        var r = (e.GetBinaryX() & 0xFF) / 255.0f;
                        var g = (e.GetBinaryX() & 0xFF) / 255.0f;
                        var b = (e.GetBinaryX() & 0xFF) / 255.0f;
                        Colors.Add(new Vector4(r, g, b, 1.0f));
                    }  
                }
                if (fields > 3)
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
                i += fields + 2;
                TrimList(UVW, Vertexes.Count);
                TrimList(EmitColor, Vertexes.Count);
                TrimList(Colors, Vertexes.Count, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
            }
        }
        public void Write(BinaryWriter writer)
        {
            //TODO: uncomment VertixesCount = (UInt32)Vertixes.Count();
            var unkVec1 = new Vector4();
            var unkVec2 = new Vector4();
            TrimList(UVW, (Int32)VertexesCount);
            TrimList(EmitColor, (Int32)VertexesCount);
            TrimList(Colors, (Int32)VertexesCount, new Vector4(0.5f, 0.5f, 0.5f, 0.5f));
            var data = new List<List<Vector4>>();
            data.Add(new List<Vector4>() { unkVec1 });
            data.Add(new List<Vector4>() { unkVec2 });
            data.Add(Vertexes);
            data.Add(UVW);
            data.Add(EmitColor);
            data.Add(Colors);
            //TODO: Pack data to VIF
            writer.Write(VertexesCount);
            writer.Write(VertexData.Length);
            writer.Write(VertexData);
            writer.Write(UnusedBlob.Length);
            writer.Write(UnusedBlob);
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
