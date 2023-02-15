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
    public class BlendSkinType1 : ITwinSerializable
    {
        public Int32 BlendsAmount { set; get; }
        public Int32 UnkInt;
        public Byte[] VifCode;
        public Byte[] UnkData;
        public List<Byte[]> UnkBlobs;
        public List<Int32> UnkInts;
        public List<Vector4> Vertexes { get; set; }
        public List<Vector4> UVW { get; set; }
        public List<Vector4> EmitColor { get; set; }
        public List<Vector4> VertexAdjustments { get; set; }
        public List<bool> Connection { get; set; }

        public BlendSkinType1()
        {
            UnkBlobs = new List<Byte[]>();
            UnkInts = new List<Int32>();
        }

        public int GetLength()
        {
            return 20 + VifCode.Length + UnkInts.Count * Constants.SIZE_UINT32 + UnkBlobs.Sum((blob) => blob.Length) + UnkBlobs.Count * Constants.SIZE_UINT32;
        }

        public void Read(BinaryReader reader, int length)
        {
            var blobLen = reader.ReadInt32();
            UnkInt = reader.ReadInt32();
            VifCode = reader.ReadBytes(blobLen);
            UnkData = reader.ReadBytes(0xC);
            for (int i = 0; i < BlendsAmount; ++i)
            {
                var blobSize = reader.ReadInt32();
                UnkInts.Add(reader.ReadInt32());
                UnkBlobs.Add(reader.ReadBytes(blobSize << 4));
            }
        }

        public void CalculateData()
        {
            var interpreter = VIFInterpreter.InterpretCode(VifCode);
            var data = interpreter.GetMem();

            Vertexes = new List<Vector4>();
            UVW = new List<Vector4>();
            EmitColor = new List<Vector4>();
            VertexAdjustments = new List<Vector4>();
            Connection = new List<bool>();

            const int VERT_DATA_INDEX = 3;
            for (int i = 0; i < data.Count;)
            {
                var verts = (data[i][0].GetBinaryX() & 0xFF);
                var fields = (data[i + 1][0].GetBinaryX() & 0xFF) / verts;
                var scaleVec = data[i + 2][0];
                Console.WriteLine($"Verts in this subskin block {verts}");
                Console.WriteLine($"Fields in this subskin block {fields}");
                Console.WriteLine($"Scale vector for this subskin block ({scaleVec.X};{scaleVec.Y})");
                var vertex_batch_1 = data[i + VERT_DATA_INDEX];
                var vertex_batch_2 = data[i + VERT_DATA_INDEX + 1];
                var vertex_batch_3 = data[i + VERT_DATA_INDEX + 3];
                var vertex_batch_4 = data[i + VERT_DATA_INDEX + 2];
                // Vertex conversion
                for (int j = 0; j < verts; ++j)
                {
                    var v1 = new Vector4(vertex_batch_1[j]);
                    var v2 = new Vector4(vertex_batch_2[j]);
                    v1.X = (int)v1.GetBinaryX();
                    v1.Y = (int)v1.GetBinaryY();
                    v1.Z = (int)v1.GetBinaryZ();
                    v1.W = (int)v1.GetBinaryW();
                    v2.X = (int)v2.GetBinaryX();
                    v2.Y = (int)v2.GetBinaryY();
                    v2.Z = (int)v2.GetBinaryZ();
                    v2.W = (int)v2.GetBinaryW();
                    v1 = v1.Multiply(scaleVec.X);
                    v2 = v2.Multiply(scaleVec.Y);
                    vertex_batch_1[j] = v1;
                    vertex_batch_2[j] = v2;
                }

                // Second conversion step
                for (int j = 0; j < verts; ++j)
                {
                    var v1 = vertex_batch_3[j];

                    var unkValue = v1.GetBinaryW() & 0xFFFF;
                    Connection.Add((unkValue >> 8) != 128);
                }

                // Save the batches into their respective vertex parts
                for (int j = 0; j < verts; j++)
                {
                    Vertexes.Add(vertex_batch_1[j]);
                    UVW.Add(vertex_batch_2[j]);
                    EmitColor.Add(vertex_batch_4[j]);
                    VertexAdjustments.Add(vertex_batch_4[j]);
                }
                i += (int)fields + 3;
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(VifCode.Length);
            writer.Write(UnkInt);
            writer.Write(VifCode);
            writer.Write(UnkData);
            for (int i = 0; i < BlendsAmount; ++i)
            {
                writer.Write(UnkBlobs[i].Length >> 4);
                writer.Write(UnkInts[i]);
                writer.Write(UnkBlobs[i]);
            }
        }
    }
}
