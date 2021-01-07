using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.Util;
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
            var proj = MiscUtils.GetFileFromDialogue("PS2 TT Lab Project|*.tson|XBox TT Lab Project|*.xson", recents != null && recents.Count != 0 ? recents[0] : "");
            if (proj != string.Empty)
            {
                var open = new OpenProjectCommand(System.IO.Path.GetDirectoryName(proj));
                open.Execute();
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
