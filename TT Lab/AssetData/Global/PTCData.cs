using Newtonsoft.Json;
using System;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Global
{
    public class PTCData : AbstractAssetData
    {
        public PTCData()
        {
            TextureID = LabURI.Empty;
            MaterialID = LabURI.Empty;
        }

        public PTCData(ITwinPTC ptc) : this()
        {
            SetTwinItem(ptc);
        }

        [JsonProperty(Required = Required.Always)]
        public LabURI TextureID { get; set; }
        [JsonProperty(Required = Required.Always)]
        public LabURI MaterialID { get; set; }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var assetManager = AssetManager.Get();

            var texture = (ITwinTexture)assetManager.GetAssetData<TextureData>(TextureID).Export(factory);
            var material = (ITwinMaterial)assetManager.GetAssetData<MaterialData>(MaterialID).Export(factory);

            return factory.GeneratePTC(assetManager.GetAsset(TextureID).ID, assetManager.GetAsset(MaterialID).ID, texture, material);
        }

        public override void Import(LabURI package, String? variant)
        {
            var assetManager = AssetManager.Get();
            var ptc = GetTwinItem<ITwinPTC>();
            var texture = new Texture(package, $"{ptc.GetName()}_Texture_{variant}", ptc.TexID, $"{ptc.GetName()}_Texture", ptc.Texture);
            assetManager.AddAssetToImport(texture);
            var material = new Material(package, $"{ptc.GetName()}_Material_{variant}", ptc.MatID, $"{ptc.GetName()}_Material", ptc.Material);
            assetManager.AddAssetToImport(material);

            TextureID = texture.URI;
            MaterialID = material.URI;
        }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
