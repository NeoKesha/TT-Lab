using Newtonsoft.Json;
using System;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class LodModel : SerializableAsset
    {
        [JsonProperty(Required = Required.Always)]
        public Int32 LodType { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Int32 UnkInt2 { get; set; }

        public override String Type => "LodModel";

        public LodModel(UInt32 id, String name, PS2AnyLOD lod) : base(id, name)
        {
            LodType = lod.Type;
            UnkInt1 = lod.UnkInt1;
            UnkInt2 = lod.UnkInt2;
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
