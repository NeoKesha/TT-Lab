using System;
using WK.Libraries.BetterFolderBrowserNS;

namespace TT_Lab.Command
{
    public class SelectFolderCommand : ICommand
    {
        private readonly object target;
        private readonly string propName;
        private readonly string startPath;

        public SelectFolderCommand(object target, string textStoragePropName, string startPath = "")
        {
            this.target = target;
            this.startPath = startPath;
            propName = textStoragePropName;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter = null)
        {
            using (BetterFolderBrowser bfb = new BetterFolderBrowser
            {
                RootFolder = startPath
            })
            {
                if (bfb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var prop = target.GetType().GetProperty(propName);
                    prop.SetValue(target, bfb.SelectedPath);
                }
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
