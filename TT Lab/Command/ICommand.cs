using System;

namespace TT_Lab.Command
{
    public interface ICommand : System.Windows.Input.ICommand
    {

        /**
        * Performs the command
        */
        new void Execute(object parameter = null);
        /**
         * Undoes the performed command
         */
        void Unexecute();
    }
}
