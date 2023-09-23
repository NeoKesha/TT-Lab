using System;

namespace TT_Lab.Command
{
    public class RedoCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private CommandManager _comManager;

        public RedoCommand(CommandManager comManager)
        {
            _comManager = comManager;
        }

        public Boolean CanExecute(Object parameter)
        {
            return _comManager.CanRedo;
        }

        public void Execute(Object parameter = null)
        {
            _comManager.Redo();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
