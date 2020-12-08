using System;
using System.ComponentModel;
using System.Windows;
using WK.Libraries.BetterFolderBrowserNS;

namespace TT_Lab
{
    /// <summary>
    /// Interaction logic for ProjectCreationWizard.xaml
    /// </summary>
    public partial class ProjectCreationWizard : Window
    {

        public ProjectCreationWizard()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Project.Project pr = new Project.Project(ProjectName.Text, ProjectPath.Text, DiscContentPath.Text);
            Console.WriteLine("Project created!");
        }
    }
}
