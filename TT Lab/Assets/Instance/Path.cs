using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels.Editors.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.Assets.Instance
{
    public class Path : SerializableInstance
    {
        public override UInt32 Section => Constants.LAYOUT_PATHS_SECTION;
        public override String IconPath => "Path.png";

        public Path(LabURI package, UInt32 id, String name, String chunk, Int32 layId, ITwinPath path) : base(package, id, name, chunk, layId)
        {
            assetData = new PathData(path);
        }

        public Path()
        {
        }

        public override Type GetEditorType()
        {
            return typeof(PathViewModel);
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
