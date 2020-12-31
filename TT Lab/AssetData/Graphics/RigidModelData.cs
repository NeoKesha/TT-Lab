using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
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
            twinRef = rigidModel;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Guid> Materials { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Guid Model { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnyRigidModel rigidModel = (PS2AnyRigidModel)twinRef;
            Header = rigidModel.Header;
            Materials = new List<Guid>();
            foreach (var mat in rigidModel.Materials)
            {
                Materials.Add(GuidManager.GetGuidByTwinId(mat, typeof(Material)));
            }
            Model = GuidManager.GetGuidByTwinId(rigidModel.Model, typeof(Model));
        }
    }
}
