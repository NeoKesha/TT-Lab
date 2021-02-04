using System;
using System.Collections.Generic;
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
using TT_Lab.Command;
using TT_Lab.Controls;
using TT_Lab.ViewModels.Instance;

namespace TT_Lab.Editors.Instance
{
    /// <summary>
    /// Interaction logic for PathEditor.xaml
    /// </summary>
    public partial class PathEditor : BaseEditor
    {
        public PathEditor()
        {
            InitializeComponent();
        }

        public PathEditor(PathViewModel pvm, CommandManager comManager) : base(pvm, comManager)
        {
            InitializeComponent();
            DataContext = new
            {
                ViewModel = pvm,
                Layers = Util.Layers
            };
            InitValidators();
        }

        private void InitValidators()
        {
            foreach (var pair in VectorEditor.GetValidators())
            {
                AcceptNewPropValuePredicate.Add(pair.Key, pair.Value);
            }
        }

        private void PointsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (PointsList.SelectedItem == null) return;

            var vm = (PathViewModel)AssetViewModel;
            vm.DeletePointCommand.Item = PointsList.SelectedItem;
            CoordEditor.VectorComponentsAmount = 4;
            CoordEditor.PropertyTarget = PointsList.SelectedItem;
        }

        private void ArgumentsList_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (ArgumentsList.SelectedItem == null) return;

            var vm = (PathViewModel)AssetViewModel;
            vm.DeleteArgumentCommand.Item = ArgumentsList.SelectedItem;
            CoordEditor.VectorComponentsAmount = 2;
            CoordEditor.PropertyTarget = ArgumentsList.SelectedItem;
        }

        private void PointsList_GotFocus(Object sender, RoutedEventArgs e)
        {
            PointsList_SelectionChanged(sender, null);
        }

        private void ArgumentsList_GotFocus(Object sender, RoutedEventArgs e)
        {
            ArgumentsList_SelectionChanged(sender, null);
        }
    }
}
