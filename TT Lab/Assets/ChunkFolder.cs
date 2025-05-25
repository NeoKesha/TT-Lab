using System;
using TT_Lab.AssetData;
using TT_Lab.ViewModels.Editors;
using TT_Lab.ViewModels.ResourceTree;

namespace TT_Lab.Assets
{
    public class ChunkFolder : Folder
    {
        private string _iconPath1;

        public override string IconPath => "Scene.png";

        public ChunkFolder() : base()
        {
            SkipExport = false;
        }

        public ChunkFolder(LabURI package, String name) : base(package, name: name, variant: name)
        {
            SkipExport = false;
        }

        public ChunkFolder(LabURI package, String name, Folder parent, String? variant = null) : base(package, name: name, variant, parent)
        {
            SkipExport = false;
        }

        protected ChunkFolder(LabURI package, UInt32 id, String name) : base(package, variant: name, id, name: name)
        {
            SkipExport = false;
        }

        public override AbstractAssetData GetData()
        {
            return base.GetData();
        }

        public override Type GetEditorType()
        {
            return typeof(ChunkEditorViewModel);
        }

        protected override ResourceTreeElementViewModel CreateResourceTreeElement(ResourceTreeElementViewModel? parent = null)
        {
            return new ChunkElementViewModel(URI, parent);
        }
    }
}
