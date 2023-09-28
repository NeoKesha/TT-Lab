﻿using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics;

namespace TT_Lab.Assets.Graphics
{
    public class BlendSkin : SerializableAsset
    {
        public BlendSkin() { }

        public BlendSkin(LabURI package, String? variant, UInt32 id, String Name, PS2AnyBlendSkin blendSkin) : base(id, Name, package, variant)
        {
            assetData = new BlendSkinData(blendSkin);
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
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
                assetData = new BlendSkinData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
