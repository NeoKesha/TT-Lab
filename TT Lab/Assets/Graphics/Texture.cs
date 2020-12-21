using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class Texture : SerializableAsset
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }

        public override String Type => "Texture";

        public Texture(UInt32 id, String name, PS2AnyTexture texture) : base(id, name)
        {
            Header = texture.HeaderSignature;
        }

        public Texture()
        {
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }
    }
}
