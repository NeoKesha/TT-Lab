using Caliburn.Micro;
using System;
using System.Linq;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Assets.Instance;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public class AiPathViewModel : InstanceSectionResourceEditorViewModel
    {
        private BindableCollection<ResourceTreeElementViewModel> positions;
        private Enums.Layouts layId;
        private LabURI pathBegin;
        private LabURI pathEnd;
        private UInt16[] args;

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset<AiPath>(EditableResource);
            asset.LayoutID = (int)LayoutID;
            var data = (AiPathData)asset.GetData();
            data.PathBegin = pathBegin;
            data.PathEnd = pathEnd;
            data.Args[0] = Arg1;
            data.Args[1] = Arg2;
            data.Args[2] = Arg3;
            asset.Serialize();
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset<AiPath>(EditableResource);
            var pathData = (AiPathData)asset.GetData();
            pathBegin = pathData.PathBegin;
            pathEnd = pathData.PathEnd;
            args = CloneUtils.CloneArray(pathData.Args);
            layId = MiscUtils.ConvertEnum<Enums.Layouts>(asset.LayoutID!.Value);

            var tree = ParentEditor.ChunkTree;
            var navPositions = tree.First(avm => avm.Alias == "AI Navigation Positions");
            foreach (var p in navPositions!.Children)
            {
                positions.Add(p);
            }
        }

        public BindableCollection<ResourceTreeElementViewModel> Positions
        {
            get => positions;
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
                    NotifyOfPropertyChange();
                }
            }
        }

        public ResourceTreeElementViewModel PathBegin
        {
            get => AssetManager.Get().GetAsset(pathBegin).GetResourceTreeElement();
            set
            {
                if (pathBegin != value.Asset.URI)
                {
                    pathBegin = value.Asset.URI;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public ResourceTreeElementViewModel PathEnd
        {
            get => AssetManager.Get().GetAsset(pathEnd).GetResourceTreeElement();
            set
            {
                if (pathEnd != value.Asset.URI)
                {
                    pathEnd = value.Asset.URI;
                    IsDirty = true;
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                }
            }
        }
    }
}
