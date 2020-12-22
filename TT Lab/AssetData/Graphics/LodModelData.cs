﻿using Newtonsoft.Json;
using System;
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
            Type = lod.Type;
            UnkInt1 = lod.UnkInt1;
            UnkInt2 = lod.UnkInt2;
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 Type { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt2 { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}