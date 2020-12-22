using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class GameObjectData : AbstractAssetData
    {
        public GameObjectData()
        {
        }

        public GameObjectData(PS2AnyObject @object) : this()
        {
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Bitfield { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
