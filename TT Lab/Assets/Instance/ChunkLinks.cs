﻿using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels.Editors.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.SM;

namespace TT_Lab.Assets.Instance
{
    public class ChunkLinks : SerializableInstance
    {
        public override UInt32 Section => Constants.SCENERY_LINK_ITEM;

        public ChunkLinks()
        {
        }

        public ChunkLinks(LabURI package, UInt32 id, String name, String chunk, ITwinLink links) : base(package, id, name, chunk, null)
        {
            assetData = new ChunkLinksData(links);
        }

        public override Type GetEditorType()
        {
            return typeof(ChunkLinkViewModel);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new ChunkLinksData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
