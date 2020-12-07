namespace TT_Lab.Command
{
    public interface ICommand
    {
        /**
         * Performs the command
         */
        void Execute();
        /**
         * Undoes the performed command
         */
        void Unexecute();
    }
}
