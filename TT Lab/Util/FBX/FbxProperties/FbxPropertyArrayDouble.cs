using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Util.FBX.FbxProperties
{
    public class FbxPropertyArrayDouble : FbxProperty
    {
        public override UInt32 GetLength()
        {
            return (UInt32)(1 + 12 + 8 * ((List<Double>)Value).Count);
        }
        public override void ReadBinary(BinaryReader reader)
        {
            var cnt = reader.ReadInt32();
            var encoding = reader.ReadInt32();
            var compressed = reader.ReadInt32();
            Value = new List<Double>();
            List<Double> list = (List<Double>)Value;
            if (encoding == 0)
            {
                for (var i = 0; i < cnt; ++i)
                {
                    list.Add(reader.ReadDouble());
                }
            }
            else
            {
                using (MemoryStream stream = new MemoryStream(DecompressArray(reader.ReadBytes(compressed), cnt * 8)))
                {
                    BinaryReader streamReader = new BinaryReader(stream);
                    for (var i = 0; i < cnt; ++i)
                    {
                        list.Add(streamReader.ReadDouble());
                    }
                }
            }
        }
        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('d');
            List<Double> list = (List<Double>)Value;
            writer.Write(list.Count);
            writer.Write(0);
            writer.Write(GetLength() - 1 - 12);
            foreach (var e in list)
            {
                writer.Write((Double)e);
            }
        }
        public override String ToString() { return "Property: Array Double"; }
    }
}
