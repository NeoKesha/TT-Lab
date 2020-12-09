using System;
using System.Windows;
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
                MessageBox.Show($"Failed to open project: {ex.Message}", "Error opening project!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
