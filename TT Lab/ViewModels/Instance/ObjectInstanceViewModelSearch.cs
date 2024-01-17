using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.ViewModels.Instance
{
    public class ObjectInstanceViewModelSearch : ObservableObject
    {
        private ObjectInstanceViewModel _viewModel;
        private ObservableCollection<object> _objects;
        private ObservableCollection<object> _behaviours;

        public ObjectInstanceViewModelSearch(ObjectInstanceViewModel apvm, ObservableCollection<object> objects, ObservableCollection<object> behaviours)
        {
            _viewModel = apvm;
            _objects = objects;
            _behaviours = behaviours;
        }

        public ObjectInstanceViewModel ViewModel
        {
            get => _viewModel;
            private set
            {
                _viewModel = value;
                NotifyChange();
            }
        }

        public ObservableCollection<object> Objects
        {
            get => _objects;
            private set
            {
                _objects = value;
                NotifyChange();
            }
        }
        
        public ObservableCollection<object> Behaviours
        {
            get => _behaviours;
            private set
            {
                _behaviours = value;
                NotifyChange();
            }
        }
    }
}
