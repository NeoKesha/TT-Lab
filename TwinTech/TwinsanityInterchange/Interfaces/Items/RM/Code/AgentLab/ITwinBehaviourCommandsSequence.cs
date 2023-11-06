using System;
using System.Collections.Generic;
using System.IO;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab
{
    public interface ITwinBehaviourCommandsSequence : ITwinItem
    {
        /// <summary>
        /// Unknown header
        /// </summary>
        public Int32 Header { get; set; }
        /// <summary>
        /// Behaviour packs with their IDs
        /// </summary>
        public List<KeyValuePair<UInt16, ITwinBehaviourCommandPack>> BehaviourPacks { get; set; }
        /// <summary>
        /// Command chain
        /// </summary>
        public List<ITwinBehaviourCommand> Commands { get; set; }

        internal bool HasNext { get; set; }

        /// <summary>
        /// Output the sequence's text form
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="tabs"></param>
        public void WriteText(StreamWriter writer, Int32 tabs = 0);
        /// <summary>
        /// Interpret sequence's text form
        /// </summary>
        /// <param name="reader"></param>
        public void ReadText(StreamReader reader);
        public String ToString();
    }
}
