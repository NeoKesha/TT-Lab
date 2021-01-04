using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.FileFormats.Fbx.FbxProperties
{
    public class FbxPropertySingle : FbxProperty
    {
        public FbxPropertySingle()
        {
        }

        public FbxPropertySingle(Single val)
        {
            Value = val;
        }
        public override UInt32 GetLength()
        {
            return 5;
        }
        public override void ReadBinary(BinaryReader reader)
        {
            Value = reader.ReadSingle();
        }
        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('S');
            writer.Write((Single)Value);
        }
        public override String ToString() { return "Property: Single"; }
    }
}
