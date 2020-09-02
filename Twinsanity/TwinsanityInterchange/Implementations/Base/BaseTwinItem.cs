using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.Base
{
    public class BaseTwinItem : ITwinItem
    {
        UInt32 id;
        Byte[] data;
        public BaseTwinItem()
        {
            data = new byte[0];
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return data.Length;
        }

        public void Read(BinaryReader reader, int length)
        {
            data = reader.ReadBytes(length);
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(data);
        }
    }
}
