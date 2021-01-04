using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.FileFormats.Fbx.FbxProperties
{
    class FbxPropertyArrayLong : FbxProperty
    {
        public FbxPropertyArrayLong()
        {
        }

        public FbxPropertyArrayLong(List<Int64> val)
        {
            Value = val;
        }
        public override UInt32 GetLength()
        {
            return (UInt32)(1 + 12 + 8 * ((List<Int64>)Value).Count);
        }
        public override void ReadBinary(BinaryReader reader)
        {
            var cnt = reader.ReadInt32();
            var encoding = reader.ReadInt32();
            var compressed = reader.ReadInt32();
            Value = new List<Int64>();
            List<Int64> list = (List<Int64>)Value;
            if (encoding == 0)
            {
                for (var i = 0; i < cnt; ++i)
                {
                    list.Add(reader.ReadInt64());
                }
            }
            else
            {
                using (MemoryStream stream = new MemoryStream(DecompressArray(reader.ReadBytes(compressed), cnt * 8)))
                {
                    BinaryReader streamReader = new BinaryReader(stream);
                    for (var i = 0; i < cnt; ++i)
                    {
                        list.Add(streamReader.ReadInt64());
                    }
                }
            }
        }
        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('l');
            List<Int64> list = (List<Int64>)Value;
            writer.Write(list.Count);
            writer.Write(0);
            writer.Write(GetLength() - 1 - 12);
            foreach (var e in list)
            {
                writer.Write((Int64)e);
            }
        }
        public override String ToString() { return "Property: Array Long"; }
    }
}
