﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.Collision
{
    public class GroupInformation : ITwinSerializable
    {
        public UInt32 Size;
        public UInt32 Offset;

        public Int32 GetLength()
        {
            return 8;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            Size = reader.ReadUInt32();
            Offset = reader.ReadUInt32();
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Size);
            writer.Write(Offset);
        }
    }
}
