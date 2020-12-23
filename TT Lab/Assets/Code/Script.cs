using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT_Lab.AssetData;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;

namespace TT_Lab.Assets.Code
{
    public abstract class Script<T> : SerializableAsset<T> where T : AbstractAssetData, new()
    {
        public override String Type => "Script";

        public Script() { }

        public Script(UInt32 id, String name) : base(id, name)
        {
        }
    }
}
