using System;
using System.Windows;

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
    }
}
