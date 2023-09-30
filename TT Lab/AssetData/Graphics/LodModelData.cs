﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using TT_Lab.Assets.Graphics;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.AssetData.Graphics
{
    public class LodModelData : AbstractAssetData
    {
        public LodModelData()
        {
        }

        public LodModelData(ITwinLOD lod) : this()
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

        public override void Import(LabURI package, String? variant)
        {
            ITwinLOD lod = GetTwinItem<ITwinLOD>();
            Type = lod.Type;
            MinDrawDistance = lod.MinDrawDistance;
            MaxDrawDistance = lod.MaxDrawDistance;
            ModelsDrawDistances = CloneUtils.CloneArray(lod.ModelsDrawDistances);
            Meshes = new List<LabURI>();
            foreach (var mesh in lod.Meshes)
            {
                Meshes.Add(AssetManager.Get().GetUri(package, typeof(Mesh).Name, variant, mesh));
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
