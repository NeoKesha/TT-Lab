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
    public class SubBlendSkinXBOX : SubBlendSkin
    {
        private UInt32 VertexesCount { get; set; }
        public List<UInt32> GroupList { get; set; }
        public List<Vector4> Vertexes { get; set; }
        //public List<UInt32> UnkVals { get; set; }
        public List<Vector4> UVW { get; set; }
        public List<Vector4> EmitColor { get; set; }
        public List<Vector4> Normals { get; set; }
        private UInt32 GroupJointsCount;
        public List<List<UInt32>> GroupJoints;
        public List<float> Weights1 { get; set; }
        public List<float> Weights2 { get; set; }
        public List<UInt32> UnkInts1 { get; set; }
        public List<UInt32> UnkInts2 { get; set; }
        public List<UInt32> UnkInts3 { get; set; }
        public List<UInt32> UnkInts4 { get; set; }

        public UInt32 BlendShapeCount { get; set; }
        public List<List<Vector4>> BlendShapes { get; set; }
        public SubBlendSkinXBOX()
        {

        }
        public SubBlendSkinXBOX(UInt32 BSCount)
        {
            BlendShapeCount = BSCount;
        }
        public override int GetLength()
        {
            int Size = 20 + (GroupList.Count * 8) + (Vertexes.Count * 0x30);
            for (int c = 0; c < GroupJoints.Count; c++)
            {
                Size += GroupJoints[c].Count * 4;
            }
            Size += (int)(BlendShapeCount * VertexesCount * 12);
            return Size;
        }

        public override void Read(BinaryReader reader, int length)
        {
            Vertexes = new List<Vector4>();
            UVW = new List<Vector4>();
            EmitColor = new List<Vector4>();
            Normals = new List<Vector4>();
            GroupList = new List<UInt32>();
            //UnkVals = new List<UInt32>();
            GroupJoints = new List<List<uint>>();
            List<UInt32> JointCountList = new List<uint>();
            Weights1 = new List<float>();
            Weights2 = new List<float>();
            UnkInts1 = new List<uint>();
            UnkInts2 = new List<uint>();
            UnkInts3 = new List<uint>();
            UnkInts4 = new List<uint>();
            BlendShapes = new List<List<Vector4>>();

            Material = reader.ReadUInt32();
            int vertexLen = reader.ReadInt32();
            VertexesCount = reader.ReadUInt32();
            uint GroupJointCount = reader.ReadUInt32();
            uint GroupCount = reader.ReadUInt32();
            // List of vertex counts for each group
            for (int g = 0; g < GroupCount; g++)
            {
                GroupList.Add(reader.ReadUInt32());
            }
            // List of joint influences for each group?
            for (int g = 0; g < GroupCount; g++)
            {
                JointCountList.Add(reader.ReadUInt32());
            }
            for (int g = 0; g < GroupCount; g++)
            {
                List<UInt32> GroupJointList = new List<uint>();
                for (int c = 0; c < JointCountList[g]; c++)
                {
                    GroupJointList.Add(reader.ReadUInt32());
                }
                GroupJoints.Add(GroupJointList);
            }
            for (int v = 0; v < VertexesCount; v++)
            {
                Vertexes.Add(new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), 1));
                Weights1.Add(reader.ReadSingle());
                Weights2.Add(reader.ReadSingle());
                UnkInts1.Add(reader.ReadUInt32());
                UnkInts2.Add(reader.ReadUInt32());
                UnkInts3.Add(reader.ReadUInt32());
                UnkInts4.Add(reader.ReadUInt32());
                //UnkVals.Add(reader.ReadUInt32());
                Vector4 emit = new Vector4();
                emit.X = reader.ReadByte() / 255.0f;
                emit.Y = reader.ReadByte() / 255.0f;
                emit.Z = reader.ReadByte() / 255.0f;
                emit.W = reader.ReadByte() / 255.0f;
                EmitColor.Add(emit);
                UVW.Add(new Vector4(reader.ReadSingle(), reader.ReadSingle(), 0, 1));
                Normals.Add(new Vector4(0.5f, 0.5f, 0.5f, 1f)); // temp
            }
            for (int b = 0; b < BlendShapeCount; b++)
            {
                List<Vector4> BlendShape = new List<Vector4>();
                for (int v = 0; v < VertexesCount; v++)
                {
                    BlendShape.Add(new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), 1));
                }
                BlendShapes.Add(BlendShape);
            }

        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Material);
            writer.Write(Vertexes.Count * 0x30);
            writer.Write(VertexesCount);

            int GroupJointCount = 0;
            for (int g = 0; g < GroupJoints.Count; g++)
            {
                GroupJointCount += GroupJoints[g].Count;
            }
            writer.Write(GroupJointCount);

            writer.Write(GroupList.Count);
            for (int g = 0; g < GroupList.Count; g++)
            {
                writer.Write(GroupList[g]);
            }
            for (int g = 0; g < GroupList.Count; g++)
            {
                writer.Write(GroupJoints.Count);
            }
            for (int g = 0; g < GroupList.Count; g++)
            {
                for (int a = 0; a < GroupJoints[g].Count; a++)
                {
                    writer.Write(GroupJoints[g][a]);
                }
            }
            for (int v = 0; v < VertexesCount; v++)
            {
                writer.Write(Vertexes[v].X);
                writer.Write(Vertexes[v].Y);
                writer.Write(Vertexes[v].Z);
                //writer.Write(UnkVals[v]);
                writer.Write(Weights1[v]);
                writer.Write(Weights2[v]);
                writer.Write(UnkInts1[v]);
                writer.Write(UnkInts2[v]);
                writer.Write(UnkInts3[v]);
                writer.Write(UnkInts4[v]);
                writer.Write((byte)(EmitColor[v].X * 255));
                writer.Write((byte)(EmitColor[v].Y * 255));
                writer.Write((byte)(EmitColor[v].Z * 255));
                writer.Write((byte)(EmitColor[v].W * 255));
                writer.Write(UVW[v].X);
                writer.Write(UVW[v].Y);
            }
            for (int b = 0; b < BlendShapes.Count; b++)
            {
                for (int v = 0; v < VertexesCount; v++)
                {
                    writer.Write(BlendShapes[b][v].X);
                    writer.Write(BlendShapes[b][v].Y);
                    writer.Write(BlendShapes[b][v].Z);
                }
            }

        }

    }
}
