using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Editors.Instance.ChunkLinks
{
    public class LinkViewModel : Conductor<IScreen>.Collection.AllActive, ISaveableViewModel<ChunkLink>
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
        private BindableCollection<ChunkLinkBoundingBoxBuilderViewModel> boundingBoxBuilders;

        public LinkViewModel()
        {
            path = "levels\\earth\\hub\\beach";
            objectMatrix = new Matrix4ViewModel();
            chunkMatrix = new Matrix4ViewModel();
            boundingBoxBuilders = new BindableCollection<ChunkLinkBoundingBoxBuilderViewModel>
            {
                new()
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
            boundingBoxBuilders = new BindableCollection<ChunkLinkBoundingBoxBuilderViewModel>();
            foreach (var builder in link.ChunkLinksCollisionData)
            {
                boundingBoxBuilders.Add(new ChunkLinkBoundingBoxBuilderViewModel(builder));
            }
        }

        public void Save(ChunkLink link)
        {
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

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(objectMatrix, cancellationToken);
            ActivateItemAsync(chunkMatrix, cancellationToken);

            if (loadingWall != null)
            {
                ActivateItemAsync(loadingWall, cancellationToken);
            }

            foreach (var builder in boundingBoxBuilders)
            {
                ActivateItemAsync(builder, cancellationToken);
            }

            return base.OnInitializeAsync(cancellationToken);
        }

        public Boolean UnkFlag
        {
            get => unkFlag;
            set
            {
                if (value != unkFlag)
                {
                    unkFlag = value;
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
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
                    NotifyOfPropertyChange();
                }
            }
        }

        public Matrix4ViewModel ObjectMatrix
        {
            get => objectMatrix;
        }

        public Matrix4ViewModel ChunkMatrix
        {
            get => chunkMatrix;
        }

        public Matrix4ViewModel? LoadingWall
        {
            get => loadingWall;
        }

        public BindableCollection<ChunkLinkBoundingBoxBuilderViewModel> BoundingBoxBuilders
        {
            get => boundingBoxBuilders;
        }
    }
}
