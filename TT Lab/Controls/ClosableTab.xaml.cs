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
        public TabControl Container;
        public object TabParent;
        public event EventHandler CloseTab;

        private static RoutedCommand closeCommand = new RoutedCommand();

        public ClosableTab()
        {
            InitializeComponent();
        }

        public ClosableTab(string name, TabControl container, object tabParent) : this()
        {
            Container = container;
            TabParent = tabParent;
            TabName.Content = name;
            var closeBinding = new CommandBinding(closeCommand, CloseExecuted);
            CommandBindings.Add(closeBinding);
            CloseButton.Command = closeBinding.Command;
        }

        private void CloseExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            CloseTab?.Invoke(this, e);
        }
    }
}
