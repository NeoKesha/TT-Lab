using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Util.FBX.FbxProperties
{
    class FbxPropertyArrayBool : FbxProperty
    {
        public override UInt32 GetLength()
        {
            return (UInt32)(1 + 12 + 1 * ((List<Boolean>)Value).Count);
        }

        public override void ReadBinary(BinaryReader reader)
        {
            var cnt = reader.ReadInt32();
            var encoding = reader.ReadInt32();
            var compressed = reader.ReadInt32();
            Value = new List<Boolean>();
            List<Boolean> list = (List<Boolean>)Value;
            if (encoding == 0)
            {
                for (var i = 0; i < cnt; ++i)
                {
                    list.Add(reader.ReadBoolean());
                }
            } 
            else
            {
                using (MemoryStream stream = new MemoryStream(DecompressArray(reader.ReadBytes(compressed), cnt * 1)))
                {
                    BinaryReader streamReader = new BinaryReader(stream);
                    for (var i = 0; i < cnt; ++i)
                    {
                        list.Add(streamReader.ReadBoolean());
                    }
                }
            }
        }

        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('l');
            List<Boolean> list = (List<Boolean>)Value;
            writer.Write(list.Count);
            writer.Write(0);
            writer.Write(GetLength() - 1 - 12);
            foreach (var e in list)
            {
                writer.Write((Boolean)e);
            }
        }

        public override String ToString(){ return "Property: Array Bool"; }
    }
}
