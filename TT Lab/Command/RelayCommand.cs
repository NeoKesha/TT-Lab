using System;

namespace TT_Lab.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private ICommand _command;
        private CommandManager _comManager;

        public RelayCommand(ICommand com, CommandManager comManager)
        {
            _command = com;
            _command.CanExecuteChanged += _command_CanExecuteChanged;
            _comManager = comManager;
        }

        private void _command_CanExecuteChanged(Object? sender, EventArgs e)
        {
            CanExecuteChanged?.Invoke(sender, e);
        }

        public Boolean CanExecute(Object? parameter)
        {
            return _command.CanExecute(parameter);
        }

        public void Execute(Object? parameter = null)
        {
            _comManager.Execute(_command);
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
