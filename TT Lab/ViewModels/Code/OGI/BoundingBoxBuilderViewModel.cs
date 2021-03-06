﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Common;

namespace TT_Lab.ViewModels.Code.OGI
{
    public class BoundingBoxBuilderViewModel : SavebleViewModel
    {
        private ObservableCollection<Vector4ViewModel> bbPoints;

        public BoundingBoxBuilderViewModel()
        {
            bbPoints = new ObservableCollection<Vector4ViewModel>
            {
                new Vector4ViewModel(0, 0, 0, 1),
                new Vector4ViewModel(0, 0, 1, 1),
                new Vector4ViewModel(0, 1, 0, 1),
                new Vector4ViewModel(0, 1, 1, 1),
                new Vector4ViewModel(1, 0, 0, 1),
                new Vector4ViewModel(1, 0, 1, 1),
                new Vector4ViewModel(1, 1, 0, 1),
                new Vector4ViewModel(1, 1, 1, 1),
            };
        }
        public BoundingBoxBuilderViewModel(BoundingBoxBuilder bb)
        {
            bbPoints = new ObservableCollection<Vector4ViewModel>();
            foreach (var p in bb.BoundingBoxPoints)
            {
                bbPoints.Add(new Vector4ViewModel(p));
            }
        }
        public override void Save(Object? o = null)
        {
            var builder = (BoundingBoxBuilder)o!;
            builder.Header[0] = (UInt16)BoundingBoxPoints.Count;
            builder.BoundingBoxPoints.Clear();
            foreach (var v in BoundingBoxPoints)
            {
                builder.BoundingBoxPoints.Add(new Vector4
                {
                    X = v.X,
                    Y = v.Y,
                    Z = v.Z,
                    W = v.W,
                });
            }
        }

        public ObservableCollection<Vector4ViewModel> BoundingBoxPoints
        {
            get => bbPoints;
        }
    }
}
