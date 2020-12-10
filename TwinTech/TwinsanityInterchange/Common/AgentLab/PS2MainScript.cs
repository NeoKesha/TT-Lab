﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class PS2MainScript : PS2AnyScript
    {
        private static AgentLabDefs AgentLabDefs = null;

        public static AgentLabDefs GetAgentLabDefs()
        {
            if (AgentLabDefs == null)
            {
                using (FileStream stream = new FileStream(@"AgentLabDefs.json", FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(stream))
                {
                    AgentLabDefs = JsonSerializer.Deserialize<AgentLabDefs>(reader.ReadToEnd());
                }            
            }
            return AgentLabDefs;
        }

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
                state.Read(reader, length);
                ScriptStates.Add(state);
            }
            foreach (var state in ScriptStates)
            {
                state.Bodies.Clear();
                var bodiesAmt = (state.Bitfield & 0x1F);
                for (var i = 0; i < bodiesAmt; ++i)
                {
                    var stateBody = new ScriptStateBody();
                    state.Bodies.Add(stateBody);
                    stateBody.Read(reader, length);
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
            foreach (var state in ScriptStates)
            {
                state.hasNext = !(ScriptStates.Last().Equals(state));
                state.Write(writer);
            }
            foreach (var state in ScriptStates)
            {
                foreach (var body in state.Bodies)
                {
                    body.hasNext = !(state.Bodies.Last().Equals(body));
                    body.Write(writer);
                }
            }
        }

        public void WriteText(StreamWriter writer)
        {
            writer.WriteLine($"Script({Name}) {"{"}");
            writer.WriteLine($"    bitfield = {UnkInt}");
            foreach(var state in ScriptStates)
            {
                state.WriteText(writer);
            }
            writer.WriteLine("}");
            writer.Flush();
        }

        public void ReadText(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
