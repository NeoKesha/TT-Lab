using System;
using TT_Lab.ViewModels.Code.OGI;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Instance.ChunkLinks
{
    public class ChunkLinkBoundingBoxBuilderViewModel : SaveableViewModel
    {
        private Int32 type;
        private BoundingBoxBuilderViewModel boundingBoxBuilder;

        public ChunkLinkBoundingBoxBuilderViewModel()
        {
            boundingBoxBuilder = new BoundingBoxBuilderViewModel(new TwinBoundingBoxBuilder());
        }
        public ChunkLinkBoundingBoxBuilderViewModel(TwinChunkLinkBoundingBoxBuilder linkBuilder)
        {
            type = linkBuilder.Type;
            boundingBoxBuilder = new BoundingBoxBuilderViewModel(linkBuilder.BondingBoxBuilder);
        }

        public override void Save(Object? o = null)
        {
            var linkBuilder = (TwinChunkLinkBoundingBoxBuilder)o!;
            linkBuilder.Type = Type;
            BoundingBoxBuilder.Save(linkBuilder.BondingBoxBuilder);
        }

        public Int32 Type
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
        public BoundingBoxBuilderViewModel BoundingBoxBuilder
        {
            get => boundingBoxBuilder;
            set
            {
                if (value != boundingBoxBuilder)
                {
                    boundingBoxBuilder = value;
                    NotifyChange();
                }
            }
        }
    }
}
