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
        /// <summary>
        /// Unknown bitfield
        /// </summary>
        public UInt16 Bitfield { get; set; }
        /// <summary>
        /// This can be a script index or point to a slot(event) in the object
        /// </summary>
        public Int16 ScriptIndexOrSlot { get; set; }
        /// <summary>
        /// Control packet for manipulation position, speed, etc. of the object executing the behaviour
        /// </summary>
        public TwinBehaviourControlPacket ControlPacket { get; set; }
        /// <summary>
        /// State bodies
        /// </summary>
        public List<ITwinBehaviourStateBody> Bodies { get; set; }

        internal bool HasNext { get; set; }

        public void Read(BinaryReader reader, int length, IList<ITwinBehaviourState> scriptStates);
        public void WriteText(StreamWriter writer, Int32 i, Int32 tabs = 0);
        public void ReadText(StreamReader reader);
    }
}
