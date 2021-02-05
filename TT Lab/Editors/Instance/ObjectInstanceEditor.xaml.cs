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
            DataContext = new
            {
                ViewModel = vm,
                Layers = Util.Layers
            };
        }
    }
}
