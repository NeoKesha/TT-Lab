using Newtonsoft.Json;
using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffect : SerializableAsset
    {
        protected override String DataExt => ".wav";
        public override UInt32 Section => Constants.CODE_SOUND_EFFECTS_SECTION;

        [JsonProperty(Required = Required.Always)]
        public UInt32 Header { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Byte UnkFlag { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 Param1 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 Param2 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 Param3 { get; set; }
        [JsonProperty(Required = Required.Always)]
        public UInt16 Param4 { get; set; }

        public SoundEffect() { }

        public SoundEffect(LabURI package, String? variant, UInt32 id, String Name, ITwinSound sound) : base(id, Name, package, variant)
        {
            assetData = new SoundEffectData(sound);
            Header = sound.Header;
            UnkFlag = sound.UnkFlag;
            Param1 = sound.Param1;
            Param2 = sound.Param2;
            Param3 = sound.Param3;
            Param4 = sound.Param4;
            Raw = false;
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override void ResolveChunkResources(ITwinItemFactory factory, ITwinSection section)
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = GetData();
            }

            var item = assetData.ResolveChunkResouces(factory, section, ID) as ITwinSound;
            item?.SetID(ID);
            item?.Compile();
            if (item != null)
            {
                item.Header = Header;
                item.UnkFlag = UnkFlag;
                item.Param1 = Param1;
                item.Param2 = Param2;
                item.Param3 = Param3;
                item.Param4 = Param4;
            }

            assetData.Dispose();
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new SoundEffectData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
