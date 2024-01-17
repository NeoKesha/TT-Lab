using System;
using System.Collections.ObjectModel;
using TT_Lab.Command;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for AiNavPathEditor.xaml
    /// </summary>
    public partial class AiNavPathEditor : BaseEditor
    {
        private ObservableCollection<object> positions;

        public AiNavPathEditor()
        {
            InitializeComponent();
        }

        public AiNavPathEditor(AiPathViewModel apvm, CommandManager commandManager) : base(apvm, commandManager)
        {
            positions = new ObservableCollection<object>();
            InitializeComponent();
            Loaded += AiNavPathEditor_Loaded;
            InitValidators();
        }

        private void AiNavPathEditor_Loaded(System.Object sender, System.Windows.RoutedEventArgs e)
        {
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var tree = chunkEditor!.ChunkTree;
            var navPositions = tree.Find(avm => avm.Alias == "AI Navigation Positions");
            foreach (var p in navPositions!.Children)
            {
                positions.Add((UInt16)p.Asset.ID);
            }
            DataContext = new AiPathViewModelSearch(GetViewModel<AiPathViewModel>(), positions);
        }

        private void InitValidators()
        {
            var vm = GetViewModel<AiPathViewModel>();
            AcceptNewPropValuePredicate[nameof(vm.Arg1)] = (n, o) =>
            {
                var nStr = (string)n;
                var oStr = (string)o;
                if (nStr == oStr) return null;
                if (string.IsNullOrEmpty(nStr))
                {
                    return (UInt16)0;
                }
                if (!UInt16.TryParse(nStr, out UInt16 result)) return null;
                return result;
            };
            AcceptNewPropValuePredicate[nameof(vm.Arg2)] = AcceptNewPropValuePredicate[nameof(vm.Arg1)];
            AcceptNewPropValuePredicate[nameof(vm.Arg3)] = AcceptNewPropValuePredicate[nameof(vm.Arg1)];
        }
    }
}
