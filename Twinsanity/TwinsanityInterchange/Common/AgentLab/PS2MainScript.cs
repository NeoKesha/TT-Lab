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
            for (int i = 0; i < statesAmt; ++i)
            {
                ScriptState state = new ScriptState();
                state.Read(reader, 0);
                ScriptStates.Add(state);
            }
            foreach (var state in ScriptStates)
            {
                if ((state.Bitfield & 0x1F) != 0)
                {
                    state.Body = new ScriptStateBody();
                    state.Body.Read(reader, length);
                }
            }
            int calculated = GetLength();
            if (calculated != length)
            {
                int a = 0; // i know about conditional breakpoints, but that's easier to copy paste
            }
        }

        public override void Write(BinaryWriter writer)
        {
            int pos1 = (int)writer.BaseStream.Position;
            base.Write(writer);
            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray());
            writer.Write(ScriptStates.Count);
            writer.Write(UnkInt);
            foreach (ScriptState state in ScriptStates)
            {
                state.hasNext = !(ScriptStates.Last().Equals(state));
                state.Write(writer);
            }
            foreach (ScriptState state in ScriptStates)
            {
                if (state.Body != null)
                {
                    state.Body.Write(writer);
                }
            }
            int pos2 = (int)writer.BaseStream.Position;
            int writen = pos2 - pos1;
            int calculated = GetLength();
            if (writen != calculated)
            {
                int a = 0; // i know about conditional breakpoints, but that's easier to copy paste
            }
        }
    }
}
