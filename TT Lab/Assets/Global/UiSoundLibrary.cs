﻿using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Global;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace TT_Lab.Assets.Global
{
    public class UiSoundLibrary : SerializableAsset
    {
        public override UInt32 Section => throw new NotImplementedException();

        public UiSoundLibrary() { }

        public UiSoundLibrary(LabURI package, Boolean needVariant, String variant, String name, ITwinSection frontend) : base((UInt32)Guid.NewGuid().GetHashCode(), name, package, needVariant, variant)
        {
            assetData = new UiSoundLibraryData(frontend);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new UiSoundLibraryData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
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
