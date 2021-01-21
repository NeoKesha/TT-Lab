using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TT_Lab.AssetData.Instance;
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.ViewModels;
using TT_Lab.ViewModels.Instance;
using Twinsanity.TwinsanityInterchange.Common;
using Twinsanity.TwinsanityInterchange.Enumerations;

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

        public PositionEditor(AssetViewModel positionModel, Command.CommandManager commandManager) : base(positionModel, commandManager)
        {
            var pvm = (PositionViewModel)positionModel;
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
            var vm = (PositionViewModel)AssetViewModel;
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
