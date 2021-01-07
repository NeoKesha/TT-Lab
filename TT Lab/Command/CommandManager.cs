using System.Collections.Generic;

namespace TT_Lab.Command
{
    public class CommandManager
    {
        private readonly Stack<ICommand> RedoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> UndoStack = new Stack<ICommand>();

        public bool CanUndo
        {
            get
            {
                return UndoStack.Count != 0;
            }
        }

        public bool CanRedo
        {
            get
            {
                return RedoStack.Count != 0;
            }
        }

        public void Execute(ICommand command)
        {
            command.Execute();
            UndoStack.Push(command);
            RedoStack.Clear();
        }

        public void Undo()
        {
            if (!CanUndo) return;

            var undo = UndoStack.Pop();
            undo.Unexecute();
            RedoStack.Push(undo);
        }

        public void Redo()
        {
            if (!CanRedo) return;

            var redo = RedoStack.Pop();
            redo.Execute();
            UndoStack.Push(redo);
        }
    }
}
