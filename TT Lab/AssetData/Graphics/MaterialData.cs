using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Header = material.Header;
            UnkInt = material.UnkInt;
            Name = new string(material.Name.ToCharArray());
            Shaders = CloneUtils.DeepClone(material.Shaders);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt64 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public String Name { get; set; }
        [JsonProperty(Required = Required.Always)]
        public List<TwinShader> Shaders { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
