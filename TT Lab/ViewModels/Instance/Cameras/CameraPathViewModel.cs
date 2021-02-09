using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common.CameraSubtypes;

namespace TT_Lab.ViewModels.Instance.Cameras
{
    public class CameraPathViewModel : BaseCameraViewModel
    {
        private ObservableCollection<Vector4ViewModel> pathPoints;
        private ObservableCollection<UInt64> unkData;

        public CameraPathViewModel(CameraSubBase cam) : base(cam)
        {
            var baseCam = (CameraPath)cam;
            pathPoints = new ObservableCollection<Vector4ViewModel>();
            pathPoints.CollectionChanged += PathPoints_CollectionChanged;
            foreach (var v in baseCam.PathPoints)
            {
                pathPoints.Add(new Vector4ViewModel(v));
            }
            unkData = new ObservableCollection<ulong>();
            foreach (var d in baseCam.UnkData)
            {
                unkData.Add(d);
            }
        }

        private void PathPoints_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
        }

        private void Vector_PropertyChanged(Object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            NotifyChange(nameof(PathPoints));
        }

        public override void Save(CameraSubBase? cam)
        {
            if (cam == null)
            {
                cam = new CameraPath();
            }
            var pathCam = (CameraPath)cam;
            pathCam.PathPoints.Clear();
            foreach (var p in PathPoints)
            {
                pathCam.PathPoints.Add(new Twinsanity.TwinsanityInterchange.Common.Vector4
                {
                    X = p.X,
                    Y = p.Y,
                    Z = p.Z,
                    W = p.W
                });
            }
            pathCam.UnkData.Clear();
            foreach (var d in UnkData)
            {
                pathCam.UnkData.Add(d);
            }
            base.Save(cam);
        }

        public override UInt32 GetIndex()
        {
            return 0x1C04;
        }

        public ObservableCollection<Vector4ViewModel> PathPoints
        {
            get => pathPoints;
        }

        public ObservableCollection<UInt64> UnkData
        {
            get => unkData;
        }
    }
}
