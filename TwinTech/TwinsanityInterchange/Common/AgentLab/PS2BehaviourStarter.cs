using System;
using System.Collections.Generic;
using System.IO;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class PS2BehaviourStarter : PS2BehaviourWrapper
    {
        public List<KeyValuePair<Int32, UInt32>> Pairs;

        public PS2BehaviourStarter()
        {
            Pairs = new List<KeyValuePair<Int32, UInt32>>();
        }

        public override int GetLength()
        {
            return base.GetLength() + 4 + Pairs.Count * (Constants.SIZE_UINT32 + Constants.SIZE_UINT32);
        }

        public override String GetName()
        {
            return $"Behaviour Starter {id:X}";
        }

        public override void Read(BinaryReader reader, int length)
        {
            base.Read(reader, length);
            var amount = reader.ReadUInt32();
            for (var i = 0; i < amount; ++i)
            {
                Pairs.Add(new KeyValuePair<Int32, UInt32>(reader.ReadInt32(), reader.ReadUInt32()));
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(Pairs.Count);
            foreach (var pair in Pairs)
            {
                writer.Write(pair.Key);
                writer.Write(pair.Value);
            }
        }
    }
}
