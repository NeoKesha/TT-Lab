using System;
using TT_Lab.Command;
using TT_Lab.Properties;

namespace TT_Lab.ViewModels
{
    public class ProjectCreator : ObservableObject
    {
        private string _projectName = "New project";
        private string _projectPath = Settings.Default.ProjectPath;
        private string _discContentPath = Settings.Default.DiscContentPath;

        public ICommand SetProjectPathCommand
        {
            get
            {
                return new SelectFolderCommand(this, "ProjectPath");
            }
        }

        public ICommand SetDiscContentPathCommand
        {
            get
            {
                return new SelectFolderCommand(this, "DiscContentPath");
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
                RaisePropertyChangedEvent("CanCreate");
                RaisePropertyChangedEvent("ProjectName");
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
                RaisePropertyChangedEvent("CanCreate");
                RaisePropertyChangedEvent("ProjectPath");
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
                RaisePropertyChangedEvent("CanCreate");
                RaisePropertyChangedEvent("DiscContentPath");
            }
        }
    }
}
