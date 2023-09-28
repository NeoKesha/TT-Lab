using System;

namespace TT_Lab.Command
{
    public class UndoCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private CommandManager _comManager;

        public UndoCommand(CommandManager comManager)
        {
            _comManager = comManager;
        }

        public Boolean CanExecute(Object? parameter)
        {
            return _comManager.CanUndo;
        }

        public void Execute(Object? parameter = null)
        {
            _comManager.Undo();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
