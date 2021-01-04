using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.FileFormats.Fbx.FbxProperties
{
    class FbxPropertyBlob : FbxProperty
    {
        public FbxPropertyBlob()
        {
        }

        public FbxPropertyBlob(byte[] val)
        {
            Value = val;
        }
        public override UInt32 GetLength()
        {
            return (UInt32)(1 + 4 + ((Byte[])Value).Length);
        }

        public override void ReadBinary(BinaryReader reader)
        {
            var len = reader.ReadInt32();
            Value = reader.ReadBytes(len);
        }

        public override void SaveBinary(BinaryWriter writer)
        {
            writer.Write('R');
            writer.Write(((Byte[])Value).Length);
            writer.Write((Byte[])Value);
        }
        public override String ToString() { return "Property: Blob"; }
    }
}
