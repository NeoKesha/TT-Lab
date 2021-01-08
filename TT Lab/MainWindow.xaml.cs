using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.Project;
using TT_Lab.ViewModels;

namespace TT_Lab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private static RoutedCommand CloseTabCommand = new RoutedCommand();
        private static RoutedCommand UndoCommand = new RoutedCommand();
        private static RoutedCommand RedoCommand = new RoutedCommand();
        private static RoutedCommand SaveCommand = new RoutedCommand();

        public MainWindow()
        {
            // Suppress Binding errors for dynamically changing item collection within UIs: ListBox, MenuItem, etc.
#if DEBUG
            System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical;
#endif
            InitializeComponent();
            Log.SetLogBox(LogText);

            About.Command = new OpenDialogueCommand(typeof(TT_Lab.About));
            CreateProject.Command = new OpenDialogueCommand(typeof(TT_Lab.ProjectCreationWizard));
            OpenProject.Command = new OpenProjectDialogueCommand();
            SaveProject.Command = new SaveProjectCommand();
            // Main window binds
            AddKeybind(OpenProject.Command, Key.O, ModifierKeys.Control);
            AddKeybind(SaveProject.Command, Key.S, ModifierKeys.Control | ModifierKeys.Shift);
            // Due to the fact that focus is required on elements to use key bindings
            // all kinda global keybinds are bound to MainWindow, instead of elements that would make sense
            // Close tab command
            var closeTabCom = new CommandBinding(CloseTabCommand, CloseTabExecuted);
            CommandBindings.Add(closeTabCom);
            AddKeybind(closeTabCom.Command, Key.W, ModifierKeys.Control);
            // Undo/Redo commands
            var redoCom = new CommandBinding(RedoCommand, RedoExecuted);
            var undoCom = new CommandBinding(UndoCommand, UndoExecuted);
            CommandBindings.Add(redoCom);
            CommandBindings.Add(undoCom);
            AddKeybind(redoCom.Command, Key.Y, ModifierKeys.Control);
            AddKeybind(undoCom.Command, Key.Z, ModifierKeys.Control);
            // Save command
            var saveTabCom = new CommandBinding(SaveCommand, SaveExecuted);
            CommandBindings.Add(saveTabCom);
            AddKeybind(saveTabCom.Command, Key.S, ModifierKeys.Control);

            DataContext = ProjectManagerSingleton.PM;
            Closed += MainWindow_Closed;
        }

        private void SaveExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            GetActiveTab()?.Save();
        }

        private void UndoExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            GetActiveTab()?.Undo();
        }

        private void RedoExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            GetActiveTab()?.Redo();
        }

        private void CloseTabExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            GetActiveTab()?.Close();
            Focus();
        }

        private ClosableTab GetActiveTab()
        {
            TabItem tab = null;
            switch (CentralViewerTabs.SelectedIndex)
            {
                // Scenes viewer
                case 0:
                    tab = (TabItem)ScenesViewerTabs.SelectedItem;
                    break;
                // Resources editor
                case 1:
                    tab = (TabItem)ResourcesEditorTabs.SelectedItem;
                    break;
            }
            return (ClosableTab)tab?.Header;
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
            AddKeybind(this, command, key, modifierKeys);
        }

        public static void AddKeybind(Control control, System.Windows.Input.ICommand command, Key key, ModifierKeys modifierKeys)
        {
            control.InputBindings.Add(new KeyBinding(command, key, modifierKeys));
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

        private void AssetBlock_MouseDown(Object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                try
                {
                    var asset = (AssetViewModel)ProjectTree.SelectedItem;
                    if (asset.Asset.Type == typeof(Folder)) return;

                    if (asset.Asset.Type == typeof(ChunkFolder))
                    {
                        if (!asset.EditorOpened)
                        {
                            ScenesViewerTabs.Items.Add(asset.GetEditor(ScenesViewerTabs));
                        }
                        ScenesViewerTabs.SelectedItem = asset.GetEditor(ScenesViewerTabs);
                        // Automatically switch to Scenes Viewer tab
                        CentralViewerTabs.SelectedItem = CentralViewerTabs.Items[0];
                    }
                    else
                    {
                        if (!asset.EditorOpened)
                        {
                            ResourcesEditorTabs.Items.Add(asset.GetEditor(ResourcesEditorTabs));
                        }
                        ResourcesEditorTabs.SelectedItem = asset.GetEditor(ResourcesEditorTabs);
                        // Automatically switch to Resources Editor tab
                        CentralViewerTabs.SelectedItem = CentralViewerTabs.Items[1];
                    }

                }
                catch (Exception ex)
                {
                    Log.WriteLine($"Failed to create editor: {ex.Message}");
                }
            }
        }

        private void AssetBlock_MouseMove(Object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var asset = (AssetViewModel)ProjectTree.SelectedItem;
                var data = new DraggedData
                {
                    Data = asset
                };
                DragDrop.DoDragDrop(ProjectTree, data, DragDropEffects.Copy);
            }
        }
    }
}
