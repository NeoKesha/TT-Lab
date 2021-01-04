using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.FileFormats.Fbx.FbxProperties
{
    public class FbxPropertyLong : FbxProperty
    {
        public FbxPropertyLong()
        {
        }

        public FbxPropertyLong(Int64 val)
        {
            Value = val;
        }
        public override UInt32 GetLength()
        {
            return 9;
        }
        public override void ReadBinary(BinaryReader reader)
        {
            Value = reader.ReadInt64();
        }
        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('L');
            writer.Write((Int64)Value);
        }
        public override String ToString() { return "Property: Long"; }
    }
}
