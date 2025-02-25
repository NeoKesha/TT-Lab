using Caliburn.Micro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TT_Lab.Command;
using TT_Lab.Project;
using TT_Lab.Properties;

namespace TT_Lab.ViewModels
{
    public class ProjectCreationViewModel : Screen, INotifyDataErrorInfo
    {
        private string _projectName = "New project";
        private string _projectPath = Settings.Default.ProjectPath;
        private string _ps2DiscContentPath = Settings.Default.PS2DiscContentPath;
        private string _xboxDiscContentPath = Settings.Default.XboxDiscContentPath;
        private readonly IWindowManager _windowManager;
        private readonly ProjectManager _projectManager;

        private readonly Dictionary<String, List<String>> _propertyErrors = new();
        private readonly Dictionary<String, Func<Boolean>> _discContentIsCurrentValidMap = new();

        const Int32 PROJECT_NAME_LIMIT = 32;
        const String PROJECT_NAME_INVALID_CHARS_ERROR = "Project name must not contain invalid characters";
        const String PROJECT_NAME_EMPTY_ERROR = "Project name must not be empty";
        const String PROJECT_NAME_TOO_LONG_ERROR = "Project name must be less than 32 characters long";
        const String PROJECT_PATH_EMPTY_ERROR = "Project path must not be empty";
        const String DISC_CONTENT_PATH_EMPTY_ERROR = "PS2 and XBox disc content paths must not be both empty";

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public ProjectCreationViewModel(IWindowManager windowManager, ProjectManager projectManager)
        {
            _windowManager = windowManager;
            _projectManager = projectManager;
            _discContentIsCurrentValidMap.Add(nameof(PS2DiscContentPath), IsPs2DiscContentPathValid);
            _discContentIsCurrentValidMap.Add(nameof(XboxDiscContentPath), IsXboxDiscContentPathValid);
        }

        public ICommand SetProjectPathCommand => new SelectFolderCommand(null, this, nameof(ProjectPath));

        public ICommand SetPS2DiscContentPathCommand => new SelectFolderCommand(null, this, nameof(PS2DiscContentPath));

        public ICommand SetXboxDiscContentPathCommand => new SelectFolderCommand(null, this, nameof(XboxDiscContentPath));

        public Task Create()
        {
#if !DEBUG
            try
            {
#endif
            if (_projectManager.OpenedProject != null)
            {
                _projectManager.CloseProject();
            }
            _projectManager.CreateProject(ProjectName, ProjectPath, PS2DiscContentPath, XboxDiscContentPath);

#if !DEBUG
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Error creating project: {ex.Message}");
            }
#endif
            return TryCloseAsync();
        }

        public Boolean IsProjectNameValid(String projectName)
        {
            var isValid = true;

            if (String.IsNullOrEmpty(projectName))
            {
                AddError(nameof(ProjectName), PROJECT_NAME_EMPTY_ERROR);
                isValid = false;
            }
            else
            {
                RemoveError(nameof(ProjectName), PROJECT_NAME_EMPTY_ERROR);
            }

            if (projectName.Length > PROJECT_NAME_LIMIT)
            {
                AddError(nameof(ProjectName), PROJECT_NAME_TOO_LONG_ERROR);
                isValid = false;
            }
            else
            {
                RemoveError(nameof(ProjectName), PROJECT_NAME_TOO_LONG_ERROR);
            }

            if (projectName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                AddError(nameof(ProjectName), PROJECT_NAME_INVALID_CHARS_ERROR);
                isValid = false;
            }
            else
            {
                RemoveError(nameof(ProjectName), PROJECT_NAME_INVALID_CHARS_ERROR);
            }

            return isValid;
        }

        public Boolean IsProjectPathValid(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                AddError(nameof(ProjectPath), PROJECT_PATH_EMPTY_ERROR);
                return false;
            }

            RemoveError(nameof(ProjectPath));
            return true;
        }

