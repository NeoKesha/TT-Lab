using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Util.FBX.FbxProperties
{
    class FbxPropertyString : FbxProperty
    {
        public override UInt32 GetLength()
        {
            return (UInt32)(1 + 4 + ((Char[])Value).Length);
        }
        public override void ReadBinary(BinaryReader reader)
        {
            var len = reader.ReadInt32();
            Value = reader.ReadChars(len);
        }
        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('S');
            writer.Write(((Char[])Value).Length);
            writer.Write((Char[])Value);
        }
        public override String ToString() { return "Property: String"; }
    }
}
