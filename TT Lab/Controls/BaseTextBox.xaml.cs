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

namespace TT_Lab.Controls
{
    /// <summary>
    /// Interaction logic for BaseTextBox.xaml
    /// </summary>
    public partial class BaseTextBox : TextBox
    {
        CommandBinding undoBinding;
        CommandBinding redoBinding;

        public event EventHandler UndoPerformed;
        public event EventHandler RedoPerformed;

        public BaseTextBox()
        {
            InitializeComponent();
            UndoLimit = 1;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (undoBinding == null)
            {
                undoBinding = new CommandBinding(ApplicationCommands.Undo, new ExecutedRoutedEventHandler(UndoExecuted), null);
                redoBinding = new CommandBinding(ApplicationCommands.Redo, new ExecutedRoutedEventHandler(RedoExecuted), null);

                CommandBindings.Add(undoBinding);
                CommandBindings.Add(redoBinding);
            }
        }


        private void UndoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var handler = UndoPerformed;
            handler?.Invoke(sender, e);
        }

        private void RedoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var handler = RedoPerformed;
            handler?.Invoke(sender, e);
        }
    }
}
