using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;

namespace TT_Lab.ViewModels.Instance
{
    public class PathViewModel : AssetViewModel
    {
        private ObservableCollection<Vector4ViewModel> points;
        private ObservableCollection<Single> paramGroup1;
        private ObservableCollection<Single> paramGroup2;

        public PathViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var pathData = (PathData)_asset.GetData();
            points = new ObservableCollection<Vector4ViewModel>();
            points.CollectionChanged += Points_CollectionChanged;
            foreach (var p in pathData.Points)
            {
                var vm = new Vector4ViewModel(p);
                points.Add(vm);
                vm.PropertyChanged += Vector_PropertyChanged;
            }
            paramGroup1 = new ObservableCollection<Single>();
            paramGroup1.CollectionChanged += ParamGroup_CollectionChanged;
            var pg1 = pathData.Parameters.FindAll(s => pathData.Parameters.IndexOf(s) < pathData.Parameters.Count / 2);
            foreach (var p in pg1)
            {
                paramGroup1.Add(p.X);
                paramGroup1.Add(p.Y);
            }
            paramGroup2 = new ObservableCollection<Single>();
            paramGroup2.CollectionChanged += ParamGroup_CollectionChanged;
            var pg2 = pathData.Parameters.FindAll(s => pathData.Parameters.IndexOf(s) >= pathData.Parameters.Count / 2);
            foreach (var p in pg2)
            {
                paramGroup2.Add(p.X);
                paramGroup2.Add(p.Y);
            }
        }

        private void ParamGroup_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyChange(nameof(ParamGroup1));
            NotifyChange(nameof(ParamGroup2));
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
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsDirty = true;
            NotifyChange(nameof(Points));
        }

        public override void Save(object? o)
        {
            var data = (PathData)_asset.GetData();
            data.Points.Clear();
            foreach (var p in Points)
            {
                var v = new Twinsanity.TwinsanityInterchange.Common.Vector4();
                p.Save(v);
                data.Points.Add(v);
            }
            data.Parameters.Clear();
            for (var i = 0; i < ParamGroup1.Count; i += 2)
            {
                data.Parameters.Add(new Twinsanity.TwinsanityInterchange.Common.Vector2
                {
                    X = ParamGroup1[i],
                    Y = ParamGroup1[i + 1]
                });
            }
            for (var i = 0; i < ParamGroup2.Count; i += 2)
            {
                data.Parameters.Add(new Twinsanity.TwinsanityInterchange.Common.Vector2
                {
                    X = ParamGroup2[i],
                    Y = ParamGroup2[i + 1]
                });
            }
            base.Save(o);
        }

        public ObservableCollection<Vector4ViewModel> Points
        {
            get => points;
        }
        public ObservableCollection<Single> ParamGroup1
        {
            get => paramGroup1;
        }
        public ObservableCollection<Single> ParamGroup2
        {
            get => paramGroup2;
        }
    }
}
