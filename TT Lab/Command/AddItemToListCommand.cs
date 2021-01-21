using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Command
{
    public class AddItemToListCommand<T> : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ObservableCollection<T> _list;
        private Stack _items = new Stack();
        private Type _item;
        private int _maxItems;

        public AddItemToListCommand(ObservableCollection<T> list, Type itemType, int maxItems = -1)
        {
            _list = list;
            _list.CollectionChanged += _list_CollectionChanged;
            _item = itemType;
            _maxItems = maxItems;
        }

        private void _list_CollectionChanged(Object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public Boolean CanExecute(Object? parameter)
        {
            return _maxItems == -1 || _maxItems > _list.Count;
        }

        public void Execute(Object? parameter = null)
        {
            var item = Activator.CreateInstance(_item);
            _items.Push(item);
            _list.Add((T)item!);
        }

        public void Unexecute()
        {
            _list.Remove((T)_items.Pop()!);
        }
    }
}
