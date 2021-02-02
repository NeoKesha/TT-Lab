using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Command
{
    public class GenerateCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action _action;
        private Action? _unEx;

        public GenerateCommand(Action a, Action? unEx = null)
        {
            _action = a;
            _unEx = unEx;
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
            _unEx?.Invoke();
        }
    }
}
