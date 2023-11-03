using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code.Behaviour;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Code;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;

namespace TT_Lab.Assets.Code
{
    public class BehaviourStarter : Behaviour
    {
        protected override String DataExt => ".data";
        public BehaviourStarter() { }

        public BehaviourStarter(LabURI package, String? variant, UInt32 id, String name, TwinBehaviourStarter script) : base(package, variant, id, name)
        {
            assetData = new BehaviourStarterData(script);
            RegenerateURI();
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
            var mainScrAlias = AssetManager.Get().GetAsset(data.Assigners[0].Behaviour).Alias;
            Alias = $"Behaviour Starter {ID:X} - {mainScrAlias}";
        }

        public override AssetViewModel GetViewModel(AssetViewModel? parent = null)
        {
            viewModel ??= new BehaviourStarterViewModel(URI, parent);
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
