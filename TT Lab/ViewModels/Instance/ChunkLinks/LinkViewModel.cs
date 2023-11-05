using System;
using System.Collections.ObjectModel;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Instance.ChunkLinks
{
    public class LinkViewModel : SaveableViewModel
    {
        private Boolean unkFlag;
        private String path;
        private Boolean isRendered;
        private Byte unkNum;
        private Boolean isLoadWallActive;
        private Boolean keepLoaded;
        private Matrix4ViewModel objectMatrix;
        private Matrix4ViewModel chunkMatrix;
        private Matrix4ViewModel? loadingWall;
        private ObservableCollection<ChunkLinkBoundingBoxBuilderViewModel> boundingBoxBuilders;

        public LinkViewModel()
        {
            path = "levels\\earth\\hub\\beach";
            objectMatrix = new Matrix4ViewModel();
            chunkMatrix = new Matrix4ViewModel();
            boundingBoxBuilders = new ObservableCollection<ChunkLinkBoundingBoxBuilderViewModel>
            {
                new ChunkLinkBoundingBoxBuilderViewModel()
            };
        }
        public LinkViewModel(ChunkLink link)
        {
            unkFlag = link.UnkFlag;
            path = link.Path;
            isRendered = link.IsRendered;
            unkNum = link.UnkNum;
            isLoadWallActive = link.IsLoadWallActive;
            keepLoaded = link.KeepLoaded;
            objectMatrix = new Matrix4ViewModel(link.ObjectMatrix);
            chunkMatrix = new Matrix4ViewModel(link.ChunkMatrix);
            if (link.LoadingWall != null)
            {
                loadingWall = new Matrix4ViewModel(link.LoadingWall);
            }
            boundingBoxBuilders = new ObservableCollection<ChunkLinkBoundingBoxBuilderViewModel>();
            foreach (var builder in link.ChunkLinksCollisionData)
            {
                boundingBoxBuilders.Add(new ChunkLinkBoundingBoxBuilderViewModel(builder));
            }
        }
        public override void Save(Object? o = null)
        {
            var link = (ChunkLink)o!;
            link.UnkFlag = UnkFlag;
            link.Path = Path;
            link.IsRendered = IsRendered;
            link.UnkNum = UnkNum;
            link.IsLoadWallActive = IsLoadWallActive;
            link.KeepLoaded = KeepLoaded;
            ObjectMatrix.Save(link.ObjectMatrix);
            ChunkMatrix.Save(link.ChunkMatrix);
            link.LoadingWall = null;
            if (LoadingWall != null)
            {
                link.LoadingWall = new Matrix4();
                LoadingWall.Save(link.LoadingWall);
            }
            link.ChunkLinksCollisionData.Clear();
            foreach (var builder in BoundingBoxBuilders)
            {
                var bbb = new TwinChunkLinkBoundingBoxBuilder();
                builder.Save(bbb);
                link.ChunkLinksCollisionData.Add(bbb);
            }
        }

        public Boolean UnkFlag
        {
            get => unkFlag;
            set
            {
                if (value != unkFlag)
                {
                    unkFlag = value;
                    NotifyChange();
                }
            }
        }
        public String Path
        {
            get
            {
                return path;
            }
            set
            {
                if (value != path)
                {
                    path = value;
                    NotifyChange();
                }
            }
        }
        public Boolean IsRendered
        {
            get
            {
                return isRendered;
            }
            set
            {
                if (value != isRendered)
                {
                    isRendered = value;
                    NotifyChange();
                }
            }
        }
        public Byte UnkNum
        {
            get
            {
                return unkNum;
            }
            set
            {
                if (value != unkNum)
                {
                    unkNum = value;
                    NotifyChange();
                }
            }
        }
        public Boolean IsLoadWallActive
        {
            get
            {
                return isLoadWallActive;
            }
            set
            {
                if (value != isLoadWallActive)
                {
                    isLoadWallActive = value;
                    NotifyChange();
                }
            }
        }
        public Boolean KeepLoaded
        {
            get
            {
                return keepLoaded;
            }
            set
            {
                if (value != keepLoaded)
                {
                    keepLoaded = value;
                    NotifyChange();
                }
            }
        }
        public Matrix4ViewModel ObjectMatrix
        {
            get
            {
                return objectMatrix;
            }
            set
            {
                if (value != objectMatrix)
                {
                    objectMatrix = value;
                    NotifyChange();
                }
            }
        }
        public Matrix4ViewModel ChunkMatrix
        {
            get
            {
                return chunkMatrix;
            }
            set
            {
                if (value != chunkMatrix)
                {
                    chunkMatrix = value;
                    NotifyChange();
                }
            }
        }
        public Matrix4ViewModel? LoadingWall
        {
            get
            {
                return loadingWall;
            }
            set
            {
                if (value != loadingWall)
                {
                    loadingWall = value;
                    NotifyChange();
                }
            }
        }
        public ObservableCollection<ChunkLinkBoundingBoxBuilderViewModel> BoundingBoxBuilders
        {
            get
            {
                return boundingBoxBuilders;
            }
        }
    }
}
