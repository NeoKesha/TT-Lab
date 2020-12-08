using System;
using System.Windows;

namespace TT_Lab.Command
{
    public class OpenDialogueCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Type window;

        public OpenDialogueCommand(Type target)
        {
            window = target;
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter = null)
        {
            var w = (Window)Activator.CreateInstance(window);
            w.ShowDialog();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
