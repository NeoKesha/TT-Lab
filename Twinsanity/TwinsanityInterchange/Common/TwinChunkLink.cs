using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class TwinChunkLink : ITwinSerializable
    {
        public UInt32 Type { get; set; }
        public String Path { get; set; }
        public UInt32 Flags { get; set; }
        public Matrix4 ObjectMatrix { get; private set; }
        public Matrix4 ChunkMatrix { get; private set; }
        public Matrix4 LoadingWall { get; set; }

        public UInt16[] UnknownShorts { get; private set; }
        public Matrix4 LoadAreaA { get; private set; }
        public Matrix4 LoadAreaB { get; private set; }
        public Vector4 AreaVectorA { get; private set; }
        public Vector4 AreaVectorB { get; private set; }
        public Matrix4 AreaMatrix { get; private set; }
        public Vector4 UnknownVectorA { get; private set; }
        public Vector4 UnknownVectorB { get; private set; }
        public Matrix4 UnknownMatrix { get; private set; }
        public Byte[] UnknownBytes { get; private set; }
        public TwinChunkLink()
        {
            ObjectMatrix = new Matrix4();
            ChunkMatrix = new Matrix4();
            LoadingWall = null;
            UnknownShorts = new ushort[15];
            LoadAreaA = new Matrix4();
            LoadAreaB = new Matrix4();
            AreaVectorA = new Vector4();
            AreaVectorB = new Vector4();
            AreaMatrix = new Matrix4();
            UnknownVectorA = new Vector4();
            UnknownVectorB = new Vector4();
            UnknownMatrix = new Matrix4();
            UnknownBytes = new byte[60];
        }
        public int GetLength()
        {
            return 4 + 4 + Path.Length + 4 + Constants.SIZE_MATRIX4 * 2
                + (LoadingWall != null ? Constants.SIZE_MATRIX4 : 0)
                + ((Type == 1 || Type == 3)
                ? Constants.SIZE_MATRIX4 * 4 + Constants.SIZE_VECTOR4 * 4 + Constants.SIZE_UINT16 * 15 + 60
                : 0);
        }

        public void Read(BinaryReader reader, int length)
        {
            var pos1 = reader.BaseStream.Position;
            Type = reader.ReadUInt32();
            int pathLen = reader.ReadInt32();
            Path = new String(reader.ReadChars(pathLen));
            Flags = reader.ReadUInt32();
            ObjectMatrix.Read(reader, Constants.SIZE_MATRIX4);
            ChunkMatrix.Read(reader, Constants.SIZE_MATRIX4);
            if ((Flags & 0x80000) != 0)
            {
                LoadingWall = new Matrix4();
                LoadingWall.Read(reader, Constants.SIZE_MATRIX4);
            }
            if (Type == 1 || Type == 3)
            {
                for (int i = 0; i < UnknownShorts.Length; ++i)
                {
                    UnknownShorts[i] = reader.ReadUInt16();
                }
                LoadAreaA.Read(reader, Constants.SIZE_MATRIX4);
                LoadAreaB.Read(reader, Constants.SIZE_MATRIX4);
                AreaVectorA.Read(reader, Constants.SIZE_VECTOR4);
                AreaVectorB.Read(reader, Constants.SIZE_VECTOR4);
                AreaMatrix.Read(reader, Constants.SIZE_MATRIX4);
                UnknownVectorA.Read(reader, Constants.SIZE_VECTOR4);
                UnknownVectorB.Read(reader, Constants.SIZE_VECTOR4);
                UnknownMatrix.Read(reader, Constants.SIZE_MATRIX4);
                reader.Read(UnknownBytes, 0, UnknownBytes.Length);
            }
            var pos2 = reader.BaseStream.Position;
            var read = pos2 - pos1;
            var calc = GetLength();
            if (calc != read)
            {
                int a = 0;
            }
        }

        public void Write(BinaryWriter writer)
        {
            if (LoadingWall != null)
            {
                Flags |= 0x80000;
            } else
            {
                Flags &= ~(uint)0x80000;
            }
            writer.Write(Type);
            writer.Write(Path.Length);
            writer.Write(Path.ToCharArray());
            writer.Write(Flags);
            ObjectMatrix.Write(writer);
            ChunkMatrix.Write(writer);
            if (LoadingWall != null)
            {
                LoadingWall.Write(writer);
            }
            if (Type == 1 || Type == 3)
            {
                for (int i = 0; i < UnknownShorts.Length; ++i)
                {
                    writer.Write(UnknownShorts[i]);
                }
                LoadAreaA.Write(writer);
                LoadAreaB.Write(writer);
                AreaVectorA.Write(writer);
                AreaVectorB.Write(writer);
                AreaMatrix.Write(writer);
                UnknownVectorA.Write(writer);
                UnknownVectorB.Write(writer);
                UnknownMatrix.Write(writer);
                writer.Write(UnknownBytes);
            }
        }
    }
}
