using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TT_Lab.AssetData.Graphics.Shaders;
using TT_Lab.Assets;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class MaterialData : AbstractAssetData
    {
        public MaterialData()
        {
        }

        public MaterialData(PS2AnyMaterial material) : this()
        {
            SetTwinItem(material);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt64 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 DmaChainIndex { get; set; }
        [JsonProperty(Required = Required.Always)]
        public String Name { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabShader> Shaders { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import(LabURI package, String? variant)
        {
            PS2AnyMaterial material = GetTwinItem<PS2AnyMaterial>();
            Header = material.Header;
            DmaChainIndex = material.DmaChainIndex;
            Name = new string(material.Name.ToCharArray());
            Shaders = new List<LabShader>();
            foreach (var shader in material.Shaders)
            {
                Shaders.Add(new LabShader(package, variant, shader));
            }
        }
    }
}
