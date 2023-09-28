using Newtonsoft.Json;
using System;
using System.IO;
using TT_Lab.Assets;
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
            SetTwinItem(sound);
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
            using FileStream fs = new(dataPath, FileMode.Create, FileAccess.Write);
            using BinaryWriter writer = new(fs);
            RIFF.SaveRiff(writer, PCM, ref Channels, ref Frequency);
        }

        public override void Load(String dataPath, JsonSerializerSettings? settings = null)
        {
            using FileStream fs = new(dataPath, FileMode.Open, FileAccess.Read);
            using BinaryReader reader = new(fs);
            PCM = RIFF.LoadRiff(reader, ref Channels, ref Frequency);
        }

        public override void Import(LabURI package, String? variant)
        {
            PS2AnySound sound = GetTwinItem<PS2AnySound>();
            Frequency = sound.GetFreq();
            Channels = 1;
            ADPCM adpcm = new ADPCM();
            using MemoryStream input = new(sound.Sound);
            using MemoryStream output = new();
            BinaryReader reader = new BinaryReader(input);
            BinaryWriter writer = new BinaryWriter(output);
            adpcm.ToPCMMono(reader, writer);
            PCM = output.ToArray();
        }
    }
}
