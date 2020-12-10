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
            using (System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog
            {
                InitialDirectory = recents != null && recents.Count != 0 ? recents[0] : "",
                Filter = "PS2 TT Lab Project|*.tson|XBox TT Lab Project|*.xson"
            })
            {
                if (System.Windows.Forms.DialogResult.OK == ofd.ShowDialog())
                {
                    var open = new OpenProjectCommand(System.IO.Path.GetDirectoryName(ofd.FileName));
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
