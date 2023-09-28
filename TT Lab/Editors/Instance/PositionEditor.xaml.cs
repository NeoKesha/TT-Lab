using System;
using System.Windows;
using TT_Lab.Controls;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for PositionEditor.xaml
    /// </summary>
    public partial class PositionEditor : BaseEditor
    {

        public PositionEditor()
        {
            InitializeComponent();
        }

        public PositionEditor(PositionViewModel positionModel, Command.CommandManager commandManager) : base(positionModel, commandManager)
        {
            var pvm = positionModel;
            InitializeComponent();
            DataContext = new
            {
                ViewModel = pvm,
                Layers = Util.Layers
            };
            InitValidators();
            Loaded += PositionEditor_Loaded;
        }

        private void PositionEditor_Loaded(Object sender, RoutedEventArgs e)
        {
            var chunkEditor = (ChunkEditor)ParentEditor!;
            var vm = GetViewModel<PositionViewModel>();
            chunkEditor?.SceneRenderer.Scene.SetCameraPosition(new GlmNet.vec3(-vm.Position.X, vm.Position.Y, vm.Position.Z));
        }

        private void InitValidators()
        {
            foreach (var pair in VectorEditor.GetValidators())
            {
                AcceptNewPropValuePredicate.Add(pair.Key, pair.Value);
            }
        }
    }
}
