using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    public class IndexedFace
    {
        public Int32[] Indexes { get; set; } 
        public IndexedFace()
        {
            Indexes = null;
        }
        public IndexedFace(Int32[] indexes)
        {
            Indexes = indexes;
        }
        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Indexes.Length.ToString());
            foreach (var index in Indexes)
            {
                builder.Append($" {index}");
            }
            return builder.ToString();
        }
    }
}
