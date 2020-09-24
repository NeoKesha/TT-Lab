using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class TwinBlob : ITwinSerializable
    {
        UInt32 UnkBlobInt;
        UInt32 BlockSizePacked;
        UInt16 BlockSizeHelper;
        public Byte[] Blob { get; private set; }
        public int GetLength()
        {
            return 6 + (Blob != null ? Blob.Length : 0);
        }

        public void Read(BinaryReader reader, int length)
        {
            BlockSizePacked = reader.ReadUInt32();
            BlockSizeHelper = reader.ReadUInt16();
            if (GetBlockLength() > 0)
            {
                Blob = reader.ReadBytes(GetBlockLength());
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(BlockSizePacked);
            writer.Write(BlockSizeHelper);
            if (GetBlockLength() > 0)
            {
                writer.Write(Blob);
            }
        }

        Int32 GetBlockLength()
        {
            return (Int32)(
                (BlockSizePacked & 0x7F) * 0x8 +
                ((BlockSizePacked >> 0xA) & 0xFFE) +
                (BlockSizePacked >> 0x16) * 0x2 * BlockSizeHelper
                );
        }
    }
}
