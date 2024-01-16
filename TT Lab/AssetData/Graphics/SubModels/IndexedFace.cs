using System;
using System.Diagnostics;
using System.Text;

namespace TT_Lab.AssetData.Graphics.SubModels
{
    public class IndexedFace
    {
        public Int32[]? Indexes { get; set; }

        public IndexedFace()
        {
            Indexes = null;
        }

        public IndexedFace(params Int32[] indexes)
        {
            Debug.Assert(indexes.Length == 3, "Amount of indexes must correspond to a triangle");
            Indexes = indexes;
        }

        public override String ToString()
        {
            Debug.Assert(Indexes != null, "Indexes must be created at this point of time");

            StringBuilder builder = new();
            builder.Append(Indexes.Length);
            foreach (var index in Indexes)
            {
                builder.Append($" {index}");
            }
            return builder.ToString();
        }
    }
}
