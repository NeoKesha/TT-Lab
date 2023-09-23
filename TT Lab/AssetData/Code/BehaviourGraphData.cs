using Newtonsoft.Json;
using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;

namespace TT_Lab.AssetData.Code
{
    public class BehaviourGraphData : BehaviourData
    {
        public BehaviourGraphData()
        {
        }

        public BehaviourGraphData(PS2BehaviourGraph mainScript) : base(mainScript)
        {
            SetTwinItem(mainScript);
        }

        public String Script { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Save(string dataPath, JsonSerializerSettings? settings = null)
        {
            using var fs = new FileStream(dataPath, FileMode.Create, FileAccess.Write);
            using var writer = new BinaryWriter(fs);
            writer.Write(Script.ToCharArray());
        }

        public override void Load(String dataPath, JsonSerializerSettings? settings = null)
        {
            using var fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(fs);
            Script = reader.ReadToEnd();
        }

        public override void Import(String package, String subpackage, String? variant)
        {
            PS2BehaviourGraph graph = GetTwinItem<PS2BehaviourGraph>();
            Script = graph.ToString();
        }
    }
}
