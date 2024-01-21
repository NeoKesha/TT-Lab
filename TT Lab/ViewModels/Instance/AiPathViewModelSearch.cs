using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TT_Lab.Assets;

namespace TT_Lab.ViewModels.Instance
{
    public class AiPathViewModelSearch : ObservableObject
    {
        private AiPathViewModel _viewModel;
        private ObservableCollection<object> _positions;

        public AiPathViewModelSearch(AiPathViewModel apvm, ObservableCollection<object> positions)
        {
            _viewModel = apvm;
            _positions = positions;
        }

        public AiPathViewModel ViewModel
        {
            get => _viewModel;
            private set
            {
                _viewModel = value;
                NotifyChange();
            }
        }

        public ObservableCollection<object> Positions
        {
            get => _positions;
            private set
            {
                _positions = value;
                NotifyChange();
            }
        }
    }
}
