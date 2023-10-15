using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab
{
    public interface ITwinBehaviourStateBody : ITwinSerializable
    {
        public UInt32 Bitfield { get; set; }
        public Boolean HasStateJump { get; set; }
        public Int32 JumpToState { get; set; }
        public TwinBehaviourCondition Condition { get; set; }
        public List<ITwinBehaviourCommand> Commands { get; set; }

        internal bool HasNext { get; set; }

        public void WriteText(StreamWriter writer, Int32 tabs = 0);
        public void ReadText(StreamReader reader);
    }
}
