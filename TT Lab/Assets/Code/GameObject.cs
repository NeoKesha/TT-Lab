﻿using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets.Factory;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code;

namespace TT_Lab.Assets.Code
{
    public class GameObject : SerializableAsset
    {
        public override UInt32 Section => Constants.CODE_GAME_OBJECTS_SECTION;
        private bool resolveTraversed = false;

        public GameObject() { }

        public GameObject(LabURI package, String? variant, UInt32 id, String Name, ITwinObject @object) : base(id, Name, package, variant)
        {
            assetData = new GameObjectData(@object);
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
                assetData = new GameObjectData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }

        public override void ResolveChunkResources(ITwinItemFactory factory, ITwinSection section)
        {
            if (resolveTraversed) return;

            resolveTraversed = true;
            base.ResolveChunkResources(factory, section);
            resolveTraversed = false;
        }
    }
}
