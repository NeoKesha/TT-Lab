using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems
{
    public class PS2SubModel : ITwinSubModel
    {
        private UInt32 VertexesCount { get; set; }
        private Byte[] VertexData { get; set; }
        public Byte[] UnusedBlob { get; set; }
        public List<Vector4> Vertexes { get; set; }
        public List<Vector4> UVW { get; set; }
        public List<Vector4> Colors { get; set; }
        public List<Vector4> EmitColor { get; set; }
        public List<Vector4> Normals { get; set; }
        public List<bool> Connection { get; set; }
        public PS2SubModel()
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

        [Flags]
        private enum FieldsPresent
        {
            Vertex = 0,
            UV_Color = 1,
            Normals = 2,
            EmitColors = 4
        }

        public void CalculateData()
        {
            var interpreter = VIFInterpreter.InterpretCode(VertexData);
            var data = interpreter.GetMem();
            Vertexes = new List<Vector4>();
            UVW = new List<Vector4>();
            EmitColor = new List<Vector4>();
            Normals = new List<Vector4>();
            Colors = new List<Vector4>();
            Connection = new List<bool>();
            var index = 0;
            for (var i = 0; i < data.Count;)
            {
                var verts = data[i][0].GetBinaryX() & 0xFF;
                var fieldsPresent = FieldsPresent.Vertex;
                var outputAddr = interpreter.GetAddressOutput();
                var fields = 0;
                foreach (var addr in outputAddr[index++])
                {
                    switch (addr)
                    {
                        case 0x3:
                            fieldsPresent |= FieldsPresent.Vertex;
                            fields++;
                            break;
                        case 0x4:
                            fieldsPresent |= FieldsPresent.UV_Color;
                            fields++;
                            break;
                        case 0x5:
                            fieldsPresent |= FieldsPresent.Normals;
                            fields++;
                            break;
                        case 0x6:
                            fieldsPresent |= FieldsPresent.EmitColors;
                            fields++;
                            break;
                    }
                    if (i + fields + 2 >= data.Count)
                        break;

                }
                Vertexes.AddRange(data[i + 2].Where((v) => v != null));
                if (fieldsPresent.HasFlag(FieldsPresent.UV_Color))
                {
                    var uv_con = data[i + 3].Where((v) => v != null);
                    foreach (var e in uv_con)
                    {
                        var conn = (e.GetBinaryW() & 0xFF00) >> 8;
                        Connection.Add(conn != 128);
                        var r = Math.Min(e.GetBinaryX() & 0xFF, 255);
                        var g = Math.Min(e.GetBinaryY() & 0xFF, 255);
                        var b = Math.Min(e.GetBinaryZ() & 0xFF, 255);
                        var a = (e.GetBinaryW() & 0xFF) << 1;

                        Color col = new Color((byte)r, (byte)g, (byte)b, (byte)a);
                        Colors.Add(Vector4.FromColor(col));

                        Vector4 uv = new Vector4(e);
                        uv.SetBinaryX(uv.GetBinaryX() & 0xFFFFFF00);
                        uv.SetBinaryY(uv.GetBinaryY() & 0xFFFFFF00);
                        uv.SetBinaryZ(uv.GetBinaryZ() & 0xFFFFFF00);
                        uv.Y = 1 - uv.Y;
                        UVW.Add(uv);
                    }
                }
                if (fieldsPresent.HasFlag(FieldsPresent.Normals))
                {
                    foreach (var e in data[i + 4])
                    {
                        if (e == null)
                            break;
                        Normals.Add(new Vector4(e.X, e.Y, e.Z, 1.0f));
                    }
                }
                if (fieldsPresent.HasFlag(FieldsPresent.EmitColors))
                {
                    foreach (var e in data[i + fields + 1])
                    {
                        if (e == null)
                            break;
                        Vector4 emit = new Vector4(e);
                        emit.X = emit.GetBinaryX() & 0xFF;// / 256.0f;
                        emit.Y = emit.GetBinaryY() & 0xFF;// / 256.0f;
                        emit.Z = emit.GetBinaryZ() & 0xFF;// / 256.0f;
                        emit.W = emit.GetBinaryW() & 0xFF;// / 256.0f;
                        EmitColor.Add(emit);
                    }
                }
                i += fields + 2;
                TrimList(UVW, Vertexes.Count);
                TrimList(EmitColor, Vertexes.Count);
                TrimList(Colors, Vertexes.Count);
                TrimList(Normals, Vertexes.Count, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
            }
        }
        public void Write(BinaryWriter writer)
        {
            //TODO: uncomment VertixesCount = (UInt32)Vertixes.Count();
            var unkVec1 = new Vector4();
            var unkVec2 = new Vector4();
            TrimList(UVW, (Int32)VertexesCount);
            TrimList(EmitColor, (Int32)VertexesCount);
            TrimList(Normals, (Int32)VertexesCount, new Vector4(0.5f, 0.5f, 0.5f, 0.5f));
            TrimList(Colors, (Int32)VertexesCount, new Vector4());
            var data = new List<List<Vector4>>();
            data.Add(new List<Vector4>() { unkVec1 });
            data.Add(new List<Vector4>() { unkVec2 });
            data.Add(Vertexes);
            data.Add(UVW);
            data.Add(Normals);
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
