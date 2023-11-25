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

        public SoundEffect() { }

        public SoundEffect(LabURI package, String? variant, UInt32 id, String Name, ITwinSound sound) : base(id, Name, package, variant)
        {
            assetData = new SoundEffectData(sound);
            Parameters["header"] = sound.Header;
            Parameters["unk_flag"] = sound.UnkFlag;
            Parameters["param_1"] = sound.Param1;
            Parameters["param_2"] = sound.Param2;
            Parameters["param_3"] = sound.Param3;
            Parameters["param_4"] = sound.Param4;
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

        public override ITwinItem Export(ITwinItemFactory factory)
        {
            var sound = (ITwinSound)base.Export(factory);
            var downCast = (IAsset)this;
            sound.Header = downCast.GetParameter<UInt32>("header");
            sound.UnkFlag = downCast.GetParameter<Byte>("unk_flag");
            sound.Param1 = downCast.GetParameter<UInt16>("param_1");
            sound.Param2 = downCast.GetParameter<UInt16>("param_2");
            sound.Param3 = downCast.GetParameter<UInt16>("param_3");
            sound.Param4 = downCast.GetParameter<UInt16>("param_4");
            return sound;
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
