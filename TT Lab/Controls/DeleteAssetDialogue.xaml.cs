using System.Windows;
using System.Windows.Controls;
using AdonisUI.Controls;
using TT_Lab.Command;
using TT_Lab.ViewModels.ResourceTree;

namespace TT_Lab.Controls;

public partial class DeleteAssetDialogue : AdonisWindow
{
    OpenDialogueCommand.DialogueResult result;

    public enum DeleteAnswerResult
    {
        Yes,
        No
    }

    public DeleteAssetDialogue()
    {
        InitializeComponent();
    }
    
    public DeleteAssetDialogue(OpenDialogueCommand.DialogueResult result, ResourceTreeElementViewModel viewModel) : this()
    {
        this.result = result;
        DataContext = viewModel;
    }

    private void YesButton_OnClick(object sender, RoutedEventArgs e)
    {
        result.Result = DeleteAnswerResult.Yes;
        Close();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        result.Result = DeleteAnswerResult.No;
        Close();
    }
}