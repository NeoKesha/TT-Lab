using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;

namespace TT_Lab.AssetData.Instance
{
    public class DynamicSceneryData : AbstractAssetData
    {
        public DynamicSceneryData()
        {
        }

        public DynamicSceneryData(PS2AnyDynamicScenery dynamicScenery) : this()
        {
            SetTwinItem(dynamicScenery);
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt;

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(String package, String subpackage, String? variant)
        {
            PS2AnyDynamicScenery dynamicScenery = GetTwinItem<PS2AnyDynamicScenery>();
            UnkInt = dynamicScenery.UnkInt;
        }
    }
}
