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

        public TwinChunkLink()
        {
            ObjectMatrix = new Matrix4();
            ChunkMatrix = new Matrix4();
            LoadingWall = null;
            ChunkLinksOGI3 = new List<TwinChunkLinkOGI3>();
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
