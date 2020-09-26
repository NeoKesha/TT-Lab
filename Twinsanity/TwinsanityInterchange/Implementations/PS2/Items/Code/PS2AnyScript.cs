using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Code
{
    public class PS2AnyScript : ITwinScript
    {
        UInt32 id;
        private UInt16 scriptID;
        public Byte Mask;

        public UInt32 GetID()
        {
            return id;
        }

        public virtual int GetLength()
        {
            return 4;
        }

        public virtual void Read(BinaryReader reader, int length)
        {
            scriptID = reader.ReadUInt16();
            Mask = reader.ReadByte();
            reader.ReadByte(); // Skip flag
        }

        public void SetID(UInt32 id)
        {
            this.id = id;
        }

        public virtual void Write(BinaryWriter writer)
        {
            writer.Write(scriptID);
            writer.Write(Mask);
            writer.Write((Byte)id % 2);
        }
    }
}
