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
        }
    }
}
