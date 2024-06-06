using Caliburn.Micro;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public class PathViewModel : InstanceSectionResourceEditorViewModel
    {
        private Enums.Layouts layoutId;
        private BindableCollection<Vector4ViewModel> points;
        private BindableCollection<Vector2ViewModel> arguments;

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            asset.LayoutID = (int)LayoutID;
            var data = asset.GetData<PathData>();
            data.Points.Clear();
            foreach (var p in Points)
            {
                var v = new Twinsanity.TwinsanityInterchange.Common.Vector4();
                p.Save(v);
                data.Points.Add(v);
            }
            data.Parameters.Clear();
            foreach (var p in Arguments)
            {
                var v = new Twinsanity.TwinsanityInterchange.Common.Vector2();
                p.Save(v);
                data.Parameters.Add(v);
            }
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var pathData = asset.GetData<PathData>();
            points = new BindableCollection<Vector4ViewModel>();
            foreach (var p in pathData.Points)
            {
                var vm = new Vector4ViewModel(p);
                points.Add(vm);
            }
            arguments = new BindableCollection<Vector2ViewModel>();
            foreach (var p in pathData.Parameters)
            {
                var vm = new Vector2ViewModel(p);
                arguments.Add(vm);
            }
            layoutId = MiscUtils.ConvertEnum<Enums.Layouts>(asset.LayoutID!.Value);

            AddArgumentCommand = new AddItemToListCommand<Vector2ViewModel>(Arguments);
            AddPointCommand = new AddItemToListCommand<Vector4ViewModel>(Points);
            DeleteArgumentCommand = new DeleteItemFromListCommand(Arguments);
            DeletePointCommand = new DeleteItemFromListCommand(Points);
        }

        public AddItemToListCommand<Vector2ViewModel> AddArgumentCommand { get; private set; }
        public AddItemToListCommand<Vector4ViewModel> AddPointCommand { get; private set; }
        public DeleteItemFromListCommand DeleteArgumentCommand { get; private set; }
        public DeleteItemFromListCommand DeletePointCommand { get; private set; }

        public Enums.Layouts LayoutID
        {
            get => layoutId;
            set
            {
                if (value != layoutId)
                {
                    layoutId = value;
                    IsDirty = true;
                    NotifyOfPropertyChange();
                }
            }
        }

        public BindableCollection<Vector4ViewModel> Points
        {
            get => points;
        }

        public BindableCollection<Vector2ViewModel> Arguments
        {
            get => arguments;
        }
    }
}
