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
        public Matrix4 ObjectMatrix { get; set; }
        public Matrix4 ChunkMatrix { get; set; }
        public Matrix4 LoadingWall { get; set; }
        public List<TwinChunkLinkOGI3> ChunkLinksOGI3 { get; set; }

        /*public UInt16[] UnknownShorts { get; set; }
        public Matrix4 LoadAreaA { get; set; }
        public Matrix4 LoadAreaB { get; set; }
        public Vector4 AreaVectorA { get; set; }
        public Vector4 AreaVectorB { get; set; }
        public Matrix4 AreaMatrix { get; set; }
        public Vector4 UnknownVectorA { get; set; }
        public Vector4 UnknownVectorB { get; set; }
        public Matrix4 UnknownMatrix { get; set; }
        public Byte[] UnknownBytes { get; set; }*/
        public TwinChunkLink()
        {
            ObjectMatrix = new Matrix4();
            ChunkMatrix = new Matrix4();
            LoadingWall = null;
            ChunkLinksOGI3 = new List<TwinChunkLinkOGI3>();
            /*UnknownShorts = new ushort[15];
            LoadAreaA = new Matrix4();
            LoadAreaB = new Matrix4();
            AreaVectorA = new Vector4();
            AreaVectorB = new Vector4();
            AreaMatrix = new Matrix4();
            UnknownVectorA = new Vector4();
            UnknownVectorB = new Vector4();
            UnknownMatrix = new Matrix4();
            UnknownBytes = new byte[60];*/
        }
        public int GetLength()
        {
            return 4 + 4 + Path.Length + 4 + Constants.SIZE_MATRIX4 * 2
                + (LoadingWall != null ? Constants.SIZE_MATRIX4 : 0)
                + ChunkLinksOGI3.Sum(l => l.GetLength());
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
            if ((Type & 0x1) != 0)
            {
                var clOgi3 = new TwinChunkLinkOGI3();
                Boolean hasNext;
                do
                {
                    clOgi3.Read(reader, length);
                    ChunkLinksOGI3.Add(clOgi3);
                    hasNext = (clOgi3.Type & 0x1) != 0;
                    if (hasNext)
                    {
                        clOgi3 = new TwinChunkLinkOGI3();
                    }
                } while (hasNext);
                /*for (int i = 0; i < UnknownShorts.Length; ++i)
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
                reader.Read(UnknownBytes, 0, UnknownBytes.Length);*/
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
            if ((Type & 0x1) != 0)
            {
                foreach (var clOgi3 in ChunkLinksOGI3)
                {
                    if (!clOgi3.Equals(ChunkLinksOGI3.Last()))
                    {
                        clOgi3.Type |= 0x1;
                    }
                    else
                    {
                        clOgi3.Type &= ~0x1;
                    }
                    clOgi3.Write(writer);
                }
                /*for (int i = 0; i < UnknownShorts.Length; ++i)
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
                writer.Write(UnknownBytes);*/
            }
        }
    }
}
