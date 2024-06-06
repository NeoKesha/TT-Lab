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

        public ChunkFolder(LabURI package, String Name) : base(package, Name: Name, variant: Name)
        {
            SkipExport = false;
        }

        public ChunkFolder(LabURI package, String Name, Folder parent, String? variant = null) : base(package, Name: Name, variant, parent)
        {
            SkipExport = false;
        }

        protected ChunkFolder(LabURI package, UInt32 id, String Name) : base(package, variant: Name, id, Name: Name)
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
