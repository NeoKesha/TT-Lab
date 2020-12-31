using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class PathData : AbstractAssetData
    {
        public PathData()
        {
        }

        public PathData(PS2AnyPath path) : this()
        {
            twinRef = path;
        }

        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Points { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Vector2> Parameters { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Points.Clear();
        }

        public override void Import()
        {
            PS2AnyPath path = (PS2AnyPath)twinRef;
            Points = CloneUtils.CloneList(path.PointList);
            Parameters = CloneUtils.CloneList(path.ParameterList);
        }
    }
}
