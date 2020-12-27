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
            return ProjectManagerSingleton.PM.ProjectOpened;
        }

        public void Execute(Object parameter = null)
        {
            ProjectManagerSingleton.PM.WorkableProject = false;
            try
            {
                var pr = ProjectManagerSingleton.PM.OpenedProject;
                pr.Serialize();
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Error saving project: {ex.Message}");
            }
            finally
            {
                ProjectManagerSingleton.PM.WorkableProject = true;
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
