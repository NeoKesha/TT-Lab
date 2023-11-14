using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;

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
            throw new NotImplementedException();
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
