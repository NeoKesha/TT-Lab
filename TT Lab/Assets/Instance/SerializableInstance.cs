using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TT_Lab.Assets.Instance
{
    public abstract class SerializableInstance : SerializableAsset
    {
        public String Chunk { get; }

        [JsonProperty(Required = Required.Always)]
        public Int32 LayoutID { get; }

        protected override String SavePath => $"Instance\\{Chunk}\\";

        public override String Type => $"InstanceAsset";

        public SerializableInstance(UInt32 id, String name, String chunk, Int32 layId) : base(id, name)
        {
            Chunk = chunk;
            LayoutID = layId;
        }

        protected SerializableInstance()
        {
        }

    }
}
