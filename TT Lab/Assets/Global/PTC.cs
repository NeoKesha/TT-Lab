﻿using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Global;
using TT_Lab.Assets.Graphics;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.Assets.Global
{
    public class PTC : SerializableAsset
    {
        protected override String TwinDataExt => "ptc";
        public override UInt32 Section => throw new NotImplementedException();

        public PTC() { }

        public PTC(LabURI package, String? variant, String name, ITwinPTC ptc) : base((UInt32)Guid.NewGuid().GetHashCode(), name, package, variant)
        {
            assetData = new PTCData(ptc);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new PTCData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }

        public override void PreResolveResources()
        {
            base.PreResolveResources();
            PTCData data = (PTCData)GetData();
            AssetManager.Get().GetAsset<Texture>(data.TextureID).PreResolveResources();
            AssetManager.Get().GetAsset<Material>(data.MaterialID).PreResolveResources();
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override Byte[] ToFormat()
        {
            throw new NotImplementedException();
        }

        public override void ToRaw(Byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
