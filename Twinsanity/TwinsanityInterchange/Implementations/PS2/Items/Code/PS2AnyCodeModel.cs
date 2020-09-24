using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Code
{
    public class PS2AnyCodeModel : ITwinCodeModel
    {
        UInt32 id;
        public Int32 Header;
        public List<ScriptPack> ScriptPacks;
        public List<UInt16> ScriptIDs;
        public ScriptCommand Command;

        public Byte PacksAmount
        {
            get
            {
                return (Byte)(Header >> 16 & 0xFF);
            }
            set
            {
                Header = (Int32)(Header & 0xFF00FFFF) | (value << 16);
            }
        }

        public PS2AnyCodeModel()
        {
            ScriptPacks = new List<ScriptPack>();
            ScriptIDs = new List<UInt16>();
        }

        public uint GetID()
        {
            return id;
        }

        public int GetLength()
        {
            return 4 + ScriptPacks.Sum((pack) => pack.GetLength()) + ScriptIDs.Count * Constants.SIZE_UINT16 + Command.GetLength();
        }

        public void Read(BinaryReader reader, int length)
        {
            Header = reader.ReadInt32();
            for (var i = 0; i < PacksAmount; ++i)
            {
                var pack = new ScriptPack();
                pack.Read(reader, length);
                ScriptPacks.Add(pack);
                ScriptIDs.Add(reader.ReadUInt16());
            }
            Command = new ScriptCommand();
            Command.Read(reader, length);
        }

        public void SetID(uint id)
        {
            this.id = id;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Header);
            for (var i = 0; i < PacksAmount; ++i)
            {
                ScriptPacks[i].Write(writer);
                writer.Write(ScriptIDs[i]);
            }
            Command.Write(writer);
        }
    }
}
