using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code.Behaviour;
using Twinsanity.TwinsanityInterchange.Common.AgentLab;

namespace TT_Lab.Assets.Code
{
    public class BehaviourStarter : Behaviour
    {
        protected override String DataExt => ".data";
        public BehaviourStarter() { }

        public BehaviourStarter(LabURI package, Boolean needVariant, String variant, UInt32 id, String name, TwinBehaviourStarter script) : base(package, needVariant, variant, id, name)
        {
            assetData = new BehaviourStarterData(script);
            RegenerateURI(needVariant);
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
