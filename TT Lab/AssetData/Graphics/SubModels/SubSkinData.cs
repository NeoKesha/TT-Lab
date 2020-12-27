using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    [JsonObject]
    public class SubSkinData
    {
        [JsonProperty(Required = Required.Always)]
        UInt32 Material { get; set; }
        [JsonProperty(Required = Required.Always)]
        List<List<Vector4>> Data { get; set; }

        public SubSkinData()
        {

        }
        public SubSkinData(SubSkin twinSubSkin)
        {
            Data = CloneUtils.DeepClone(twinSubSkin.Data);
            Material = twinSubSkin.Material;
        }
    }
}
