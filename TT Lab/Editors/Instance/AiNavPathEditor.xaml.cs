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
            DataContext = new
            {
                ViewModel = (AiPathViewModel)AssetViewModel,
                Layers = Util.Layers,
                Positions = positions
            };
        }
    }
}
