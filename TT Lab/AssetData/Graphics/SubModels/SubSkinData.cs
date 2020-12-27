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
        public List<Vector4> Vertexes { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> UVW { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Normals { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Colors { get; set; }


        public SubSkinData()
        {

        }
        public SubSkinData(SubSkin twinSubSkin)
        {
            Vertexes = CloneUtils.CloneList(twinSubSkin.Vertexes);
            UVW = CloneUtils.CloneList(twinSubSkin.UVW);
            Normals = CloneUtils.CloneList(twinSubSkin.Normals);
            Colors = CloneUtils.CloneList(twinSubSkin.Colors);
            Material = twinSubSkin.Material;
        }
    }
}
