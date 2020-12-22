using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            UnkInt = dynamicScenery.UnkInt;
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt;

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
