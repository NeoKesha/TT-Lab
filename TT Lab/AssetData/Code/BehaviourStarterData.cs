using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData.Code
{
    public class BehaviourStarterData : BehaviourData
    {
        public BehaviourStarterData()
        {
        }

        public BehaviourStarterData(TwinBehaviourStarter headerScript) : base(headerScript)
        {
            SetTwinItem(headerScript);
        }

        [JsonProperty(Required = Required.Always)]
        public List<KeyValuePair<LabURI, UInt32>> Pairs { get; set; } = new();

        protected override void Dispose(Boolean disposing)
        {
            Pairs.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            TwinBehaviourStarter behaviourStarter = GetTwinItem<TwinBehaviourStarter>();
            foreach (var pair in behaviourStarter.Pairs)
            {
                if (pair.Key - 1 == -1)
                {
                    Pairs.Add(new KeyValuePair<LabURI, uint>(LabURI.Empty, pair.Value));
                }
                else
                {
                    Pairs.Add(new KeyValuePair<LabURI, uint>(AssetManager.Get().GetUri(package, typeof(BehaviourGraph).Name, variant, (UInt32)pair.Key - 1), pair.Value));
                }
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
