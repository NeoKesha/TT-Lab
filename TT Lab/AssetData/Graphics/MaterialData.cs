﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData.Graphics.Shaders;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;
using static Twinsanity.TwinsanityInterchange.Enumerations.Enums;

namespace TT_Lab.AssetData.Graphics
{
    public class MaterialData : AbstractAssetData
    {
        public MaterialData()
        {
        }

        public MaterialData(ITwinMaterial material) : this()
        {
            SetTwinItem(material);
        }

        [JsonProperty(Required = Required.Always)]
        public AppliedShaders ActivatedShaders { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 DmaChainIndex { get; set; }
        [JsonProperty(Required = Required.Always)]
        public String Name { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabShader> Shaders { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            Shaders.Clear();
        }

        public override void Import(LabURI package, String? variant)
        {
            ITwinMaterial material = GetTwinItem<ITwinMaterial>();
            ActivatedShaders = material.ActivatedShaders;
            DmaChainIndex = material.DmaChainIndex;
            Name = new string(material.Name.ToCharArray());
            Shaders = new List<LabShader>();
            foreach (var shader in material.Shaders)
            {
                Shaders.Add(new LabShader(package, variant, shader));
            }
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            throw new NotImplementedException();
        }
    }
}
