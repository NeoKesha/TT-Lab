using System;
using System.Collections.Generic;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.SM2;

namespace TT_Lab.Assets.Instance
{
    public class Scenery : SerializableInstance
    {

        public Scenery()
        {
            Parameters = new Dictionary<string, object?>();
        }

        public Scenery(String package, String subpackage, UInt32 id, String name, String chunk, PS2AnyScenery scenery) : base(package, subpackage, id, name, chunk, null)
        {
            assetData = new SceneryData(scenery);
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

        public override AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new SceneryViewModel(URI, parent);
            return viewModel;
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new SceneryData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
