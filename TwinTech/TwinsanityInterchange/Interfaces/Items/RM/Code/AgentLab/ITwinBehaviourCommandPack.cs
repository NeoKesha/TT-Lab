using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab
{
    public interface ITwinBehaviourCommandPack : ITwinSerializable
    {
        public List<ITwinBehaviourCommand> Commands { get; set; }

        public void WriteText(StreamWriter writer, Int32 tabs = 0);
        public void ReadText(StreamReader reader);
        public String ToString();
    }
}
