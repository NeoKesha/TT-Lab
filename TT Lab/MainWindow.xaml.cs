using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using TT_Lab.Command;
using TT_Lab.Project;
using WK.Libraries.BetterFolderBrowserNS;

namespace TT_Lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            About.Command = new OpenDialogueCommand(typeof(TT_Lab.About));
            CreateProject.Command = new OpenDialogueCommand(typeof(TT_Lab.ProjectCreationWizard));
            DataContext = ProjectManagerSingleton.PM;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(Object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            var recents = Properties.Settings.Default.RecentProjects;
            using (BetterFolderBrowser bfb = new BetterFolderBrowser
            {
                RootFolder = recents != null ? recents[0] : ""
            })
            {
                if (System.Windows.Forms.DialogResult.OK == bfb.ShowDialog())
                {
                    var open = new OpenProjectCommand(bfb.SelectedPath);
                    open.Execute();
                }
            }
        }

        private void CloseProject_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagerSingleton.PM.CloseProject();
        }
    }
}
