using Newtonsoft.Json;
using System;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData.Code
{
    public class BehaviourGraphData : BehaviourData
    {
        public BehaviourGraphData()
        {
        }

        public BehaviourGraphData(TwinBehaviourGraph mainScript) : base(mainScript)
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

        public override void Import(LabURI package, String? variant)
        {
            TwinBehaviourGraph graph = GetTwinItem<TwinBehaviourGraph>();
            Script = graph.ToString();
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
