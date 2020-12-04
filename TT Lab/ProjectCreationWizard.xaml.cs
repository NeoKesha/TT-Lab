using System;
using System.ComponentModel;
using System.Windows;
using WK.Libraries.BetterFolderBrowserNS;

namespace TT_Lab
{
    /// <summary>
    /// Interaction logic for ProjectCreationWizard.xaml
    /// </summary>
    public partial class ProjectCreationWizard : Window, INotifyPropertyChanged
    {

        private bool HasProjectName { get { return ProjectName.Text.Length != 0; } }
        private bool HasProjectPath { get { return ProjectPath.Text.Length != 0; } }
        private bool HasDiscContentPath { get { return DiscContentPath.Text.Length != 0; } }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public bool CanCreate { get { return _initComponent && HasProjectName && HasProjectPath && HasDiscContentPath; } }

        private readonly bool _initComponent = false;

        public ProjectCreationWizard()
        {
            InitializeComponent();
            _initComponent = true;
            DataContext = this;
        }

        private void ProjectPathButton_Click(Object sender, RoutedEventArgs e)
        {
            using (BetterFolderBrowser bfb = new BetterFolderBrowser
            {
                Title = "Select project path..."
            })
            {
                if (System.Windows.Forms.DialogResult.OK == bfb.ShowDialog())
                {
                    ProjectPath.Text = bfb.SelectedPath;
                }
            }
            RaisePropertyChanged("CanCreate");
        }

        private void DiscContentPathButton_Click(Object sender, RoutedEventArgs e)
        {
            using (BetterFolderBrowser bfb = new BetterFolderBrowser
            {
                Title = "Select disc content path..."
            })
            {
                if (System.Windows.Forms.DialogResult.OK == bfb.ShowDialog())
                {
                    DiscContentPath.Text = bfb.SelectedPath;
                    // TODO: Check for proper contents
                }
            }
            RaisePropertyChanged("CanCreate");
        }

        private void CreateButton_Click(Object sender, RoutedEventArgs e)
        {
            
        }

        private void ProjectName_TextChanged(Object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            RaisePropertyChanged("CanCreate");
        }

        private void RaisePropertyChanged(string propName)
        {
            var handlers = PropertyChanged;

            handlers(this, new PropertyChangedEventArgs(propName));
        }
    }
}
