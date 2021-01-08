using System;
using System.Windows;
using TT_Lab.Command;
using TT_Lab.Properties;

namespace TT_Lab.ViewModels
{
    public class ProjectCreator : ObservableObject
    {
        private string _projectName = "New project";
        private string _projectPath = Settings.Default.ProjectPath;
        private string _discContentPath = Settings.Default.DiscContentPath;
        private Window _owner = null;

        public ProjectCreator(Window owner)
        {
            _owner = owner;
        }

        public ICommand SetProjectPathCommand
        {
            get
            {
                return new SelectFolderCommand(_owner, this, "ProjectPath");
            }
        }

        public ICommand SetDiscContentPathCommand
        {
            get
            {
                return new SelectFolderCommand(_owner, this, "DiscContentPath");
            }
        }

        public bool CanCreate
        {
            get
            {
                return _projectName.Length != 0 && _projectPath.Length != 0 && _discContentPath.Length != 0;
            }
        }

        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                _projectName = value;
                NotifyChange("CanCreate");
                NotifyChange("ProjectName");
            }
        }
        public string ProjectPath
        {
            get
            {
                return _projectPath;
            }
            set
            {
                _projectPath = value;
                Settings.Default.ProjectPath = _projectPath;
                NotifyChange("CanCreate");
                NotifyChange("ProjectPath");
            }
        }
        public string DiscContentPath
        {
            get
            {
                return _discContentPath;
            }
            set
            {
                _discContentPath = value;
                Settings.Default.DiscContentPath = _discContentPath;
                NotifyChange("CanCreate");
                NotifyChange("DiscContentPath");
            }
        }
    }
}
