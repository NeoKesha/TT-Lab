using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Util.FBX.FbxProperties
{
    public class FbxPropertyInt : FbxProperty
    {
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
