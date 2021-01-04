using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.FileFormats.Fbx.FbxProperties { 
    public class FbxPropertyArraySingle : FbxProperty
    {
        public FbxPropertyArraySingle()
        {
        }

        public FbxPropertyArraySingle(List<Single> val)
        {
            Value = val;
        }
        public override UInt32 GetLength()
        {
            return (UInt32)(1 + 12 + 4 * ((List<Single>)Value).Count);
        }
        public override void ReadBinary(BinaryReader reader)
        {
            var cnt = reader.ReadInt32();
            var encoding = reader.ReadInt32();
            var compressed = reader.ReadInt32();
            Value = new List<Single>();
            List<Single> list = (List<Single>)Value;
            if (encoding == 0)
            {
                for (var i = 0; i < cnt; ++i)
                {
                    list.Add(reader.ReadSingle());
                }
            }
            else
            {
                using (MemoryStream stream = new MemoryStream(DecompressArray(reader.ReadBytes(compressed), cnt * 4)))
                {
                    BinaryReader streamReader = new BinaryReader(stream);
                    for (var i = 0; i < cnt; ++i)
                    {
                        list.Add(streamReader.ReadSingle());
                    }
                }
            }
        }
        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('s');
            List<Single> list = (List<Single>)Value;
            writer.Write(list.Count);
            writer.Write(0);
            writer.Write(GetLength() - 1 - 12);
            foreach (var e in list)
            {
                writer.Write((Single)e);
            }
        }
        public override String ToString() { return "Property: Array Single"; }
    }
}
