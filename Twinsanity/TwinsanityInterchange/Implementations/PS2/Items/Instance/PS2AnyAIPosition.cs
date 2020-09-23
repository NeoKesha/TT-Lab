using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Instance
{
    public class PS2AnyAIPosition : ITwinCamera
    {
        UInt32 id;
        public Vector4 Position { get; private set; }
        public UInt32 UnkInt { get; set; }

        public PS2AnyAIPosition()
        {
            Position = new Vector4();
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return Constants.SIZE_VECTOR4 + 4;
        }

        public void Read(BinaryReader reader, int length)
        {
            Position.Read(reader, Constants.SIZE_VECTOR4);
            UnkInt = reader.ReadUInt32();
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            Position.Write(writer);
            writer.Write(UnkInt);
        }
    }
}
