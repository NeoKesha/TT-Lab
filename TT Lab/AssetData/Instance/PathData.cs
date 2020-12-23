using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Points.AddRange(path.PointList);
        }

        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Points { get; set; } = new List<Vector4>();

        protected override void Dispose(Boolean disposing)
        {
            Points.Clear();
        }
    }
}
