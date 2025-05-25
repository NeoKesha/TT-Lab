using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using TT_Lab.Assets;
using TT_Lab.Project;

namespace TT_Lab.ViewModels.ResourceTree;

public class ChunkElementViewModel : ResourceTreeElementViewModel
{
    public ChunkElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null) : base(asset, parent)
    {
    }

    protected override void CreateContextMenu()
    {
        RegisterMenuItem(new MenuItemSettings
        {
            Header = "Build chunk",
            Action = RebuildChunk
        });
    }

    private async void RebuildChunk()
    {
        try
        {
            var projectManager = IoC.Get<ProjectManager>();
            projectManager.WorkableProject = false;
            var buildTask = Task.Factory.StartNew(() =>
            {
                IoC.Get<ProjectManager>().OpenedProject!.PackChunk(Asset.URI);
            });
            await buildTask;
            projectManager.WorkableProject = true;
        }
        catch (Exception e)
        {
            IoC.Get<ProjectManager>().WorkableProject = true;
            Log.WriteLine($"Error when building chunk: {e.Message}");
        }
    }
}