using System;
using System.Windows;

namespace TT_Lab.Command
{
    public class OpenDialogueCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public class DialogueResult
        {
            public object? Result;
        }

        private readonly Type window;
        private readonly DialogueResult? result;

        public OpenDialogueCommand(Type target)
        {
            window = target;
        }

        public OpenDialogueCommand(Type target, DialogueResult resultRef) : this(target)
        {
            result = resultRef;
        }

        public Boolean CanExecute(Object? parameter)
        {
            return true;
        }

        public void Execute(Object? parameter = null)
        {
            Window w;
            if (result == null)
            {
                w = (Window)Activator.CreateInstance(window)!;
            }
            else
            {
                w = (Window)Activator.CreateInstance(window, result)!;
            }
            w.ShowDialog();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
