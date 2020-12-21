using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            // Suppress Binding errors for dynamically changing item collection within UIs: ListBox, MenuItem, etc.
#if DEBUG
            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
#endif
            InitializeComponent();
            About.Command = new OpenDialogueCommand(typeof(TT_Lab.About));
            CreateProject.Command = new OpenDialogueCommand(typeof(TT_Lab.ProjectCreationWizard));
            OpenProject.Command = new OpenProjectDialogueCommand();
            SaveProject.Command = new SaveProjectCommand();
            AddKeybind(OpenProject.Command, Key.O, ModifierKeys.Control);
            AddKeybind(SaveProject.Command, Key.S, ModifierKeys.Control);
            DataContext = ProjectManagerSingleton.PM;
            Closed += MainWindow_Closed;
            Log.SetLogBox(LogText);
        }

        private void MainWindow_Closed(Object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void CloseProject_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagerSingleton.PM.CloseProject();
        }

        private void AddKeybind(System.Windows.Input.ICommand command, Key key, ModifierKeys modifierKeys)
        {
            InputBindings.Add(new KeyBinding(command, key, modifierKeys));
        }

        // Props to https://stackoverflow.com/a/25765336
        private void LogViewer_ScrollChanged(Object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer sv = sender as ScrollViewer;
            bool AutoScrollToEnd = true;
            if (sv.Tag != null)
            {
                AutoScrollToEnd = (bool)sv.Tag;
            }
            if (e.ExtentHeightChange == 0)// user scroll
            {
                AutoScrollToEnd = sv.ScrollableHeight == sv.VerticalOffset;
            }
            else// content change
            {
                if (AutoScrollToEnd)
                {
                    sv.ScrollToEnd();
                }
            }
            sv.Tag = AutoScrollToEnd;
        }
    }
}
