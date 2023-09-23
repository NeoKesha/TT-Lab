using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Code;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;

namespace TT_Lab.Assets.Code
{
    public class BehaviourStarter : Behaviour
    {
        protected override String DataExt => ".data";
        public BehaviourStarter() { }

        public BehaviourStarter(String package, String subpackage, String? variant, UInt32 id, String name, PS2BehaviourStarter script) : base(package, subpackage, variant, id, name)
        {
            assetData = new BehaviourStarterData(script);
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

        public override void Import()
        {
            base.Import();
            // Generate better Alias for header scripts
            var data = (BehaviourStarterData)assetData;
            var mainScrAlias = AssetManager.Get().GetAsset<BehaviourStarter>(data.Pairs[0].Key).Alias;
            Alias = $"Header Script {ID:X} - {mainScrAlias}";
        }

        public override AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new HeaderScriptViewModel(URI, parent);
            return base.GetViewModel(parent);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new BehaviourStarterData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
