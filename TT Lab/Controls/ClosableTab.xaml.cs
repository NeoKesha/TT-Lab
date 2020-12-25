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
using TT_Lab.Command;
using TT_Lab.ViewModels;

namespace TT_Lab.Controls
{
    /// <summary>
    /// Interaction logic for ClosableTab.xaml
    /// </summary>
    public partial class ClosableTab : UserControl
    {

        public ClosableTab()
        {
            InitializeComponent();
        }

        public ClosableTab(string name, TabControl container, object tabParent, AssetViewModel viewModel) : this()
        {
            TabName.Content = name;
            CloseButton.Command = new CloseTabCommand(container, tabParent, viewModel);
        }
    }
}
