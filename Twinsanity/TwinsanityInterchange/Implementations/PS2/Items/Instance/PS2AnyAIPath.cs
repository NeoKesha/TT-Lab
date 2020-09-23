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
    public class PS2AnyAIPath : ITwinCamera
    {
        UInt32 id;
        public UInt16[] Args { get; private set; }
        
        public PS2AnyAIPath()
        {
            Args = new ushort[5];
        }
        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return 10;
        }

        public void Read(BinaryReader reader, int length)
        {
            for (int i = 0; i < Args.Length; ++i)
            {
                Args[i] = reader.ReadUInt16();
            }
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            for (int i = 0; i < Args.Length; ++i)
            {
                writer.Write(Args[i]);
            }
        }
    }
}
