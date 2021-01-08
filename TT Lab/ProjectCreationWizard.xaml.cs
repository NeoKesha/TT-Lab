using System;
using System.ComponentModel;
using System.Windows;
using TT_Lab.Project;
using TT_Lab.ViewModels;

namespace TT_Lab
{
    /// <summary>
    /// Interaction logic for ProjectCreationWizard.xaml
    /// </summary>
    public partial class ProjectCreationWizard : Window
    {
        public ProjectCreationWizard()
        {
            DataContext = new ProjectCreator(this);
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProjectManagerSingleton.PM.CreateProject(ProjectName.Text, ProjectPath.Text, DiscContentPath.Text);
                Close();
            }
            catch(Exception ex)
            {
                Log.WriteLine($"Error creating project: {ex.Message}");
            }
        }
    }
}
