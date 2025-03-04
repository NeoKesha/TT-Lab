using AdonisUI.Controls;
using System;
using System.Windows;
using TT_Lab.Command;
using TT_Lab.ViewModels.ResourceTree;

namespace TT_Lab.Controls
{
    /// <summary>
    /// Interaction logic for UnsavedChangedDialogue.xaml
    /// </summary>
    public partial class UnsavedChangesDialogue : AdonisWindow
    {
        OpenDialogueCommand.DialogueResult result;
        private ResourceTreeElementViewModel viewModel;

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

        public UnsavedChangesDialogue(OpenDialogueCommand.DialogueResult result, ResourceTreeElementViewModel viewModel) : this()
        {
            this.result = result;
            DataContext = viewModel;
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
