using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Command
{
    public class DeleteItemFromListCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private object _item;
        private Stack _deletedItems = new Stack();
        private IList _list;

        public DeleteItemFromListCommand(IList list)
        {
            _list = list;
        }

        public object Item
        {
            set
            {
                _item = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }
            private get => _item;
        }

        public Boolean CanExecute(Object parameter)
        {
            return Item != null;
        }

        public void Execute(Object parameter = null)
        {
            _deletedItems.Push(_item);
            _list.Remove(_item);
        }

        public void Unexecute()
        {
            _list.Add(_deletedItems.Pop());
        }
    }
}
