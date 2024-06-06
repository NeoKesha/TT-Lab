using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Interfaces;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Editors.Instance.ChunkLinks
{
    public class ChunkLinkBoundingBoxBuilderViewModel : Conductor<IScreen>, ISaveableViewModel<TwinChunkLinkBoundingBoxBuilder>
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

        public void Save(TwinChunkLinkBoundingBoxBuilder linkBuilder)
        {
            linkBuilder.Type = Type;
            BoundingBoxBuilder.Save(linkBuilder.BondingBoxBuilder);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(boundingBoxBuilder, cancellationToken);

            return base.OnInitializeAsync(cancellationToken);
        }

        public Int32 Type
        {
            get => type;
            set
            {
                if (value != type)
                {
                    type = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public BoundingBoxBuilderViewModel BoundingBoxBuilder
        {
            get => boundingBoxBuilder;
        }
    }
}
