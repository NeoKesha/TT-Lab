using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab
{
    public interface ITwinBehaviourGraph : ITwinBehaviour
    {
        public String Name { get; set; }
        public Int32 UnkInt { get; set; }
        public List<ITwinBehaviourState> ScriptStates { get; set; }

        public void WriteText(StreamWriter writer, Int32 tabs = 0);

        public void ReadText(StreamReader reader);
        public String ToString();
    }
}
