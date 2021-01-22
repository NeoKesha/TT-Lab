using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Instance
{
    public class AiPathViewModel : AssetViewModel
    {
        private Enums.Layouts layId;
        private UInt16 pathBegin;
        private UInt16 pathEnd;
        private UInt16[] args;

        public AiPathViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var pathData = (AiPathData)_asset.GetData();
            pathBegin = pathData.PathBegin;
            pathEnd = pathData.PathEnd;
            args = CloneUtils.CloneArray(pathData.Args);
            layId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);
        }

        public override void Save()
        {
            _asset.LayoutID = (int)LayoutID;
            var data = (AiPathData)_asset.GetData();
            data.PathBegin = PathBegin;
            data.PathEnd = PathEnd;
            data.Args[0] = Arg1;
            data.Args[1] = Arg2;
            data.Args[2] = Arg3;
            base.Save();
        }

        public Enums.Layouts LayoutID
        {
            get => layId;
            set
            {
                if (layId != value)
                {
                    layId = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 PathBegin
        {
            get => pathBegin;
            set
            {
                if (pathBegin != value)
                {
                    pathBegin = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 PathEnd
        {
            get => pathEnd;
            set
            {
                if (pathEnd != value)
                {
                    pathEnd = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 Arg1
        {
            get => args[0];
            set
            {
                if (args[0] != value)
                {
                    args[0] = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 Arg2
        {
            get => args[1];
            set
            {
                if (args[1] != value)
                {
                    args[1] = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
        public UInt16 Arg3
        {
            get => args[2];
            set
            {
                if (args[2] != value)
                {
                    args[2] = value;
                    IsDirty = true;
                    NotifyChange();
                }
            }
        }
    }
}
