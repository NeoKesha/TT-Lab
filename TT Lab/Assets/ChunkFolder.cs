using System;
using TT_Lab.AssetData;
using TT_Lab.ViewModels.Editors;

namespace TT_Lab.Assets
{
    public class ChunkFolder : Folder
    {

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
    }
}
