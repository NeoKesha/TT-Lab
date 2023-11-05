using Newtonsoft.Json;
using System;
using System.IO;
using TT_Lab.Assets;
using TT_Lab.Assets.Factory;
using Twinsanity.Libraries;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.AssetData.Code
{
    public class SoundEffectData : AbstractAssetData
    {
        public SoundEffectData()
        {
        }

        public SoundEffectData(ITwinSound sound) : this()
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
            ITwinSound sound = GetTwinItem<ITwinSound>();
            Frequency = sound.GetFreq();
            Channels = 1;
            PCM = sound.ToPCM();
        }

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var sound = factory.GenerateSound();
            sound.SetDataFromPCM(PCM);
            sound.SetFreq((UInt16)Frequency);

            return sound;
        }
    }
}
