using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData.Instance;

namespace TT_Lab.ViewModels.Instance.ChunkLinks
{
    public class LinkViewModel : SavebleViewModel
    {
        public LinkViewModel() { }
        public LinkViewModel(ChunkLink link)
        {

        }
        public override void Save(Object? o = null)
        {
            throw new NotImplementedException();
        }
    }
}
