using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData.Code.Behaviour
{
    public class BehaviourStarterData : BehaviourData
    {
        public BehaviourStarterData()
        {
        }

        public BehaviourStarterData(TwinBehaviourStarter behaviourStarter) : base(behaviourStarter)
        {
            SetTwinItem(behaviourStarter);
        }

        [JsonProperty(Required = Required.Always)]
        public List<BehaviourAssignerData> Assigners { get; set; } = new();

        protected override void Dispose(Boolean disposing)
        {
            Assigners.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            TwinBehaviourStarter behaviourStarter = GetTwinItem<TwinBehaviourStarter>();
            foreach (var assigner in behaviourStarter.Assigners)
            {
                Assigners.Add(new BehaviourAssignerData(package, variant, assigner));
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
