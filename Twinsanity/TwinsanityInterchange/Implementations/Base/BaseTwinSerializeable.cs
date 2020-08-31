using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.Base
{
    public class BaseTwinSerializeable : ITwinSerializeable
    {
        UInt32 ID;
        Byte[] Data;
        public Int32 FromAsset(BinaryReader reader, Int32 length)
        {
            return Read(reader, length);
        }

        public UInt32 GetID()
        {
            return ID;
        }

        public Int32 GetLength()
        {
            return Data.Length;
        }

        public Int32 Read(BinaryReader reader, Int32 length)
        {
            Data = reader.ReadBytes(length);
            return Data.Length;
        }

        public Int32 ToAsset(BinaryWriter writer)
        {
            return Write(writer);
        }

        public Int32 Write(BinaryWriter writer)
        {
            writer.Write(Data);
            return Data.Length;
        }
    }
}
