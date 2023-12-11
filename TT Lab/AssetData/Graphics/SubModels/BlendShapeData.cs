using Newtonsoft.Json;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class BlendShapeData
    {
        [JsonProperty(Required = Required.Always)]
        public Vector4 Offset { get; set; } = new();
    }
}
