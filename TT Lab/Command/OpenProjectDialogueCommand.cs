using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK.Libraries.BetterFolderBrowserNS;

namespace TT_Lab.Command
{
    public class OpenProjectDialogueCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter = null)
        {
            var recents = Properties.Settings.Default.RecentProjects;
            using (BetterFolderBrowser bfb = new BetterFolderBrowser
            {
                RootFolder = recents != null && recents.Count != 0 ? recents[0] : ""
            })
            {
                if (System.Windows.Forms.DialogResult.OK == bfb.ShowDialog())
                {
                    var open = new OpenProjectCommand(bfb.SelectedPath);
                    open.Execute();
                }
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
