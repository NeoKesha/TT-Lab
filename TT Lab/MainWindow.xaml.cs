using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
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
                    if (Directory.GetFiles(bfb.SelectedPath, "*.tson").Length > 0)
                    {
                        var prFile = Directory.GetFiles(bfb.SelectedPath, "*.tson")[0];
                        using (FileStream fs = new FileStream(prFile, FileMode.Open, FileAccess.Read))
                        using (BinaryReader reader = new BinaryReader(fs))
                        {
                            var prText = new string(reader.ReadChars((Int32)fs.Length));
                            Project.Project pr = JsonConvert.DeserializeObject<Project.Project>(prText);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can't find project file!", "No project file found!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
