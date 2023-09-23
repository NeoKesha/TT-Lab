using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;

namespace TT_Lab.AssetData.Code
{
    public class BehaviourStarterData : BehaviourData
    {
        public BehaviourStarterData()
        {
        }

        public BehaviourStarterData(PS2BehaviourStarter headerScript) : base(headerScript)
        {
            SetTwinItem(headerScript);
        }

        [JsonProperty(Required = Required.Always)]
        public List<KeyValuePair<LabURI, UInt32>> Pairs { get; set; } = new();

        protected override void Dispose(Boolean disposing)
        {
            Pairs.Clear();
        }

        public override void Import(String package, String subpackage, String? variant)
        {
            PS2BehaviourStarter behaviourStarter = GetTwinItem<PS2BehaviourStarter>();
            foreach (var pair in behaviourStarter.Pairs)
            {
                if (pair.Key - 1 == -1)
                {
                    Pairs.Add(new KeyValuePair<LabURI, uint>(LabURI.Empty, pair.Value));
                }
                else
                {
                    Pairs.Add(new KeyValuePair<LabURI, uint>(AssetManager.Get().GetUri(package, subpackage, typeof(BehaviourStarter).Name, variant, pair.Value), pair.Value));
                }
            }
        }
    }
}
