using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using TT_Lab.Command;
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
        private Project _openedProject;
        private CommandManager commandManager = new CommandManager();
        private MenuItem[] _recentMenus = new MenuItem[0];

        public Project OpenedProject
        {
            get
            {
                return _openedProject;
            }
            set
            {
                _openedProject = value;
                RaisePropertyChangedEvent("OpenedProject");
                RaisePropertyChangedEvent("ProjectOpened");
                RaisePropertyChangedEvent("ProjectTitle");
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
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                            VerticalAlignment = System.Windows.VerticalAlignment.Center
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
            var discFiles = Directory.GetFiles(discContentPath).Select(s => Path.GetFileName(s)).ToArray();
            // Check for either XBOX or PS2 required root disc files
            if (!discFiles.Contains("default.xbe") && !discFiles.Contains("System.cnf"))
            {
                throw new Exception("Improper disc content provided!");
            }
            OpenedProject = new Project(name, path, discContentPath);
            AddRecentlyOpened(OpenedProject.ProjectPath);
        }

        public void OpenProject(string path)
        {
            try
            {
                if (Directory.GetFiles(path, "*.tson").Length == 0)
                {
                    throw new Exception("No project root found!");
                }

                var prFile = Directory.GetFiles(path, "*.tson")[0];
                using (FileStream fs = new FileStream(prFile, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    var prText = new string(reader.ReadChars((Int32)fs.Length));
                    OpenedProject = JsonConvert.DeserializeObject<Project>(prText);
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
                if (Properties.Settings.Default.RecentProjects.Count >= 10)
                {
                    Properties.Settings.Default.RecentProjects.RemoveAt(9);
                }
                RaisePropertyChangedEvent("RecentlyOpened");
                RaisePropertyChangedEvent("HasRecents");
            }
        }

        private void RemoveRecentlyOpened(string path)
        {
            if (Properties.Settings.Default.RecentProjects == null || !Properties.Settings.Default.RecentProjects.Contains(path)) return;

            var recents = Properties.Settings.Default.RecentProjects;
            recents.Remove(path);
            RaisePropertyChangedEvent("RecentlyOpened");
            RaisePropertyChangedEvent("HasRecents");
        }
    }
}
