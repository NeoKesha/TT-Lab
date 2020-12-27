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
    public class SubModelData
    {
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Vertexes { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> UVW { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Normals { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Colors { get; set; }

        public SubModelData()
        {

        }
        public SubModelData(SubModel twinSubModel)
        {
            Vertexes = CloneUtils.CloneList(twinSubModel.Vertexes);
            UVW = CloneUtils.CloneList(twinSubModel.UVW);
            Normals = CloneUtils.CloneList(twinSubModel.Normals);
            Colors = CloneUtils.CloneList(twinSubModel.Colors);
        }
    }
}
