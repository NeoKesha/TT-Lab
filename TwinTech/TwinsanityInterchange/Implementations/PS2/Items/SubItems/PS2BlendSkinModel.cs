﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.PS2Hardware;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SubItems
{
    public class PS2BlendSkinModel : ITwinBlendSkinModel
    {
        Byte[] vifCode;

        public Int32 BlendsAmount { get; set; }
        public Int32 VertexesAmount { get; set; }
        public Vector3 BlendShape { get; set; }
        public List<ITwinBlendSkinFace> Faces { get; set; }
        public List<Vector4> Vertexes { get; set; }
        public List<Vector4> UVW { get; set; }
        public List<Vector4> Colors { get; set; }
        public List<VertexJointInfo> SkinJoints { get; set; }

        public int GetLength()
        {
            return 20 + vifCode.Length + Faces.Sum(f => f.GetLength());
        }

        public void Read(BinaryReader reader, int length)
        {
            var blobLen = reader.ReadInt32();
            VertexesAmount = reader.ReadInt32();
            vifCode = reader.ReadBytes(blobLen);
            BlendShape = new();
            BlendShape.Read(reader, Constants.SIZE_VECTOR3);
            for (int i = 0; i < BlendsAmount; ++i)
            {
                var face = new PS2BlendSkinFace(BlendShape);
                face.Read(reader, length);
            }
        }

        public void CalculateData()
        {
            var interpreter = VIFInterpreter.InterpretCode(vifCode);
            var data = interpreter.GetMem();

            Vertexes = new List<Vector4>();
            UVW = new List<Vector4>();
            Colors = new List<Vector4>();
            SkinJoints = new List<VertexJointInfo>();

            const int VERT_DATA_INDEX = 3;
            for (int i = 0; i < data.Count;)
            {
                var verts = data[i][0].GetBinaryX() & 0xFF;
                var fields = (data[i + 1][0].GetBinaryX() & 0xFF) / verts;
                var scaleVec = data[i + 2][0];
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

                // Convert joints information
                var jointInfos = new List<VertexJointInfo>();
                for (int j = 0; j < verts; ++j)
                {
                    var v1 = vertex_batch_3[j];

                    var connValue = v1.GetBinaryW() & 0xFF00;

                    var weightAmount = v1.GetBinaryW() & 0xFF;
                    var weight1 = 0f;
                    var weight2 = 0f;
                    var weight3 = 0f;
                    var jointIndex1 = 0u;
                    var jointIndex2 = 0u;
                    var jointIndex3 = 0u;
                    if (weightAmount > 0)
                    {
                        jointIndex1 = v1.GetBinaryX() & 0xFF;
                        jointIndex1 /= 4;
                        v1.SetBinaryX(v1.GetBinaryX() & 0xFFFFFF00);
                        weight1 = v1.X;
                    }
                    if (weightAmount > 1)
                    {
                        jointIndex2 = v1.GetBinaryY() & 0xFF;
                        jointIndex2 /= 4;
                        v1.SetBinaryY(v1.GetBinaryY() & 0xFFFFFF00);
                        weight2 = v1.Y;
                    }
                    if (weightAmount > 2)
                    {
                        jointIndex3 = v1.GetBinaryZ() & 0xFF;
                        jointIndex3 /= 4;
                        v1.SetBinaryZ(v1.GetBinaryZ() & 0xFFFFFF00);
                        weight3 = v1.Z;
                    }

                    var joint = new VertexJointInfo()
                    {
                        Weight1 = weight1,
                        Weight2 = weight2,
                        Weight3 = weight3,
                        JointIndex1 = (int)jointIndex1,
                        JointIndex2 = (int)jointIndex2,
                        JointIndex3 = (int)jointIndex3,
                        Connection = connValue >> 8 != 128
                    };

                    jointInfos.Add(joint);
                }

                // Save the batches into their respective vertex parts
                for (int j = 0; j < verts; j++)
                {
                    Vertexes.Add(vertex_batch_1[j]);
                    UVW.Add(vertex_batch_2[j]);
                    Colors.Add(vertex_batch_4[j]);
                    SkinJoints.Add(jointInfos[j]);
                }

                i += (int)fields + 3;
            }

            foreach (var face in Faces)
            {
                face.CalculateData();
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(vifCode.Length);
            writer.Write(VertexesAmount);
            writer.Write(vifCode);
            BlendShape.Write(writer);
            foreach (var face in Faces)
            {
                face.Write(writer);
            }
        }
    }
}