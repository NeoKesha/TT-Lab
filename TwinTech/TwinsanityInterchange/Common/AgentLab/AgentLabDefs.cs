using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Twinsanity.TwinsanityInterchange.Common.AgentLab
{
    public class AgentLabDefs
    {
        public Dictionary<String, String> condition_map { get; set; }
        public Dictionary<String, AgentLabCommandDef> command_map { get; set; }
    }
    public class AgentLabCommandDef
    {
        public String name { get; set; }
        public List<String> arguments { get; set; }
    }
}
