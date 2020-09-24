using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class PS2HeaderScript : ITwinSerializable
    {
        public UInt32 UnkPairsAmount;
        public List<Int32> MainScriptIndices;
        public List<UInt32> UnkUI32s;

        public PS2HeaderScript()
        {
            MainScriptIndices = new List<Int32>();
            UnkUI32s = new List<UInt32>();
        }

        public int GetLength()
        {
            return 4 + MainScriptIndices.Count * Constants.SIZE_UINT32 + UnkUI32s.Count * Constants.SIZE_UINT32;
        }

        public void Read(BinaryReader reader, int length)
        {
            UnkPairsAmount = reader.ReadUInt32();
            for (var i = 0; i < UnkPairsAmount; ++i)
            {
                MainScriptIndices.Add(reader.ReadInt32());
                UnkUI32s.Add(reader.ReadUInt32());
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(UnkPairsAmount);
            for (var i = 0; i < UnkPairsAmount; ++i)
            {
                writer.Write(MainScriptIndices[i]);
                writer.Write(UnkUI32s[i]);
            }
        }
    }
}
