using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    public class RigidModelData : AbstractAssetData
    {
        public RigidModelData()
        {
        }

        public RigidModelData(ITwinRigidModel rigidModel) : this()
        {
            SetTwinItem(rigidModel);
        }

        [JsonProperty(Required = Required.Always)]
        public List<LabURI> Materials { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI Model { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Materials.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinRigidModel rigidModel = GetTwinItem<ITwinRigidModel>();
            Materials = new List<LabURI>();
            foreach (var mat in rigidModel.Materials)
            {
                Materials.Add(AssetManager.Get().GetUri(package, typeof(Material).Name, variant, mat));
            }
            Model = AssetManager.Get().GetUri(package, typeof(Model).Name, variant, rigidModel.Model);
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(257); // Unused header
            writer.Write(Materials.Count);
            foreach (var mat in Materials)
            {
                writer.Write(assetManager.GetAsset(mat).ID);
            }
            writer.Write(assetManager.GetAsset(Model).ID);

            ms.Position = 0;
            return factory.GenerateRigidModel(ms);
        }
    }
}
