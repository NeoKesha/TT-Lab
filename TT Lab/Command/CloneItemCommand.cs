using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Command
{
    public class CloneItemIntoCollectionCommand<T> : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private object? _item;
        private Stack _items = new Stack();
        private ObservableCollection<T> _list;
        private int _maxItems;

        public object? Item
        {
            set
            {
                _item = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
            private get
            {
                return _item;
            }
        }

        public CloneItemIntoCollectionCommand(ObservableCollection<T> list, int maxItems = -1)
        {
            _list = list;
            _list.CollectionChanged += _list_CollectionChanged;
            _maxItems = maxItems;
        }

        private void _list_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public Boolean CanExecute(Object? parameter)
        {
            return Item != null && (_maxItems == -1 || _maxItems > _list.Count);
        }

        public void Execute(Object? parameter = null)
        {
            var clone = Util.CloneUtils.DeepClone((T)_item!);
            _items.Push(clone);
            _list.Add(clone);
            
        }

        public void Unexecute()
        {
            _list.Remove((T)_items.Pop()!);
        }
    }
}
