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
        }

        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            using (BetterFolderBrowser bfb = new BetterFolderBrowser())
            {
                if (System.Windows.Forms.DialogResult.OK == bfb.ShowDialog())
                {
                    try
                    {
                        ProjectManagerSingleton.PM.OpenProject(bfb.SelectedPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to open project: {ex.Message}", "Error opening project!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void CloseProject_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagerSingleton.PM.CloseProject();
        }
    }
}
