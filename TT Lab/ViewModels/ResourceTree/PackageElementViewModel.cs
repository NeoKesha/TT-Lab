using System.Windows;
using System.Windows.Data;
using TT_Lab.Assets;

namespace TT_Lab.ViewModels.ResourceTree;

public class PackageElementViewModel : FolderElementViewModel
{
    public PackageElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null) : base(asset, parent)
    {
    }

    protected override void CreateContextMenu()
    {
        base.CreateContextMenu();

        var binding = new Binding
        {
            Mode = BindingMode.TwoWay,
            Source = this,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            Path = new PropertyPath(nameof(IsPackageEnabled)),
            NotifyOnSourceUpdated = true,
            NotifyOnTargetUpdated = true
        };
        RegisterMenuItem(new MenuItemSettings
        {
            Header = "Is Enabled",
            IsCheckable = true,
            IsChecked = binding
        });
    }

    protected override void ListCreatableAssets(CreateAssetViewModel createAssetViewModel)
    {
        // Only allow creating folders in packages
        createAssetViewModel.RegisterAssetToCreate<Folder>("Folder");
    }

    public bool IsPackageEnabled
    {
        get => ((Package)Asset).Enabled;
        set
        {
            if (value != ((Package)Asset).Enabled)
            {
                ((Package)Asset).Enabled = value;
                NotifyOfPropertyChange();
            }
        }
    }
}