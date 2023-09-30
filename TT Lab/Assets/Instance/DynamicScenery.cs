﻿using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.Assets.Instance
{
    public class DynamicScenery : SerializableInstance
    {
        public DynamicScenery()
        {
        }

        public DynamicScenery(LabURI package, UInt32 id, String Name, String chunk, ITwinDynamicScenery dynamicScenery) : base(package, id, Name, chunk, null)
        {
            assetData = new DynamicSceneryData(dynamicScenery);
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

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new DynamicSceneryData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
