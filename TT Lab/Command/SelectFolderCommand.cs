using System;
using WK.Libraries.BetterFolderBrowserNS;

namespace TT_Lab.Command
{
    public class SelectFolderCommand : ICommand
    {
        private readonly object target;
        private readonly string propName;

        public SelectFolderCommand(object target, string textStoragePropName)
        {
            this.target = target;
            propName = textStoragePropName;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter = null)
        {
            using (BetterFolderBrowser bfb = new BetterFolderBrowser())
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
