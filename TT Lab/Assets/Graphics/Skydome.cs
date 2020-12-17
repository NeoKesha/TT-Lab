using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class Skydome : SerializableAsset
    {
        [JsonProperty(Required = Required.Always)]
        public Int32 Header { get; set; }

        public override String Type => "Skydome";

        public Skydome(UInt32 id, String name, PS2AnySkydome skydome) : base(id, name)
        {
            Header = skydome.Header;
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
