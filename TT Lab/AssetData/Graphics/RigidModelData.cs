using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class RigidModelData : AbstractAssetData
    {
        public RigidModelData()
        {
        }

        public RigidModelData(PS2AnyRigidModel rigidModel) : this()
        {
            Header = rigidModel.Header;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
