using System;
using System.Collections.ObjectModel;
using TT_Lab.AssetData.Instance;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Instance.ChunkLinks
{
    public class LinkViewModel : SaveableViewModel
    {
        public UInt32 type;
        public String path;
        public UInt32 flags;
        public Matrix4ViewModel objectMatrix;
        public Matrix4ViewModel chunkMatrix;
        public Matrix4ViewModel? loadingWall;
        public ObservableCollection<ChunkLinkBoundingBoxBuilderViewModel> boundingBoxBuilders;

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
            type = link.Type;
            path = link.Path;
            flags = link.Flags;
            objectMatrix = new Matrix4ViewModel(link.ObjectMatrix);
            chunkMatrix = new Matrix4ViewModel(link.ChunkMatrix);
            if (link.LoadingWall != null)
            {
                loadingWall = new Matrix4ViewModel(link.LoadingWall);
            }
            boundingBoxBuilders = new ObservableCollection<ChunkLinkBoundingBoxBuilderViewModel>();
            foreach (var builder in link.BoundingBoxBuilders)
            {
                boundingBoxBuilders.Add(new ChunkLinkBoundingBoxBuilderViewModel(builder));
            }
        }
        public override void Save(Object? o = null)
        {
            var link = (ChunkLink)o!;
            link.Type = Type;
            link.Path = Path;
            link.Flags = Flags;
            ObjectMatrix.Save(link.ObjectMatrix);
            ChunkMatrix.Save(link.ChunkMatrix);
            link.LoadingWall = null;
            if (LoadingWall != null)
            {
                link.LoadingWall = new Matrix4();
                LoadingWall.Save(link.LoadingWall);
            }
            link.BoundingBoxBuilders.Clear();
            foreach (var builder in BoundingBoxBuilders)
            {
                var bbb = new TwinChunkLinkBoundingBoxBuilder();
                builder.Save(bbb);
                link.BoundingBoxBuilders.Add(bbb);
            }
        }

        public UInt32 Type
        {
            get => type;
            set
            {
                if (value != type)
                {
                    type = value;
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
        public UInt32 Flags
        {
            get
            {
                return flags;
            }
            set
            {
                if (value != flags)
                {
                    flags = value;
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
