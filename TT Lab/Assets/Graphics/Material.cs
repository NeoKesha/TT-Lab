using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class Material : SerializableAsset
    {
        [JsonProperty(Required = Required.Always)]
        public UInt64 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkInt { get; set; }

        public override String Type => "Material";

        public Material(UInt32 id, String name, PS2AnyMaterial material) : base(id, name)
        {
            Header = material.Header;
            UnkInt = material.UnkInt;
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
