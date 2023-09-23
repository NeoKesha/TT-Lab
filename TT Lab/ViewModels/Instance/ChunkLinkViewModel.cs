using System;
using System.Collections.ObjectModel;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Instance.ChunkLinks;

namespace TT_Lab.ViewModels.Instance
{
    public class ChunkLinkViewModel : AssetViewModel
    {
        private ObservableCollection<LinkViewModel> links;

        public ChunkLinkViewModel(LabURI asset, AssetViewModel? parent) : base(asset, parent)
        {
            var data = _asset.GetData<ChunkLinksData>();
            links = new ObservableCollection<LinkViewModel>();
            foreach (var link in data.Links)
            {
                links.Add(new LinkViewModel(link));
            }
        }

        public override void Save(Object? o)
        {
            var data = _asset.GetData<ChunkLinksData>();
            data.Links.Clear();
            foreach (var link in Links)
            {
                var newLink = new ChunkLink();
                link.Save(newLink);
                data.Links.Add(newLink);
            }
            base.Save(o);
        }

        public ObservableCollection<LinkViewModel> Links
        {
            get => links;
        }
    }
}
