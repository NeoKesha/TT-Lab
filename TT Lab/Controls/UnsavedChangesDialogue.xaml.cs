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
using System.Windows.Shapes;
using TT_Lab.Command;

namespace TT_Lab.Controls
{
    /// <summary>
    /// Interaction logic for UnsavedChangedDialogue.xaml
    /// </summary>
    public partial class UnsavedChangesDialogue : Window
    {
        OpenDialogueCommand.DialogueResult result;

        public enum AnswerResult
        {
            YES,
            DISCARD,
            CANCEL
        }

        public UnsavedChangesDialogue()
        {
            InitializeComponent();
        }

        public UnsavedChangesDialogue(OpenDialogueCommand.DialogueResult result) : this()
        {
            this.result = result;
        }

        private void YesButton_Click(Object sender, RoutedEventArgs e)
        {
            result.Result = AnswerResult.YES;
            Close();
        }

        private void DiscardButton_Click(Object sender, RoutedEventArgs e)
        {
            result.Result = AnswerResult.DISCARD;
            Close();
        }

        private void CancelButton_Click(Object sender, RoutedEventArgs e)
        {
            result.Result = AnswerResult.CANCEL;
            Close();
        }
    }
}
