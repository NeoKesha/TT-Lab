using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Util.FBX.FbxProperties
{
    public class FbxPropertyBool : FbxProperty
    {
        public override UInt32 GetLength()
        {
            return 2;
        }

        public override void ReadBinary(BinaryReader reader)
        {
            Value = reader.ReadBoolean();
        }

        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('C');
            writer.Write((Boolean)Value);
        }
        public override String ToString() { return "Property: Bool"; }
    }
}
