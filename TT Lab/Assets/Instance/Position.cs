using System;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels.Editors.Instance;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Layout;

namespace TT_Lab.Assets.Instance
{
    public class Position : SerializableInstance
    {
        public override UInt32 Section => Constants.LAYOUT_POSITIONS_SECTION;
        public override String IconPath => "Position.png";

        public Position(LabURI package, UInt32 id, String name, String chunk, Int32 layId, ITwinPosition position) : base(package, id, name, chunk, layId)
        {
            assetData = new PositionData(position);
        }

        public Position()
        {
        }

        public override Type GetEditorType()
        {
            return typeof(PositionViewModel);
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
    }
}
