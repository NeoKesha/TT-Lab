﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Editors.Instance;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Layout;

namespace TT_Lab.Assets.Instance
{
    public class Path : SerializableInstance
    {
        public Path(UInt32 id, String name, String chunk, Int32 layId, PS2AnyPath path) : base(id, name, chunk, layId)
        {
            assetData = new PathData(path);
        }

        public Path()
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
            return typeof(PathEditor);
        }

        public override AssetViewModel GetViewModel(AssetViewModel parent = null)
        {
            if (viewModel == null)
            {
                viewModel = new PathViewModel(UUID, parent);
            }
            return viewModel;
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new PathData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
