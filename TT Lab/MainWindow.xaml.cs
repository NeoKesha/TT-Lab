using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
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
        }

        private void About_Click(Object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void CreateProject_Click(Object sender, RoutedEventArgs e)
        {
            ProjectCreationWizard wizard = new ProjectCreationWizard();
            wizard.ShowDialog();
        }

        private void OpenProject_Click(Object sender, RoutedEventArgs e)
        {
            using (BetterFolderBrowser bfb = new BetterFolderBrowser())
            {
                if (System.Windows.Forms.DialogResult.OK == bfb.ShowDialog())
                {
                    var suc = ProjectManager.OpenProject(bfb.SelectedPath);
                    if (!suc)
                    {
                        MessageBox.Show("Failed to open project!", "Error opening project!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void CloseProject_Click(Object sender, RoutedEventArgs e)
        {
            ProjectManager.CloseProject();
        }
    }
}
