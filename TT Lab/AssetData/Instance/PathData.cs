using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
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
            SetTwinItem(path);
        }

        [JsonProperty(Required = Required.Always)]
        public List<Vector4> Points { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<Vector2> Parameters { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Points.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            PS2AnyPath path = GetTwinItem<PS2AnyPath>();
            Points = CloneUtils.CloneList(path.PointList);
            Parameters = CloneUtils.CloneList(path.ParameterList);
        }
    }
}
