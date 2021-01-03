using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Util.FBX.FbxProperties
{
    public class FbxPropertyArrayInt : FbxProperty
    {
        public override UInt32 GetLength()
        {
            return (UInt32)(1 + 12 + 4 * ((List<Int32>)Value).Count);
        }
        public override void ReadBinary(BinaryReader reader)
        {
            var cnt = reader.ReadInt32();
            var encoding = reader.ReadInt32();
            var compressed = reader.ReadInt32();
            Value = new List<Int32>();
            List<Int32> list = (List<Int32>)Value;
            if (encoding == 0)
            {
                for (var i = 0; i < cnt; ++i)
                {
                    list.Add(reader.ReadInt32());
                }
            }
            else
            {
                using (MemoryStream stream = new MemoryStream(DecompressArray(reader.ReadBytes(compressed), cnt * 4)))
                {
                    BinaryReader streamReader = new BinaryReader(stream);
                    for (var i = 0; i < cnt; ++i)
                    {
                        list.Add(streamReader.ReadInt32());
                    }
                }
            }
        }
        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('i');
            List<Int32> list = (List<Int32>)Value;
            writer.Write(list.Count);
            writer.Write(0);
            writer.Write(GetLength() - 1 - 12);
            foreach (var e in list)
            {
                writer.Write((Int32)e);
            }
        }
        public override String ToString() { return "Property: Array Int"; }
    }
}
