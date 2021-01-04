using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.FileFormats.Fbx
{
    public class FbxHeader
    {
        public static char[] FBXHeaderString = "Kaydara FBX Binary  \0".ToCharArray();
        public static UInt16 MagicalBytes = 0x001A;
        public UInt32 Version { get; set; } = 7400;
        public FbxHeader()
        {

        }
        public FbxHeader(UInt32 Version) : this()
        {
            this.Version = Version;
        }
        public void SaveBinary(BinaryWriter writer)
        {
            writer.Write(FBXHeaderString);
            writer.Write(MagicalBytes);
            writer.Write(Version);
        }
        public void ReadBinary(BinaryReader reader)
        {
            reader.BaseStream.Position += FBXHeaderString.Length + 2;
            Version = reader.ReadUInt32();
        }
    }
}
