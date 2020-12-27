using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData.Graphics.SubModels;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class ModelData : AbstractAssetData
    {
        public ModelData()
        {
        }

        public ModelData(PS2AnyModel model) : this()
        {
            SubModels = new List<SubModelData>();
            foreach (var e in model.SubModels)
            {
                e.CalculateData();
                SubModels.Add(new SubModelData(e));
            }
        }
        [JsonProperty(Required = Required.Always)]
        List<SubModelData> SubModels { get; set; }
        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
