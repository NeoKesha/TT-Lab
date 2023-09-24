﻿using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public class SoundEffect : SerializableAsset
    {
        protected override String DataExt => ".wav";

        public SoundEffect() { }

        public SoundEffect(String package, String subpackage, String? variant, UInt32 id, String Name, PS2AnySound sound) : base(id, Name, package, subpackage, variant)
        {
            assetData = new SoundEffectData(sound);
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
