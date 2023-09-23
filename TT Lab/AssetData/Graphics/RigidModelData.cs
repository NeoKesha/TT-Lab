using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
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
            SetTwinItem(rigidModel);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> Materials { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI Model { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(String package, String subpackage, String? variant)
        {
            PS2AnyRigidModel rigidModel = GetTwinItem<PS2AnyRigidModel>();
            Header = rigidModel.Header;
            Materials = new List<LabURI>();
            foreach (var mat in rigidModel.Materials)
            {
                Materials.Add(AssetManager.Get().GetUri(package, subpackage, typeof(Material).Name, variant, mat)/*GuidManager.GetGuidByTwinId(mat, typeof(Material))*/);
            }
            Model = AssetManager.Get().GetUri(package, subpackage, typeof(Model).Name, variant, rigidModel.Model);//GuidManager.GetGuidByTwinId(rigidModel.Model, typeof(Model));
        }
    }
}
