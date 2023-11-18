using System;
using TT_Lab.AssetData;
using TT_Lab.Editors;

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

        public ChunkFolder(LabURI package, String Name, Folder parent) : base(package, Name: Name, variant: Name, parent)
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
            return typeof(ChunkEditor);
        }
    }
}