        public Boolean IsDiscContentPathValid(String otherContentPathProperty, String discContentPath)
        {
            Debug.Assert(_discContentIsCurrentValidMap.ContainsKey(otherContentPathProperty), "Invalid property name passed to check!");

            if (!_discContentIsCurrentValidMap[otherContentPathProperty]() && String.IsNullOrEmpty(discContentPath))
            {
                AddError(nameof(PS2DiscContentPath), DISC_CONTENT_PATH_EMPTY_ERROR);
                AddError(nameof(XboxDiscContentPath), DISC_CONTENT_PATH_EMPTY_ERROR);
                return false;
            }

            RemoveError(nameof(PS2DiscContentPath));
            RemoveError(nameof(XboxDiscContentPath));
            return true;
        }

        public Boolean IsPs2DiscContentPathValid()
        {
            return !String.IsNullOrEmpty(PS2DiscContentPath);
        }

        public Boolean IsXboxDiscContentPathValid()
        {
            return !String.IsNullOrEmpty(XboxDiscContentPath);
        }

        public void AddError(String propertyName, String message)
        {
            if (!_propertyErrors.ContainsKey(propertyName))
            {
                _propertyErrors[propertyName] = new List<String>();
            }

            if (!_propertyErrors[propertyName].Contains(message))
            {
                _propertyErrors[propertyName].Add(message);
                RaiseErrorsChanged(propertyName);
            }
        }

        public void RemoveError(String propertyName, String? message = null)
        {
            if (!_propertyErrors.TryGetValue(propertyName, out List<String>? errorsList))
            {
                return;
            }

            if (String.IsNullOrEmpty(message))
            {
                errorsList.Clear();
            }
            else
            {
                if (!errorsList.Contains(message))
                {
                    return;
                }
                errorsList.Remove(message);
            }

            if (errorsList.Count == 0)
            {
                _propertyErrors.Remove(propertyName);
            }
            RaiseErrorsChanged(propertyName);
        }

        public void RaiseErrorsChanged(String propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(String? propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) || !_propertyErrors.TryGetValue(propertyName, out List<String>? errorList))
            {
                return Enumerable.Empty<String>();
            }

            return errorList;
        }

        public bool CanCreate => !HasErrors;

        public string ProjectName
        {
            get => _projectName;
            set
            {
                IsProjectNameValid(value);
                _projectName = value;
                NotifyOfPropertyChange(nameof(ProjectName));
                NotifyOfPropertyChange(nameof(CanCreate));
            }
        }

        public string ProjectPath
        {
            get => _projectPath;
            set
            {
                if (IsProjectPathValid(value) && _projectPath != value)
                {
                    _projectPath = value;
                    Settings.Default.ProjectPath = _projectPath;
                    NotifyOfPropertyChange(nameof(ProjectPath));
                }
                NotifyOfPropertyChange(nameof(CanCreate));
            }
        }

        public string PS2DiscContentPath
        {
            get => _ps2DiscContentPath;
            set
            {
                if (IsDiscContentPathValid(nameof(XboxDiscContentPath), value) && _ps2DiscContentPath != value)
                {
                    _ps2DiscContentPath = value;
                    Settings.Default.PS2DiscContentPath = _ps2DiscContentPath;
                    NotifyOfPropertyChange(nameof(PS2DiscContentPath));
                }
                NotifyOfPropertyChange(nameof(CanCreate));
            }
        }

        public string XboxDiscContentPath
        {
            get => _xboxDiscContentPath;
            set
            {
                if (IsDiscContentPathValid(nameof(PS2DiscContentPath), value) && _xboxDiscContentPath != value)
                {
                    _xboxDiscContentPath = value;
                    Settings.Default.XboxDiscContentPath = _xboxDiscContentPath;
                    NotifyOfPropertyChange(nameof(XboxDiscContentPath));
                }
                NotifyOfPropertyChange(nameof(CanCreate));
            }
        }

        public Boolean HasErrors => _propertyErrors.Count > 0;
    }
}
