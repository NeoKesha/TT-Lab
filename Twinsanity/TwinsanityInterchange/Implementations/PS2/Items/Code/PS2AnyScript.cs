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
        public Byte Flag;
        public PS2HeaderScript Header;
        public PS2MainScript Main;
        public bool IsHeader
        {
            get
            {
                return (scriptID & 0x1) == 0;
            }
        }

        public UInt32 GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return 4 + (IsHeader ? Header.GetLength() : Main.GetLength());
        }

        public void Read(BinaryReader reader, int length)
        {
            scriptID = reader.ReadUInt16();
            Mask = reader.ReadByte();
            Flag = reader.ReadByte();
            if (IsHeader)
            {
                Header = new PS2HeaderScript();
                Header.Read(reader, length);
            }
            else
            {
                Main = new PS2MainScript();
                Main.Read(reader, length);
            }
        }

        public void SetID(UInt32 id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(scriptID);
            writer.Write(Mask);
            writer.Write(Flag);
            if (IsHeader)
            {
                Header.Write(writer);
            }
            else
            {
                Main.Write(writer);
            }
        }
    }
}
