using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.FileFormats.Fbx.FbxProperties
{
    public class FbxPropertyDouble : FbxProperty
    {
        public FbxPropertyDouble()
        {
        }

        public FbxPropertyDouble(Double val)
        {
            Value = val;
        }
        public override UInt32 GetLength()
        {
            return 9;
        }
        public override void ReadBinary(BinaryReader reader)
        {
            Value = reader.ReadDouble();
        }
        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('D');
            writer.Write((Double)Value);
        }
        public override String ToString() { return "Property: Double"; }
    }
}
