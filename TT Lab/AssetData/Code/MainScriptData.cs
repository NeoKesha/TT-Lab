using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            using(MemoryStream stream = new MemoryStream())
            using(StreamWriter writer = new StreamWriter(stream))
            using(StreamReader reader = new StreamReader(stream))
            {
                mainScript.WriteText(writer);
                stream.Position = 0;
                Script = reader.ReadToEnd();
            }
        }

        [JsonProperty(Required = Required.Always)]
        public String Script { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Save(string dataPath)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(dataPath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs))
            {
                writer.Write(Script);
            }
        }
    }
}
