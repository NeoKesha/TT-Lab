using System;
using System.Windows.Controls;
using System.Windows.Input;

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
                undoBinding = new CommandBinding(ApplicationCommands.Undo, UndoExecuted, null);
                redoBinding = new CommandBinding(ApplicationCommands.Redo, RedoExecuted, null);

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
