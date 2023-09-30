using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SubItems;

namespace Twinsanity.TwinsanityInterchange.Implementations.Xbox.Items.SubItems
{
    public class XboxSubModel : ITwinSubModel
    {
        List<UInt32> groupList = new();

        public List<Vector4> Vertexes { get; set; }
        public List<Vector4> UVW { get; set; }
        public List<Color> Colors { get; set; }
        public List<Vector4> EmitColor { get; set; }
        public List<Vector4> Normals { get; set; }
        public List<Boolean> Connection { get; set; }

        public void CalculateData()
        {
            // Data needs no decompression as it is already presented decompressed on read
        }

        public Int32 GetLength()
        {
            return 16 + groupList.Count * 4 + Vertexes.Count * 0x1C;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            var vertexAmount = reader.ReadInt32();
            reader.ReadUInt32(); // Vertex amount * 0x1C
            var groupAmount = reader.ReadUInt32();
            for (Int32 i = 0; i < groupAmount; i++)
            {
                groupList.Add(reader.ReadUInt32());
            }

            Vertexes = new();
            UVW = new();
            Colors = new();
            EmitColor = new();
            Normals = new();
            Connection = new();
            for (Int32 i = 0; i < vertexAmount; i++)
            {
                Vertexes.Add(new Vector4
                {
                    X = reader.ReadSingle(),
                    Y = reader.ReadSingle(),
                    Z = reader.ReadSingle(),
                    W = 1.0f
                });
                var packedNormal = reader.ReadUInt32();
                var nx = (Int32)(packedNormal & 0xFFF);
                var ny = (Int32)(packedNormal >> 11 & 0xFFF);
                var nz = (Int32)(packedNormal >> 22 & 0x7FF);
                Normals.Add(new Vector4
                {
                    X = PackedIntToFloat(nx, 1023f, 1024f),
                    Y = PackedIntToFloat(ny, 1023f, 1024f),
                    Z = PackedIntToFloat(nz, 511f, 512f),
                    W = 1.0f
                });
                var color = new Color();
                color.Read(reader, 4);
                Colors.Add(color);
                EmitColor.Add(new Vector4());
                UVW.Add(new Vector4
                {
                    X = reader.ReadSingle(),
                    Y = reader.ReadSingle(),
                    Z = 0f,
                    W = 1.0f
                });
                Connection.Add(true);
            }
            reader.ReadUInt32(); // Zero
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Vertexes.Count);
            writer.Write(Vertexes.Count * 0x1C);
            writer.Write(groupList.Count);
            for (Int32 i = 0; i < groupList.Count; i++)
            {
                writer.Write(groupList[i]);
            }
            for (Int32 i = 0; i < Vertexes.Count; i++)
            {
                writer.Write(Vertexes[i].X);
                writer.Write(Vertexes[i].Y);
                writer.Write(Vertexes[i].Z);
                var nx = FloatToPackedInt(Normals[i].X, 1023, 1024);
                var ny = FloatToPackedInt(Normals[i].Y, 1023, 1024);
                var nz = FloatToPackedInt(Normals[i].Z, 511, 512);
                var packedNormal = (UInt32)((nz & 0x7FF << 22) | (ny & 0xFFF << 11) | (nx & 0xFFF));
                writer.Write(packedNormal);
                Colors[i].Write(writer);
                writer.Write(UVW[i].X);
                writer.Write(UVW[i].Y);
            }
            writer.Write(0U);
        }

        private float PackedIntToFloat(Int32 value, Single posFactor, Single negFactor)
        {
            if (value >= 0)
            {
                return value / posFactor;
            }

            return value / negFactor;
        }

        private Int32 FloatToPackedInt(Single value, Int32 posFactor, Int32 negFactor)
        {
            if (value >= 0)
            {
                return (Int32)(value * posFactor);
            }

            return (Int32)(value * negFactor);
        }
    }
}
