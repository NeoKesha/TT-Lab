using System.Collections.ObjectModel;

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
