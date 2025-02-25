using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code.Behaviour;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code.AgentLab;

namespace TT_Lab.Assets.Code
{
    public class BehaviourGraph : Behaviour
    {

        protected override String DataExt => ".lab";

        public BehaviourGraph() { }

        public BehaviourGraph(LabURI package, Boolean needVariant, String variant, UInt32 id, String Name, PS2BehaviourGraph script) : base(package, needVariant, variant, id, Name)
        {
            assetData = new BehaviourGraphData(script);
            RegenerateURI(needVariant);
        }

        public override Type GetEditorType()
        {
            throw new NotImplementedException();
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new BehaviourGraphData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
