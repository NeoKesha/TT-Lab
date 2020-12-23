using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.AssetData.Graphics
{
    public class TextureData : AbstractAssetData
    {
        public TextureData()
        {
        }

        public TextureData(PS2AnyTexture texture) : this()
        {
            Header = texture.HeaderSignature;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
