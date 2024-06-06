using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels.Editors.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.Assets.Instance
{
    public class AiPath : SerializableInstance
    {
        public override UInt32 Section => Constants.LAYOUT_AI_PATHS_SECTION;

        public AiPath(LabURI package, UInt32 id, String name, String chunk, Int32 layId, ITwinAIPath path) : base(package, id, name, chunk, layId)
        {
            assetData = new AiPathData(path);
        }

        public AiPath()
        {
        }

        public override Type GetEditorType()
        {
            return typeof(AiPathViewModel);
        }

        public override AbstractAssetData GetData()
        {
            if (!IsLoaded || assetData.Disposed)
            {
                assetData = new AiPathData();
                assetData.Load(System.IO.Path.Combine("assets", SavePath, Data));
                IsLoaded = true;
            }
            return assetData;
        }
    }
}
