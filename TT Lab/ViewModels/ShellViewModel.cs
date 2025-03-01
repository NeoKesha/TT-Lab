using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TT_Lab.Assets;
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.Project;
using TT_Lab.Project.Messages;
using TT_Lab.Rendering;
using TT_Lab.Util;
using TT_Lab.ViewModels.Composite;
using TT_Lab.ViewModels.Editors;

namespace TT_Lab.ViewModels
{
    public class ShellViewModel : Conductor<EditorsViewModel>, IHandle<ProjectManagerMessage>
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ProjectManager _projectManager;
        private readonly OgreWindowManager _ogreWindowManager;
        private readonly Dictionary<String, String> _managerPropsToShellProps = new();
        private readonly DispatcherTimer _renderTimer = new();

        public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, ProjectManager projectManager, OgreWindowManager ogreWindowManager)
        {
            _windowManager = windowManager;
            _projectManager = projectManager;
            _eventAggregator = eventAggregator;
            _ogreWindowManager = ogreWindowManager;
            _eventAggregator.SubscribeOnUIThread(this);

            _managerPropsToShellProps.Add(nameof(ProjectManager.ProjectTitle), nameof(WindowTitle));
            _managerPropsToShellProps.Add(nameof(ProjectManager.ProjectOpened), nameof(ProjectOpened));
            _managerPropsToShellProps.Add(nameof(ProjectManager.RecentlyOpened), nameof(RecentlyOpened));
            _managerPropsToShellProps.Add(nameof(ProjectManager.ProjectTree), nameof(ProjectTree));
            _managerPropsToShellProps.Add(nameof(ProjectManager.HasRecents), nameof(HasRecents));
            _managerPropsToShellProps.Add(nameof(ProjectManager.SearchAsset), nameof(SearchAsset));

            CompositionTarget.Rendering += (sender, args) =>
            {
                _ogreWindowManager.Render();
            };
        }

        // private void PerformRender(Object? sender, EventArgs e)
        // {
        //     _ogreWindowManager.Render();
        // }

        public Task About()
        {
            return _windowManager.ShowDialogAsync(IoC.Get<AboutViewModel>());
        }

        public Task CreateProject()
        {
            return _windowManager.ShowDialogAsync(IoC.Get<ProjectCreationViewModel>());
        }

        public void SaveProject()
        {
            if (!_projectManager.ProjectOpened) return;

            _projectManager.WorkableProject = false;
            try
            {
                Log.WriteLine($"Saving project...");
                var now = DateTime.Now;
                ActiveItem.Save();
                Log.WriteLine($"Saved project in {DateTime.Now - now}");
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Error saving project: {ex.Message}");
            }
            finally
            {
                _projectManager.WorkableProject = true;
            }
        }

        public void AssetBlockMouseDown(object selectedItem, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
            {
                return;
            }

            try
            {
                var editorsViewModel = ActiveItem;
                var asset = (ResourceTreeElementViewModel)selectedItem;
                if (asset.Asset.Type == typeof(Folder) || asset.Asset.Type == typeof(Package)) return;

                if (asset.Asset.Type == typeof(ChunkFolder))
                {
                    // Automatically switch to Scenes Viewer tab
                    editorsViewModel.ActivateItemAsync(editorsViewModel.Items[0]);
                    _eventAggregator.PublishOnUIThreadAsync(new CreateEditorMessage<ChunkEditorViewModel>(asset.Asset.URI, typeof(ChunkEditorViewModel)));
                    return;
                }

                // Automatically switch to Resources Editor tab
                editorsViewModel.ActivateItemAsync(editorsViewModel.Items[1]);
                var editorType = asset.Asset.GetEditorType();
                var message = new CreateEditorMessage<ResourceEditorViewModel>(asset.Asset.URI, editorType);
                _eventAggregator.PublishOnUIThreadAsync(message);
            }
            catch (Exception ex)
            {
                Log.WriteLine($"Failed to create editor: {ex.Message}");
            }
        }

        public void AssetBlockMouseMove(TreeView projectTree, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            var asset = (ResourceTreeElementViewModel)projectTree.SelectedItem;
            var data = new DraggedData
            {
                Data = asset
            };
            DragDrop.DoDragDrop(projectTree, data, DragDropEffects.Copy);
        }

        // public void ClosingApplication(CancelEventArgs e)
        // {
        //     _projectManager.CloseApplication();
        //     SaveProject();
        // }

        // Props to https://stackoverflow.com/a/25765336
        public void LogViewerScroll(ScrollViewer sv, ScrollChangedEventArgs e)
        {
            bool autoScrollToEnd = true;
            if (sv.Tag != null)
            {
                autoScrollToEnd = (bool)sv.Tag;
            }
            if (e.ExtentHeightChange == 0)// user scroll
            {
                autoScrollToEnd = sv.ScrollableHeight == sv.VerticalOffset;
            }
            else// content change
            {
                if (autoScrollToEnd)
                {
                    sv.ScrollToEnd();
                }
            }
            sv.Tag = autoScrollToEnd;
        }

        public void BuildPs2()
        {
            _projectManager.BuildPs2Project();
        }

        public void CloseProject()
        {
            _projectManager.CloseProject();
        }

        public void OpenProject()
        {
            var recents = Properties.Settings.Default.RecentProjects;
            var proj = MiscUtils.GetFileFromDialogue("PS2 TT Lab Project|*.tson|XBox TT Lab Project|*.xson", (recents != null && recents.Count != 0 ? recents[0] : "")!);
            if (proj != string.Empty)
            {
                var open = new OpenProjectCommand(System.IO.Path.GetDirectoryName(proj)!);
                open.Execute();
            }
        }

        public override async Task<Boolean> CanCloseAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await ActiveItem.CanCloseAsync(cancellationToken);
        }

        public Task HandleAsync(ProjectManagerMessage message, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
                {
                    if (!_managerPropsToShellProps.TryGetValue(message.PropertyName, out String? propName))
                    {
                        return;
                    }

                    NotifyOfPropertyChange(propName);
                },
                cancellationToken);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ActivateItemAsync(IoC.Get<EditorsViewModel>(), cancellationToken);
            return base.OnInitializeAsync(cancellationToken);
        }

        protected override Task OnDeactivateAsync(Boolean close, CancellationToken cancellationToken)
        {
            if (close)
            {
                Properties.Settings.Default.Save();
                Preferences.Save();
            }
            return base.OnDeactivateAsync(close, cancellationToken);
        }

        public BindableCollection<MenuItem> RecentlyOpened
        {
            get => _projectManager.RecentlyOpened;
        }

        public String WindowTitle
        {
            get => _projectManager.ProjectTitle;
        }

        public String SearchAsset
        {
            get => _projectManager.SearchAsset;
            set => _projectManager.SearchAsset = value;
        }

        public Boolean HasRecents
        {
            get => _projectManager.HasRecents;
        }

        public BindableCollection<ResourceTreeElementViewModel> ProjectTree
        {
            get => _projectManager.ProjectTree;
        }

        public Boolean ProjectOpened
        {
            get => _projectManager.ProjectOpened;
        }
    }
}
