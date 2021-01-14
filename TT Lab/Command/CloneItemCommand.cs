using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Command
{
    public class CloneItemIntoCollectionCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private object _item;
        private Stack _items = new Stack();
        private IList _list;

        public object Item
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

        public CloneItemIntoCollectionCommand(IList list)
        {
            _list = list;
        }

        public Boolean CanExecute(Object parameter)
        {
            return Item != null;
        }

        public void Execute(Object parameter = null)
        {
            var clone = Util.CloneUtils.DeepClone<T>((T)_item);
            _items.Push(clone);
            _list.Add(clone);
        }

        public void Unexecute()
        {
            _list.Remove(_items.Pop());
        }
    }
}
