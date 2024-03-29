﻿using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Editors.Instance;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.Assets.Instance
{
    public class Position : SerializableInstance
    {
        public override UInt32 Section => Constants.LAYOUT_POSITIONS_SECTION;

        public Position(LabURI package, UInt32 id, String name, String chunk, Int32 layId, ITwinPosition position) : base(package, id, name, chunk, layId)
        {
            assetData = new PositionData(position);
        }

        public Position()
        {
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
            return typeof(PositionEditor);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new PositionData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }

        public override AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new PositionViewModel(URI, parent);
            return viewModel;
        }
    }
}
