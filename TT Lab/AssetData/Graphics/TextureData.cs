using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;
using static Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics.PS2AnyTexture;

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
            ImageWidthPower = texture.ImageWidthPower;
            ImageHeightPower = texture.ImageHeightPower;
            MipLevels = texture.MipLevels;
            TextureFormat = texture.TextureFormat;
            DestinationTextureFormat = texture.DestinationTextureFormat;
            ColorComponent = texture.ColorComponent;
            TexFun = texture.TexFun;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 ImageWidthPower { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 ImageHeightPower { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte MipLevels { get; set; }
        [JsonProperty(Required = Required.Always)]
        public TexturePixelFormat TextureFormat { get; set; }
        [JsonProperty(Required = Required.Always)]
        public TexturePixelFormat DestinationTextureFormat { get; set; }
        [JsonProperty(Required = Required.Always)]
        public TextureColorComponent ColorComponent { get; set; }
        [JsonProperty(Required = Required.Always)]
        public TextureFunction TexFun { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
