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
    /// Interaction logic for LabeledTextBox.xaml
    /// </summary>
    public partial class LabeledTextBox : UserControl
    {
        public event EventHandler UndoPerformed;
        public event EventHandler RedoPerformed;
        public event EventHandler TextChanged;

        public string TextBoxName
        {
            get { return (string)GetValue(TextBoxNameProperty); }
            set { SetValue(TextBoxNameProperty, value); }
        }

        public string Text
        {
            get { return TextContainer.Text; }
        }

        // Using a DependencyProperty as the backing store for TextBoxName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxNameProperty =
            DependencyProperty.Register("TextBoxName", typeof(string), typeof(LabeledTextBox), new PropertyMetadata(""));



        public LabeledTextBox()
        {
            InitializeComponent();
            TextBoxLabel.Content = TextBoxName;
        }

        private void BaseTextBox_UndoPerformed(Object sender, EventArgs e)
        {
            var handler = UndoPerformed;
            handler?.Invoke(this, e);
        }

        private void BaseTextBox_RedoPerformed(Object sender, EventArgs e)
        {
            var handler = RedoPerformed;
            handler?.Invoke(this, e);
        }

        private void BaseTextBox_TextChanged(Object sender, TextChangedEventArgs e)
        {
            var handler = TextChanged;
            handler?.Invoke(this, e);
        }
    }
}
