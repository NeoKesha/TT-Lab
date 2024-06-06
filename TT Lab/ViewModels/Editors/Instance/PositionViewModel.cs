using System.Threading;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public class PositionViewModel : InstanceSectionResourceEditorViewModel
    {
        private Vector4ViewModel position = new();
        private Enums.Layouts layId;

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            asset.LayoutID = (int)LayoutID;
            var data = asset.GetData<PositionData>();
            data.Coords = new Twinsanity.TwinsanityInterchange.Common.Vector4
            {
                X = Position.X,
                Y = Position.Y,
                Z = Position.Z,
                W = Position.W
            };
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var posData = asset.GetData<PositionData>();
            position = new Vector4ViewModel(posData.Coords);
            layId = MiscUtils.ConvertEnum<Enums.Layouts>(asset.LayoutID!.Value);
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            await ActivateItemAsync(position, cancellationToken);
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

        public Vector4ViewModel Position
        {
            get => position;
        }
    }
}
