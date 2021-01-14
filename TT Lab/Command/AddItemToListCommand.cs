using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Command
{
    public class AddItemToListCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IList _list;
        private Stack _items = new Stack();
        private Type _item;

        public AddItemToListCommand(IList list, Type item)
        {
            _list = list;
            _item = item;
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter = null)
        {
            var item = Activator.CreateInstance(_item);
            _items.Push(item);
            _list.Add(item);
        }

        public void Unexecute()
        {
            _list.Remove(_items.Pop());
        }
    }
}
