using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TT_Lab.Command
{
    public class AddItemToListCommand<T> : ICommand
    {
        private class DelItemInfo
        {
            public object? Item;
            public Int32 Index;
        }

        public event EventHandler? CanExecuteChanged;

        private ObservableCollection<T> _list;
        private Stack<DelItemInfo> _items = new Stack<DelItemInfo>();
        private Type _item;
        private int _maxItems;

        public AddItemToListCommand(ObservableCollection<T> list, int maxItems = -1)
        {
            _list = list;
            _list.CollectionChanged += _list_CollectionChanged;
            _item = typeof(T);
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
            _items.Push(new DelItemInfo
            {
                Index = _list.Count,
                Item = item
            });
            _list.Add((T)item!);
        }

        public void Unexecute()
        {
            _list.RemoveAt(_items.Pop()!.Index);
        }
    }
}
