using System;
using System.Windows.Controls;
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for TriggerEditor.xaml
    /// </summary>
    public partial class TriggerEditor : BaseEditor
    {
        public TriggerEditor()
        {
            InitializeComponent();
        }

        public TriggerEditor(TriggerViewModel viewModel, Command.CommandManager commandManager) : base(viewModel, commandManager)
        {
            InitializeComponent();
            DataContext = new
            {
                ViewModel = viewModel,
                Layers = Util.Layers
            };
            InitValidators();
            InstancesListContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(viewModel.DeleteInstanceFromListCommand, commandManager)
            });
            Loaded += TriggerEditor_Loaded;
        }

        private void TriggerEditor_Loaded(Object sender, System.Windows.RoutedEventArgs e)
        {
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var vm = (TriggerViewModel)AssetViewModel;
            chunkEditor?.SceneRenderer.Scene.SetCameraPosition(new GlmNet.vec3(-vm.Position.X, vm.Position.Y, vm.Position.Z));
        }

        private void InitValidators()
        {
            foreach (var pair in VectorEditor.GetValidators())
            {
                AcceptNewPropValuePredicate.Add(pair.Key, pair.Value);
            }
            var vm = (TriggerViewModel)AssetViewModel;
            AcceptNewPropValuePredicate[nameof(vm.HeaderT)] = AcceptNewPropValuePredicate["X"];
            AcceptNewPropValuePredicate[nameof(vm.Header1)] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (nStr == oStr) return null;
                if (string.IsNullOrEmpty(nStr))
                {
                    return 0U;
                }
                if (!UInt32.TryParse(nStr, out UInt32 result)) return null;
                if (result > 27) return null;
                return result;
            };
            AcceptNewPropValuePredicate[nameof(vm.HeaderH)] = AcceptNewPropValuePredicate[nameof(vm.Header1)];
        }

        private void InstancesList_SelectionChanged(Object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var viewModel = (TriggerViewModel)AssetViewModel;
            viewModel.DeleteInstanceFromListCommand.Item = InstancesList.SelectedItem;
        }
    }
}
