using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.ViewModels;

namespace TT_Lab.Editors
{
    /// <summary>
    /// Interaction logic for SceneEditor.xaml
    /// </summary>
    public partial class SceneEditor : BaseEditor
    {
        private List<AssetViewModel> chunkTree = new List<AssetViewModel>();

        public SceneEditor() : this(null)
        {
        }

        public SceneEditor(ChunkFolder chunk)
        {
            foreach (var item in ((FolderData)chunk.GetData()).Children)
            {
                chunkTree.Add(new AssetViewModel(item));
            }
            DataContext = new { Items = chunkTree };
            InitializeComponent();
        }
    }
}
