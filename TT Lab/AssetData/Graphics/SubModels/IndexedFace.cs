using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    public class IndexedFace
    {
        public Int32[] indexes { get; set; } 
        public IndexedFace()
        {
            indexes = null;
        }
        public IndexedFace(Int32[] indexes)
        {
            this.indexes = indexes;
        }
        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(indexes.Length.ToString());
            foreach (var index in indexes)
            {
                builder.Append($" {index}");
            }
            return builder.ToString();
        }
    }
}
