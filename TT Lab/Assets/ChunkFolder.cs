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

        public ChunkFolder(String package, String subpackage, String name) : base(package, subpackage, name: name, variant: name)
        {
        }

        public ChunkFolder(String package, String subpackage, String name, Folder parent) : base(package, subpackage, name: name, variant: name, parent)
        {
        }

        protected ChunkFolder(String package, String subpackage, UInt32 id, String name) : base(package, subpackage, variant: name, id, name: name)
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
