using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.AssetData.Instance
{
    public class PathData : AbstractAssetData
    {
        public PathData()
        {
        }

        public PathData(ITwinPath path) : this()
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
            ITwinPath path = GetTwinItem<ITwinPath>();
            Points = CloneUtils.CloneList(path.PointList);
            Parameters = CloneUtils.CloneList(path.ParameterList);
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
