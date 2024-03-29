﻿using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Graphics;
using TT_Lab.Editors.Graphics;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items;

namespace TT_Lab.Assets.Graphics
{
    public class RigidModel : SerializableAsset
    {
        public override UInt32 Section => Constants.GRAPHICS_RIGID_MODELS_SECTION;

        public RigidModel(LabURI package, Boolean needVariant, String variant, UInt32 id, String Name, ITwinRigidModel rigidModel) : base(id, Name, package, needVariant, variant)
        {
            assetData = new RigidModelData(rigidModel);
        }

        public RigidModel()
        {
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
            return typeof(RigidModelEditor);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new RigidModelData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
