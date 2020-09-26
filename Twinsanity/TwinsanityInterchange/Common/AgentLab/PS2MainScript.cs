using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Code;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class PS2MainScript : PS2AnyScript
    {
        public String Name;
        public Int32 UnkInt;
        public List<ScriptState> ScriptStates;

        public PS2MainScript()
        {
            ScriptStates = new List<ScriptState>();
        }

        public override int GetLength()
        {
            var totalLen = 4 + Name.Length + 8;
            totalLen += ScriptStates.Sum(s => s.GetLength());
            foreach (var state in ScriptStates)
            {
                if (state.Body != null)
                {
                    totalLen += state.Body.GetLength();
                }
            }
            totalLen += base.GetLength();
            return totalLen;
        }

        public override void Read(BinaryReader reader, int length)
        {
            base.Read(reader, length);
            var nameLen = reader.ReadInt32();
            Name = new String(reader.ReadChars(nameLen));
            var statesAmt = reader.ReadInt32();
            UnkInt = reader.ReadInt32();
            ScriptStates.Clear();
            if (statesAmt > 0)
            {
                var st = new ScriptState();
                ScriptStates.Add(st);
                st.Read(reader, length, ScriptStates);
                foreach (var state in ScriptStates)
                {
                    if ((state.Bitfield & 0x1F) != 0)
                    {
                        state.Body = new ScriptStateBody();
                        state.Body.Read(reader, length);
                    }
                }
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray());
            writer.Write(ScriptStates.Count);
            writer.Write(UnkInt);
            for (var i = 0; i < ScriptStates.Count; ++i)
            {
                var state = ScriptStates[i];
                if (i + 1 < ScriptStates.Count)
                {
                    state.next = ScriptStates[i + 1];
                }
            }
            if (ScriptStates.Count > 0)
            {
                ScriptStates[0].Write(writer);
                foreach (var state in ScriptStates)
                {
                    if (state.Body != null)
                    {
                        state.Body.Write(writer);
                    }
                }
            }
        }
    }
}
