using System;

namespace TT_Lab.Command
{
    public class GenerateCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action _action;
        private Action? _unEx;

        public GenerateCommand(Action ex, Action? unEx = null)
        {
            _action = ex;
            _unEx = unEx;
        }

        public Boolean CanExecute(Object? parameter)
        {
            return true;
        }

        public void Execute(Object? parameter = null)
        {
            _action.Invoke();
        }

        public void Unexecute()
        {
            _unEx?.Invoke();
        }
    }
}
