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
            foreach (var v in baseCam.PathPoints)
            {
                pathPoints.Add(new Vector4ViewModel(v));
            }
            interpolationPoints = new ObservableCollection<Vector4ViewModel>();
            foreach (var v in baseCam.InterpolationPoints)
            {
                interpolationPoints.Add(new Vector4ViewModel(v));
            }
            unkData = new ObservableCollection<UInt64>();
            foreach (var d in baseCam.UnkData)
            {
                unkData.Add(d);
            }
            unkShort = baseCam.UnkShort;
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
