using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Global;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.AssetData.Global
{
    public class PSMData : AbstractAssetData
    {
        public PSMData()
        {
            PTCs = new();
        }

        public PSMData(ITwinPSM psm) : this()
        {
            SetTwinItem(psm);
        }

        [JsonProperty(Required = Required.Always)]
        public List<LabURI> PTCs { get; set; }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }

        public override void Import(LabURI package, String? variant)
        {
            var psm = GetTwinItem<ITwinPSM>();
            var ptcIndex = 0;
            foreach (var ptc in psm.PTCs)
            {
                var asset = new PTC(package, $"{psm.GetName()}_{variant}_{ptcIndex++}", $"{psm.GetName()}_{ptcIndex++}", ptc);
                AssetManager.Get().AddAssetToImport(asset);
                PTCs.Add(asset.URI);
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            PTCs.Clear();
        }
    }
}
