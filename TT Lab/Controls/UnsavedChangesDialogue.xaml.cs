using System;
using System.Windows;
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
