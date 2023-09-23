using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class LodModelData : AbstractAssetData
    {
        public LodModelData()
        {
        }

        public LodModelData(PS2AnyLOD lod) : this()
        {
            SetTwinItem(lod);
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 Type { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 MinDrawDistance { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 MaxDrawDistance { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32[] ModelsDrawDistances { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabURI> Meshes { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(String package, String subpackage, String? variant)
        {
            PS2AnyLOD lod = GetTwinItem<PS2AnyLOD>();
            Type = lod.Type;
            MinDrawDistance = lod.MinDrawDistance;
            MaxDrawDistance = lod.MaxDrawDistance;
            ModelsDrawDistances = CloneUtils.CloneArray(lod.ModelsDrawDistances);
            Meshes = new List<LabURI>();
            foreach (var mesh in lod.Meshes)
            {
                Meshes.Add(AssetManager.Get().GetUri(package, subpackage, typeof(Mesh).Name, variant, mesh));
            }
        }
    }
}
