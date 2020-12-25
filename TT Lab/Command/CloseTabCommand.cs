using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.ViewModels;

namespace TT_Lab.Command
{
    public class CloseTabCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private TabControl _tabsContainer;
        private object _tabToClose;
        private AssetViewModel _viewModel;

        public CloseTabCommand(TabControl tabs)
        {
            _tabsContainer = tabs;
        }

        public CloseTabCommand(TabControl tabs, object tabToClose, AssetViewModel viewModel) : this(tabs)
        {
            _viewModel = viewModel;
            _tabToClose = tabToClose;
        }

        public Boolean CanExecute(Object parameter)
        {
            return true;
        }

        public void Execute(Object parameter = null)
        {
            if (_tabToClose != null)
            {
                _tabsContainer.Items.Remove(_tabToClose);
                return;
            }
            if (_tabsContainer.SelectedIndex != -1)
            {
                _tabsContainer.Items.RemoveAt(_tabsContainer.SelectedIndex);
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
