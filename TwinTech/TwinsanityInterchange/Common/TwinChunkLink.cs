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
        public List<TwinChunkLinkCollisionData> ChunkLinksCollisionData { get; set; }

        public TwinChunkLink()
        {
            ObjectMatrix = new Matrix4();
            ChunkMatrix = new Matrix4();
            LoadingWall = null;
            ChunkLinksCollisionData = new List<TwinChunkLinkCollisionData>();
        }
        public int GetLength()
        {
            return 4 + 4 + Path.Length + 4 + Constants.SIZE_MATRIX4 * 2
                + (LoadingWall != null ? Constants.SIZE_MATRIX4 : 0)
                + ChunkLinksCollisionData.Sum(l => l.GetLength());
        }

        public void Read(BinaryReader reader, int length)
        {
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
                var clOgi3 = new TwinChunkLinkCollisionData();
                Boolean hasNext;
                do
                {
                    clOgi3.Read(reader, length);
                    ChunkLinksCollisionData.Add(clOgi3);
                    hasNext = (clOgi3.Type & 0x1) != 0;
                    if (hasNext)
                    {
                        clOgi3 = new TwinChunkLinkCollisionData();
                    }
                } while (hasNext);
            }
        }

        public void Write(BinaryWriter writer)
        {
            Flags &= ~(uint)0x80000;
            Type &= ~(uint)0x1;
            if (LoadingWall != null)
            {
                Flags |= 0x80000;
            }
            if (ChunkLinksCollisionData.Count != 0)
            {
                Type |= 0x1;
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
                foreach (var colData in ChunkLinksCollisionData)
                {
                    colData.Type &= ~0x1;
                    if (!colData.Equals(ChunkLinksCollisionData.Last()))
                    {
                        colData.Type |= 0x1;
                    }
                    colData.Write(writer);
                }
            }
        }
    }
}
