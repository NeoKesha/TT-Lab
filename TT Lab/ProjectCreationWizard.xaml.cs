using AdonisUI.Controls;
using System.Windows;
using TT_Lab.Project;
using TT_Lab.ViewModels;

namespace TT_Lab
{
    /// <summary>
    /// Interaction logic for ProjectCreationWizard.xaml
    /// </summary>
    public partial class ProjectCreationWizard : AdonisWindow
    {
        public ProjectCreationWizard()
        {
            DataContext = new ProjectCreationViewModel(this);
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
#if !DEBUG
            try
            {
#endif
            ProjectManagerSingleton.PM.CreateProject(ProjectName.Text, ProjectPath.Text, PS2DiscContentPath.Text, XboxDiscContentPath.Text);
            Close();
#if !DEBUG
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Error creating project: {ex.Message}");
            }
#endif
        }
    }
}
