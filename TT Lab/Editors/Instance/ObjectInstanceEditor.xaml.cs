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
using TT_Lab.Assets.Code;
using TT_Lab.Command;
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
            var allObj = ProjectManagerSingleton.PM.OpenedProject.Assets.Values.Where(a => a.Type == typeof(GameObject)).Cast<object>().ToList();
            var allScripts = ProjectManagerSingleton.PM.OpenedProject.Assets.Values.Where(a => a.Type == typeof(HeaderScript)).Cast<object>().ToList();
            DataContext = new
            {
                ViewModel = vm,
                Layers = Util.Layers,
                AllObjects = allObj,
                AllScripts = allScripts,
            };
            InitValidators();
        }

        private void InitValidators()
        {
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
        }
    }
}
