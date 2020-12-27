using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData.Graphics.SubModels;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class SkinData : AbstractAssetData
    {
        public SkinData()
        {
        }

        public SkinData(PS2AnySkin skin) : this()
        {
            SubSkins = new List<SubSkinData>();
            foreach (var e in skin.SubSkins)
            {
                e.CalculateData();
                SubSkins.Add(new SubSkinData(e));
            }
        }
        [JsonProperty(Required = Required.Always)]
        List<SubSkinData> SubSkins { get; set; }
        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
