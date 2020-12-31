using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Graphics.Shaders;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common;
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
            twinRef = material;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt64 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public String Name { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<LabShader> Shaders { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }

        public override void Import()
        {
            PS2AnyMaterial material = (PS2AnyMaterial)twinRef;
            Header = material.Header;
            UnkInt = material.UnkInt;
            Name = new string(material.Name.ToCharArray());
            Shaders = new List<LabShader>();
            foreach (var shader in material.Shaders)
            {
                Shaders.Add(new LabShader(shader));
            }
        }
    }
}
