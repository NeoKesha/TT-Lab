using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Util;
using TT_Lab.ViewModels;

namespace TT_Lab.Project
{
    // Wrapper for keeping ProjectManager singleton and keep the ability to have ProjectManager as an observable object
    public static class ProjectManagerSingleton
    {
        private static ProjectManager _pm;

        public static ProjectManager PM
        {
            get
            {
                if (_pm == null)
                {
                    _pm = new ProjectManager();
                }
                return _pm;
            }
            private set
            {
                _pm = value;
            }
        }
    }

    public class ProjectManager : ObservableObject
    {
        private IProject _openedProject;
        private CommandManager _commandManager = new CommandManager();
        private MenuItem[] _recentMenus = new MenuItem[0];
        private List<AssetViewModel> _projectTree = new List<AssetViewModel>();
        private bool _workableProject = false;

        public IProject OpenedProject
        {
            get
            {
                return _openedProject;
            }
            set
            {
                _openedProject = value;
                NotifyChange("OpenedProject");
            }
        }

        public bool WorkableProject
        {
            get
            {
                return _workableProject;
            }
            set
            {
                if (value != _workableProject)
                {
                    _workableProject = value;
                    NotifyChange("WorkableProject");
                }
            }
        }

        public List<AssetViewModel> ProjectTree
        {
            get
            {
                return _projectTree;
            }
            private set
            {
                _projectTree = value;
            }
        }

        public bool ProjectOpened
        {
            get
            {
                return OpenedProject != null;
            }
        }

        public string ProjectTitle
        {
            get
            {
                return OpenedProject != null ? $"TT Lab - {OpenedProject.Name}" : "TT Lab";
            }
        }

        public MenuItem[] RecentlyOpened
        {
            get
            {
                var recents = Properties.Settings.Default.RecentProjects;
                if (recents != null && _recentMenus.Length != recents.Count)
                {
                    var menus = new MenuItem[recents.Count];
                    for (var i = 0; i < recents.Count; ++i)
                    {
                        menus[i] = new MenuItem
                        {
                            Header = $"{i + 1}. {recents[i]}",
                            Command = new OpenProjectCommand(recents[i]),
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                            VerticalAlignment = System.Windows.VerticalAlignment.Center,
                            
                        };
                    }
                    _recentMenus = menus;
                }
                return _recentMenus;
            }
        }
        
        public bool HasRecents
        {
            get
            {
                return RecentlyOpened.Length != 0;
            }
        }

        public void CreateProject(string name, string path, string discContentPath)
        {
            Log.Clear();
            DateTime projCreateStart = DateTime.Now;
            var discFiles = Directory.GetFiles(discContentPath).Select(s => Path.GetFileName(s)).ToArray();
            // Check for either XBOX or PS2 required root disc files
            if (!discFiles.Contains("default.xbe") && !discFiles.Contains("System.cnf"))
            {
                throw new Exception("Improper disc content provided!");
            }
            // Create PS2 type project
            if (discFiles.Contains("System.cnf"))
            {
                OpenedProject = new PS2Project(name, path, discContentPath);
            }
            else
            {
                // TODO: XBox type project
                throw new Exception("XBox project type not supported!");
            }
            OpenedProject.CreateProjectStructure();
            // Unpack assets
            Directory.CreateDirectory("assets");
            Directory.SetCurrentDirectory("assets");
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Log.WriteLine("Unpacking assets...");
                    OpenedProject.UnpackAssets();
                    Log.WriteLine($"Creating GUID Mapper...");
                    GuidManager.InitMappers(OpenedProject.Assets);
                    Log.WriteLine($"Converting assets...");
                    foreach (var asset in OpenedProject.Assets)
                    {
                        asset.Value.Import();
                    }
                    Log.WriteLine("Serializing assets...");
                    OpenedProject.Serialize(); // Call to serialize the asset list and chunk list
                    AddRecentlyOpened(OpenedProject.ProjectPath);
                    Log.WriteLine("Building project tree...");
                    BuildProjectTree();
                    WorkableProject = true;
                    NotifyChange("ProjectOpened");
                    NotifyChange("ProjectTitle");
                    Log.WriteLine($"Project created in {DateTime.Now - projCreateStart}");
                }
                catch(Exception ex)
                {
                    Log.WriteLine($"Error when working with assets: {ex.Message}");
                }
            });
        }

        public void OpenProject(string path)
        {
            Log.Clear();
            try
            {
                // Check for PS2 and XBox project root files
                if (Directory.GetFiles(path, "*.tson").Length == 0 && Directory.GetFiles(path, "*.xson").Length == 0)
                {
                    throw new Exception("No project root found!");
                }
                if (Directory.GetFiles(path, "*.tson").Length != 0)
                {
                    var prFile = Directory.GetFiles(path, "*.tson")[0];
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            Log.WriteLine($"Opening project {Path.GetFileName(prFile)}...");
                            OpenedProject = PS2Project.Deserialize(prFile);
                            Log.WriteLine($"Building project tree...");
                            BuildProjectTree();
                            WorkableProject = true;
                            NotifyChange("ProjectOpened");
                            NotifyChange("ProjectTitle");
                        }
                        catch (Exception ex)
                        {
                            Log.WriteLine($"Error opening project: {ex.Message}");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                RemoveRecentlyOpened(path);
                throw ex;
            }
            AddRecentlyOpened(path);
        }

        public void CloseProject()
        {
            OpenedProject = null;
            ProjectTree = null;
            Log.Clear();
            NotifyChange("ProjectOpened");
            NotifyChange("ProjectTitle");
            NotifyChange("ProjectTree");
        }

        public void ExecuteCommand(ICommand command)
        {
            _commandManager.Execute(command);
        }

        public void Undo()
        {
            _commandManager.Undo();
        }

        public void Redo()
        {
            _commandManager.Redo();
        }

        private void BuildProjectTree()
        {
            ProjectTree = (from asset in OpenedProject.Assets.Values
                           where asset.Type == "Folder"
                           let folder = asset as Folder
                           where ((FolderData)folder.GetData()).Parent == null
                           orderby folder.Order
                           select new AssetViewModel(folder.UUID)).ToList();
            NotifyChange("ProjectTree");
        }

        private void AddRecentlyOpened(string path)
        {
            if (Properties.Settings.Default.RecentProjects == null)
            {
                Properties.Settings.Default.RecentProjects = new System.Collections.Specialized.StringCollection();
            }
            if (!Properties.Settings.Default.RecentProjects.Contains(path))
            {
                Properties.Settings.Default.RecentProjects.Insert(0, path);
                // Store only last 10 paths
                if (Properties.Settings.Default.RecentProjects.Count > 10)
                {
                    Properties.Settings.Default.RecentProjects.RemoveAt(10);
                }
                NotifyChange("RecentlyOpened");
                NotifyChange("HasRecents");
            }
        }

        private void RemoveRecentlyOpened(string path)
        {
            if (Properties.Settings.Default.RecentProjects == null || !Properties.Settings.Default.RecentProjects.Contains(path)) return;

            var recents = Properties.Settings.Default.RecentProjects;
            recents.Remove(path);
            NotifyChange("RecentlyOpened");
            NotifyChange("HasRecents");
        }
    }
}
