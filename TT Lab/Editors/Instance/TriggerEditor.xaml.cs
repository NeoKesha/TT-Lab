using System;
using System.Collections.Generic;
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
    }
}
