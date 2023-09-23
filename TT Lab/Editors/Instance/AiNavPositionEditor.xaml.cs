using System;
using System.Windows;
using TT_Lab.Controls;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for AiNavPositionEditor.xaml
    /// </summary>
    public partial class AiNavPositionEditor : BaseEditor
    {
        public AiNavPositionEditor()
        {
            InitializeComponent();
        }

        public AiNavPositionEditor(AIPositionViewModel aiPosViewModel, Command.CommandManager commandManager) : base(aiPosViewModel, commandManager)
        {
            InitializeComponent();
            DataContext = new
            {
                ViewModel = aiPosViewModel,
                Layers = Util.Layers
            };
            InitValidators();
            Loaded += AiNavPositionEditor_Loaded;
        }

        private void InitValidators()
        {
            foreach (var pair in VectorEditor.GetValidators())
            {
                AcceptNewPropValuePredicate.Add(pair.Key, pair.Value);
            }
            var vm = (AIPositionViewModel)AssetViewModel;
            AcceptNewPropValuePredicate[nameof(vm.Argument)] = (n, o) =>
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
        }

        private void AiNavPositionEditor_Loaded(Object sender, RoutedEventArgs e)
        {
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var vm = (AIPositionViewModel)AssetViewModel;
            chunkEditor?.SceneRenderer.Scene.SetCameraPosition(new GlmNet.vec3(-vm.Position.X, vm.Position.Y, vm.Position.Z));
        }
    }
}
