using Newtonsoft.Json;
using System;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code.AgentLab;

namespace TT_Lab.AssetData.Code.Behaviour
{
    public class BehaviourGraphData : BehaviourData
    {
        public BehaviourGraphData()
        {
        }

        public BehaviourGraphData(ITwinBehaviourGraph mainScript) : base(mainScript)
        {
            SetTwinItem(mainScript);
        }

        public String Graph { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Graph = "";
        }

        public override void Save(string dataPath, JsonSerializerSettings? settings = null)
        {
            using var fs = new FileStream(dataPath, FileMode.Create, FileAccess.Write);
            using var writer = new BinaryWriter(fs);
            writer.Write(Graph.ToCharArray());
        }

        protected override void LoadInternal(String dataPath, JsonSerializerSettings? settings = null)
        {
            using var fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(fs);
            Graph = reader.ReadToEnd();
        }

        public override void Import(LabURI package, String? variant, Int32? layoutId)
        {
            var graph = GetTwinItem<ITwinBehaviourGraph>();
            Graph = graph.ToString();
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            writer.Write(Graph);
            writer.Flush();

            ms.Position = 0;
            return factory.GenerateBehaviourGraph(ms);
        }
    }
}
