using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.FileFormats.Fbx.FbxProperties
{
    public class FbxPropertyInt : FbxProperty
    {
        public FbxPropertyInt()
        {
        }

        public FbxPropertyInt(Int32 val)
        {
            Value = val;
        }
        public override UInt32 GetLength()
        {
            return 5;
        }
        public override void ReadBinary(BinaryReader reader)
        {
            Value = reader.ReadInt32();
        }
        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('I');
            writer.Write((Int32)Value);
        }
        public override String ToString() { return "Property: Int"; }
    }
}
