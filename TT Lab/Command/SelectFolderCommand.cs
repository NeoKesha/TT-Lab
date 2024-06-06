using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows;

namespace TT_Lab.Command
{
    public class SelectFolderCommand : ICommand
    {
        private readonly object target;
        private readonly string propName;
        private readonly string startPath;
        private readonly Window? owner;

        public SelectFolderCommand(object target, string textStoragePropName, string startPath = "")
        {
            this.target = target;
            this.startPath = startPath;
            propName = textStoragePropName;
        }

        public SelectFolderCommand(Window? owner, object target, string textStoragePropName, string startPath = "")
            : this(target, textStoragePropName, startPath)
        {
            this.owner = owner;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter = null)
        {
            using CommonOpenFileDialog ofd = new()
            {
                IsFolderPicker = true,
                InitialDirectory = startPath
            };
            var dialRes = owner == null ? ofd.ShowDialog() : ofd.ShowDialog(owner);
            if (dialRes == CommonFileDialogResult.Ok)
            {
                var prop = target.GetType().GetProperty(propName)!;
                prop.SetValue(target, ofd.FileName);
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
