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

        public CameraPathViewModel(CameraPath baseCam) : base(baseCam)
        {
            pathPoints = new ObservableCollection<Vector4ViewModel>();
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
