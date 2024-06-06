using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Editors.Instance.ChunkLinks;

namespace TT_Lab.ViewModels.Editors.Instance
{
    public class ChunkLinkViewModel : InstanceSectionResourceEditorViewModel
    {
        private BindableCollection<LinkViewModel> links;

        protected override void Save()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<ChunkLinksData>();
            data.Links.Clear();
            foreach (var link in Links)
            {
                var newLink = new ChunkLink();
                link.Save(newLink);
                data.Links.Add(newLink);
            }
        }

        public override void LoadData()
        {
            var asset = AssetManager.Get().GetAsset(EditableResource);
            var data = asset.GetData<ChunkLinksData>();
            links = new BindableCollection<LinkViewModel>();
            foreach (var link in data.Links)
            {
                links.Add(new LinkViewModel(link));
            }
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            if (links.Count > 0)
            {
                await ActivateItemAsync(links[0], cancellationToken);
            }
        }

        public BindableCollection<LinkViewModel> Links
        {
            get => links;
        }
    }
}
