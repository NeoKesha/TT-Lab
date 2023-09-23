using System.Windows;
using TT_Lab.Command;
using TT_Lab.Properties;

namespace TT_Lab.ViewModels
{
    public class ProjectCreationViewModel : ObservableObject
    {
        private string _projectName = "New project";
        private string _projectPath = Settings.Default.ProjectPath;
        private string _ps2DiscContentPath = Settings.Default.PS2DiscContentPath;
        private string _xboxDiscContentPath = Settings.Default.XboxDiscContentPath;
        private Window _owner;

        public ProjectCreationViewModel(Window owner)
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

        public ICommand SetPS2DiscContentPathCommand
        {
            get
            {
                return new SelectFolderCommand(_owner, this, "PS2DiscContentPath");
            }
        }

        public ICommand SetXboxDiscContentPathCommand
        {
            get
            {
                return new SelectFolderCommand(_owner, this, "XboxDiscContentPath");
            }
        }

        public bool CanCreate
        {
            get
            {
                return _projectName.Length != 0 && _projectPath.Length != 0 && (_ps2DiscContentPath.Length != 0 || _xboxDiscContentPath.Length != 0);
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
        public string PS2DiscContentPath
        {
            get
            {
                return _ps2DiscContentPath;
            }
            set
            {
                _ps2DiscContentPath = value;
                Settings.Default.PS2DiscContentPath = _ps2DiscContentPath;
                NotifyChange("CanCreate");
                NotifyChange("PS2DiscContentPath");
            }
        }

        public string XboxDiscContentPath
        {
            get
            {
                return _xboxDiscContentPath;
            }
            set
            {
                _xboxDiscContentPath = value;
                Settings.Default.XboxDiscContentPath = _xboxDiscContentPath;
                NotifyChange("CanCreate");
                NotifyChange("XboxDiscContentPath");
            }
        }
    }
}
