using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class SoundEffectData : AbstractAssetData
    {
        public SoundEffectData()
        {
        }

        public SoundEffectData(PS2AnySound sound) : this()
        {
            Header = sound.Header;
            UnkFlag = sound.UnkFlag;
            FreqFac = sound.FreqFac;
        }

        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkFlag { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte FreqFac { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
