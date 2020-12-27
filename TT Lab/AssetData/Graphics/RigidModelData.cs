using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            Materials = CloneUtils.CloneList(rigidModel.Materials);
            Model = rigidModel.Model;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<UInt32> Materials { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 Model { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
