using System;
using System.Collections.ObjectModel;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Util;
using Twinsanity.TwinsanityInterchange.Enumerations;

namespace TT_Lab.ViewModels.Instance
{
    public class PathViewModel : AssetViewModel
    {
        private Enums.Layouts layoutId;
        private ObservableCollection<Vector4ViewModel> points;
        private ObservableCollection<Vector2ViewModel> arguments;

        public PathViewModel(LabURI asset, AssetViewModel? parent) : base(asset, parent)
        {
            var pathData = _asset.GetData<PathData>();
            points = new ObservableCollection<Vector4ViewModel>();
            points.CollectionChanged += Points_CollectionChanged;
            foreach (var p in pathData.Points)
            {
                var vm = new Vector4ViewModel(p);
                points.Add(vm);
                vm.PropertyChanged += Vector_PropertyChanged;
            }
            arguments = new ObservableCollection<Vector2ViewModel>();
            arguments.CollectionChanged += ParamGroup_CollectionChanged;
            foreach (var p in pathData.Parameters)
            {
                var vm = new Vector2ViewModel(p);
                arguments.Add(vm);
                vm.PropertyChanged += Arguments_PropertyChanged;
            }
            layoutId = MiscUtils.ConvertEnum<Enums.Layouts>(_asset.LayoutID!.Value);

            AddArgumentCommand = new AddItemToListCommand<Vector2ViewModel>(Arguments);
            AddPointCommand = new AddItemToListCommand<Vector4ViewModel>(Points);
            DeleteArgumentCommand = new DeleteItemFromListCommand(Arguments);
            DeletePointCommand = new DeleteItemFromListCommand(Points);
        }

        private void Arguments_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(nameof(Arguments));
            IsDirty = true;
        }

        private void ParamGroup_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var vm = (Vector2ViewModel)e.NewItems![0]!;
                vm.PropertyChanged += Arguments_PropertyChanged;
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                var vm = (Vector2ViewModel)e.OldItems![0]!;
                vm.PropertyChanged -= Arguments_PropertyChanged;
            }
            NotifyChange(nameof(Arguments));
            IsDirty = true;
        }

        private void Points_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var vm = (Vector4ViewModel)e.NewItems![0]!;
                vm.PropertyChanged += Vector_PropertyChanged;
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                var vm = (Vector4ViewModel)e.OldItems![0]!;
                vm.PropertyChanged -= Vector_PropertyChanged;
            }
            NotifyChange(nameof(Points));
            IsDirty = true;
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Points));
        }

        public override void Save(object? o)
        {
            _asset.LayoutID = (int)LayoutID;
            var data = _asset.GetData<PathData>();
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
            base.Save(o);
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
                    NotifyChange();
                }
            }
        }
        public ObservableCollection<Vector4ViewModel> Points
        {
            get => points;
        }
        public ObservableCollection<Vector2ViewModel> Arguments
        {
            get => arguments;
        }
    }
}
