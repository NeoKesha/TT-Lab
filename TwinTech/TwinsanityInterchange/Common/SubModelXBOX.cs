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
    public class SubModelXBOX : SubModel
    {
        private UInt32 VertexesCount { get; set; }
        public List<UInt32> GroupList { get; set; }
        public List<UInt32> UnkVals { get; set; }
        public SubModelXBOX()
        {

        }
        public override int GetLength()
        {
            return 16 + (GroupList.Count * 4) + (Vertexes.Count * 0x1C);
        }

        public override void CalculateData()
        {
            
        }

        public override void Read(BinaryReader reader, int length)
        {
            Vertexes = new List<Vector4>();
            UVW = new List<Vector4>();
            EmitColor = new List<Vector4>();
            Normals = new List<Vector4>();
            GroupList = new List<UInt32>();
            UnkVals = new List<UInt32>();
            Connection = new List<bool>();

            VertexesCount = reader.ReadUInt32();
            int vertexLen = reader.ReadInt32();
            uint GroupCount = reader.ReadUInt32();
            // List of vertex counts for each group
            for (int g = 0; g < GroupCount; g++)
            {
                GroupList.Add(reader.ReadUInt32());
            }
            for (int v = 0; v < VertexesCount; v++)
            {
                Vertexes.Add(new Vector4(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), 1));
                UnkVals.Add(reader.ReadUInt32());
                Vector4 emit = new Vector4();
                emit.X = reader.ReadByte() / 255.0f;
                emit.Y = reader.ReadByte() / 255.0f;
                emit.Z = reader.ReadByte() / 255.0f;
                emit.W = reader.ReadByte() / 255.0f;
                EmitColor.Add(emit);
                UVW.Add(new Vector4(reader.ReadSingle(), reader.ReadSingle(), 0, 1));
                Normals.Add(new Vector4(0.5f, 0.5f, 0.5f, 1f)); // temp for compatibility
                Connection.Add(true); // temp for compatibility
            }
            uint zero = reader.ReadUInt32();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(VertexesCount);
            writer.Write(Vertexes.Count * 0x1C);
            writer.Write(GroupList.Count);
            for (int g = 0; g < GroupList.Count; g++)
            {
                writer.Write(GroupList[g]);
            }
            for (int v = 0; v < VertexesCount; v++)
            {
                writer.Write(Vertexes[v].X);
                writer.Write(Vertexes[v].Y);
                writer.Write(Vertexes[v].Z);
                writer.Write(UnkVals[v]);
                writer.Write((byte)(EmitColor[v].X * 255));
                writer.Write((byte)(EmitColor[v].Y * 255));
                writer.Write((byte)(EmitColor[v].Z * 255));
                writer.Write((byte)(EmitColor[v].W * 255));
                writer.Write(UVW[v].X);
                writer.Write(UVW[v].Y);
            }
            writer.Write((uint)0);
        }

    }
}
