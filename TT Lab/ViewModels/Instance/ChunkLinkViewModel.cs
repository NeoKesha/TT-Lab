using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;
using TT_Lab.ViewModels.Instance.ChunkLinks;

namespace TT_Lab.ViewModels.Instance
{
    public class ChunkLinkViewModel : AssetViewModel
    {
        private ObservableCollection<LinkViewModel> links;

        public ChunkLinkViewModel(Guid asset, AssetViewModel parent) : base(asset, parent)
        {
            var data = (ChunkLinksData)_asset.GetData();
            links = new ObservableCollection<LinkViewModel>();
            foreach (var link in data.Links)
            {
                links.Add(new LinkViewModel(link));
            }
        }

        public override void Save(Object? o)
        {
            var data = (ChunkLinksData)_asset.GetData();
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
