using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TT_Lab.Assets.Code;
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.Project;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for ObjectInstanceEditor.xaml
    /// </summary>
    public partial class ObjectInstanceEditor : BaseEditor
    {
        public ObjectInstanceEditor()
        {
            InitializeComponent();
        }

        public ObjectInstanceEditor(ObjectInstanceViewModel vm, CommandManager commandManager) : base(vm, commandManager)
        {
            InitializeComponent();
            InitValidators();
            Loaded += ObjectInstanceEditor_Loaded;
        }

        private void ObjectInstanceEditor_Loaded(Object sender, RoutedEventArgs e)
        {
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            var chunkEditor = (ChunkEditor)ParentEditor!;
            chunkEditor?.SceneRenderer.Scene.SetCameraPosition(new GlmNet.vec3(-vm.Position.X, vm.Position.Y, vm.Position.Z));

            var allObj = new ObservableCollection<object>(ProjectManagerSingleton.PM.OpenedProject.Assets.Values.Where(a => a.Type == typeof(GameObject)).Select(a => a.GetViewModel()).Cast<object>());
            var allScripts = new ObservableCollection<object>(ProjectManagerSingleton.PM.OpenedProject.Assets.Values.Where(a => a.Type == typeof(HeaderScript)).Select(a => a.GetViewModel()).Cast<object>());
            DataContext = new
            {
                ViewModel = vm,
                Layers = Util.Layers,
                AllObjects = allObj,
                AllScripts = allScripts,
            };

            IntPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Add",
                Command = new RelayCommand(vm.AddIntParamCommand, CommandManager)
            });
            IntPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(vm.DeleteIntParamCommand, CommandManager)
            });
            FlagPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Add",
                Command = new RelayCommand(vm.AddFlagParamCommand, CommandManager)
            });
            FlagPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(vm.DeleteFlagParamCommand, CommandManager)
            });
            FloatPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Add",
                Command = new RelayCommand(vm.AddFloatParamCommand, CommandManager)
            });
            FloatPropsContextMenu.Items.Add(new MenuItem
            {
                Header = "Delete",
                Command = new RelayCommand(vm.DeleteFloatParamCommand, CommandManager)
            });
        }

        private void InitValidators()
        {
            foreach (var rule in VectorEditor.GetValidators())
            {
                AcceptNewPropValuePredicate.Add(rule.Key, rule.Value);
            }
            var vm = (ObjectInstanceViewModel)AssetViewModel;
            AcceptNewPropValuePredicate[nameof(vm.StateFlags)] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (nStr == oStr) return null;
                if (string.IsNullOrEmpty(nStr))
                {
                    return (UInt32)0;
                }
                if (!UInt32.TryParse(nStr, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out UInt32 result)) return null;
                return result;
            };
            AcceptNewPropValuePredicate[nameof(vm.SelectedFlag)] = AcceptNewPropValuePredicate[nameof(vm.StateFlags)];
            AcceptNewPropValuePredicate[nameof(vm.SelectedFloat)] = AcceptNewPropValuePredicate["X"];
            AcceptNewPropValuePredicate[nameof(vm.SelectedInt)] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (nStr == oStr) return null;
                if (string.IsNullOrEmpty(nStr))
                {
                    return (UInt32)0;
                }
                if (!UInt32.TryParse(nStr, out UInt32 result)) return null;
                return result;
            };
        }

        private void IntPropsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (IntPropsList.SelectedIndex == -1) return;

            var vm = (ObjectInstanceViewModel)AssetViewModel;
            vm.DeleteIntParamCommand.Index = IntPropsList.SelectedIndex;
            vm.IntIndex = IntPropsList.SelectedIndex;
        }

        private void FlagPropsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (FlagPropsList.SelectedIndex == -1) return;

            var vm = (ObjectInstanceViewModel)AssetViewModel;
            vm.DeleteFlagParamCommand.Index = FlagPropsList.SelectedIndex;
            vm.FlagIndex = FlagPropsList.SelectedIndex;
        }

        private void FloatPropsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (FloatPropsList.SelectedIndex == -1) return;

            var vm = (ObjectInstanceViewModel)AssetViewModel;
            vm.DeleteFloatParamCommand.Index = FloatPropsList.SelectedIndex;
            vm.FloatIndex = FloatPropsList.SelectedIndex;
        }
    }
}
