using ICSharpCode.SharpZipLib.Zip.Compression;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TT_Lab.FileFormats.Fbx.FbxProperties
{
    public abstract class FbxProperty
    {
        public Object Value { get; set; }
        public abstract void SaveBinary(BinaryWriter writer);
        public abstract void ReadBinary(BinaryReader reader);
        public abstract UInt32 GetLength();
        public static FbxProperty ReadProp(BinaryReader reader)
        {
            Char type = reader.ReadChar();
            FbxProperty prop = null;
            switch (type)
            {
                case 'Y':
                    prop = new FbxPropertyShort();
                    break;
                case 'C':
                    prop = new FbxPropertyBool();
                    break;
                case 'I':
                    prop = new FbxPropertyInt();
                    break;
                case 'F':
                    prop = new FbxPropertySingle();
                    break;
                case 'D':
                    prop = new FbxPropertyDouble();
                    break;
                case 'L':
                    prop = new FbxPropertyLong();
                    break;
                case 'b':
                    prop = new FbxPropertyArrayBool();
                    break;
                case 'i':
                    prop = new FbxPropertyArrayInt();
                    break;
                case 'f':
                    prop = new FbxPropertyArraySingle();
                    break;
                case 'd':
                    prop = new FbxPropertyArrayDouble();
                    break;
                case 'l':
                    prop = new FbxPropertyArrayLong();
                    break;
                case 'S':
                    prop = new FbxPropertyString();
                    break;
                case 'R':
                    prop = new FbxPropertyBlob();
                    break;
            }
            prop.ReadBinary(reader);
            return prop;
        }

        public static byte[] DecompressArray(Byte[] array, Int32 uncompressed)
        {
            Inflater inflater = new Inflater();
            inflater.SetInput(array);
            byte[] output = new byte[uncompressed];
            var pos = 0;
            while (!inflater.IsFinished && pos < uncompressed)
            {
                pos += inflater.Inflate(output, pos, uncompressed - pos);
            }
            return output;
        }
    }
}
