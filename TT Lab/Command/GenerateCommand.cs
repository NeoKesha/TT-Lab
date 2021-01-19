using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Command
{
    public class GenerateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action _action;

        public GenerateCommand(Action a)
        {
            _action = a;
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter = null)
        {
            _action.Invoke();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
