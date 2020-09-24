using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class PS2MainScript : ITwinSerializable
    {
        public String Name;
        public Int32 StatesAmount;
        public Int32 UnkInt;
        public PS2ScriptState ScriptState;

        public int GetLength()
        {
            var totalLen = 4 + Name.Length + 8;
            totalLen += (StatesAmount > 0 ? ScriptState.GetLength() : 0);
            var state = ScriptState;
            while (state != null)
            {
                if (state.HasBody)
                {
                    totalLen += state.Body.GetLength();
                }
                state = state.NextState;
            }
            return totalLen;
        }

        public void Read(BinaryReader reader, int length)
        {
            var nameLen = reader.ReadInt32();
            Name = new String(reader.ReadChars(nameLen));
            StatesAmount = reader.ReadInt32();
            UnkInt = reader.ReadInt32();
            if (StatesAmount > 0)
            {
                ScriptState = new PS2ScriptState();
                ScriptState.Read(reader, length);
                var state = ScriptState;
                while (state != null)
                {
                    if (state.HasBody)
                    {
                        state.Body = new PS2ScriptStateBody();
                        state.Body.Read(reader, length);
                    }
                    state = state.NextState;
                }
            }
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray());
            writer.Write(StatesAmount);
            writer.Write(UnkInt);
            if (StatesAmount > 0)
            {
                ScriptState.Write(writer);
                var state = ScriptState;
                while (state != null)
                {
                    if (state.HasBody)
                    {
                        state.Body.Write(writer);
                    }
                    state = state.NextState;
                }
            }
        }
    }
}
