using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using TT_Lab.Project;

namespace TT_Lab.Command
{
    public class SaveProjectCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public Boolean CanExecute(Object? parameter)
        {
            return true;
        }

        public void Execute(Object? parameter = null)
        {
            if (!IoC.Get<ProjectManager>().ProjectOpened) return;

            IoC.Get<ProjectManager>().WorkableProject = false;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Log.WriteLine($"Saving project...");
                    var now = DateTime.Now;
                    var pr = IoC.Get<ProjectManager>().FullProjectTree;
                    //foreach (var viewModel in pr)
                    //{
                    //    viewModel.Save(null);
                    //}
                    Log.WriteLine($"Saved project in {DateTime.Now - now}");
                }
                catch (Exception ex)
                {
                    Log.WriteLine($"Error saving project: {ex.Message}");
                }
                finally
                {
                    IoC.Get<ProjectManager>().WorkableProject = true;
                }
            });
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
