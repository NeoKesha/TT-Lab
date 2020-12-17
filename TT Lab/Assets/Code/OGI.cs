using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class OGI : SerializableAsset
    {
        [JsonProperty(Required = Required.Always)]
        public Vector4[] BoundingBox { get; set; } = new Vector4[2];

        public override String Type => "OGI";

        public OGI(UInt32 id, String name, PS2AnyOGI ogi) : base(id, name)
        {
            BoundingBox = ogi.BoundingBox;
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
