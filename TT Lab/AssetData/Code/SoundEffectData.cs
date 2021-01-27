using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.Libraries;
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
            twinRef = sound;
        }
        Byte[] PCM;
        UInt32 Frequency;
        Int16 Channels;

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
        public override void Save(string dataPath, JsonSerializerSettings? settings = null)
        {
            using (FileStream fs = new FileStream(dataPath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                RIFF.SaveRiff(writer, PCM, ref Channels, ref Frequency);
            }
        }

        public override void Load(String dataPath, JsonSerializerSettings? settings = null)
        {
            using (FileStream fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                PCM = RIFF.LoadRiff(reader, ref Channels, ref Frequency);
            }
        }

        public override void Import()
        {
            PS2AnySound sound = (PS2AnySound)twinRef;
            Frequency = sound.GetFreq();
            Channels = 1;
            ADPCM adpcm = new ADPCM();
            using (MemoryStream input = new MemoryStream(sound.Sound))
            using (MemoryStream output = new MemoryStream())
            {
                BinaryReader reader = new BinaryReader(input);
                BinaryWriter writer = new BinaryWriter(output);
                adpcm.ToPCMMono(reader, writer);
                PCM = output.ToArray();
            }
        }
    }
}
