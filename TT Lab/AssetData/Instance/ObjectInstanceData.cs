using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class ObjectInstanceData : AbstractAssetData
    {
        public ObjectInstanceData()
        {
        }

        public ObjectInstanceData(PS2AnyInstance instance) : this()
        {
            Position = instance.Position;
        }

        [JsonProperty(Required = Required.Always)]
        public Vector4 Position { get; set; } = new Vector4();

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
