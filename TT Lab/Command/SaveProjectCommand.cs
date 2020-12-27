using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Project;

namespace TT_Lab.Command
{
    public class SaveProjectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter = null)
        {
            if (!ProjectManagerSingleton.PM.ProjectOpened) return;

            ProjectManagerSingleton.PM.WorkableProject = false;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Log.WriteLine($"Saving project...");
                    var now = DateTime.Now;
                    var pr = ProjectManagerSingleton.PM.OpenedProject;
                    pr.Serialize();
                    Log.WriteLine($"Saved project in {DateTime.Now - now}");
                }
                catch (Exception ex)
                {
                    Log.WriteLine($"Error saving project: {ex.Message}");
                }
                finally
                {
                    ProjectManagerSingleton.PM.WorkableProject = true;
                }
            });
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
