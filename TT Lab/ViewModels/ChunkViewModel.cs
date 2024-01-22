using System.Collections.Generic;

namespace TT_Lab.ViewModels
{
    public class ChunkViewModel : ObservableObject
    {
        private List<AssetViewModel> _assets;

        public ChunkViewModel(List<AssetViewModel> chunkTree)
        {
            _assets = chunkTree;
        }

        public List<AssetViewModel> Items
        {
            get => _assets;
            set
            {
                _assets = value;
                NotifyChange();
            }
        }
    }
}
