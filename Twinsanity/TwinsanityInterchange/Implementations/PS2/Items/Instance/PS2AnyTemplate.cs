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
    public class PS2AnyTemplate : ITwinTemplate
    {
        UInt32 id;
        public String Name { get; set; }
        public UInt16 ObjectId { get; set; }
        public UInt16 Bitfield { get; set; }
        public UInt32 Header1 { get; set; }
        public UInt32 Header2 { get; set; }
        public UInt32 Header3 { get; set; }
        public UInt16 UnkShort { get; set; }
        public Byte[] UnkFlags { get; private set; }
        public UInt32 Properties { get; set; }
        public List<UInt32> Flags { get; private set; }
        public List<Single> Floats { get; private set; }
        public List<UInt32> Ints { get; private set; }
        public PS2AnyTemplate()
        {
            UnkFlags = new byte[6];
            Floats = new List<float>();
            Ints = new List<uint>();
            Flags = new List<uint>();
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return Constants.SIZE_VECTOR4;
        }

        public void Read(BinaryReader reader, int length)
        {
            
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            
        }
    }
}
