using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class RigidModel : SerializableAsset
    {
        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }

        public override String Type => "RigidModel";

        public RigidModel(UInt32 id, String name, PS2AnyRigidModel rigidModel) : base(id, name)
        {
            Header = rigidModel.Header;
        }

        public RigidModel()
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
