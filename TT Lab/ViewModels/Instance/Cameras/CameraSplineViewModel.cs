using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
{
    public class CameraSplineViewModel : BaseCameraViewModel
    {
        private Single unkFloat3;
        private ObservableCollection<Vector4ViewModel> pathPoints;
        private ObservableCollection<Vector4ViewModel> interpolationPoints;
        private ObservableCollection<UInt64> unkData;
        private UInt16 unkShort;

        public CameraSplineViewModel(CameraSpline baseCam) : base(baseCam)
        {
            unkFloat3 = baseCam.UnkFloat3;
            pathPoints = new ObservableCollection<Vector4ViewModel>();
            pathPoints.CollectionChanged += Points_CollectionChanged;
            foreach (var v in baseCam.PathPoints)
            {
                var vm = new Vector4ViewModel(v);
                pathPoints.Add(vm);
                vm.PropertyChanged += Vector_PropertyChanged;
            }
            interpolationPoints = new ObservableCollection<Vector4ViewModel>();
            interpolationPoints.CollectionChanged += Points_CollectionChanged;
            foreach (var v in baseCam.InterpolationPoints)
            {
                var vm = new Vector4ViewModel(v);
                interpolationPoints.Add(vm);
                vm.PropertyChanged += Vector_PropertyChanged;
            }
            unkData = new ObservableCollection<UInt64>();
            foreach (var d in baseCam.UnkData)
            {
                unkData.Add(d);
            }
            unkShort = baseCam.UnkShort;
        }

        private void Points_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                var vm = (Vector4ViewModel)e.NewItems![e.NewStartingIndex]!;
                vm.PropertyChanged += Vector_PropertyChanged;
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                var vm = (Vector4ViewModel)e.OldItems![e.OldStartingIndex]!;
                vm.PropertyChanged -= Vector_PropertyChanged;
            }
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(nameof(PathPoints));
            NotifyChange(nameof(InterpolationPoints));
        }

        public override void Save(CameraSubBase? cam)
        {
            if (cam == null)
            {
                cam = new CameraSpline();
            }
            var splineCam = (CameraSpline)cam;
            splineCam.UnkFloat3 = UnkFloat3;
            splineCam.UnkShort = UnkShort;
            splineCam.PathPoints.Clear();
            foreach (var p in PathPoints)
            {
                splineCam.PathPoints.Add(new Twinsanity.TwinsanityInterchange.Common.Vector4
                {
                    X = p.X,
                    Y = p.Y,
                    Z = p.Z,
                    W = p.W,
                });
            }
            splineCam.InterpolationPoints.Clear();
            foreach (var ip in InterpolationPoints)
            {
                splineCam.InterpolationPoints.Add(new Twinsanity.TwinsanityInterchange.Common.Vector4
                {
                    X = ip.X,
                    Y = ip.Y,
                    Z = ip.Z,
                    W = ip.W,
                });
            }
            splineCam.UnkData.Clear();
            foreach (var d in UnkData)
            {
                splineCam.UnkData.Add(d);
            }
            base.Save(cam);
        }

        public override UInt32 GetIndex()
        {
            return 0x1C06;
        }

        public Single UnkFloat3
        {
            get => unkFloat3;
            set
            {
                if (unkFloat3 != value)
                {
                    unkFloat3 = value;
                    NotifyChange();
                }
            }
        }
        public ObservableCollection<Vector4ViewModel> PathPoints
        {
            get => pathPoints;
        }
        public ObservableCollection<Vector4ViewModel> InterpolationPoints
        {
            get => interpolationPoints;
        }
        public ObservableCollection<UInt64> UnkData
        {
            get => unkData;
        }
        public UInt16 UnkShort
        {
            get => unkShort;
            set
            {
                if (unkShort != value)
                {
                    unkShort = value;
                    NotifyChange();
                }
            }
        }
    }
}
