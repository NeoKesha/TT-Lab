using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab
{
    public interface ITwinBehaviourCommandsSequence : ITwinItem
    {
        public Int32 Header { get; set; }
        public List<KeyValuePair<UInt16, ITwinBehaviourCommandPack>> BehaviourPacks { get; set; }
        public List<ITwinBehaviourCommand> Commands { get; set; }

        internal bool HasNext { get; set; }

        public void WriteText(StreamWriter writer, Int32 tabs = 0);
        public void ReadText(StreamReader reader);
        public String ToString();
    }
}
