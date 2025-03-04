using System.Windows;
using System.Windows.Controls;
using AdonisUI.Controls;

namespace TT_Lab.Views;

public partial class CreateAssetView : AdonisWindow
{
    public CreateAssetView()
    {
        InitializeComponent();
    }

    private void AssetName_OnLoaded(object sender, RoutedEventArgs e)
    {
        AssetName.Focus();
        ResetTextSelection();
    }

    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ResetTextSelection();
    }

    private void ResetTextSelection()
    {
        AssetName.CaretIndex = AssetName.Text.Length;
        AssetName.SelectionStart = 0;
        AssetName.SelectionLength = AssetName.Text.Length;
    }
}