using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab
{
    public interface ITwinBehaviourState : ITwinSerializable
    {
        public UInt16 Bitfield { get; set; }
        public Int16 ScriptIndexOrSlot { get; set; }
        public TwinBehaviourControlPacket ControlPacket { get; set; }
        public List<ITwinBehaviourStateBody> Bodies { get; set; }

        internal bool HasNext { get; set; }

        public void Read(BinaryReader reader, int length, IList<ITwinBehaviourState> scriptStates);
        public void WriteText(StreamWriter writer, Int32 i, Int32 tabs = 0);
        public void ReadText(StreamReader reader);
    }
}
