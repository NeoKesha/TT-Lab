using System;
using TT_Lab.AssetData;
using TT_Lab.Editors;

namespace TT_Lab.Assets
{
    public class ChunkFolder : Folder
    {

        public ChunkFolder() : base()
        {
        }

        public ChunkFolder(String package, String subpackage, String Name) : base(package, subpackage, Name: Name, variant: Name)
        {
        }

        public ChunkFolder(String package, String subpackage, String Name, Folder parent) : base(package, subpackage, Name: Name, variant: Name, parent)
        {
        }

        protected ChunkFolder(String package, String subpackage, UInt32 id, String Name) : base(package, subpackage, variant: Name, id, Name: Name)
        {
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
