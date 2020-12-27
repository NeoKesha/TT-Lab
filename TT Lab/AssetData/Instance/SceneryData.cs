using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Common.Lights;
using Twinsanity.TwinsanityInterchange.Common.ScenerySubtypes;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;

namespace TT_Lab.AssetData.Instance
{
    public class SceneryData : AbstractAssetData
    {
        public SceneryData()
        {
        }

        public SceneryData(PS2AnyScenery scenery) : this()
        {
            Flags = scenery.Flags;
            SceneryName = scenery.Name.Substring(0);
            UnkUInt = scenery.UnkUInt;
            UnkByte = scenery.UnkByte;
            SkydomeID = scenery.SkydomeID;
            AmbientLights = CloneUtils.DeepClone(scenery.AmbientLights);
            DirectionalLights = CloneUtils.DeepClone(scenery.DirectionalLights);
            PointLights = CloneUtils.DeepClone(scenery.PointLights);
            NegativeLights = CloneUtils.DeepClone(scenery.NegativeLights);
            Sceneries = CloneUtils.DeepClone(scenery.Sceneries);
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Flags { get; set; }
        [JsonProperty(Required = Required.Always)]
        public String SceneryName { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt32 UnkUInt { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkByte { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public UInt32? SkydomeID { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<AmbientLight> AmbientLights { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<DirectionalLight> DirectionalLights { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<PointLight> PointLights { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<NegativeLight> NegativeLights { get; set; }
        [JsonProperty(Required = Required.AllowNull)]
        public List<SceneryBaseType> Sceneries { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
