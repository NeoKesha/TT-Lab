using System;
using System.Windows;
using System.Windows.Controls;
using TT_Lab.Project;

namespace TT_Lab.Command
{
    public class OpenProjectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private string path;

        public OpenProjectCommand(string path)
        {
            this.path = path;
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter = null)
        {
            try
            {
                ProjectManagerSingleton.PM.OpenProject(path);
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Failed to open project: {ex.Message}");
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
