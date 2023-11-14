using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Global;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData.Global
{
    public class FontData : AbstractAssetData
    {
        public FontData()
        {
            FontPages = new();
            UnkVecs = new();
        }

        public FontData(ITwinPSF psf) : this()
        {
            SetTwinItem(psf);
        }

        [JsonProperty(Required = Required.Always)]
        public List<LabURI> FontPages { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> UnkVecs { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt { get; set; }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }

        public override void Import(LabURI package, String? variant)
        {
            var psf = GetTwinItem<ITwinPSF>();
            var psfIndex = 0;
            foreach (var ptc in psf.FontPages)
            {
                var asset = new PTC(package, $"{psf.GetName()}_{variant}_page_{psfIndex++}", $"{psf.GetName()}_page_{psfIndex++}", ptc);
                AssetManager.Get().AddAssetToImport(asset);
                FontPages.Add(asset.URI);
            }
            UnkVecs = CloneUtils.CloneList(psf.UnkVecs);
            UnkInt = psf.UnkInt;
        }

        protected override void Dispose(Boolean disposing)
        {
            FontPages.Clear();
            UnkVecs.Clear();
        }
    }
}
