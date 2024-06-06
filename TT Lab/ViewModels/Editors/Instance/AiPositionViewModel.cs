using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Extensions;
using TT_Lab.Project.Messages;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public class AiPositionViewModel : InstanceSectionResourceEditorViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private Vector4ViewModel position;
        private UInt16 arg;
        private Enums.Layouts layId;

        public AiPositionViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            asset.LayoutID = (int)LayoutID;
            var data = asset.GetData<AiPositionData>();
            Position.Save(data.Coords);
            data.Arg = Argument;
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            DeactivateItemAsync(Position, close, cancellationToken);

            return base.OnDeactivateAsync(close, cancellationToken);
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var posData = asset.GetData<AiPositionData>();
            position = new Vector4ViewModel(posData.Coords);
            // TODO: Receive notifications from vector editor to change the position in the scene/chunk renderer
            ActivateItemAsync(position);
            arg = posData.Arg;
            layId = MiscUtils.ConvertEnum<Enums.Layouts>(asset.LayoutID!.Value);
            _eventAggregator.PublishOnUIThreadAsync(new ChangeRenderCameraPositionMessage { NewCameraPosition = posData.Coords.ToGLM().xyz });
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

        public UInt16 Argument
        {
            get => arg;
            set
            {
                if (arg != value)
                {
                    arg = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public Vector4ViewModel Position
        {
            get => position;
        }
    }
}
