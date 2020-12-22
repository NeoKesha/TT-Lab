using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.AssetData.Code
{
    public class CodeModelData : AbstractAssetData
    {
        public CodeModelData()
        {
        }

        public CodeModelData(PS2AnyCodeModel codeModel) : this()
        {
        }

        [JsonProperty(Required = Required.Always)]
        public Int32 Header { get; set; }

        protected override void Dispose(Boolean disposing)
        {
            return;
        }
    }
}
