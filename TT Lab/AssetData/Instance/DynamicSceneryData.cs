using Newtonsoft.Json;
using System;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.AssetData.Instance
{
    public class DynamicSceneryData : AbstractAssetData
    {
        public DynamicSceneryData()
        {
        }

        public DynamicSceneryData(ITwinDynamicScenery dynamicScenery) : this()
        {
            SetTwinItem(dynamicScenery);
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt;

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinDynamicScenery dynamicScenery = GetTwinItem<ITwinDynamicScenery>();
            UnkInt = dynamicScenery.UnkInt;
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
