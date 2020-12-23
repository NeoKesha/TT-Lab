using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class MainScriptData : ScriptData
    {
        public MainScriptData()
        {
        }

        public MainScriptData(PS2MainScript mainScript) : base(mainScript)
        {
            UnkInt = mainScript.UnkInt;
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
