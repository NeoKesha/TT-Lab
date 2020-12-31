using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TT_Lab.AssetData;
using TT_Lab.Editors;

namespace TT_Lab.Assets
{
    public class ChunkFolder : Folder
    {

        public ChunkFolder() : base()
        {
        }

        public ChunkFolder(String name) : base(name)
        {
        }

        public ChunkFolder(String name, Folder parent) : base(name, parent)
        {
        }

        protected ChunkFolder(UInt32 id, String name) : base(id, name)
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
